using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Presenters
{
    public class ViajesConsolidadosPresenter : BasePresenter<IViajesConsolidadosView>
    {
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;

        public ViajesConsolidadosPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            INominaRepositorio nominaRepositorio,
            IProgramaRepositorio programaRepositorio)
            : base(sesionService, navigationService)
        {
            _programaRepositorio = programaRepositorio;
            _nominaRepositorio = nominaRepositorio;
        }

        public async Task InicializarAsync()
        {
            var programas = await _programaRepositorio.ObtenerVistaProgramasAsync();
            var lista = programas
                .OrderByDescending(x => x.EntregaSalida)
                .Select(x => new VistaProgramaGridDto
                {
                    Id = x.IdPrograma,
                    Tractor = x.Tractor,
                    Chofer = x.Chofer,
                    Empresa = x.Empresa,
                    AlbaranDespacho = x.AlbaranDespacho,
                    Producto = x.Producto,
                    Origen = x.Origen,
                    Carga = x.CargaSalida,
                    RtoC = x.CargaRemito.HasValue ? "OK" : "",
                    Destino = x.Destino,
                    Entrega = x.EntregaIngreso,
                    RtoE = x.EntregaRemito.HasValue ? "OK" : "",
                    TotalProd = CalcularPorcentaje(x.CargaRemitoKg, x.EntregaRemitoKg),
                    HoraCarga = x.HoraCarga,
                    HoraEnViaje = x.HoraEnViaje,
                    HoraEntrega = x.HoraEntrega,
                    Recorrido = x.KmTotales.ToString() + " KM"
                }).ToList();

            int remitosCargaFaltan = lista.Count(x => string.IsNullOrEmpty(x.RtoC));
            int remitosEntregaFaltan = lista.Count(x => string.IsNullOrEmpty(x.RtoE));
            _view.MostrarRemitosFaltantes(remitosCargaFaltan, remitosEntregaFaltan);
            _view.CargarProgramas(lista);
        }

        private string CalcularPorcentaje(int? carga, int? entrega)
        {
            if (carga.HasValue && entrega.HasValue && carga != 0)
            {
                var diff = Math.Abs(carga.Value - entrega.Value);
                var percent = carga.Value > 0 ? (diff * 100.0 / carga.Value) : 0;
                return percent.ToString("0.##") + " %";
            }
            return "";
        }


        public async Task<Shared.Models.Ruteo?> MapearARuteoAsync(int idPrograma)
        {
            // Obtener la vistaPrograma original (podrías hacer un método extra en el repo)
            var programa = (await _programaRepositorio.ObtenerVistaProgramasAsync())
                .FirstOrDefault(x => x.IdPrograma == idPrograma);

            if (programa == null)
                return null;

            // Obtener IdChofer en base a idNomina (usa tu repositorio de nomina)
            int idChofer = 0;
            try
            {
                idChofer = await _nominaRepositorio.ObtenerPorIdAsync(programa.IdNomina)
                    .ContinueWith(n => n.Result?.IdChofer ?? 0);
            }
            catch { /* podés dejarlo en 0 si no existe */ }

            return new Shared.Models.Ruteo
            {
                IdNomina = programa.IdNomina,
                IdPrograma = programa.IdPrograma,
                Tractor = programa.Tractor,
                Semi = programa.Semi,
                Empresa = programa.Empresa,
                IdChofer = idChofer,
                Chofer = programa.Chofer,
                IdOrigen = programa.IdOrigen,
                Origen = programa.Origen,
                FechaCarga = programa.CargaSalida,
                IdDestino = programa.IdDestino,
                Destino = programa.Destino,
                FechaEntrega = programa.EntregaIngreso,
                AlbaranDespacho = programa.AlbaranDespacho,
                PedidoOr = programa.PedidoOr,
                IdProducto = 0, // Podés obtenerlo si lo necesitás
                Nombre = programa.Producto,
                CargaSalida = programa.CargaSalida,
                EntregaLlegada = programa.EntregaLlegada,
                EntregaIngreso = programa.EntregaIngreso,
                // Odometer, Location, Sat, Estado => vacíos o valores por defecto
            };
        }




        public async Task AbrirEditarProgramaAsync(Shared.Models.Ruteo ruteo)
        {
            await AbrirFormularioAsync<EditarProgramaForm>(async f =>
            {
                await f._presenter.InicializarAsync(ruteo);
            });
        }
    }
}
