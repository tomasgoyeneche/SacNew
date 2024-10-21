using Castle.DynamicProxy;
using Dapper;
using Newtonsoft.Json;
using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public class AuditoriaInterceptor : IInterceptor
    {
        private readonly ISesionService _sesionService;
        private readonly string _connectionString;

        public AuditoriaInterceptor(ISesionService sesionService, string connectionString)
        {
            _sesionService = sesionService;
            _connectionString = connectionString;
        }

        public void Intercept(IInvocation invocation)
        {
            var metodo = invocation.Method.Name;
            var tipo = invocation.TargetType.Name;

            var valoresAnteriores = invocation.Arguments.FirstOrDefault();
            invocation.Proceed(); // Ejecuta el método original
            var resultado = invocation.ReturnValue;

            var valoresNuevos = invocation.Arguments.FirstOrDefault();

            RegistrarAuditoriaAsync(tipo, metodo, valoresAnteriores, valoresNuevos).Wait();
        }

        private async Task RegistrarAuditoriaAsync(string tabla, string accion, object? valoresAnteriores, object? valoresNuevos)
        {
            var valoresAnterioresJson = valoresAnteriores != null ? JsonConvert.SerializeObject(valoresAnteriores) : string.Empty;
            var valoresNuevosJson = valoresNuevos != null ? JsonConvert.SerializeObject(valoresNuevos) : string.Empty;

            var auditoria = new Auditoria
            {
                IdUsuario = _sesionService.IdUsuario,
                TablaModificada = tabla,
                Accion = accion,
                ValoresAnteriores = valoresAnterioresJson,
                ValoresNuevos = valoresNuevosJson,
                FechaHora = DateTime.Now
            };

            const string query = @"
            INSERT INTO Auditoria (IdUsuario, TablaModificada, Accion, ValoresAnteriores, ValoresNuevos, FechaHora)
            VALUES (@IdUsuario, @TablaModificada, @Accion, @ValoresAnteriores, @ValoresNuevos, @FechaHora)";

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(query, auditoria);
        }
    }
}
