using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views;
using Shared.Models;

namespace Servicios.Presenters
{
    public class AgregarEditarVaporizadoExternoPresenter : BasePresenter<IAgregarEditarVaporizadoExternoView>
    {
        private readonly IVaporizadoRepositorio _vaporizadoRepositorio;
        private readonly IVaporizadoMotivoRepositorio _motivoRepo;
        private readonly IUnidadRepositorio _unidadRepo;
        private readonly INominaRepositorio _nominaRepo;

        public Vaporizado? VaporizadoActual { get; private set; }

        public AgregarEditarVaporizadoExternoPresenter(
            IVaporizadoRepositorio vaporizadoRepositorio,
            IVaporizadoMotivoRepositorio motivoRepo,
            IUnidadRepositorio unidadRepo,
            INominaRepositorio nominaRepo,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _vaporizadoRepositorio = vaporizadoRepositorio;
            _unidadRepo = unidadRepo;
            _nominaRepo = nominaRepo;
            _motivoRepo = motivoRepo;
        }

        public async Task CargarDatosAsync(Vaporizado? vaporizado, UnidadDto? unidadDto)
        {
            var motivos = await _motivoRepo.ObtenerTodosAsync();
            _view.CargarMotivos(motivos.Where(m => m.Activo).ToList());
            List<UnidadDto> unidades = await _unidadRepo.ObtenerUnidadesDtoAsync();
            _view.CargarUnidades(unidades);

            if (unidadDto != null)
            {
                string textoGuardia = $"{unidadDto.Tractor_Patente} - {unidadDto.Semirremolque_Patente} ({unidadDto.Empresa_Unidad})";
                _view.MostrarDatos(textoGuardia);
            }
            if (vaporizado != null)
            {
                Nomina? nomina = await _nominaRepo.ObtenerPorIdAsync(vaporizado.IdNomina.Value);
                VaporizadoActual = vaporizado;

                _view.CargarDatos(vaporizado, nomina.IdUnidad);
            }
        }

        public void CalcularTiempoVaporizado()
        {
            if (_view.FechaInicio.HasValue && _view.FechaFin.HasValue)
            {
                var inicio = _view.FechaInicio.Value;
                var fin = _view.FechaFin.Value;
                if (fin > inicio)
                {
                    var tiempo = fin - inicio;
                    string txt;
                    if (tiempo.TotalMinutes < 60)
                        txt = $"{tiempo.TotalMinutes:N0} min";
                    else
                        txt = $"{(int)tiempo.TotalHours} hs {tiempo.Minutes} min";
                    _view.SetTiempoVaporizado(txt);
                }
                else
                {
                    _view.SetTiempoVaporizado("0 min");
                }
            }
            else
            {
                _view.SetTiempoVaporizado(string.Empty);
            }
        }

        public async Task GuardarAsync()
        {
            // Validación mínima
            if (!_view.IdMotivo.HasValue || !_view.FechaInicio.HasValue || !_view.FechaFin.HasValue || !_view.IdUnidad.HasValue)
            {
                _view.MostrarMensaje("Debe completar unidad, motivo y fechas de vaporizado.");
                return;
            }

            Nomina? nomina = await _nominaRepo.ObtenerNominaActivaPorUnidadAsync(_view.IdUnidad.Value, DateTime.Now);
            // Guardar
            Vaporizado vap = VaporizadoActual ?? new Vaporizado();
            vap.CantidadCisternas = _view.CantidadCisternas;
            vap.IdVaporizadoMotivo = _view.IdMotivo;
            vap.IdNomina = nomina?.IdNomina; // Asignar nómina activa si existe
            vap.IdPosta = VaporizadoActual?.IdPosta ?? _sesionService.IdPosta; // Asignar posta de la guardia actual
            vap.FechaInicio = _view.FechaInicio;
            vap.FechaFin = _view.FechaFin;
            vap.IdVaporizadoZona = 1;
            vap.EsExterno = true; // Indica que es un vaporizado externo
            vap.IdTe = null;
            vap.TipoIngreso = 1;
            vap.NroCertificado = _view.NroCertificado;
            vap.RemitoDanes = _view.RemitoDanes ?? "0";
            vap.Observaciones = _view.Observaciones ?? "0";
            vap.Activo = true;

            // Guardar/actualizar
            if (vap.IdVaporizado > 0)
                await _vaporizadoRepositorio.EditarAsync(vap, _sesionService.IdUsuario);
            else
                await _vaporizadoRepositorio.AgregarAsync(vap, _sesionService.IdUsuario);

            _view.MostrarMensaje("Datos de vaporizado guardados correctamente.");
            _view.Cerrar();
        }
    }
}