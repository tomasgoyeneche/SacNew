using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Repositories;
using SacNew.Views;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.IngresarConsumo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Presenters
{
    public class MenuIngresaGasoilOtrosPresenter
    {
        private IMenuIngresaGasoilOtrosView _view;
        private readonly IEmpresaCreditoRepositorio _empresaCreditoRepositorio;
        private readonly IRepositorioPOC _pocRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IServiceProvider _serviceProvider;

        public MenuIngresaGasoilOtrosPresenter(
            IEmpresaCreditoRepositorio empresaCreditoRepositorio,
            IRepositorioPOC pocRepositorio,
            INominaRepositorio nominaRepositorio
            , IServiceProvider serviceProvider)
        {
            _empresaCreditoRepositorio = empresaCreditoRepositorio ?? throw new ArgumentNullException(nameof(empresaCreditoRepositorio));
            _pocRepositorio = pocRepositorio ?? throw new ArgumentNullException(nameof(pocRepositorio));
            _nominaRepositorio = nominaRepositorio ?? throw new ArgumentNullException(nameof(nominaRepositorio));
            _serviceProvider = serviceProvider;
        }

        public void SetView(IMenuIngresaGasoilOtrosView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }


        public void IngresaGasoil()
        {
            var ingresaGasoil = _serviceProvider.GetService<IngresaGasoil>();
            ingresaGasoil.Show();
        }

        public async Task CargarDatosAsync(int idPoc)
        {
            if (idPoc <= 0)
            {
                _view.MostrarMensaje("El ID del POC no es válido.");
                return;
            }

            try
            {
                // Obtener los detalles de la POC
                var poc = await _pocRepositorio.ObtenerPorIdAsync(idPoc);
                if (poc == null)
                {
                    _view.MostrarMensaje("No se encontró el POC seleccionado.");
                    return;
                }

                // Mostrar el número de POC en el TextBox
                _view.NumeroPoc = poc.NumeroPOC;

                // Obtener la nomina para el POC y extraer el IdEmpresa
                var nomina = await _nominaRepositorio.ObtenerPorIdAsync(poc.IdNomina);
                if (nomina == null)
                {
                    _view.MostrarMensaje("No se encontró la nomina asociada al POC.");
                    return;
                }

                // Obtener el crédito de la empresa asociada
                var empresaCredito = await _empresaCreditoRepositorio.ObtenerPorEmpresaAsync(nomina.idEmpresa);
                if (empresaCredito == null)
                {
                    _view.MostrarMensaje("No se encontraron créditos para la empresa.");
                    return;
                }

                // Cargar el crédito total y el crédito disponible
                _view.CreditoTotal = empresaCredito.CreditoAsignado.ToString("C", new CultureInfo("es-AR"));
                _view.CreditoDisponible = empresaCredito.CreditoDisponible.ToString("C", new CultureInfo("es-AR"));
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Ocurrió un error al cargar los datos: {ex.Message}");
            }
        }
    }
}
