using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew.Presenters
{
    public class AgregarEditarPOCPresenter
    {
        private IAgregarEditarPOCView _view;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;
        private readonly ISesionService _sesionService;
        private readonly IRepositorioPOC _pocRepositorio;
        public POC _pocActual;

        public AgregarEditarPOCPresenter(
            INominaRepositorio nominaRepositorio,
            IPostaRepositorio postaRepositorio,
            ISesionService sesionService,
            IRepositorioPOC pocRepositorio)
        {
            _nominaRepositorio = nominaRepositorio;
            _postaRepositorio = postaRepositorio;
            _sesionService = sesionService;
            _pocRepositorio = pocRepositorio;
        }

        public void SetView(IAgregarEditarPOCView view)
        {
            _view = view;
        }

        public int IdUsuario => _sesionService.IdUsuario;

        public async void Inicializar()
        {
            var nominas = _nominaRepositorio.ObtenerTodasLasNominas();
            var postas = await _postaRepositorio.ObtenerTodasLasPostasAsync();
            var nominasOrdenadas = nominas.OrderBy(n => n.DescripcionNomina).ToList();

            _view.CargarNominas(nominasOrdenadas);
            _view.CargarPostas(postas);
        }

        public void CargarDatosParaEditar(POC poc)
        {
            _pocActual = poc;
            _view.MostrarDatosPOC(poc);
        }

        public void GuardarPOC()
        {
            var poc = _pocActual ?? new POC { Activo = true };

            poc.NumeroPOC = _view.NumeroPOC;
            poc.IdNomina = _view.IdNomina;
            poc.IdPosta = _view.IdPosta;
            poc.Odometro = _view.Odometro;
            poc.Comentario = _view.Comentario;
            poc.FechaCreacion = _view.FechaCreacion;
            poc.IdUsuario = _view.IdUsuario;

            if (_pocActual == null)
            {
                _pocRepositorio.AgregarPOC(poc);
                _view.MostrarMensaje("POC creada exitosamente.");
            }
            else
            {
                _pocRepositorio.ActualizarPOC(poc);
                _view.MostrarMensaje("POC actualizada exitosamente.");
            }
        }
    }
}