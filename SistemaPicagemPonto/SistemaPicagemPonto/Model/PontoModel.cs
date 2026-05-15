using SistemaPicagemPonto.Interfaces;

namespace SistemaPicagemPonto.Model
{
    public class PontoModel : IPontoModel
    {
        private List<RegistoPonto> registos;
        private JsonRepository repo;

        public delegate void RegistosAtualizados(object origem);
        public event RegistosAtualizados? AlteracaoRegistos;

        private readonly List<Colaborador> Colaboradores =
        [
            new Colaborador { Id = 1, Nome = "Colaborador1", Password = "1234" },
            new Colaborador { Id = 42, Nome = "Colaborador2", Password = "abcd" }
        ];

        public PontoModel()
        {
            repo = new JsonRepository();

            try
            {
                registos = repo.Carregar();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar dados.", ex);
            }
        }

        public void RegistarEntrada(int colaboradorId)
        {
            if (!Colaboradores.Any(c => c.Id == colaboradorId))
                throw new ArgumentException("Colaborador não existe.");

            bool existeAberto = registos.Any(r =>
                r.IdColaborador == colaboradorId &&
                r.HoraSaida == null);

            if (existeAberto)
                throw new InvalidOperationException("Já existe uma entrada sem saída.");

            RegistoPonto novo = new()
            {
                IdColaborador = colaboradorId,
                Data = DateTime.Today,
                HoraEntrada = DateTime.Now
            };

            registos.Add(novo);

            try
            {
                repo.Guardar(registos);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao guardar dados.", ex);
            }

            AlteracaoRegistos?.Invoke(this);
        }

        public void RegistarSaida(int colaboradorId)
        {
            RegistoPonto? registoAberto = registos
                .LastOrDefault(r => r.IdColaborador == colaboradorId && r.HoraSaida == null);

            if (registoAberto == null)
                throw new InvalidOperationException("Não existe entrada para fechar.");

            registoAberto.HoraSaida = DateTime.Now;

            try
            {
                repo.Guardar(registos);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao guardar dados.", ex);
            }

            AlteracaoRegistos?.Invoke(this);
        }

        public void ObterRegistos(ref List<RegistoPonto> lista)
        {
            lista = [.. registos];
        }

        public string GetPassword(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username inválido.");

            Colaborador? c = Colaboradores
                .FirstOrDefault(c => c.Nome.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (c == null)
                throw new InvalidOperationException("Utilizador não encontrado.");

            return c.Password;
        }
    }
}