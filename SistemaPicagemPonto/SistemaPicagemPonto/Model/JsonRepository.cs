using Newtonsoft.Json;
using SistemaPicagemPonto.Interfaces;

namespace SistemaPicagemPonto.Model
{
    public class JsonRepository : IJsonRepository
    {
        private const string FILE = "registos.json";

        public void Guardar(IEnumerable<IRegistoPonto> dados)
        {
            List<RegistoPonto> registos = dados
                .Select(r => new RegistoPonto
                {
                    IdColaborador = r.IdColaborador,
                    Data = r.Data,
                    HoraEntrada = r.HoraEntrada,
                    HoraSaida = r.HoraSaida
                })
                .ToList();

            string json = JsonConvert.SerializeObject(registos, Formatting.Indented);
            File.WriteAllText(FILE, json);
        }

        public List<IRegistoPonto> Carregar()
        {
            if (!File.Exists(FILE))
                return [];

            string json = File.ReadAllText(FILE);
            List<RegistoPonto> registos = JsonConvert.DeserializeObject<List<RegistoPonto>>(json) ?? [];

            return registos.Cast<IRegistoPonto>().ToList();
        }
    }
}
