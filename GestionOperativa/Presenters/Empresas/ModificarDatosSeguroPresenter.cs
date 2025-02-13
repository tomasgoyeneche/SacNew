using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Empresas;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionOperativa.Presenters.Empresas
{
    public class ModificarDatosSeguroPresenter : BasePresenter<IModificarDatosSeguroView>
    {
        private readonly IEmpresaSeguroRepositorio _empresaSeguroRepositorio;
        private readonly ICiaRepositorio _ciaRepositorio;
        private readonly ICoberturaRepositorio _coberturaRepositorio;

        public ModificarDatosSeguroPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IEmpresaSeguroRepositorio empresaSeguroRepositorio,
            ICiaRepositorio ciaRepositorio,
            ICoberturaRepositorio coberturaRepositorio)
            : base(sesionService, navigationService)
        {
            _empresaSeguroRepositorio = empresaSeguroRepositorio;
            _ciaRepositorio = ciaRepositorio;
            _coberturaRepositorio = coberturaRepositorio;
        }

        public async Task InicializarAsync(int idEmpresa)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var seguro = await _empresaSeguroRepositorio.ObtenerPorEmpresaAsync(idEmpresa);
                var cias = await _ciaRepositorio.ObtenerTodasAsync();
                var coberturas = await _coberturaRepositorio.ObtenerTodasAsync();

                if (seguro == null)
                {
                    _view.MostrarMensaje("No se encontró el seguro asociado a la empresa.");
                    return;
                }

                _view.CargarDatosSeguro(seguro, cias, coberturas);
            });
        }

        public async Task GuardarCambios()
        {
            var seguro = new EmpresaSeguro
            {
                IdSeguroEmpresa = _view.IdSeguroEmpresa,
                IdEmpresa = _view.IdEmpresa,
                IdCia = _view.IdCia,
                IdCobertura = _view.IdCobertura,
                NumeroPoliza = _view.NumeroPoliza,
                VigenciaHasta = _view.VigenciaHasta,
                PagoDesde = _view.PagoDesde,
                PagoHasta = _view.PagoHasta
            };

                 await _empresaSeguroRepositorio.ActualizarAsync(seguro);
                _view.MostrarMensaje("Datos del seguro actualizados correctamente.");
         
        }
    }
}
