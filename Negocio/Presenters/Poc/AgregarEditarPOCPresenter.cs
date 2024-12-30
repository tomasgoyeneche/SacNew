using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class AgregarEditarPOCPresenter : BasePresenter<IAgregarEditarPOCView>
    {
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IPeriodoRepositorio _periodoRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;

        public POC? PocActual { get; private set; }

        public AgregarEditarPOCPresenter(
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IPeriodoRepositorio periodoRepositorio,
            IPOCRepositorio pocRepositorio,
            IPostaRepositorio postaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _choferRepositorio = choferRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _periodoRepositorio = periodoRepositorio;
            _pocRepositorio = pocRepositorio;
            _postaRepositorio = postaRepositorio;
        }

        public int IdUsuario => _sesionService.IdUsuario;

        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var unidades = await _unidadRepositorio.ObtenerUnidadesPatenteDtoAsync();
                var periodos = await _periodoRepositorio.ObtenerPeriodosActivosAsync();
                var unidadesOrdenadas = unidades.OrderBy(n => n.DescripcionUnidad).ToList();
                var choferes = await _choferRepositorio.ObtenerTodosLosChoferes();
                var choferesOrdenados = choferes.OrderBy(c => c.Apellido).ToList();

                _view.CargarChoferes(choferesOrdenados);
                _view.CargarNominas(unidadesOrdenadas);
                _view.CargarPeriodo(periodos);
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

                // Obtener el código de la posta
                var posta = await _postaRepositorio.ObtenerPorIdAsync(_sesionService.IdPosta);
                if (posta == null)
                {
                    throw new InvalidOperationException("La posta asociada no fue encontrada.");
                }

                var codigoPosta = posta.Codigo; // Campo 'Codigo' de la posta
                if (string.IsNullOrWhiteSpace(codigoPosta))
                {
                    throw new InvalidOperationException("La posta asociada no tiene un código definido.");
                }

                poc.NumeroPoc = $"{codigoPosta}-{_view.NumeroPOC}";
                poc.IdUnidad = _view.IdUnidad;
                poc.IdPosta = _sesionService.IdPosta;
                poc.IdChofer = _view.IdChofer;
                poc.IdPeriodo = _view.IdPeriodo;
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
                    poc.NumeroPoc = _view.NumeroPOC;
                    await _pocRepositorio.ActualizarPOCAsync(poc);
                    _view.MostrarMensaje("POC actualizada exitosamente.");
                }
            });
        }
    }
}