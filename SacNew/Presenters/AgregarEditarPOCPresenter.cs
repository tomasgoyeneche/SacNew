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
            var postas = await _postaRepositorio.ObtenerTodasLasPostasAsync();  // // Esperar el resultado de la tarea

            _view.CargarNominas(nominas);
            _view.CargarPostas(postas);
        }

        public void CargarDatosParaEditar(POC poc)
        {
            _pocActual = poc;
            _view.MostrarDatosPOC(poc);
        }

        public void GuardarPOC()
        {
            if (_pocActual == null)
            {
                // Crear nueva POC
                var nuevaPOC = new POC
                {
                    NumeroPOC = _view.NumeroPOC,
                    IdNomina = _view.IdNomina,
                    IdPosta = _view.IdPosta,
                    Odometro = _view.Odometro,
                    Comentario = _view.Comentario,
                    FechaCreacion = _view.FechaCreacion,
                    IdUsuario = _view.IdUsuario,
                    Activo = true
                };

                _pocRepositorio.AgregarPOC(nuevaPOC);
                _view.MostrarMensaje("POC creada exitosamente.");
            }
            else
            {
                // Actualizar POC existente
                _pocActual.NumeroPOC = _view.NumeroPOC;
                _pocActual.IdNomina = _view.IdNomina;
                _pocActual.IdPosta = _view.IdPosta;
                _pocActual.Odometro = _view.Odometro;
                _pocActual.Comentario = _view.Comentario;
                _pocActual.FechaCreacion = _view.FechaCreacion;
                _pocActual.IdUsuario = _view.IdUsuario;// Actualizar fecha de modificación

                _pocRepositorio.ActualizarPOC(_pocActual);
                _view.MostrarMensaje("POC actualizada exitosamente.");
            }
        }
    }
}