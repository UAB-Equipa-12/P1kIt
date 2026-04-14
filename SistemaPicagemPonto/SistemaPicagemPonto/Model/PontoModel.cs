namespace SistemaPicagemPonto.Model
{
    public class PontoModel
    {
        private List<RegistoPonto> registos;
        private JsonRepository repo;

        public delegate void RegistosAtualizados(object origem);
        public event RegistosAtualizados AlteracaoRegistos;

        public PontoModel()
        {
            repo = new JsonRepository();
            registos = repo.Carregar();
        }

        public void RegistarEntrada(int colaboradorId)
        {
            RegistoPonto r = new()
            {
                IdColaborador = colaboradorId,
                Data = DateTime.Today,
                HoraEntrada = DateTime.Now
            };

            registos.Add(r);

            repo.Guardar(registos);

            AlteracaoRegistos?.Invoke(this);
        }

        public void RegistarSaida(int colaboradorId)
        {
            AlteracaoRegistos?.Invoke(this);
        }

        public void ObterRegistos(ref List<RegistoPonto> lista)
        {
            lista = [.. registos];
        }
    }
}
