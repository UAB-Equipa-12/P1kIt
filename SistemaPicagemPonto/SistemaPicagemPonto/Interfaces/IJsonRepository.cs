namespace SistemaPicagemPonto.Interfaces
{
    public interface IJsonRepository
    {
        void Guardar(IEnumerable<IRegistoPonto> dados);
        List<IRegistoPonto> Carregar();
    }
}
