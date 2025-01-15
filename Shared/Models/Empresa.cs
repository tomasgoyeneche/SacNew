using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }              // Identificador único de la empresa
        public string Cuit { get; set; }                // Número de CUIT de la empresa
        public int IdEmpresaTipo { get; set; }          // Identificador del tipo de empresa
        public string RazonSocial { get; set; }         // Razón social de la empresa
        public string NombreFantasia { get; set; }      // Nombre comercial de la empresa
        public int IdLocalidad { get; set; }            // Identificador de la localidad
        public string Domicilio { get; set; }           // Dirección específica de la empresa
        public string Telefono { get; set; }            // Teléfono de contacto
        public string Email { get; set; }               // Correo electrónico de contacto
        public bool Activo { get; set; }                // Indicador de si la empresa está activa
    }
}
