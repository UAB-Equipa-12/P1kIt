using SistemaPicagemPonto.Model;
using SistemaPicagemPonto.Interfaces;

namespace SistemaPicagemPonto.Controller
{
    public class PontoController
    {
        private readonly IPontoModel model;

        public PontoController(IPontoModel model)
        {
            this.model = model;
        }

        // Regista a entrada de um colaborador após validar o ID
        public bool RegistarEntrada(string inputId)
        {
            if (!IdValido(inputId, out int id))
                return false;

            try
            {
                model.RegistarEntrada(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Regista a saída de um colaborador após validar o ID
        public bool RegistarSaida(string inputId)
        {
            if (!IdValido(inputId, out int id))
                return false;

            try
            {
                model.RegistarSaida(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Verifica se o username e password correspondem a um colaborador existente
        public bool ValidarLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                string passwordCorreta = model.GetPassword(username);
                return password == passwordCorreta;
            }
            catch
            {
                return false;
            }
        }

        // Devolve o histórico de registos, por colaborador e datas
        public List<RegistoPonto> ObterHistorico(int? colaboradorId = null, DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            List<RegistoPonto> lista = [];
            model.ObterRegistos(ref lista);

            if (colaboradorId.HasValue)
                lista = lista.Where(r => r.IdColaborador == colaboradorId.Value).ToList();

            if (dataInicio.HasValue)
                lista = lista.Where(r => r.Data >= dataInicio.Value).ToList();

            if (dataFim.HasValue)
                lista = lista.Where(r => r.Data <= dataFim.Value).ToList();

            return lista;
        }

        // Calcula o total de horas trabalhadas com base nos registos completos
        public double CalcularTotalHoras(int colaboradorId, DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            List<RegistoPonto> registos = ObterHistorico(colaboradorId, dataInicio, dataFim);

            double total = registos
                .Where(r => r.HoraEntrada.HasValue && r.HoraSaida.HasValue)
                .Sum(r => (r.HoraSaida!.Value - r.HoraEntrada!.Value).TotalHours);

            return Math.Round(total, 2);
        }

        // Garante que o ID introduzido é numérico e positivo
        private static bool IdValido(string input, out int id)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                id = 0;
                return false;
            }

            return int.TryParse(input.Trim(), out id) && id > 0;
        }
    }
}