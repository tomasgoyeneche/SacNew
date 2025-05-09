using Core.Base;
using Core.Repositories;
using Core.Services;
using DevExpress.Map.Dashboard;
using GestionDocumental.Views;
using SacNew.Interfaces;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDocumental.Presenters
{
    public class NovedadesChoferesPresenter : BasePresenter<INovedadesChoferesView>
    {
        private readonly IChoferEstadoRepositorio _repositorioChoferEstado;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly ICalendarioRepositorio _calendarioRepositorio;
        private readonly ICsvService _csvService;


        public NovedadesChoferesPresenter(
            IChoferEstadoRepositorio repositorioChoferEstado,
            IChoferRepositorio choferRepositorio,
            ICalendarioRepositorio calendarioRepositorio,
            ICsvService csvService,

            ISesionService sesionService,
            INavigationService navigationService)
        : base(sesionService, navigationService)  // Aquí pasamos las dependencias a la clase base
        {
            _repositorioChoferEstado = repositorioChoferEstado;
            _choferRepositorio = choferRepositorio;
            _calendarioRepositorio = calendarioRepositorio;
            _csvService = csvService;
        }

        public async Task CargarNovedadesAsync(bool chequeado)
        {
            await EjecutarConCargaAsync(
                () => chequeado
                    ? _repositorioChoferEstado.ObtenerTodasLasNovedadesDto()
                    : _repositorioChoferEstado.ObtenerNovedadesDto(),
                _view.MostrarNovedades
            );

            await CargarMesesParaExportacionAsync();
        }

        public async Task EliminarNovedadAsync(NovedadesChoferesDto novedadChofer)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar esta Novedad?");
                if (confirmacion == DialogResult.Yes)
                {
                    await _repositorioChoferEstado.EliminarNovedadAsync(novedadChofer, _sesionService.IdUsuario);
                    _view.MostrarMensaje("POC eliminada correctamente.");
                }
            }, async () => await CargarNovedadesAsync(false));
        }


        public async Task EditarNovedadAsync(NovedadesChoferesDto novedad)
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarNovedadChoferForm>(async form =>
                {
                    form._presenter.InicializarAsync(novedad);
                    await Task.CompletedTask;
                });
            }, async () => await CargarNovedadesAsync(false));
        }

        public async Task AgregarNovedadAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarNovedadChoferForm>(async form =>
                {
                    form._presenter.InicializarAsync(null);
                    await Task.CompletedTask;
                });
            }, async () => await CargarNovedadesAsync(false));
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
                // Obtener todos los choferes y sus empresas
                List<ChoferDto> choferes = await _choferRepositorio.ObtenerTodosLosChoferesDto();

                // Obtener novedades de ese mes y año
                List<NovedadesChoferesDto> novedades = await _repositorioChoferEstado.ObtenerPorMesYAnioAsync(mes, anio);

                // Obtener días del mes
                List<(DateTime Fecha, int Dia)> diasMes = await _calendarioRepositorio.ObtenerDiasPorMesYAnioAsync(mes, anio);

                // Lógica de armado CSV...
                List<String> filas = new List<string>();

                string titulo = "Empresa;Chofer;" + string.Join(";", diasMes.Select(d => d.Dia.ToString("00")))
                            + ";;Disponible;%;Ausente;%";
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

                // Referencias
                filas.Add("");
                filas.Add("");
                filas.Add("REFERENCIAS");

                List<ChoferTipoEstado> tipos = await _repositorioChoferEstado.ObtenerEstados();
                filas.AddRange(tipos.Select(t => $"{t.Descripcion};{t.Abreviado}"));

                filas.Add("");
                filas.Add($"Fecha:;{DateTime.Now}");
                filas.Add($"Usuario:;{_sesionService.NombreCompleto}");

                // await _csvService.ExportarACsvAsync(filas.Select(f => new { Fila = f }), ruta, false);

                string ruta = @"C:\Compartida\Exportaciones\ChoferesNovedades.csv";
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


