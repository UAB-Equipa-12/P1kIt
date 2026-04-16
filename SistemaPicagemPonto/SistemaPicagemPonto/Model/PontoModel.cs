namespace SistemaPicagemPonto.Model
{
    public class PontoModel
    {
        private List<RegistoPonto> registos;
        private JsonRepository repo;

        public delegate void RegistosAtualizados(object origem);
        public event RegistosAtualizados AlteracaoRegistos;
        private readonly List<Colaborador> Colaboradores =
        [
            new Colaborador { Id = 1, Nome = "Colaborador1", Password = "1234" },
            new Colaborador { Id = 42, Nome = "Colaborador2", Password = "abcd" }
        ];

        public PontoModel()
        {
            repo = new JsonRepository();
            registos = repo.Carregar();
        }

        public bool RegistarEntrada(int colaboradorId)
        {
            // Verifica se já existe um registo aberto
            bool existeAberto = registos.Any(r =>
                r.IdColaborador == colaboradorId &&
                r.HoraSaida == null);

            if (existeAberto)
                return false;

            RegistoPonto novo = new()
            {
                IdColaborador = colaboradorId,
                Data = DateTime.Today,
                HoraEntrada = DateTime.Now
            };

            registos.Add(novo);

            repo.Guardar(registos);

            AlteracaoRegistos?.Invoke(this);

            return true;
        }

        public bool RegistarSaida(int colaboradorId)
        {
            RegistoPonto? registoAberto = registos
                .LastOrDefault(r => r.IdColaborador == colaboradorId && r.HoraSaida == null);

            if (registoAberto == null)
                return false;

            registoAberto.HoraSaida = DateTime.Now;

            repo.Guardar(registos);

            AlteracaoRegistos?.Invoke(this);

            return true;
        }

        public void ObterRegistos(ref List<RegistoPonto> lista)
        {
            lista = [.. registos];
        }

        public string? GetPassword(string username)
        {
            Colaborador? c = Colaboradores
                .FirstOrDefault(c => c.Nome.Equals(username, StringComparison.OrdinalIgnoreCase));

            return c?.Password;
        }
    }
}
