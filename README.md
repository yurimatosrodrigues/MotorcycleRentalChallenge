# 🏍️ Motorcycle Rental Challenge

Sistema de **aluguel de motos** desenvolvido em **.NET 8**, banco de dados PostgreSQL e utilização de mensageria com **RabbitMQ**, seguindo os princípios do **Clean Architecture**.

---

## 🧱 Arquitetura

### 1️⃣ **API (`MotorcycleRentalChallenge.API`)**
- WebAPI que dispões os endpoints seguindo o padrão REST.

### 2️⃣ **Aplicação (`MotorcycleRentalChallenge.Application`)**
- Responsável por gerenciar as requisições com validações e regras de aplicação.
- Aciona a publicação de mensagens no **RabbitMQ** ao cadastrar uma nova moto.

### 3️⃣ **Domínio (`MotorcycleRentalChallenge.Core`)**
- Responsável por encapsular as regras de negócio.
- Faz validações do domínio e disponibiliza as interfaces para acesso a dados.

### 3️⃣ **Infraestrutura (`MotorcycleRentalChallenge.Infrastructure`)**
- Responsável por lidar com a infraestrutura (storage, acesso a dados, configurações de banco e mensageria)

### 4️⃣ **Worker (`MotorcycleRentalChallenge.Worker`)**
- Serviço em background que **ouve mensagens da fila RabbitMQ** (`motorcycles-registered`).
- Ao detectar um evento de motocicleta registrada com o **ano 2024**, cria uma **notificação** no banco.

- ### 5️⃣ **Testes (`MotorcycleRentalChallenge.Test`)**
- Centraliza os Testes Unitários do projeto.

## ⚙️ Tecnologias Utilizadas

| Categoria | Tecnologias |
|------------|-------------|
| Linguagem | **C# (.NET 8)** |
| Banco de Dados | **PostgreSQL** |
| Mensageria | **RabbitMQ** |
| ORM | **Entity Framework Core** |
| Testes | **MsTest** |
| Containerização | **Docker** |

---

## 🧰 Como Executar o Projeto

### Pré-requisitos
## 1. Docker instalado

### 1. Clonar o repositório

### 2. Acessar o diretório do projeto e executar o comando abaixo (cmd):
```docker compose up --build -d```

#### A aplicação estará disponível em http://localhost:8080

## 📄 Documentação
### Documentação da API disponível via Swagger: 
- http://localhost:8080/swagger

<img width="1624" height="979" alt="image" src="https://github.com/user-attachments/assets/75feaf4d-8dcb-4806-bcdc-177feca680d8" />

