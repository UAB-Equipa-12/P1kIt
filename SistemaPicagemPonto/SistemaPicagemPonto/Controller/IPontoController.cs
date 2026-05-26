using SistemaPicagemPonto.Interfaces;

namespace SistemaPicagemPonto.Controller
{
    public interface IPontoController
    {
        bool RegistarEntrada(string inputId);
        bool RegistarSaida(string inputId);
        bool ValidarLogin(string username, string password);
        double CalcularTotalHoras(int colaboradorId, DateTime? dataInicio = null, DateTime? dataFim = null);
        List<IRegistoPonto> ObterHistorico(int? colaboradorId = null, DateTime? dataInicio = null, DateTime? dataFim = null);
    }
}