using SistemaPicagemPonto.Model;

namespace SistemaPicagemPonto.Interfaces
{
    public interface IPontoModel
    {
        void RegistarEntrada(int colaboradorId);
        void RegistarSaida(int colaboradorId);
        string GetPassword(string username);
        void ObterRegistos(ref List<RegistoPonto> registos);
    }
}