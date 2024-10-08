using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public interface IAuditoriaService
    {
        Task RegistrarAuditoriaAsync(string tablaModificada, string accion, int registroModificadoId, string valoresAnteriores, string valoresNuevos);
    }
}
