# Sistema de Picagem de Ponto

## Descrição

O Sistema de Picagem de Ponto é uma aplicação desenvolvida em C# que permite efetuar o registo de entradas e saídas de colaboradores, consultar históricos de assiduidade e exportar relatórios para PDF.

O projeto foi desenvolvido no âmbito da unidade curricular Laboratório de Desenvolvimento de Software da Universidade Aberta, seguindo a arquitetura MVC (Model-View-Controller) e aplicando princípios de redução de dependências entre componentes através da utilização de interfaces.

---

## Funcionalidades

* Autenticação de utilizadores.
* Registo de entrada.
* Registo de saída.
* Consulta do histórico de registos.
* Cálculo de horas trabalhadas num determinado período.
* Persistência de dados em JSON.
* Exportação de relatórios para PDF.
* Tratamento de exceções e validação de entradas.

---

## Arquitetura

O sistema segue o padrão MVC (Model-View-Controller).

### Model

Responsável pelas regras de negócio, gestão dos colaboradores, registos de ponto e persistência de dados.

### View

Responsável pela interação com o utilizador através da consola.

### Controller

Responsável pela coordenação entre a View e o Model.

### Interfaces Utilizadas

Para reduzir o acoplamento entre componentes e aumentar a adaptabilidade do sistema foram utilizadas as seguintes interfaces:

* IPontoController
* IPontoModel
* IRegistoPonto
* IJsonRepository

---

## Persistência

A aplicação utiliza ficheiros JSON para armazenamento de dados.

### colaboradores.json

Contém a informação necessária para autenticação dos utilizadores.

### registos.json

O ficheiro de registos é criado automaticamente pela aplicação durante a primeira execução, caso ainda não exista.

Os dados de entrada e saída dos colaboradores são persistidos neste ficheiro.

---

## Configuração da Exportação PDF (Ubuntu / WSL)

A funcionalidade de exportação PDF utiliza a biblioteca PDFsharp e requer a disponibilidade da fonte Arial no sistema operativo.

Em Ubuntu/WSL recomenda-se a instalação das fontes Microsoft Core Fonts:

```bash
sudo apt update
sudo apt install ttf-mscorefonts-installer
```

Após a instalação, confirme a existência da fonte Arial:

```bash
find /usr/share/fonts -iname "*arial*.ttf"
```

Caso a aplicação não consiga localizar a fonte, verifique o caminho configurado na classe `CustomFontResolver.cs`.

Esta configuração é necessária para permitir a geração correta dos relatórios PDF em ambiente Linux/WSL.

## Estrutura do Projeto

```text
SistemaPicagemPonto/

├── Controller/
├── Interfaces/
├── Model/
├── Services/
│   ├── PdfExporter.cs
│   └── CustomFontResolver.cs
├── View/
├── colaboradores.json
└── Program.cs
```


## Tecnologias Utilizadas

* C#
* .NET 10
* Newtonsoft.Json
* PDFsharp

---

## Instalação

### Restaurar dependências

```bash
dotnet restore
```

### Compilar

```bash
dotnet build
```

### Executar

```bash
dotnet run
```

---

## Compatibilidade

A aplicação foi validada em:

* Windows
* Ubuntu (WSL)

---

## Equipa

### Pedro Ramalho

Team Leader e Integração Final

### Rui Araújo

Desenvolvimento do Model e Persistência

### Andreia

Desenvolvimento do Controller

### Fabiana

Desenvolvimento da View

### Jorge Priolé

Verificação e QA

---

## Melhorias Futuras

* Persistência em base de dados.
* Gestão avançada de perfis e permissões.
* Interface gráfica.
* Testes automatizados.
* Exportação para formatos adicionais.

---

## Licença

Projeto desenvolvido para fins académicos no âmbito da unidade curricular Laboratório de Desenvolvimento de Software da Universidade Aberta.
