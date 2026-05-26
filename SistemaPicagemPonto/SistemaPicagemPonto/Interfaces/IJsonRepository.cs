using SistemaPicagemPonto.Model;

namespace SistemaPicagemPonto.Interfaces
{
    public interface IJsonRepository
    {
        void Guardar(List<RegistoPonto> dados);
        List<RegistoPonto> Carregar();
    }
}
