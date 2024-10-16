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

        protected async Task ManejarErroresAsync(Func<Task> accion)
        {
            try
            {
                await accion();
            }
            catch (Exception ex)
            {
                if (_view is IViewConMensajes viewConMensajes)
                    viewConMensajes.MostrarMensaje($"Ocurrió un error: {ex.Message}");
            }
        }

        protected void MostrarNombreUsuario()
        {
            if (_view is IViewConUsuario viewConUsuario)
            {
                viewConUsuario.MostrarNombreUsuario(_sesionService.NombreCompleto);
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

        protected async Task AbrirFormularioAsync<TForm>(Func<TForm, Task> configurarFormulario) where TForm : Form
        {
            var formulario = _serviceProvider.GetService<TForm>();
            if (formulario != null)
            {
                await configurarFormulario(formulario);
                formulario.ShowDialog();
            }
        }

        

    }
}