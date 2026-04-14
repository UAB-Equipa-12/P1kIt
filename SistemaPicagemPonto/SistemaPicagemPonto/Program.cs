using SistemaPicagemPonto.Model;
using SistemaPicagemPonto.Controller;

Console.WriteLine("Sistema de Picagem de Ponto");

PontoModel model = new();
PontoController controller = new(model);

Console.WriteLine("=== Teste do Controller ===\n");

// Teste 1: ID inválido
bool resultado = controller.RegistarEntrada("abc");
Console.WriteLine($"Entrada com ID inválido ('abc'): {(resultado ? "Sucesso" : "Bloqueado")}");

// Teste 2: ID vazio
resultado = controller.RegistarEntrada("");
Console.WriteLine($"Entrada com ID vazio: {(resultado ? "Sucesso" : "Bloqueado")}");

// Teste 3: Entrada válida
resultado = controller.RegistarEntrada("42");
Console.WriteLine($"Entrada com ID 42: {(resultado ? "Sucesso" : "Bloqueado")}");

// Teste 4: Histórico do colaborador 42
var historico = controller.ObterHistorico(colaboradorId: 42);
Console.WriteLine($"\nHistórico do colaborador 42: {historico.Count} registo(s)");

// Teste 5: Total de horas (será 0 porque o RegistarSaida ainda está vazio)
double horas = controller.CalcularTotalHoras(42);
Console.WriteLine($"Total de horas do colaborador 42: {horas}h");