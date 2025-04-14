using DevExpress.XtraEditors;
using GestionOperativa.Presenters.AdministracionDocumental.Altas.Empresas;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public partial class AgregarEditarSeguro : DevExpress.XtraEditors.XtraForm, IAgregarEditarSeguroView
    {
        public readonly AgregarEditarSeguroPresenter _presenter;

        public AgregarEditarSeguro(AgregarEditarSeguroPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
            InitializeComponent();
        }

        public int IdEmpresa { get; private set; }
        public int IdEmpresaSeguroEntidad => (int)cmbEntidad.SelectedValue;
        public int IdCia => (int)cmbCia.SelectedValue;
        public int IdCobertura => (int)cmbCobertura.SelectedValue;
        public string NumeroPoliza => txtNumPoliza.Text.Trim();
        public DateTime CertificadoMensual => dtpCertificadoMensual.Value;
        public DateTime VigenciaAnual => dtpVigenciaAnual.Value;

        public void CargarEntidades(List<EmpresaSeguroEntidad> entidades)
        {
            cmbEntidad.DataSource = entidades;
            cmbEntidad.DisplayMember = "Descripcion";
            cmbEntidad.ValueMember = "IdEmpresaSeguroEntidad";
        }

        public void CargarCias(List<Cia> cias)
        {
            cmbCia.DataSource = cias;
            cmbCia.DisplayMember = "NombreFantasia";
            cmbCia.ValueMember = "IdCia";
        }

        public void CargarCoberturas(List<Cobertura> coberturas)
        {
            cmbCobertura.DataSource = coberturas;
            cmbCobertura.DisplayMember = "TipoCobertura";
            cmbCobertura.ValueMember = "IdCobertura";
        }

        public void InicializarValores(EmpresaSeguro seguro)
        {
            IdEmpresa = seguro.idEmpresa;
            cmbEntidad.SelectedValue = seguro.idEmpresaSeguroEntidad;
            cmbCobertura.SelectedValue = seguro.idCobertura;
            cmbCia.SelectedValue = seguro.idCia;
            txtNumPoliza.Text = seguro.numeroPoliza;
            dtpVigenciaAnual.Value = seguro.vigenciaAnual;
            dtpCertificadoMensual.Value = seguro.certificadoMensual;
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void cmbEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEntidad.SelectedValue is int idEntidad)
                await _presenter.ActualizarCiasPorEntidad(idEntidad);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await _presenter.GuardarAsync();
            cerrar();
        }

        public void cerrar()
        {
            _presenter._seguro = null;  
            IdEmpresa = 0;
            txtNumPoliza.Clear();
            dtpCertificadoMensual.Value = DateTime.Now; 
            dtpVigenciaAnual.Value = DateTime.Now;    
            Close();
        }   

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cerrar();
        }
    }
}