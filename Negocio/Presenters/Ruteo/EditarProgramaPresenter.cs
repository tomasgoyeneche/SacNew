using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;
using System.IO;

namespace GestionFlota.Presenters
{
    public class EditarProgramaPresenter : BasePresenter<IEditarProgramaView>
    {
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IProgramaExtranjeroRepositorio _programaExtranjeroRepositorio; // injectalo por constructor

        private Shared.Models.Ruteo _Ruteo;

        public EditarProgramaPresenter(
            ISesionService sesionService,
            IProgramaRepositorio programaRepositorio,
            IAlertaRepositorio alertaRepositorio,
            INominaRepositorio nominaRepositorio,
            ILocacionRepositorio locacionRepositorio,
            IProgramaExtranjeroRepositorio programaExtranjeroRepositorio,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _programaRepositorio = programaRepositorio;
            _alertaRepositorio = alertaRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _locacionRepositorio = locacionRepositorio;
            _programaExtranjeroRepositorio = programaExtranjeroRepositorio; // inicializalo aquí
        }

        public async Task InicializarAsync(Shared.Models.Ruteo ruteo)
        {
            _Ruteo = ruteo;

            Locacion? locacion = await _locacionRepositorio.ObtenerPorIdAsync(ruteo.IdDestino);
            _Ruteo.Destino = locacion?.Nombre ?? "Desconocido";

            Programa? programa = null;
            if (ruteo.IdPrograma > 0)
            {
                programa = await _programaRepositorio.ObtenerPorIdAsync(ruteo.IdPrograma);
            }

            List<ProgramaExtranjero> hitosExtranjero = new();
            if (programa?.Extranjero == true)
            {
                hitosExtranjero = await _programaExtranjeroRepositorio.ObtenerHitosextranjerosPorProgramaAsync(programa.IdPrograma);
            }
           

            List<AlertaDto> alertasNomina = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(ruteo.IdNomina);
            _view.MostrarAlertas(alertasNomina);
            // Si necesitás cargar otros datos, hacelo acá antes de mostrar
            _view.MostrarDatos(ruteo, programa, hitosExtranjero);

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
            await _programaRepositorio.ActualizarFechaYRegistrarAsync(_Ruteo.IdPrograma, campo, fechaNueva, _Ruteo.IdNomina, _sesionService.IdUsuario);
            if(campo == "EntregaSalida" && fechaNueva == null)
            {
                ProgramaTramo programaTramo = new ProgramaTramo
                {
                    IdPrograma = _Ruteo.IdPrograma,
                    IdDestino = _Ruteo.IdDestino,
                    IdNomina = _Ruteo.IdNomina,
                    FechaInicio = DateTime.Now,
                    FechaFin = null,
                };
                await _programaRepositorio.InsertarProgramaTramoAsync(programaTramo);
            }
            else if (campo == "EntregaSalida")
            {
                await _programaRepositorio.CerrarTramosActivosPorProgramaAsync(_Ruteo.IdPrograma, fechaNueva);
            }
            
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
            await InicializarAsync(_Ruteo);
        }

        public async Task RegistrarComentarioAsync(string comentario)
        {
            await _nominaRepositorio.RegistrarNominaAsync(
                _Ruteo.IdNomina, // o el disponible.IdNomina según contexto
                "Comentario",
                comentario,
                _sesionService.IdUsuario
            );
            _view.MostrarMensaje("Comentario registrado correctamente.");
        }

        public async Task ControlarAsync(string campoCheck)
        {
            await _programaRepositorio.ActualizarCheck(_Ruteo.IdPrograma, campoCheck, _sesionService.NombreUsuario);
            // Actualizar el campo correspondiente en Programa

            // Refrescá los datos en la UI
            await InicializarAsync(_Ruteo);
        }

        public async Task CambiarChoferAsync()
        {
            await AbrirFormularioAsync<CambioChoferForm>(async form =>
            {
                await form._presenter.InicializarAsync(_Ruteo.IdNomina);
            });
            _view.Close();
        }


        public async Task GuardarFechaExtranjeroAsync(
            int idProgramaTipoPunto,
            int idProgramaTipoEvento,
            DateTime? fechaNueva,
            string evento // Ej: "Llegada Aduana Arg"
        )
        {
            // 1. Buscar si ya existe el registro activo para este punto/evento
            var hitos = await _programaExtranjeroRepositorio.ObtenerHitosextranjerosPorProgramaAsync(_Ruteo.IdPrograma);
            var hitoExistente = hitos.FirstOrDefault(x =>
                x.IdProgramaTipoPunto == idProgramaTipoPunto &&
                x.IdProgramaTipoEvento == idProgramaTipoEvento);

            decimal? odometro = null;
            if (fechaNueva.HasValue)
                odometro = await _programaRepositorio.ObtenerOdometerPorNomina(_Ruteo.IdNomina);

            if (fechaNueva == null)
            {
                // Eliminar (baja lógica)
                if (hitoExistente != null)
                {
                    await _programaExtranjeroRepositorio.BajaHitoExtranjeroAsync(hitoExistente.IdProgramaExtranjero);

                    await _programaRepositorio.RegistrarProgramaAsync(
                        _Ruteo.IdPrograma,
                        $"Borró {evento}",
                        hitoExistente.Fecha.ToString("dd/MM/yyyy HH:mm"),
                        _sesionService.IdUsuario
                    );
                }
            }
            else
            {
                if (hitoExistente == null)
                {
                    // Agregar
                    var nuevoHito = new ProgramaExtranjero
                    {
                        IdPrograma = _Ruteo.IdPrograma,
                        IdProgramaTipoPunto = idProgramaTipoPunto,
                        IdProgramaTipoEvento = idProgramaTipoEvento,
                        Odometro = odometro ?? 0,
                        Fecha = fechaNueva.Value,
                        Activo = true
                    };
                    await _programaExtranjeroRepositorio.InsertarHitoExtranjeroAsync(nuevoHito);

                    await _programaRepositorio.RegistrarProgramaAsync(
                        _Ruteo.IdPrograma,
                        $"Agregó {evento}",
                        fechaNueva.Value.ToString("dd/MM/yyyy HH:mm"),
                        _sesionService.IdUsuario
                    );
                }
                else
                {
                    // Editar
                    await _programaExtranjeroRepositorio.ActualizarHitoExtranjeroAsync(
                        hitoExistente.IdProgramaExtranjero,
                        fechaNueva.Value,
                        odometro ?? 0
                    );

                    await _programaRepositorio.RegistrarProgramaAsync(
                        _Ruteo.IdPrograma,
                        $"Editó {evento}",
                        fechaNueva.Value.ToString("dd/MM/yyyy HH:mm"),
                        _sesionService.IdUsuario
                    );
                }
            }

            // Refrescá la pantalla
            await InicializarAsync(_Ruteo);
        }
    }
}