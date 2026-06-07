# P1kIt - Sistema de Picagem de Ponto

Aplicação de consola em C# para registo de entrada e saída de colaboradores, organizada segundo a arquitectura MVC.

## Arquitectura

- `Model`: entidades, regras de negócio e persistência em JSON.
- `View`: interface de consola.
- `Controller`: coordenação entre View e Model.
- `Interfaces`: contratos usados para reduzir dependências entre componentes.

## Integração das interfaces

A persistência JSON é exposta através de `IJsonRepository`, que trabalha com `IRegistoPonto`.
A implementação concreta `JsonRepository` converte internamente os dados para `RegistoPonto` apenas no momento da serialização/deserialização.

O `Program.cs` instancia os componentes por interface:

```csharp
IJsonRepository repo = new JsonRepository();
IPontoModel model = new PontoModel(repo);
IPontoController controller = new PontoController(model);
ConsoleView view = new(controller);
```

## Credenciais de teste

- Username: `Colaborador1` | Password: `1234`
- Username: `Colaborador2` | Password: `abcd`

## Funcionalidades

- Login de colaborador.
- Registo de entrada.
- Registo de saída.
- Consulta de histórico.
- Cálculo de total de horas.
- Persistência em `registos.json`.

## Validação sugerida em Ubuntu/WSL

```bash
cd SistemaPicagemPonto/SistemaPicagemPonto
dotnet restore
dotnet run
```

## Nota

A opção de exportação PDF encontra-se preparada na View, mas a geração efectiva do ficheiro PDF ainda não está implementada.
