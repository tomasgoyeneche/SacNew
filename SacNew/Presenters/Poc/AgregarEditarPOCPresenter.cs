using SacNew.Models;
using SacNew.Repositories;
using SacNew.Repositories.Chofer;
using SacNew.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;

namespace SacNew.Presenters
{
    public class AgregarEditarPOCPresenter : BasePresenter<IAgregarEditarPOCView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;

        public POC PocActual { get; private set; }

        public AgregarEditarPOCPresenter(
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IPostaRepositorio postaRepositorio,
            IPOCRepositorio pocRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _choferRepositorio = choferRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _postaRepositorio = postaRepositorio;
            _pocRepositorio = pocRepositorio;
        }

        public int IdUsuario => _sesionService.IdUsuario;

        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var unidades = _unidadRepositorio.ObtenerUnidadesPatenteDto();
                var postas = await _postaRepositorio.ObtenerTodasLasPostasAsync();
                var unidadesOrdenadas = unidades.OrderBy(n => n.DescripcionUnidad).ToList();
                var choferes = await _choferRepositorio.ObtenerTodosLosChoferes();
                var choferesOrdenados = choferes.OrderBy(c => c.Apellido).ToList();


                _view.CargarChoferes(choferesOrdenados);
                _view.CargarNominas(unidadesOrdenadas);
                _view.CargarPostas(postas);
            });
        }

        public void CargarDatosParaEditar(POC poc)
        {
            PocActual = poc;
            _view.MostrarDatosPOC(poc);
        }

        public async Task GuardarPOCAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var poc = PocActual ?? new POC { Estado = "Abierta" };

                // Validaciones antes de guardar
                if (string.IsNullOrWhiteSpace(_view.NumeroPOC))
                {
                    throw new InvalidOperationException("El número de POC es requerido.");
                }

                poc.NumeroPoc = _view.NumeroPOC;
                poc.IdUnidad = _view.IdUnidad;
                poc.IdChofer = _view.IdChofer;
                poc.IdPosta = _view.IdPosta;
                poc.Odometro = _view.Odometro;
                poc.Comentario = _view.Comentario;
                poc.FechaCreacion = _view.FechaCreacion;
                poc.IdUsuario = _view.IdUsuario;

                if (PocActual == null)
                {
                    await _pocRepositorio.AgregarPOCAsync(poc);
                    _view.MostrarMensaje("POC creada exitosamente.");
                }
                else
                {
                    await _pocRepositorio.ActualizarPOCAsync(poc);
                    _view.MostrarMensaje("POC actualizada exitosamente.");
                }
            });
        }
    }
}