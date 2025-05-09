using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionOperativa.Views.AdministracionDocumental.Altas;

namespace GestionOperativa.Presenters.AdministracionDocumental.Altas
{
    public class EditarVencimientosPresenter : BasePresenter<IModificarVencimientosView>
    {
        private readonly IChoferRepositorio _choferRepo;
        private readonly ITractorRepositorio _tractorRepo;
        private readonly ISemiRepositorio _semiRepo;
        private readonly IUnidadRepositorio _unidadRepo;

        private string _entidad;
        private int _idEntidad;

        public EditarVencimientosPresenter(
            IChoferRepositorio choferRepo,
            ITractorRepositorio tractorRepo,
            ISemiRepositorio semiRepo,
            IUnidadRepositorio unidadRepo,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _choferRepo = choferRepo;
            _tractorRepo = tractorRepo;
            _semiRepo = semiRepo;
            _unidadRepo = unidadRepo;
        }

        public async Task InicializarAsync(string entidad, int idEntidad)
        {
            _entidad = entidad.ToLower();
            _idEntidad = idEntidad;

            var vencimientos = new Dictionary<int, (string etiqueta, DateTime? fecha)>();

            switch (_entidad)
            {
                case "chofer":
                    var chofer = await _choferRepo.ObtenerPorIdDtoAsync(idEntidad);
                    vencimientos[1] = ("Licencia", chofer.Licencia);
                    vencimientos[2] = ("Psicofísico Apto", chofer.PsicofisicoApto);
                    vencimientos[3] = ("Psicofísico Curso", chofer.PsicofisicoCurso);
                    vencimientos[4] = ("Examen Anual", chofer.ExamenAnual);
                    break;

                case "tractor":
                    var tractor = await _tractorRepo.ObtenerPorIdDtoAsync(idEntidad);
                    vencimientos[2] = ("VTV", tractor.Vtv);
                    break;

                case "semi":
                    var semi = await _semiRepo.ObtenerPorIdDtoAsync(idEntidad);
                    vencimientos[2] = ("VTV", semi.Vtv);
                    vencimientos[5] = ("Espesor", semi.CisternaEspesor);
                    vencimientos[6] = ("Visual Interna", semi.VisualInterna);
                    vencimientos[7] = ("Visual Externa", semi.VisualExterna);
                    vencimientos[8] = ("Estanqueidad", semi.Estanqueidad);
                    break;

                case "unidad":
                    var unidad = await _unidadRepo.ObtenerPorIdDtoAsync(idEntidad);
                    vencimientos[1] = ("Más YPF", unidad.MasYPF);
                    vencimientos[2] = ("Calibrado", unidad.Calibrado);
                    vencimientos[3] = ("CheckList", unidad.Checklist);
                    break;

                default:
                    _view.MostrarMensaje("Entidad no soportada.");
                    return;
            }

            _view.MostrarVencimientos(vencimientos);
        }

        public async Task GuardarCambiosAsync()
        {
            var fechasActualizadas = _view.ObtenerFechasActualizadas();

            foreach (var (idVencimiento, nuevaFecha) in fechasActualizadas)
            {
                if (!nuevaFecha.HasValue) continue;

                switch (_entidad)
                {
                    case "chofer":
                        await _choferRepo.ActualizarVencimientoChoferAsync(_idEntidad, idVencimiento, nuevaFecha.Value, _sesionService.IdUsuario);
                        break;

                    case "tractor":
                        await _tractorRepo.ActualizarVencimientoTractorAsync(_idEntidad, idVencimiento, nuevaFecha.Value, _sesionService.IdUsuario);
                        break;

                    case "semi":
                        await _semiRepo.ActualizarVencimientoSemiAsync(_idEntidad, idVencimiento, nuevaFecha.Value, _sesionService.IdUsuario);
                        break;

                    case "unidad":
                        await _unidadRepo.ActualizarVencimientoUnidadAsync(_idEntidad, idVencimiento, nuevaFecha.Value, _sesionService.IdUsuario);
                        break;
                }
            }

            _view.MostrarMensaje("Vencimientos actualizados correctamente.");
        }
    }
}