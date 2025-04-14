using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    internal class EmpresaSeguroEntidadRepositorio : BaseRepositorio, IEmpresaSeguroEntidadRepositorio
    {
        public EmpresaSeguroEntidadRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<EmpresaSeguroEntidad>> ObtenerTodasAsync()
        {
            const string query = @"SELECT idEmpresaSeguroEntidad, Descripcion 
                           FROM EmpresaSeguroEntidad 
                           WHERE Activo = 1 
                           ORDER BY Descripcion";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<EmpresaSeguroEntidad>(query).ContinueWith(task => task.Result.ToList());
            });
        }
    }
}
