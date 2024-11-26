using FluentValidation;
using FluentValidation.Results;
using SacNew.Interfaces;
using SacNew.Services;

namespace SacNew.Presenters
{
    public abstract class BasePresenter<TView>
    {
        protected readonly ISesionService _sesionService;
        protected readonly INavigationService _navigationService;
        protected TView _view;

        public BasePresenter(ISesionService sesionService, INavigationService navigationService)
        {
            _sesionService = sesionService;
            _navigationService = navigationService;
        }

        public void SetView(TView view) => _view = view;

        protected async Task<bool> ValidarAsync<T>(T entidad)
        {
            var validador = _navigationService.ResolverServicio<IValidator<T>>();

            if (validador == null)
            {
                MostrarMensaje($"No se encontró un validador para {typeof(T).Name}.");
                return false;
            }

            var resultado = await validador.ValidateAsync(entidad);
            if (!resultado.IsValid)
            {
                MostrarErrores(resultado.Errors);
                return false;
            }

            return true;
        }

        protected async Task AbrirFormularioAsync<TForm>(Func<TForm, Task> configurarFormulario) where TForm : Form
        {
            var formulario = _navigationService.ObtenerFormulario<TForm>();
            await configurarFormulario(formulario);
            _navigationService.ShowDialog<TForm>();
        }

        protected async Task EjecutarConCargaAsync(Func<Task> accion, Func<Task>? postAccion = null)
        {
            await ManejarErroresAsync(async () =>
            {
                await accion();
                if (postAccion != null) await postAccion();
            });
        }

        protected async Task EjecutarConCargaAsync<T>(Func<Task<T>> accion, Action<T> mostrarResultado)
        {
            await ManejarErroresAsync(async () =>
            {
                var resultado = await accion();
                mostrarResultado(resultado);
            });
        }

        protected async Task ManejarErroresAsync(Func<Task> accion)
        {
            try
            {
                await accion();
            }
            catch (ValidationException ex)
            {
                MostrarErrores(ex.Errors);
            }
        }

        private void MostrarErrores(IEnumerable<ValidationFailure> errores) =>
                   MostrarMensaje($"Errores de validación:\n{string.Join("\n", errores.Select(e => e.ErrorMessage))}");

        private void MostrarMensaje(string mensaje)
        {
            if (_view is IViewConMensajes viewConMensajes)
            {
                viewConMensajes.MostrarMensaje(mensaje);
            }
        }

        protected void MostrarNombreUsuario()
        {
            if (_view is IViewConUsuario viewConUsuario)
            {
                viewConUsuario.MostrarNombreUsuario(_sesionService.NombreCompleto);
            }
        }
    }
}