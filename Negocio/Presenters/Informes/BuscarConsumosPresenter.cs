using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views.Postas.Informes.ConsultarConsumos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Presenters.Informes
{
    public class BuscarConsumosPresenter : BasePresenter<IBuscarConsumosView>
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IConsumoOtrosRepositorio _consumoOtrosRepositorio;

        public BuscarConsumosPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IConceptoRepositorio conceptoRepositorio,
            IPostaRepositorio postaRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IChoferRepositorio choferRepositorio,
            IConsumoOtrosRepositorio consumoOtrosRepositorio
        ) : base(sesionService, navigationService)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _postaRepositorio = postaRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _choferRepositorio = choferRepositorio;
            _consumoOtrosRepositorio = consumoOtrosRepositorio;
        }

        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var conceptos = await _conceptoRepositorio.ObtenerTodosLosConceptosAsync();
                var postas = await _postaRepositorio.ObtenerTodasLasPostasAsync();
                var empresas = await _empresaRepositorio.ObtenerTodasLasEmpresasAsync();
                var unidades = await _unidadRepositorio.ObtenerUnidadesPatenteDtoAsync();
                var choferes = await _choferRepositorio.ObtenerTodosLosChoferes();

                _view.CargarConceptos(conceptos);
                _view.CargarPostas(postas);
                _view.CargarEmpresas(empresas);
                _view.CargarUnidades(unidades);
                _view.CargarChoferes(choferes);
            });
        }

        public async Task BuscarConsumosAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<InformeConsumoPocDto> resultados = await _consumoOtrosRepositorio.BuscarConsumosAsync(
                    _view.IdConcepto,
                    _view.IdPosta,
                    _view.IdEmpresa,
                    _view.IdUnidad,
                    _view.IdChofer,
                    _view.NumeroPoc,
                    _view.Estado,
                    _view.FechaCreacionDesde,
                    _view.FechaCreacionHasta,
                    _view.FechaCierreDesde,
                    _view.FechaCierreHasta
                );

                await AbrirFormularioAsync<MostrarResultadosConsumosForm>(async form =>
                {
                    form.MostrarResultados(resultados);
                });
            });
        }
    }
}
