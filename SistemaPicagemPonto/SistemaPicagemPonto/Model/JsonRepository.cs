using Newtonsoft.Json;

namespace SistemaPicagemPonto.Model
{
    public class JsonRepository
    {
        private const string FILE = "registos.json";

        public void Guardar(List<RegistoPonto> dados)
        {
            string json = JsonConvert.SerializeObject(dados, Formatting.Indented);
            File.WriteAllText(FILE, json);
        }

        public List<RegistoPonto> Carregar()
        {
            if (!File.Exists(FILE))
                return [];

            string json = File.ReadAllText(FILE);
            return JsonConvert.DeserializeObject<List<RegistoPonto>>(json);
        }
    }
}
