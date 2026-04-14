namespace SistemaPicagemPonto.Model
{
    public class RegistoPonto
    {
        public int IdColaborador { get; set; }
        public DateTime Data { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSaida { get; set; }
    }
}
