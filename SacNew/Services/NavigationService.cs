using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, Form> _formularios;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _formularios = new Dictionary<Type, Form>();
        }

        public void ShowDialog<TForm>() where TForm : Form
        {
            var form = ObtenerFormulario<TForm>();
            if (form.Visible)
            {
                form.BringToFront();  // Si ya está abierto, lo lleva al frente.
            }
            else
            {
                form.ShowDialog();  // Mostrar como modal.
            }
        }

        public void HideForm<TForm>() where TForm : Form
        {
            if (_formularios.TryGetValue(typeof(TForm), out var form))
            {
                form.Hide();
            }
        }

        private TForm ObtenerFormulario<TForm>() where TForm : Form
        {
            if (!_formularios.TryGetValue(typeof(TForm), out var form) || form.IsDisposed)
            {
                form = _serviceProvider.GetService<TForm>();
                if (form == null) throw new InvalidOperationException($"No se pudo obtener el formulario {typeof(TForm).Name}.");
                _formularios[typeof(TForm)] = form;

                // Suscribirse al evento FormClosing para ocultar el formulario en lugar de cerrarlo.
                form.FormClosing += (sender, e) =>
                {
                    e.Cancel = true;  // Cancelar el cierre.
                    form.Hide();  // Ocultar el formulario.
                };
            }
            return (TForm)form;
        }
    }
}
