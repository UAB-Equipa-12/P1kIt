using SistemaPicagemPonto.Controller;
using SistemaPicagemPonto.Interfaces;
using SistemaPicagemPonto.Model;

namespace SistemaPicagemPonto.View
{
    public class ConsoleView
    {
        private readonly IPontoController controller;

        public ConsoleView(IPontoController controller)
        {
            this.controller = controller;
        }

        public void Iniciar()
        {
            bool loginValido = FazerLogin();

            if (!loginValido)
            {
                Console.WriteLine("Programa encerrado.");
                return;
            }

            bool sair = false;

            while (!sair)
            {
                MostrarMenu();
                Console.Write("Escolha uma opção: ");
                string? opcao = Console.ReadLine();

                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                        RegistarEntrada();
                        break;

                    case "2":
                        RegistarSaida();
                        break;

                    case "3":
                        ConsultarHistorico();
                        break;

                    case "4":
                        CalcularHoras();
                        break;

                    case "5":
                        ExportarPdf();
                        break;

                    case "0":
                        Console.WriteLine("Programa encerrado.");
                        sair = true;
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (!sair)
                {
                    Console.WriteLine("\nPrima ENTER para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private bool FazerLogin()
        {
            const int maxTentativas = 3;
            int tentativa = 0;

            while (tentativa < maxTentativas)
            {
                Console.Clear();
                Console.WriteLine("=== Login Plataforma de Gestão ===");
                Console.Write("Username: ");
                string username = Console.ReadLine() ?? "";

                Console.Write("Password: ");
                string password = Console.ReadLine() ?? "";

                bool sucesso = controller.ValidarLogin(username, password);

                if (sucesso)
                {
                    Console.WriteLine("\nLogin sucedido.");
                    Console.WriteLine("Prima ENTER para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                    return true;
                }

                tentativa++;
                Console.WriteLine("\nLogin falhado.");

                if (tentativa < maxTentativas)
                {
                    Console.WriteLine($"Tentativas restantes: {maxTentativas - tentativa}");
                    Console.WriteLine("Prima ENTER para tentar novamente...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("\nNúmero máximo de tentativas excedido.");
            Console.WriteLine("Prima ENTER para sair...");
            Console.ReadLine();
            return false;
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("=== Sistema de Picagem de Ponto ===");
            Console.WriteLine("1 - Registar Entrada");
            Console.WriteLine("2 - Registar Saída");
            Console.WriteLine("3 - Consultar Histórico");
            Console.WriteLine("4 - Calcular Total de Horas");
            Console.WriteLine("5 - Exportar PDF");
            Console.WriteLine("0 - Sair");
            Console.WriteLine();
        }

        private static int? LerIdColaborador()
        {
            Console.Write("Introduza o ID do colaborador: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int id) || id <= 0)
            {
                Console.WriteLine("Erro: ID inválido.");
                return null;
            }

            return id;
        }

        private void RegistarEntrada()
        {
            int? id = LerIdColaborador();
            if (!id.HasValue)
                return;

            bool sucesso = controller.RegistarEntrada(id.Value.ToString());

            Console.WriteLine(sucesso
                ? "Entrada registada com sucesso."
                : "Erro: não foi possível registar a entrada.");
        }

        private void RegistarSaida()
        {
            int? id = LerIdColaborador();
            if (!id.HasValue)
                return;

            bool sucesso = controller.RegistarSaida(id.Value.ToString());

            Console.WriteLine(sucesso
                ? "Saída registada com sucesso."
                : "Erro: não foi possível registar a saída.");
        }

        private void ConsultarHistorico()
        {
            int? id = LerIdColaborador();
            if (!id.HasValue)
                return;

            List<IRegistoPonto> historico = controller.ObterHistorico(colaboradorId: id.Value);

            if (historico.Count == 0)
            {
                Console.WriteLine("Não existem registos para este colaborador.");
                return;
            }

            Console.WriteLine($"\n=== Histórico do colaborador {id.Value} ===");

            foreach (IRegistoPonto registo in historico)
            {
                string entrada = registo.HoraEntrada?.ToString("HH:mm:ss") ?? "-";
                string saida = registo.HoraSaida?.ToString("HH:mm:ss") ?? "-";

                Console.WriteLine(
                    $"Data: {registo.Data:dd/MM/yyyy} | Entrada: {entrada} | Saída: {saida}"
                );
            }
        }

        private void CalcularHoras()
        {
            int? id = LerIdColaborador();
            if (!id.HasValue)
                return;

            List<IRegistoPonto> historico = controller.ObterHistorico(colaboradorId: id.Value);

            if (historico.Count == 0)
            {
                Console.WriteLine("Não existem registos para este colaborador.");
                return;
            }

            double totalHoras = controller.CalcularTotalHoras(id.Value);
            Console.WriteLine($"Total de horas trabalhadas: {totalHoras}h");
        }

        private void ExportarPdf()
        {
            int? id = LerIdColaborador();
            if (!id.HasValue)
                return;

            Console.Write("Data inicial (dd/MM/yyyy) ou ENTER para ignorar: ");
            string? inputInicio = Console.ReadLine();

            Console.Write("Data final (dd/MM/yyyy) ou ENTER para ignorar: ");
            string? inputFim = Console.ReadLine();

            DateTime? dataInicio = null;
            DateTime? dataFim = null;

            if (!string.IsNullOrWhiteSpace(inputInicio))
            {
                if (!DateTime.TryParse(inputInicio, out DateTime di))
                {
                    Console.WriteLine("Erro: data inicial inválida.");
                    return;
                }

                dataInicio = di;
            }

            if (!string.IsNullOrWhiteSpace(inputFim))
            {
                if (!DateTime.TryParse(inputFim, out DateTime df))
                {
                    Console.WriteLine("Erro: data final inválida.");
                    return;
                }

                dataFim = df;
            }

            List<IRegistoPonto> historico = controller.ObterHistorico(id.Value, dataInicio, dataFim);

            if (historico.Count == 0)
            {
                Console.WriteLine("Sem dados para o período selecionado.");
                return;
            }

            Console.WriteLine("Exportação PDF preparada na View. Falta integrar a geração do ficheiro.");
        }
    }
}