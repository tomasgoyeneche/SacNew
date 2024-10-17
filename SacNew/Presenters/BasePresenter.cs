using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Services;

namespace SacNew.Presenters
{
    public abstract class BasePresenter<TView>
    {
        protected readonly ISesionService _sesionService;
        protected readonly IServiceProvider _serviceProvider;
        protected TView _view;

        public BasePresenter(ISesionService sesionService, IServiceProvider serviceProvider)
        {
            _sesionService = sesionService;
            _serviceProvider = serviceProvider;
        }

        public void SetView(TView view) => _view = view;

        private TServicio ObtenerServicio<TServicio>()
        {
            return _serviceProvider.GetService<TServicio>()
                   ?? throw new InvalidOperationException($"No se pudo obtener el servicio {typeof(TServicio).Name}.");
        }

        protected async Task AbrirFormularioAsync<TForm>(Func<TForm, Task> configurarFormulario) where TForm : Form
        {
            var formulario = ObtenerServicio<TForm>();
            using (formulario)
            {
                await configurarFormulario(formulario);
                formulario.ShowDialog();
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

        protected async Task<bool> ValidarAsync<T>(T entidad)
        {
            var validador = ObtenerServicio<IValidator<T>>();
            var resultado = await validador.ValidateAsync(entidad);

            if (!resultado.IsValid)
            {
                MostrarErrores(resultado.Errors);
                return false;
            }
            return true;
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