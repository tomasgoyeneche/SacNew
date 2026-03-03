using Configuraciones.Views;
using Core.Base;
using Core.Reports;
using Core.Repositories.RequerimientoCompra;
using Core.Services;
using Servicios.Views.RequerimientosDeCompra;
using Shared.Models;
using Shared.Models.RequerimientoCompra;

namespace Servicios.Presenters
{
    public class CrearRequerimientoPresenter : BasePresenter<ICrearRequerimientoView>
    {
        private readonly IRcRepositorio _rcRepositorio;
        private readonly IProveedorRccRepositorio _proveedorRepositorio;
        private readonly IUsuarioRccRepositorio _usuarioRepositorio;

        private readonly List<RcDetalleRcc> _detalles = new();

        public CrearRequerimientoPresenter(
            IRcRepositorio rcRepositorio,
            IProveedorRccRepositorio proveedorRepositorio,
            IUsuarioRccRepositorio usuarioRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _rcRepositorio = rcRepositorio;
            _proveedorRepositorio = proveedorRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        // ================= INIT =================
        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                _detalles.Clear();
                _view.MostrarDetalles(_detalles); // limpia grid

                _view.NumeroRc = await _rcRepositorio.ObtenerProximoNumeroAsync();
                _view.Fecha = DateTime.Now;

                var proveedores = await _proveedorRepositorio.ObtenerActivosAsync();
                _view.CargarProveedores(proveedores);

                var usuarios = await _usuarioRepositorio.ObtenerActivosAsync();
                _view.CargarUsuarios(usuarios);

                var usuarioSesion = usuarios.FirstOrDefault(u =>
                    u.UsuarioLogin.Equals(_sesionService.NombreUsuario, StringComparison.OrdinalIgnoreCase));

                if (usuarioSesion == null)
                {
                    _view.MostrarMensaje("Usuario no encontrado.");
                    return;
                }

                _view.IdEmitido = usuarioSesion.IdUsuario;
                _view.FuncionEmitido = usuarioSesion.Funcion;
                _view.BloquearEmitido();
            });
        }

        // ================= DETALLE =================
        public Task AbrirAgregarImputacionAsync(int porcentajeActual)
        {
            return AbrirFormularioAsync<AgregarImputacionDependenciaForm>(form =>
            {
                form.Inicializar(porcentajeActual);
                return Task.CompletedTask;
            });
        }

        public void AgregarDatosDesdeAntes(List<RcDetalleRcc> detalles, string PrecioTotal, string Posta)
        {
            _detalles.AddRange(detalles);
            _view.MostrarDetalles(_detalles);
            _view.EntregaLugar = Posta;
            _view.Importe = PrecioTotal;
        }

        public void AgregarDetalle()
        {
            if (string.IsNullOrWhiteSpace(_view.DetalleDescripcion))
            {
                _view.MostrarMensaje("Ingrese descripción.");
                return;
            }

            if (_view.DetalleCantidad <= 0)
            {
                _view.MostrarMensaje("Cantidad inválida.");
                return;
            }

            _detalles.Add(new RcDetalleRcc
            {
                Descripcion = _view.DetalleDescripcion.Trim(),
                Cantidad = _view.DetalleCantidad,
                Activo = true
            });

            _view.MostrarDetalles(_detalles);
            _view.LimpiarDetalle();
        }

        public void EliminarDetalle(RcDetalleRcc detalle)
        {
            _detalles.Remove(detalle);
            _view.MostrarDetalles(_detalles);
        }

        // ================= GUARDAR =================
        public async Task GuardarAsync()
        {
            //await GenerarReporteRcAsync(9535); para probar
            //return;
            var rc = new RcRcc
            {
                Fecha = _view.Fecha,
                IdProveedor = _view.IdProveedor,
                Emitido = _view.IdEmitido,
                Aprobado = _view.IdAprobado,
                EntregaLugar = _view.EntregaLugar,
                EntregaFecha = _view.EntregaFecha,
                Importe = _view.Importe,
                CondicionPago = _view.CondicionPago,
                Observaciones = _view.Observaciones,
                IdEstado = 1
            };

            if (!await ValidarAsync(rc))
                return;

            if (!await ValidarDetallesImputacionesAsync())
                return;

            // tu regla
            if (rc.Aprobado == 16)
                rc.Aprobado = _sesionService.IdUsuario;

            await EjecutarConCargaAsync(async () =>
            {
                int idRc = await _rcRepositorio.AgregarAsync(rc);

                // 2) Insert detalles (esperados!)
                var tareasDetalles = _detalles.Select(det =>
                {
                    det.IdRc = idRc;
                    return _rcRepositorio.InsertarRcDetalleAsync(idRc, det.Descripcion, Convert.ToInt32(det.Cantidad));
                }).ToList();

                // 3) Insert imputaciones (esperadas!)
                var imputaciones = _view.ObtenerImputaciones();
                var tareasImputaciones = imputaciones.Select(i =>
                    _rcRepositorio.InsertarRcImputacionAsync(idRc, i.IdImputacion, i.Porcentaje)
                ).ToList();

                await Task.WhenAll(tareasDetalles.Concat(tareasImputaciones));

                _view.MostrarMensaje("RC creado correctamente.");
                _view.Cerrar();
                await GenerarReporteRcAsync(idRc);
            });
        }

        public async Task GenerarReporteRcAsync(int idRc)
        {
            await EjecutarConCargaAsync(async () =>
            {
                RcDetalleDto? RcDetalle = await _rcRepositorio.ObtenerDetalleRcAsync(idRc);
                var RcImputacion = await _rcRepositorio.ObtenerImputacionesRcAsync(idRc);
                var RcDescripcion = await _rcRepositorio.ObtenerDescripcionesRcAsync(idRc);

                var porcentajesConcatenados = string.Join("\n", RcImputacion.Select(i => $"{i.Porcentaje}%"));
                var imputacionesConcatenadas = string.Join("\n", RcImputacion.Select(i => i.Imputacion));

                int filasFijas = 10;

                RcDescripcion = RcDescripcion.Take(filasFijas).ToList();

                while (RcDescripcion.Count < filasFijas)
                {
                    RcDescripcion.Add(new RcDetalleRcc
                    {
                        IdRc = idRc,
                        Descripcion = "",
                        Cantidad = 0
                    });
                }

                // Crear una instancia del nuevo reporte DevExpress
                ReporteRequerimientoCompra reporte = new ReporteRequerimientoCompra();
                reporte.DataSource = new List<RcDetalleDto> { RcDetalle }; ;
                reporte.Parameters["impuDes"].Value = imputacionesConcatenadas;
                reporte.Parameters["impuPor"].Value = porcentajesConcatenados;
                reporte.DataMember = "";
                reporte.DetailReport.DataSource = RcDescripcion;
                reporte.DetailReport.DataMember = ""; // si es lista directa

                await AbrirFormularioAsync<VisualizadorReportesDevForm>(form =>
                {
                    form.MostrarReporteDevExpress(reporte);
                    return Task.CompletedTask;
                });
            });
        }

        // ================= AUX =================
        public async Task UsuarioAprobadorSeleccionadoAsync(int idUsuario)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(idUsuario);
            if (usuario != null)
                _view.FuncionAprobado = usuario.Funcion;
        }

        private Task<bool> ValidarDetallesImputacionesAsync()
        {
            // Validar detalles
            if (_detalles == null || !_detalles.Any())
            {
                _view.MostrarMensaje("Debe ingresar al menos un detalle.");
                return Task.FromResult(false);
            }

            // Validar imputaciones
            var imputaciones = _view.ObtenerImputaciones();
            int totalPorcentaje = imputaciones.Sum(i => i.Porcentaje);

            if (totalPorcentaje != 100)
            {
                _view.MostrarMensaje("Las imputaciones no llegan al 100%.");
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}