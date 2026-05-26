using SistemaPicagemPonto.Model;
using SistemaPicagemPonto.Controller;
using SistemaPicagemPonto.View;
using SistemaPicagemPonto.Interfaces;

IJsonRepository repo = new JsonRepository();
PontoModel model = new(repo);
PontoController controller = new(model);
ConsoleView view = new(controller);


view.Iniciar();