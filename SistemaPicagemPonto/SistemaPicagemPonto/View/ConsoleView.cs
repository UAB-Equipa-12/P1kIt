using SistemaPicagemPonto.Controller;
using SistemaPicagemPonto.Model;

namespace SistemaPicagemPonto.View
{
    public class ConsoleView
    {
        private readonly PontoController controller;

        public ConsoleView(PontoController controller)
        {
            this.controller = controller;
        }

        public void Iniciar()
        {
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

        private static void MostrarMenu()
        {
            Console.WriteLine("=== Sistema de Picagem de Ponto ===");
            Console.WriteLine("1 - Registar Entrada");
            Console.WriteLine("2 - Registar Saída");
            Console.WriteLine("3 - Consultar Histórico");
            Console.WriteLine("4 - Calcular Total de Horas");
            Console.WriteLine("0 - Sair");
            Console.WriteLine();
        }

        private void RegistarEntrada()
        {
            Console.Write("Introduza o ID do colaborador: ");
            string inputId = Console.ReadLine() ?? "";

            bool sucesso = controller.RegistarEntrada(inputId);

            Console.WriteLine(sucesso
                ? "Entrada registada com sucesso."
                : "Erro: ID inválido ou registo não efetuado.");
        }

        private void RegistarSaida()
        {
            Console.Write("Introduza o ID do colaborador: ");
            string inputId = Console.ReadLine() ?? "";

            bool sucesso = controller.RegistarSaida(inputId);

            Console.WriteLine(sucesso
                ? "Saída registada com sucesso."
                : "Erro: ID inválido ou registo não efetuado.");
        }

        private void ConsultarHistorico()
        {
            Console.Write("Introduza o ID do colaborador: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int id) || id <= 0)
            {
                Console.WriteLine("Erro: ID inválido.");
                return;
            }

            List<RegistoPonto> historico = controller.ObterHistorico(colaboradorId: id);

            if (historico.Count == 0)
            {
                Console.WriteLine("Não existem registos para este colaborador.");
                return;
            }

            Console.WriteLine($"\n=== Histórico do colaborador {id} ===");

            foreach (RegistoPonto registo in historico)
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
            Console.Write("Introduza o ID do colaborador: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int id) || id <= 0)
            {
                Console.WriteLine("Erro: ID inválido.");
                return;
            }

            double totalHoras = controller.CalcularTotalHoras(id);

            Console.WriteLine($"Total de horas trabalhadas: {totalHoras}h");
        }
    }
}