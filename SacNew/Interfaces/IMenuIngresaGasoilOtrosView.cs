namespace SacNew.Interfaces
{
    public interface IMenuIngresaGasoilOtrosView
    {
        string NumeroPoc { get; set; }
        string CreditoTotal { get; set; }
        string CreditoDisponible { get; set; }

        void MostrarMensaje(string mensaje);
    }
}