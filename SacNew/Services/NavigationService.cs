using Microsoft.Extensions.DependencyInjection;

namespace SacNew.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, Form> _formularios;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _formularios = new Dictionary<Type, Form>();
        }

        public void ShowDialog<TForm>() where TForm : Form
        {
            var form = ObtenerFormulario<TForm>();
            if (form.Visible)
            {
                form.BringToFront();
            }
            else
            {
                form.ShowDialog();
            }
        }

        public void Show<TForm>() where TForm : Form
        {
            var form = ObtenerFormulario<TForm>();
            if (!form.Visible)
            {
                form.Show();
            }
            form.BringToFront();
        }

        public void HideForm<TForm>() where TForm : Form
        {
            if (_formularios.TryGetValue(typeof(TForm), out var form))
            {
                form.Hide();
            }
        }

        public void CloseForm<TForm>() where TForm : Form
        {
            if (_formularios.TryGetValue(typeof(TForm), out var form))
            {
                form.Close();
                _formularios.Remove(typeof(TForm));
            }
        }

        public bool IsFormOpen<TForm>() where TForm : Form
        {
            return _formularios.TryGetValue(typeof(TForm), out var form) && !form.IsDisposed && form.Visible;
        }

        private TForm ObtenerFormulario<TForm>() where TForm : Form
        {
            if (!_formularios.TryGetValue(typeof(TForm), out var form) || form.IsDisposed)
            {
                form = _serviceProvider.GetService<TForm>()
                    ?? throw new InvalidOperationException($"No se pudo obtener el formulario {typeof(TForm).Name}.");

                _formularios[typeof(TForm)] = form;

                form.FormClosing += (sender, e) =>
                {
                    e.Cancel = true;
                    form.Hide();
                };
            }

            return (TForm)form;
        }
    }
}