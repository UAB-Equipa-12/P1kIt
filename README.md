# Sistema de Picagem de Ponto

## Descrição

O Sistema de Picagem de Ponto é uma aplicação desenvolvida em C# que permite efetuar o registo e consulta de entradas e saídas de colaboradores. A aplicação segue a arquitetura MVC (Model-View-Controller), promovendo a separação de responsabilidades e facilitando a manutenção e evolução do código.

Os registos são persistidos em formato JSON e podem ser exportados para PDF para efeitos de consulta e arquivo.

---

## Arquitetura

O projeto segue o padrão MVC:

### Model

Responsável pela gestão dos dados, regras de negócio e persistência da informação.

### View

Responsável pela interação com o utilizador através da consola.

### Controller

Responsável pela coordenação entre a View e o Model.

### Interfaces Utilizadas

Para reduzir o acoplamento entre componentes e melhorar a reutilização do código, foram utilizadas as seguintes interfaces:

* IPontoController
* IPontoModel
* IRegistoPonto
* IJsonRepository

---

## Funcionalidades

* Autenticação de colaboradores
* Registo de entrada
* Registo de saída
* Consulta de histórico de registos
* Persistência de dados em JSON
* Exportação de histórico para PDF
* Validação básica de operações

---

## Tecnologias Utilizadas

* C#
* .NET 10
* Newtonsoft.Json
* PDFsharp

---

## Estrutura do Projeto

SistemaPicagemPonto/

├── Controller/

├── Interfaces/

├── Model/

├── Services/

├── View/

└── Program.cs

---

## Persistência de Dados

Os registos são armazenados no ficheiro:

registos.json

O ficheiro é criado automaticamente durante a execução da aplicação.

---

## Exportação PDF

A aplicação permite exportar o histórico de registos de um colaborador para um ficheiro PDF.

O relatório inclui:

* Identificação do colaborador
* Datas dos registos
* Horas de entrada
* Horas de saída

Os ficheiros PDF são gerados na pasta de execução da aplicação.

---

## Instalação

Restaurar dependências:

```bash
dotnet restore
```

Compilar:

```bash
dotnet build
```

Executar:

```bash
dotnet run
```

---

## Credenciais de Teste

### Colaborador 1

Utilizador: Colaborador1

Password: 1234

### Colaborador 2

Utilizador: Colaborador2

Password: abcd

---

## Equipa

### Team Leader

Pedro Ramalho

### Desenvolvimento do Model

Rui Araújo

### Desenvolvimento do Controller

Andreia

### Desenvolvimento da View

Fabiana

### Verificação e QA

Jorge Priolé

---

## Observações

Durante o desenvolvimento foram aplicados princípios de separação entre interface e implementação, utilização de interfaces para redução de dependências diretas entre componentes e organização do código segundo o padrão MVC, promovendo uma solução mais modular e de fácil manutenção.
