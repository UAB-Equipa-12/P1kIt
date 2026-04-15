using SistemaPicagemPonto.Model;
using SistemaPicagemPonto.Controller;
using SistemaPicagemPonto.View;

PontoModel model = new();
PontoController controller = new(model);
ConsoleView view = new(controller);

view.Iniciar();