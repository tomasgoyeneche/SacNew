using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Properties;
using GestionDocumental.Views.Novedades;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDocumental.Presenters
{
    public class NovedadesChoferesCalendarioPresenter : BasePresenter<INovedadesChoferesCalendarioView>
    {
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly ISemiRepositorio _semiRepositorio;
        private readonly ITractorRepositorio _tractorRepositorio;

        private readonly IChoferEstadoRepositorio _choferEstadoRepositorio;
        private readonly IUnidadMantenimientoRepositorio _unidadMantenimientoRepositorio;


        private readonly IUnidadRepositorio _unidadRepositorio;
        public string TipoAusenciaMan;

        public NovedadesChoferesCalendarioPresenter(
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio,
            ISesionService sesionService,
            ITractorRepositorio tractorRepositorio,
            ISemiRepositorio semiRepositorio,
            IUnidadMantenimientoRepositorio unidadMantenimientoRepositorio,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _choferRepositorio = choferRepositorio;
            _unidadMantenimientoRepositorio = unidadMantenimientoRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _semiRepositorio = semiRepositorio;
            _tractorRepositorio = tractorRepositorio;

            _choferEstadoRepositorio = choferEstadoRepositorio;
        }

        public Task InicializarAsync(string tipoAusenciaMan, DateTime? mes = null)
        {
            TipoAusenciaMan = tipoAusenciaMan;
            _view.ConfigurarScheduler();
            return SetMesAsync(mes ?? DateTime.Today);
        }

        public Task CambiarMesAsync(DateTime fechaDelMes) => SetMesAsync(fechaDelMes);

        public Task MesAnteriorAsync() => SetMesAsync(_view.MesSeleccionado.AddMonths(-1));

        public Task MesSiguienteAsync() => SetMesAsync(_view.MesSeleccionado.AddMonths(1));

        private async Task SetMesAsync(DateTime fecha)
        {
            var mes = new DateTime(fecha.Year, fecha.Month, 1);
            _view.SetMesSeleccionado(mes);
            await CargarMesAsync(mes);
        }

        private Task CargarMesAsync(DateTime mes)
        {
            var desde = new DateTime(mes.Year, mes.Month, 1);
            var hasta = desde.AddMonths(1);

            return EjecutarConCargaAsync(async () =>
            {
                await CargarChoferesAsync(desde, hasta);
            });
        }



        private async Task CargarChoferesAsync(DateTime desde, DateTime hasta)
        {
            List<UnidadChoferResourceDto> resources;
            List<UnidadChoferSchedulerDto> listaFinal;

            if (TipoAusenciaMan == "CHOFER")
            {
                List<Chofer?> choferes = await _choferRepositorio
                .ObtenerTodosLosChoferesIncluyendoInactivos();

                resources = choferes
                    .Where(c =>
                        c.FechaAlta < hasta &&
                        (!c.FechaBaja.HasValue || c.FechaBaja >= desde))
                    .Select(c => new UnidadChoferResourceDto
                    {
                        IdEntidad = c.IdChofer,
                        Descripcion = $"{c.Apellido} {c.Nombre}".Trim()
                    })
                    .OrderBy(c => c.Descripcion)
                    .ToList();

                List<NovedadesChoferesDto> ausencias = await _choferEstadoRepositorio
                    .ObtenerAusenciasPorRangoDeFechas(desde, hasta);

                listaFinal = ausencias
                    .Select(n => new UnidadChoferSchedulerDto
                    {
                        IdEstadoChoferUnidad = n.idEstadoChofer,
                        IdChoferUnidad = n.idChofer,

                        Inicio = n.FechaInicio.Date,
                        FinExclusivo = n.FechaFin.Date.AddDays(1), // 👈 clave NO inclusivo

                        Abreviado = n.Abreviado,
                        DescripcionEstado = n.Descripcion,
                        Observaciones = n.Observaciones,

                        IdEstado = n.idEstado,

                        Disponible = n.Disponible == "SI"
                    })
                    .ToList();

                var cantidadChoferes = resources.Count;

                var promedio = (cantidadChoferes * 6.5 / 30.0) + 2.5;

                int promedioRedondeado =
                    (int)Math.Round(promedio, MidpointRounding.AwayFromZero);

                _view.PromedioAusenciasChofer = promedioRedondeado;
            }
            else
            {
                _view.PromedioAusenciasChofer = 0;
                List<Unidad> unidades = await _unidadRepositorio.ObtenerUnidadesAsync();
                var tractores = await _tractorRepositorio.ObtenerTodosLosTractores();
                var semis = await _semiRepositorio.ObtenerTodosLosSemis();

                var tractoresDict = tractores.ToDictionary(t => t.IdTractor);
                var semisDict = semis.ToDictionary(s => s.IdSemi);

                resources = unidades
                    .Where(u =>
                        u.AltaUnidad < hasta &&
                        (!u.BajaUnidad.HasValue || u.BajaUnidad >= desde))
                    .Select(u =>
                    {
                        tractoresDict.TryGetValue(u.IdTractor, out var tractor);
                        semisDict.TryGetValue(u.IdSemi, out var semi);

                        var patenteTractor = tractor?.Patente ?? "—";
                        var patenteSemi = semi?.Patente ?? "—";

                        return new UnidadChoferResourceDto
                        {
                            IdEntidad = u.IdUnidad,
                            Descripcion = $"{patenteTractor} - {patenteSemi}"
                        };
                    })
                    .OrderBy(u => u.Descripcion)
                    .ToList();

                List<UnidadMantenimientoDto> mantenimientos = await _unidadMantenimientoRepositorio
                   .ObtenerMantenimientoPorRangoDeFechas(desde, hasta);

                listaFinal = mantenimientos
                    .Select(n => new UnidadChoferSchedulerDto
                    {
                        IdEstadoChoferUnidad = n.idUnidadMantenimiento,
                        IdChoferUnidad = n.idUnidad,

                        Inicio = n.FechaInicio.Date,
                        FinExclusivo = n.FechaFin.Date.AddDays(1), // 👈 clave NO inclusivo

                        Abreviado = n.Abreviado,
                        DescripcionEstado = n.Descripcion,
                        Observaciones = n.Observaciones,

                        IdEstado = n.idMantenimientoEstado,

                        Disponible = false,
                    })
                    .ToList();

            }
            var ids = resources.Select(r => r.IdEntidad).ToHashSet();
            var filtradas = listaFinal
                .Where(a => ids.Contains(a.IdChoferUnidad))
                .ToList();

            AgregarTotalesPorDia(resources, filtradas, desde, hasta);
            _view.BindearScheduler(resources, filtradas, desde, hasta);
        }

        private const int ResourceTotalTopId = int.MaxValue - 1;
        private const int ResourceTotalBottomId = int.MaxValue;
        private void AgregarTotalesPorDia(
         List<UnidadChoferResourceDto> resources,
         List<UnidadChoferSchedulerDto> ausencias,
         DateTime desde,
         DateTime hasta)
        {
            resources.Insert(0,
                new UnidadChoferResourceDto { IdEntidad = ResourceTotalTopId, Descripcion = "TOTAL" });

            resources.Add(
                new UnidadChoferResourceDto { IdEntidad = ResourceTotalBottomId, Descripcion = "TOTAL" });

            var nextId = (ausencias.Count > 0
                ? ausencias.Max(a => a.IdEstadoChoferUnidad)
                : 0) + 1;

            for (var dia = desde.Date; dia < hasta.Date; dia = dia.AddDays(1))
            {
                var diaFin = dia.AddDays(1);

                var totalDia = ausencias.Count(a =>
                    a.Inicio < diaFin &&
                    a.FinExclusivo > dia);

                var texto = totalDia == 0 ? "" : totalDia.ToString();

                ausencias.Add(new UnidadChoferSchedulerDto
                {
                    IdEstadoChoferUnidad = nextId++,
                    IdChoferUnidad = ResourceTotalTopId,
                    Inicio = dia,
                    FinExclusivo = diaFin,
                    DescripcionEstado = texto,
                    IdEstado = 0
                });

                ausencias.Add(new UnidadChoferSchedulerDto
                {
                    IdEstadoChoferUnidad = nextId++,
                    IdChoferUnidad = ResourceTotalBottomId,
                    Inicio = dia,
                    FinExclusivo = diaFin,
                    DescripcionEstado = texto,
                    IdEstado = 0
                });
            }
        }
    }
}
