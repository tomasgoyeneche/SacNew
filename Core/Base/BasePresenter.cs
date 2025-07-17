using Core.Interfaces;
using Core.Services;
using Core.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Base
{
    public abstract class BasePresenter<TView>
    {
        protected readonly ISesionService _sesionService;
        protected readonly INavigationService _navigationService;
        protected TView? _view;

        public BasePresenter(ISesionService sesionService, INavigationService navigationService)
        {
            _sesionService = sesionService;
            _navigationService = navigationService;
        }

        public void SetView(TView? view) => _view = view;

        protected async Task<bool> ValidarAsync<T>(T entidad, params object[] parametros)
        {
            var validador = _navigationService.ResolverServicio<IValidator<T>>();

            if (validador == null)
            {
                MostrarMensaje($"No se encontró un validador para {typeof(T).Name}.");
                return false;
            }

            // Si el validador implementa una interfaz personalizada para configuraciones adicionales
            if (validador is IConfigurableValidator<T> configurableValidator)
            {
                configurableValidator.Configurar(parametros);
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

        public async Task AbrirFormularioConPermisosAsync<TForm>(
        string permisoRequerido,
        Func<TForm, Task>? configurarFormulario = null,
         bool modal = true) where TForm : Form
        {
            if (_sesionService.Permisos.Contains(permisoRequerido) || _sesionService.Permisos.Contains("0000-SuperAdmin"))
            {
                var formulario = _navigationService.ObtenerFormulario<TForm>();

                if (configurarFormulario != null)
                    await configurarFormulario(formulario);

                if (modal)
                    _navigationService.ShowDialog<TForm>();
                else
                    _navigationService.Show<TForm>();
            }
            else
            {
                MessageBox.Show("No tienes permisos para acceder a esta sección.",
                                "Permiso Denegado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
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
            catch (Exception ex)
            {
                MostrarMensaje($"Ocurrió un error: {ex.Message}");
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
    }
}