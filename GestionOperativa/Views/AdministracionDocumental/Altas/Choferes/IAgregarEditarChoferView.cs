using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Choferes
{
    public interface IAgregarEditarChoferView : IViewConMensajes
    {
        void MostrarDatosChofer(ChoferDto chofer);
        void ConfigurarFotoChofer(bool habilitar, string? rutaArchivo);
        void ConfigurarBotonAltaTemprana(bool habilitar, string? rutaArchivo);
        void ConfigurarBotonApto(bool habilitar, string? rutaArchivo);
        void ConfigurarBotonCurso(bool habilitar, string? rutaArchivo);
        void ConfigurarBotonDNI(bool habilitar, string? rutaArchivo);
        void ConfigurarBotonLicencia(bool habilitar, string? rutaArchivo);
        void ConfigurarBotonSeguro(bool habilitar, string? rutaArchivo);
    }
}
