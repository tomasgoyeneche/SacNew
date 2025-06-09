using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Presenters
{
    public class EditarProgramaPresenter : BasePresenter<IEditarProgramaView>
    {
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;


        private Shared.Models.Ruteo _Ruteo;
        public EditarProgramaPresenter(
            ISesionService sesionService,
            IProgramaRepositorio programaRepositorio,
            IAlertaRepositorio alertaRepositorio,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _programaRepositorio = programaRepositorio;
            _alertaRepositorio = alertaRepositorio;
        }

        public async Task InicializarAsync(Shared.Models.Ruteo ruteo)
        {
            if (_Ruteo == null)
            {
                _Ruteo = ruteo;
            }

            Programa? programa = null;
            if (ruteo.IdPrograma > 0)
            {
                programa = await _programaRepositorio.ObtenerPorIdAsync(ruteo.IdPrograma);
            }

            List<AlertaDto> alertasNomina = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(ruteo.IdNomina);
            _view.MostrarAlertas(alertasNomina);
            // Si necesitás cargar otros datos, hacelo acá antes de mostrar
            _view.MostrarDatos(ruteo, programa);
            CargarArchivos();
        }

        public void CargarArchivos()
        {
            string carpeta = Path.Combine("S:", "Prog", _Ruteo.IdPrograma.ToString());
            List<ArchivoDocRuteo> archivos = new();

            if (Directory.Exists(carpeta))
                archivos = Directory.GetFiles(carpeta, "*.pdf")
                    .Select(x => new ArchivoDocRuteo
                    {
                        NombreArchivo = Path.GetFileName(x),
                        Ruta = x
                    })
                    .ToList();

            _view.MostrarArchivos(archivos);
        }

        public async Task GuardarFechaProgramaAsync(string campo, DateTime? fechaNueva)
        {
            await _programaRepositorio.ActualizarFechaYRegistrarAsync(_Ruteo.IdPrograma, campo, fechaNueva, _sesionService.IdUsuario, _Ruteo.IdNomina);

            await InicializarAsync(_Ruteo);
        }

        public async Task SubirArchivoRemitoAsync(string tipo, string archivoOrigen)
        {
            string campoRuta = tipo == "RC" ? "CargaRemitoRuta" :
                               tipo == "RE" ? "EntregaRemitoRuta" : "OtrosDocsRuta";

            string destinoBase = Path.Combine("S:", "Prog", _Ruteo.IdPrograma.ToString());
            if (!Directory.Exists(destinoBase))
                Directory.CreateDirectory(destinoBase);

            string fechaHora = DateTime.Now.ToString("dd MM yyyy HH mm");
            string nombreUsuario = _sesionService.NombreUsuario.Replace(" ", "").ToUpper();
            string extension = Path.GetExtension(archivoOrigen);

            string nuevoNombre = tipo == "Otros"
                ? $"{tipo} {nombreUsuario} {fechaHora}{extension}"
                : $"{tipo} {nombreUsuario} {fechaHora}.pdf";

            string rutaDestino = Path.Combine(destinoBase, nuevoNombre);

            if (tipo == "RC" || tipo == "RE")
            {
                var existentes = Directory.GetFiles(destinoBase, $"{tipo} *.pdf");
                foreach (var archivo in existentes)
                {
                    try { File.Delete(archivo); } catch { }
                }
            }

            File.Copy(archivoOrigen, rutaDestino, true);

            if (campoRuta != "OtrosDocsRuta")
            {
                await _programaRepositorio.ActualizarRutaRemitoAsync(
                    _Ruteo.IdPrograma,
                    campoRuta,
                    rutaDestino);
            }

            _view.MostrarMensaje("Documento subido correctamente.");

            await InicializarAsync(_Ruteo);
        }

        public async Task AbrirCargaRemitoFormAsync(string tipoRemito)
        {
            Programa? programa = await _programaRepositorio.ObtenerPorIdAsync(_Ruteo.IdPrograma);
            await AbrirFormularioAsync<CargarRemitoForm>(async form =>
            {
                await form._presenter.InicializarAsync(programa, _Ruteo, tipoRemito);
            });
        }

        public async Task ControlarAsync(string campoCheck)
        {
            await _programaRepositorio.ActualizarCheck(_Ruteo.IdPrograma, campoCheck, _sesionService.NombreUsuario);
            // Actualizar el campo correspondiente en Programa

            // Refrescá los datos en la UI
            await InicializarAsync(_Ruteo);
        }
    }
}
