namespace Core.Validators
{
    public interface IConfigurableValidator<T>
    {
        void Configurar(params object[] parametros);
    }
}