using SistemaPicagemPonto.Interfaces;

namespace SistemaPicagemPonto.Controller
{
    public class PontoController : IPontoController
    {
        private readonly IPontoModel model;

        public PontoController(IPontoModel model)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public string? UtilizadorLogado { get; private set; }

        public bool EstaAutenticado => !string.IsNullOrWhiteSpace(UtilizadorLogado);

        public bool RegistarEntrada(string inputId)
        {
            if (!IdValido(inputId, out int id))
                return false;

            return ExecutarOperacaoModelo(() => model.RegistarEntrada(id));
        }

        public bool RegistarSaida(string inputId)
        {
            if (!IdValido(inputId, out int id))
                return false;

            return ExecutarOperacaoModelo(() => model.RegistarSaida(id));
        }

        public bool ValidarLogin(string username, string password)
        {
            UtilizadorLogado = null;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                bool loginValido = model.ValidarLogin(username, password);

                if (loginValido)
                    UtilizadorLogado = username.Trim();

                return loginValido;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void TerminarSessao()
        {
            UtilizadorLogado = null;
        }

        public List<IRegistoPonto> ObterHistorico(int? colaboradorId = null, DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            if (colaboradorId.HasValue && colaboradorId.Value <= 0)
                return [];

            if (!IntervaloDatasValido(dataInicio, dataFim))
                return [];

            List<IRegistoPonto> lista;

            try
            {
                lista = model.ObterRegistos();
            }
            catch (Exception)
            {
                return [];
            }

            if (colaboradorId.HasValue)
                lista = lista.Where(r => r.IdColaborador == colaboradorId.Value).ToList();

            if (dataInicio.HasValue)
                lista = lista.Where(r => r.Data >= dataInicio.Value).ToList();

            if (dataFim.HasValue)
                lista = lista.Where(r => r.Data <= dataFim.Value).ToList();

            return lista;
        }

        public double CalcularTotalHoras(int colaboradorId, DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            if (colaboradorId <= 0 || !IntervaloDatasValido(dataInicio, dataFim))
                return 0;

            List<IRegistoPonto> registos = ObterHistorico(colaboradorId, dataInicio, dataFim);

            double total = registos
                .Where(r => r.HoraEntrada.HasValue && r.HoraSaida.HasValue)
                .Sum(r => (r.HoraSaida!.Value - r.HoraEntrada!.Value).TotalHours);

            return Math.Round(total, 2);
        }

        private static bool IdValido(string input, out int id)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                id = 0;
                return false;
            }

            return int.TryParse(input.Trim(), out id) && id > 0;
        }

        private static bool IntervaloDatasValido(DateTime? dataInicio, DateTime? dataFim)
        {
            return !dataInicio.HasValue || !dataFim.HasValue || dataInicio.Value <= dataFim.Value;
        }

        private static bool ExecutarOperacaoModelo(Action operacao)
        {
            try
            {
                operacao();
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}