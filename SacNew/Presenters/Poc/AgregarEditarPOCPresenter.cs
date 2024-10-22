using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew.Presenters
{
    public class AgregarEditarPOCPresenter : BasePresenter<IAgregarEditarPOCView>
    {
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly IPOCRepositorio _pocRepositorio;

        public POC PocActual { get; private set; }

        public AgregarEditarPOCPresenter(
            INominaRepositorio nominaRepositorio,
            IPostaRepositorio postaRepositorio,
            IPOCRepositorio pocRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _nominaRepositorio = nominaRepositorio;
            _postaRepositorio = postaRepositorio;
            _pocRepositorio = pocRepositorio;
        }

        public int IdUsuario => _sesionService.IdUsuario;

        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var nominas = _nominaRepositorio.ObtenerTodasLasNominas();
                var postas = await _postaRepositorio.ObtenerTodasLasPostasAsync();
                var nominasOrdenadas = nominas.OrderBy(n => n.DescripcionNomina).ToList();

                _view.CargarNominas(nominasOrdenadas);
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
                var poc = PocActual ?? new POC { Activo = true };

                // Validaciones antes de guardar
                if (string.IsNullOrWhiteSpace(_view.NumeroPOC))
                {
                    throw new InvalidOperationException("El número de POC es requerido.");
                }

                poc.NumeroPOC = _view.NumeroPOC;
                poc.IdNomina = _view.IdNomina;
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