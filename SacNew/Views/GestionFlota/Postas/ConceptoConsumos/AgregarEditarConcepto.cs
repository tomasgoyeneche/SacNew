using SacNew.Models;
using SacNew.Repositories;

namespace SacNew.Views.GestionFlota.Postas.ConceptoConsumos
{
    public partial class AgregarEditarConcepto : Form
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConceptoTipoRepositorio _conceptoTipoRepositorio;
        private Concepto _conceptoActual;

        public AgregarEditarConcepto(IConceptoRepositorio conceptoRepositorio, IConceptoTipoRepositorio conceptoTipoRepositorio)
        {
            InitializeComponent();
            _conceptoRepositorio = conceptoRepositorio;
            _conceptoTipoRepositorio = conceptoTipoRepositorio;

            CargarTiposDeConsumo();
            CargarProveedores();
        }

        private void AgregarEditarConcepto_Load(object sender, EventArgs e)
        {
        }

        private void CargarTiposDeConsumo()
        {
            var tiposDeConsumo = _conceptoTipoRepositorio.ObtenerTodosLosTipos();

            // Asignar los tipos de consumo al ComboBox
            cmbTipoConsumo.DataSource = tiposDeConsumo;
            cmbTipoConsumo.DisplayMember = "Descripcion";  // Mostrar la descripción
            cmbTipoConsumo.ValueMember = "IdConsumoTipo";  // Usar el IdConsumoTipo como valor
        }

        private void CargarProveedores()
        {
            // Agregar los proveedores manualmente al ComboBox
            cmbProveedor.Items.Add("Bahía Blanca");
            cmbProveedor.Items.Add("Pz Huincul");
            // Puedes agregar otros proveedores si es necesario
        }

        public void CargarDatos(Concepto concepto)
        {
            _conceptoActual = concepto;

            // Cargar los datos del concepto en los controles del formulario
            txtCodigo.Text = _conceptoActual.Codigo;
            txtCodigo.Enabled = false;  // El código no se puede modificar al editar
            txtDescripcion.Text = _conceptoActual.Descripcion;
            cmbTipoConsumo.SelectedValue = _conceptoActual.IdTipoConsumo;
            txtPrecioActual.Text = _conceptoActual.PrecioActual.ToString("F2");
            txtPrecioAnterior.Text = _conceptoActual.PrecioAnterior.ToString("F2");
            dtpVigencia.Value = _conceptoActual.Vigencia;
            // Aquí puedes configurar el proveedor según tu lógica, si lo tienes como parte del concepto
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_conceptoActual == null)
            {
                // Crear un nuevo concepto si no existe
                var nuevoConcepto = new Concepto
                {
                    Codigo = txtCodigo.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    IdTipoConsumo = Convert.ToInt32(cmbTipoConsumo.SelectedValue),
                    PrecioActual = Convert.ToDecimal(txtPrecioActual.Text),
                    Vigencia = dtpVigencia.Value,
                    PrecioAnterior = Convert.ToDecimal(txtPrecioAnterior.Text),
                    Activo = true,  // Por defecto activo
                    IdUsuario = ObtenerUsuarioActual(),  // Función para obtener el usuario
                    FechaModificacion = DateTime.Now
                };

                _conceptoRepositorio.AgregarConcepto(nuevoConcepto);
                MessageBox.Show("Concepto agregado exitosamente.");
            }
            else
            {
                // Actualizar el concepto existente
                _conceptoActual.Descripcion = txtDescripcion.Text.Trim();
                _conceptoActual.IdTipoConsumo = Convert.ToInt32(cmbTipoConsumo.SelectedValue);
                _conceptoActual.PrecioActual = Convert.ToDecimal(txtPrecioActual.Text);
                _conceptoActual.Vigencia = dtpVigencia.Value;
                _conceptoActual.PrecioAnterior = Convert.ToDecimal(txtPrecioAnterior.Text);
                _conceptoActual.FechaModificacion = DateTime.Now;
                _conceptoActual.IdUsuario = ObtenerUsuarioActual();

                _conceptoRepositorio.ActualizarConcepto(_conceptoActual);
                MessageBox.Show("Concepto actualizado exitosamente.");
            }

            this.Close(); // Cerrar el formulario después de guardar
        }

        private int ObtenerUsuarioActual()
        {
            // Lógica para obtener el usuario actual, puede ser desde SesionService o similar
            return 1;  // Temporalmente se puede retornar un ID fijo
        }
    }
}