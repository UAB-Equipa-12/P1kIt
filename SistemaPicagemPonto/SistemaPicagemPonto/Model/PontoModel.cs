using SistemaPicagemPonto.Interfaces;

namespace SistemaPicagemPonto.Model
{
    public class PontoModel : IPontoModel
    {
        private readonly List<IRegistoPonto> registos;
        private readonly IJsonRepository repo;

        public delegate void RegistosAtualizados(object origem);
        public event RegistosAtualizados? AlteracaoRegistos;

        private readonly List<Colaborador> Colaboradores =
        [
            new Colaborador { Id = 1, Nome = "Colaborador1", Password = "1234" },
            new Colaborador { Id = 2, Nome = "Colaborador2", Password = "abcd" }
        ];

        public PontoModel(IJsonRepository repository)
        {
            repo = repository;

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

            IRegistoPonto novo = new RegistoPonto()
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
            IRegistoPonto? registoAberto = registos
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

        public List<IRegistoPonto> ObterRegistos()
        {
            return registos.ToList();
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

        public bool ValidarLogin(string username, string password)
        {
            Colaborador? c = Colaboradores
                .FirstOrDefault(c =>
                    c.Nome.Equals(username, StringComparison.OrdinalIgnoreCase));

            return c != null && c.Password == password;
        }
    }
}