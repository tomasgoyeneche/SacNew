using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;
using Shared.Models;

namespace GestionOperativa.Presenters.AdministracionDocumental.Altas.Empresas
{
    public class AgregarEditarSeguroPresenter : BasePresenter<IAgregarEditarSeguroView>
    {
        private readonly IEmpresaSeguroEntidadRepositorio _entidadRepo;
        private readonly ICiaRepositorio _ciaRepo;
        private readonly ICoberturaRepositorio _coberturaRepo;
        private readonly IEmpresaSeguroRepositorio _seguroRepo;

        private int _idEmpresa;
        public EmpresaSeguro? _seguro;

        public AgregarEditarSeguroPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IEmpresaSeguroEntidadRepositorio entidadRepo,
            ICiaRepositorio ciaRepo,
            ICoberturaRepositorio coberturaRepo,
            IEmpresaSeguroRepositorio seguroRepo)
            : base(sesionService, navigationService)
        {
            _entidadRepo = entidadRepo;
            _ciaRepo = ciaRepo;
            _coberturaRepo = coberturaRepo;
            _seguroRepo = seguroRepo;
        }

        public async Task InicializarAsync(EmpresaSeguro? EmpresaSeguro, int idEmpresa)
        {
            if (EmpresaSeguro != null)
            {
                _seguro = EmpresaSeguro;
            }
            _idEmpresa = idEmpresa;

            List<EmpresaSeguroEntidad> entidades = await _entidadRepo.ObtenerTodasAsync();
            List<Cobertura> coberturas = await _coberturaRepo.ObtenerTodasAsync();

            _view.CargarEntidades(entidades);
            _view.CargarCoberturas(coberturas);

            if (_seguro != null)
            {
                List<Cia> cias = await _ciaRepo.ObtenerPorTipoAsync(_seguro.idEmpresaSeguroEntidad);
                _view.CargarCias(cias);
                _view.InicializarValores(_seguro);
            }
        }

        public async Task ActualizarCiasPorEntidad(int idEntidad)
        {
            var cias = await _ciaRepo.ObtenerPorTipoAsync(idEntidad);
            _view.CargarCias(cias);
        }

        public async Task GuardarAsync()
        {
            var nuevo = new EmpresaSeguro
            {
                idEmpresa = _idEmpresa,
                idEmpresaSeguroEntidad = _view.IdEmpresaSeguroEntidad,
                idCia = _view.IdCia,
                idCobertura = _view.IdCobertura,
                numeroPoliza = _view.NumeroPoliza,
                certificadoMensual = _view.CertificadoMensual,
                vigenciaAnual = _view.VigenciaAnual
            };

            if (_seguro == null)
            {
                await _seguroRepo.AgregarSeguroAsync(nuevo);
                _view.MostrarMensaje("Seguro agregado correctamente.");
            }
            else
            {
                nuevo.idEmpresaSeguro = _seguro.idEmpresaSeguro;
                await _seguroRepo.ActualizarSeguroAsync(nuevo);
                _view.MostrarMensaje("Seguro actualizado correctamente.");
            }
        }
    }
}