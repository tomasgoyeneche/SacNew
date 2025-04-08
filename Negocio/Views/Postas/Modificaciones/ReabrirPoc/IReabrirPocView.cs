using Core.Interfaces;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views.Postas.Modificaciones.ReabrirPoc
{
    public interface IReabrirPocView : IViewConMensajes
    {
        void MostrarPOC(List<POCDto> listaPOC);

        DialogResult ConfirmarEliminacion(string mensaje);  
    }
}
