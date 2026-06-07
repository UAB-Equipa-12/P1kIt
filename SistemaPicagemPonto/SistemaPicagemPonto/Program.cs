using SistemaPicagemPonto.Model;
using SistemaPicagemPonto.Controller;
using SistemaPicagemPonto.View;
using SistemaPicagemPonto.Interfaces;

IJsonRepository repo = new JsonRepository();
IPontoModel model = new PontoModel(repo);
IPontoController controller = new PontoController(model);
ConsoleView view = new(controller);

view.Iniciar();
