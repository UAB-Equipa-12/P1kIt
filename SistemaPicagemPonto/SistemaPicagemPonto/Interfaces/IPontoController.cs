using SistemaPicagemPonto.Interfaces;

namespace SistemaPicagemPonto.Controller
{
    public interface IPontoController
    {
        string? UtilizadorLogado { get; }
        bool EstaAutenticado { get; }
        bool RegistarEntrada(string inputId);
        bool RegistarSaida(string inputId);
        bool ValidarLogin(string username, string password);
        void TerminarSessao();
        double CalcularTotalHoras(int colaboradorId, DateTime? dataInicio = null, DateTime? dataFim = null);
        List<IRegistoPonto> ObterHistorico(int? colaboradorId = null, DateTime? dataInicio = null, DateTime? dataFim = null);
    }
}