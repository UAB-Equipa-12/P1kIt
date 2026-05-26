namespace SistemaPicagemPonto.Interfaces
{
    public interface IPontoModel
    {
        void RegistarEntrada(int colaboradorId);
        void RegistarSaida(int colaboradorId);
        string GetPassword(string username);
        List<IRegistoPonto> ObterRegistos();
        bool ValidarLogin(string username, string password);
    }
}