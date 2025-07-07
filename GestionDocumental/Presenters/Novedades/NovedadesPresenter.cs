using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Views;
using GestionDocumental.Views.Novedades;
using Shared.Models;
using System.IO;

namespace GestionDocumental.Presenters
{
    public class NovedadesPresenter : BasePresenter<INovedadesView>
    {
        private readonly IChoferEstadoRepositorio _repositorioChoferEstado;
        private readonly IUnidadMantenimientoRepositorio _unidadMantenimientoRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly ICalendarioRepositorio _calendarioRepositorio;
        private readonly ICsvService _csvService;

        public string _Entidad;

        public NovedadesPresenter(
            IChoferEstadoRepositorio repositorioChoferEstado,
            IChoferRepositorio choferRepositorio,
            ICalendarioRepositorio calendarioRepositorio,
            ICsvService csvService,
            IUnidadMantenimientoRepositorio unidadMantenimientoRepositorio,
            IUnidadRepositorio unidadRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _unidadMantenimientoRepositorio = unidadMantenimientoRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _repositorioChoferEstado = repositorioChoferEstado;
            _choferRepositorio = choferRepositorio;
            _calendarioRepositorio = calendarioRepositorio;
            _csvService = csvService;
        }

        public async Task CargarNovedadesAsync(bool chequeado, string Entidad)
        {
            _Entidad = Entidad;

            if (_Entidad == "Chofer")
            {
                await EjecutarConCargaAsync(
                () => _view.activoChecked
                    ? _repositorioChoferEstado.ObtenerTodasLasNovedadesDto()
                    : _repositorioChoferEstado.ObtenerNovedadesDto(),
                _view.MostrarNovedades
                );
            }
            else if (_Entidad == "Unidad")
            {
                await EjecutarConCargaAsync(
                () => _view.activoChecked
                    ? _unidadMantenimientoRepositorio.ObtenerTodasLasNovedadesDto()
                    : _unidadMantenimientoRepositorio.ObtenerNovedadesDto(),
                _view.MostrarNovedades
                );
            }

            await CargarMesesParaExportacionAsync();
        }

        public async Task EliminarNovedadAsync(object novedad)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta Novedad?");
                if (confirmacion != DialogResult.Yes) return;

                if (_Entidad == "Chofer" && novedad is NovedadesChoferesDto novChofer)
                {
                    await _repositorioChoferEstado.EliminarNovedadAsync(novChofer, _sesionService.IdUsuario);
                }
                else if (_Entidad == "Unidad" && novedad is UnidadMantenimientoDto novUnidad)
                {
                    await _unidadMantenimientoRepositorio.EliminarNovedadAsync(novUnidad, _sesionService.IdUsuario);
                }

                _view.MostrarMensaje("Novedad eliminada correctamente.");
            }, async () => await CargarNovedadesAsync(_view.activoChecked, _Entidad));
        }

        public async Task EditarNovedadAsync(object novedad)
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (_Entidad == "Chofer" && novedad is NovedadesChoferesDto novChofer)
                {
                    await AbrirFormularioAsync<AgregarEditarNovedadChoferForm>(async form =>
                    {
                        await form._presenter.InicializarAsync(novChofer);
                    });
                }
                else if (_Entidad == "Unidad" && novedad is UnidadMantenimientoDto novUnidad)
                {
                    await AbrirFormularioAsync<AgregarEditarNovedadUnidadForm>(async form =>
                    {
                        await form._presenter.InicializarAsync(novUnidad);
                    });
                }
            }, async () => await CargarNovedadesAsync(_view.activoChecked, _Entidad));
        }

        public async Task AgregarNovedadAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (_Entidad == "Chofer")
                {
                    await AbrirFormularioAsync<AgregarEditarNovedadChoferForm>(async form =>
                    {
                        await form._presenter.InicializarAsync(null);
                    });
                }
                else if (_Entidad == "Unidad")
                {
                    await AbrirFormularioAsync<AgregarEditarNovedadUnidadForm>(async form =>
                    {
                        await form._presenter.InicializarAsync(null);
                    });
                }
            }, async () => await CargarNovedadesAsync(_view.activoChecked, _Entidad));
        }

        public async Task CargarMesesParaExportacionAsync()
        {
            var fechas = await _calendarioRepositorio.ObtenerMesesAniosDisponiblesAsync();
            _view.CargarMesesDisponibles(fechas);
        }

        public async Task ExportarCsvAsync()
        {
            var (mes, anio) = _view.ObtenerMesSeleccionado();

            if (mes == 0 || anio == 0)
            {
                _view.MostrarMensaje("Seleccione un mes/año válido.");
                return;
            }

            await EjecutarConCargaAsync(async () =>
            {
                List<string> filas = new List<string>();
                List<(DateTime Fecha, int Dia)> diasMes = await _calendarioRepositorio.ObtenerDiasPorMesYAnioAsync(mes, anio);

                string ruta = _Entidad == "Chofer"
                       ? $@"C:\Compartida\Exportaciones\ChoferesNovedades-{mes}-{anio}.csv"
                       : $@"C:\Compartida\Exportaciones\UnidadesNovedades-{mes}-{anio}.csv";

                if (_Entidad == "Chofer")
                {
                    List<ChoferDto> choferes = await _choferRepositorio.ObtenerTodosLosChoferesDto();
                    List<NovedadesChoferesDto> novedades = await _repositorioChoferEstado.ObtenerPorMesYAnioAsync(mes, anio);

                    string titulo = "Empresa;Chofer;" + string.Join(";", diasMes.Select(d => d.Dia.ToString("00"))) + ";;Disponible;%;Ausente;%";
                    filas.Add(titulo);

                    var agrupadas = novedades
                        .GroupBy(n => n.idChofer)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    foreach (var chofer in choferes)
                    {
                        string fila = $"{chofer.Empresa_Nombre};{chofer.Nombre} {chofer.Apellido}";
                        int disponibles = 0, ausentes = 0;

                        foreach (var dia in diasMes)
                        {
                            var novedad = agrupadas.ContainsKey(chofer.IdChofer)
                                ? agrupadas[chofer.IdChofer].FirstOrDefault(n =>
                                    n.FechaInicio <= dia.Fecha && n.FechaFin >= dia.Fecha)
                                : null;

                            var abreviado = novedad?.Abreviado ?? "";
                            fila += $";{abreviado}";

                            if (string.IsNullOrWhiteSpace(abreviado))
                                disponibles++;
                            else
                                ausentes++;
                        }

                        int total = disponibles + ausentes;
                        string disponibleStr = total > 0 ? $"{disponibles};{Math.Round((double)disponibles / total * 100)}%" : "0;0%";
                        string ausenteStr = total > 0 ? $"{ausentes};{Math.Round((double)ausentes / total * 100)}%" : "0;0%";
                        fila += $";;{disponibleStr};{ausenteStr}";
                        filas.Add(fila);
                    }

                    // Referencias Chofer
                    filas.Add("");
                    filas.Add("");
                    filas.Add("REFERENCIAS");
                    List<ChoferTipoEstado> tipos = await _repositorioChoferEstado.ObtenerEstados();
                    filas.AddRange(tipos.Select(t => $"{t.Descripcion};{t.Abreviado}"));
                }
                else if (_Entidad == "Unidad")
                {
                    List<UnidadDto> unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();
                    List<UnidadMantenimientoDto> novedades = await _unidadMantenimientoRepositorio.ObtenerPorMesYAnioAsync(mes, anio);

                    string titulo = "Empresa;Unidad;" + string.Join(";", diasMes.Select(d => d.Dia.ToString("00"))) + ";;Disponible;%;Ausente;%";
                    filas.Add(titulo);

                    var agrupadas = novedades
                        .GroupBy(n => n.idUnidad)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    foreach (var unidad in unidades)
                    {
                        string fila = $"{unidad.Empresa_Unidad};{unidad.PatenteCompleta}";
                        int disponibles = 0, ausentes = 0;

                        foreach (var dia in diasMes)
                        {
                            var novedad = agrupadas.ContainsKey(unidad.IdUnidad)
                                ? agrupadas[unidad.IdUnidad].FirstOrDefault(n =>
                                    n.FechaInicio <= dia.Fecha && n.FechaFin >= dia.Fecha)
                                : null;

                            var abreviado = novedad?.Abreviado ?? "";
                            fila += $";{abreviado}";

                            if (string.IsNullOrWhiteSpace(abreviado))
                                disponibles++;
                            else
                                ausentes++;
                        }

                        int total = disponibles + ausentes;
                        string disponibleStr = total > 0 ? $"{disponibles};{Math.Round((double)disponibles / total * 100)}%" : "0;0%";
                        string ausenteStr = total > 0 ? $"{ausentes};{Math.Round((double)ausentes / total * 100)}%" : "0;0%";
                        fila += $";;{disponibleStr};{ausenteStr}";
                        filas.Add(fila);
                    }

                    // Referencias Unidad
                    filas.Add("");
                    filas.Add("");
                    filas.Add("REFERENCIAS");
                    List<UnidadMantenimientoEstado> tipos = await _unidadMantenimientoRepositorio.ObtenerEstados();
                    filas.AddRange(tipos.Select(t => $"{t.Descripcion};{t.Abreviado}"));
                }

                // Info adicional
                filas.Add("");
                filas.Add($"Fecha:;{DateTime.Now}");
                filas.Add($"Usuario:;{_sesionService.NombreCompleto}");

                await _csvService.ExportarFilasSeparadasAsync(filas.Select(f => f.Split(';')), ruta);

                _view.MostrarMensaje("CSV generado exitosamente.");

                if (File.Exists(ruta))
                {
                    System.Diagnostics.Process.Start("explorer", ruta);
                }
            });
        }
    }
}