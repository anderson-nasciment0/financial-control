# Financial Control

O financial control é uma aplicação para controle financeiro pessoal, desenvolvida com ASP.NET Core no backend e React + Typescript no frontend, utilizando MySQL como banco de dados

## A aplicação permite
- Gerenciar pessoas, categorias e transações
- Consultar relatórios financeiros de despesas e receitas pelas pessoas ou categorias

## Tecnologias utilizadas
### Backend
- C#/ ASP.NET
- Entity Framework Core
- AutoMapper

### Frontend
- React
- Typescript
- Axios
- Reactstrap

### Banco de dados
- MySQL

Estrutura do projeto
```bash
financial-control/
│
├─ FinancialControl.Api/          # Backend (.NET Core)
├─ financial-control-web/         # Frontend (React + TS)
├─ database/                      # Scripts SQL
└─ README.md                      
```
## Como rodar o projeto
### Configurar o banco de dados
- Instale o MySQL Workbench
- Importe o banco de dados

### Rodar o backend
- Instale o Visual Studio 2022
- Abra os projetos do backend
- Atualize a connection string no appsettings.json para o seu usuario e senha
 ```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ControleFinanceiroDB;Uid=root;Pwd=root;"
}
```
- Abra o terminal, instale as dependencias do backend e execute a aplicação
```bash
dotnet restore
dotnet run
```

### Rodar o frontend
- Instale o VS code
- Abra o terminal na pasta do frontend, instale as dependencias e execute a aplicação
```bash
npm install
npm start
```

O frontend estará configurado para consumir a api do backend via axios, caso venha a ter algum problema com a porta http, altere para a correta no seu computador
