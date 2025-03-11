using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITractorRepositorio
    {

        Task<List<TractorDto>> ObtenerTodosLosTractoresDto();

        Task<TractorDto> ObtenerPorIdDtoAsync(int idTractor);
    }
}
