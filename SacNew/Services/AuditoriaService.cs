using SacNew.Models;
using SacNew.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly ISesionService _sesionService;
        private readonly IAuditoriaRepositorio _auditoriaRepositorio;

        public AuditoriaService(ISesionService sesionService, IAuditoriaRepositorio auditoriaRepositorio)
        {
            _sesionService = sesionService;
            _auditoriaRepositorio = auditoriaRepositorio;
        }

        public async Task RegistrarAuditoriaAsync(string tablaModificada, string accion, int registroModificadoId, string valoresAnteriores, string valoresNuevos)
        {
            var auditoria = new Auditoria
            {
                IdUsuario = _sesionService.IdUsuario,
                TablaModificada = tablaModificada,
                Accion = accion,
                RegistroModificadoId = registroModificadoId,
                ValoresAnteriores = valoresAnteriores,
                ValoresNuevos = valoresNuevos,
                FechaHora = DateTime.Now
            };

            await _auditoriaRepositorio.AgregarAuditoriaAsync(auditoria);
        }
    }
}
