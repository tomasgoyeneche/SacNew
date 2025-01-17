using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas.Empresas;
using Shared.Models;

namespace GestionOperativa.Presenters
{
    public class AgregarEditarEmpresaPresenter : BasePresenter<IAgregarEditarEmpresaView>
    {
        private readonly IConfRepositorio _confRepositorio;
        private readonly IEmpresaSatelitalRepositorio _empresaSatelitalRepositorio;
        private readonly IEmpresaPaisRepositorio _empresaPaisRepositorio;

        public AgregarEditarEmpresaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IConfRepositorio confRepositorio,
            IEmpresaSatelitalRepositorio empresaSatelitalRepositorio,
            IEmpresaPaisRepositorio empresaPaisRepositorio)
            : base(sesionService, navigationService)
        {
            _confRepositorio = confRepositorio;
            _empresaSatelitalRepositorio = empresaSatelitalRepositorio;
            _empresaPaisRepositorio = empresaPaisRepositorio;
        }

        public async Task CargarDatosParaMostrarAsync(EmpresaDto empresa)
        {
            await EjecutarConCargaAsync(async () =>
            {
                _view.MostrarDatosEmpresa(empresa);
                await VerificarArchivosLegajoAsync(empresa.Cuit);

                var satelitales = await _empresaSatelitalRepositorio.ObtenerSatelitalesPorEmpresaAsync(empresa.IdEmpresa);
                _view.MostrarSatelitales(satelitales);

                var paises = await _empresaPaisRepositorio.ObtenerPaisesPorEmpresaAsync(empresa.IdEmpresa);
                _view.MostrarPaises(paises);
            });
        }

        private async Task VerificarArchivosLegajoAsync(string cuit)
        {
            var rutas = new Dictionary<string, Action<bool, string?>>
        {
            { "EMPRESA\\ART", (existe, ruta) => { _view.HabilitarBotonLegajoArt(existe); if (existe) _view.ConfigurarRutaArchivoArt(ruta); } },
            { "EMPRESA\\CUIT", (existe, ruta) => { _view.HabilitarBotonLegajoCuit(existe); if (existe) _view.ConfigurarRutaArchivoCuit(ruta); } }
        };

            foreach (var (subDirectorio, accion) in rutas)
            {
                var rutaArchivo = await ObtenerRutaPorIdAsync(5, subDirectorio, cuit); // ID 5 usado como ejemplo
                var archivoExiste = !string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo);
                accion(archivoExiste, rutaArchivo);
            }
        }

        private async Task<string?> ObtenerRutaPorIdAsync(int idConf, string subDirectorio, string cuit)
        {
            var conf = await _confRepositorio.ObtenerRutaPorIdAsync(idConf);
            return conf == null || string.IsNullOrEmpty(conf.Ruta)
                ? null
                : Path.Combine(conf.Ruta, subDirectorio, $"{cuit}.pdf");
        }
    }
}