# PORTLY - Sistema de Controle de Visitantes

[![Frontend](https://img.shields.io/badge/Frontend-Portly%20React-blue.svg)](https://github.com/maranzatto/Portly)

## 📋 Descrição

PORTLY é uma API para gestão de visitantes em portarias e condomínios, desenvolvida com arquitetura limpa e boas práticas de software. O sistema oferece controle completo do ciclo de vida de visitantes, desde o cadastro até a exclusão lógica, com validações de negócio e logging estruturado.

## 🏗️ Arquitetura

### **Domain-Driven Design (DDD)**
- **Entidades:** `Visitor` com comportamentos e regras de negócio
- **Value Objects:** `Document` para validação de CPF/CNPJ
- **Exceções de Domínio:** `BusinessRuleException` e `DomainException`

### **Clean Architecture**
- **Domain:** Camada central sem dependências externas
- **Application:** Use cases e DTOs com regras de aplicação
- **Infrastructure:** Implementações de repositories e acesso a dados
- **API:** Controllers e middlewares de apresentação

### **Hexagonal Architecture**
- **Ports:** Interfaces de use cases (input) e repositories (output)
- **Adapters:** Implementações concretas das interfaces
- **Desacoplamento:** Inversão de dependências em toda a aplicação

## 🛠️ Tecnologias Utilizadas

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-8.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-green.svg)](https://docs.microsoft.com/en-us/ef/core/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue.svg)](https://www.postgresql.org/)
[![Supabase](https://img.shields.io/badge/Supabase-3FCF8E.svg)](https://supabase.com/)
[![Npgsql](https://img.shields.io/badge/Npgsql-8.0-336791.svg)](https://www.npgsql.org/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D.svg)](https://swagger.io/)

### **Padrões e Práticas**
- **Dependency Injection** - Injeção de dependências
- **Repository Pattern** - Abstração de acesso a dados
- **CQRS** - Command Query Responsibility Segregation
- **Structured Logging** - Logging com Microsoft.Extensions.Logging
- **Global Exception Handling** - Middleware centralizado de exceções
- **Clean Architecture** - Separação clara de responsabilidades
- **Domain-Driven Design** - Foco no domínio do negócio
- **Hexagonal Architecture** - Ports & Adapters pattern

## 🚀 Funcionalidades

### **Gerenciamento de Visitantes**
- ✅ **Cadastro** - Novos visitantes com validação de documento/email
- ✅ **Consulta** - Busca individual e listagem completa
- ✅ **Atualização** - Edição de dados com validação de duplicidade
- ✅ **Exclusão Lógica** - Soft delete com opção de restauração
- ✅ **Validações** - CPF/CNPJ, e-mail, regras de negócio

### **Observabilidade**
- ✅ **Logging Estruturado** - Logs em diferentes níveis (Information, Warning, Error)
- ✅ **Exception Handling** - Tratamento centralizado com mensagens claras
- ✅ **HTTP Status Codes** - Respostas padronizadas (200, 201, 204, 400, 404, 422, 409, 500)

## 📁 Estrutura do Projeto

```
Portly/
├── Api/                          # Camada de Apresentação
│   ├── Controllers/              # API Controllers
│   ├── Middlewares/              # Middlewares customizados
│   └── Program.cs               # Configuração da aplicação
├── Application/                  # Camada de Aplicação
│   ├── DTOs/                    # Data Transfer Objects
│   ├── UseCases/                # Casos de uso
│   ├── Ports/                   # Interfaces (Input/Output)
│   └── Exceptions/              # Exceções da aplicação
├── Domain/                      # Camada de Domínio
│   ├── Entities/                # Entidades de domínio
│   ├── ValueObjects/            # Value objects
│   └── Exceptions/              # Exceções de domínio
├── Infrastructure/               # Camada de Infraestrutura
│   ├── Data/                    # Contexto do EF Core
│   ├── Repositories/            # Implementações de repositories
│   └── Migrations/              # Migrations do banco
└── README.md                    # Documentação do projeto
```

## 🛣️ Rotas da API

### **Visitantes**
| Método | Rota | Descrição |
|--------|------|----------|
| `GET` | `/api/v1/admin/visitor` | Listar todos os visitantes (ativos e excluídos) |
| `GET` | `/api/v1/admin/visitor/{id}` | Buscar visitante por ID |
| `POST` | `/api/v1/admin/visitor` | Criar novo visitante |
| `PUT` | `/api/v1/admin/visitor/{id}` | Atualizar visitante existente |
| `DELETE` | `/api/v1/admin/visitor/{id}` | Excluir visitante (soft delete) |
| `POST` | `/api/v1/admin/visitor/{id}/restore` | Restaurar visitante excluído |

### **Payloads**

#### **Criar Visitante**
```json
{
  "fullName": "João Silva",
  "document": "12345678901",
  "phone": "11987654321",
  "email": "joao@example.com"
}
```

#### **Atualizar Visitante**
```json
{
  "fullName": "João Silva",
  "document": "12345678901",
  "phone": "11987654321",
  "email": "joao@example.com"
}
```

#### **Resposta - Visitante**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "fullName": "João Silva",
  "document": "12345678901",
  "phone": "11987654321",
  "email": "joao@example.com",
  "isDeleted": false
}
```

## 🚀 Como Começar

### **Pré-requisitos**
- **.NET 8 SDK** - [Download aqui](https://dotnet.microsoft.com/download/dotnet/8.0)
- **PostgreSQL** - [Download aqui](https://www.postgresql.org/download/)
- **IDE** - Visual Studio 2022 ou VS Code

### **1. Clonar o Repositório**
```bash
git clone <repository-url>
cd Portly
```

### **2. Configurar Banco de Dados**
```bash
# Configurar connection string no appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your-project.supabase.co;Database=postgres;Username=postgres;Password=your-password"
  }
}
```

### **3. Executar a Aplicação**
```bash
# Restaurar pacotes
dotnet restore

# Executar migrations automaticamente (Development)
dotnet run

# Ou executar migrations manualmente
dotnet ef database update
```

### **4. Acessar a API**
- **Swagger UI:** `https://localhost:5000/swagger`
- **API Base:** `https://localhost:5000/api/v1/admin/visitor`

## 🔧 Manutenção e Desenvolvimento

### **Adicionar Novos Use Cases**
1. Criar interface em `Application/Ports/Input/`
2. Implementar use case em `Application/UseCases/`
3. Registrar no `ApplicationModule.cs`
4. Adicionar controller se necessário

### **Adicionar Novas Entidades**
1. Criar entidade em `Domain/Entities/`
2. Criar value objects se necessário
3. Adicionar ao `DbContext`
4. Criar repository e migrations
5. Implementar use cases correspondentes

### **Configuração de Logging**
```csharp
// Em appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "nexum": "Debug"
    }
  }
}
```

### **Migrations do Banco**
```bash
# Criar nova migration
dotnet ef migrations add AddNewEntity

# Aplicar migration
dotnet ef database update

# Remover última migration
dotnet ef database update previous
dotnet ef migrations remove
```

## 📊 Logging e Observabilidade

### **Logs Estruturados**
O sistema implementa logging estruturado em diferentes níveis:

- **Information:** Operações principais (criação, atualização, exclusão)
- **Warning:** Violações de regras de negócio (duplicatas)
- **Error:** Problemas técnicos (exceções não tratadas)
- **Debug:** Operações detalhadas (acesso ao banco)

### **Visualização dos Logs**
```bash
# Logs em tempo real durante desenvolvimento
dotnet run

# Logs com verbosidade específica
dotnet run --verbosity normal
```

## 🔐 Segurança

### **Configuração de Produção**
- Usar `appsettings.Production.json` para ambiente de produção
- Configurar secrets em serviços de nuvem (Azure Key Vault, etc.)
- Não expor connection strings em código ou repositório

### **CORS Configuration**
```json
{
  "Cors": {
    "AllowedOrigins": [
      "https://yourdomain.com",
      "https://admin.yourdomain.com"
    ]
  }
}
```

## 🤝 Contribuição

1. Fork do projeto
2. Criar branch para feature (`git checkout -b feature/nova-funcionalidade`)
3. Commit das mudanças (`git commit -m 'Add: Nova funcionalidade'`)
4. Push para branch (`git push origin feature/nova-funcionalidade`)
5. Abrir Pull Request.
