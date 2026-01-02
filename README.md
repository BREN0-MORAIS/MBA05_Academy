# 🎓 AcademyIO — Documentação Unificada

### 🧩 MÓDULO 5 — DevOps para Desenvolvedores

## 📌 1. Apresentação

Bem-vindo ao repositório do projeto **AcademyIO**, desenvolvido como parte da entrega do MBA **DevXpert Full Stack .NET**, no módulo **MÓDULO 5 - DevOps para Desenvolvedores**.


> 👨‍💻 **Autor:** Breno Morais

---

## 🎯 2. Objetivo do Projeto

O **AcademyIO** visa oferecer:

- API RESTful para integração com diferentes aplicações
- Separação clara de responsabilidades por *bounded context*
- Autenticação e autorização com JWT
- Processamento assíncrono com mensageria
- Arquitetura preparada para Docker e Kubernetes

---

## 🛠️ 3. Tecnologias Utilizadas

| Categoria | Tecnologia |
|--------|-----------|
| Linguagem | C# |
| Framework | ASP.NET Core |
| Arquitetura | DDD, CQRS, TDD |
| Mensageria | RabbitMQ |
| Autenticação | JWT |
| ORM | Entity Framework Core |
| Mapeamento | AutoMapper |
| Validação | FluentValidation |
| Documentação | Swagger |
| Containers | Docker, Docker Compose |
| Orquestração | Kubernetes (Minikube) |

---

## 🧱 4. Arquitetura da Solução

A solução é composta pelos seguintes microserviços:

- **Auth** — Autenticação e geração de tokens JWT
- **Courses** — Gestão de cursos, módulos e conteúdos
- **Students** — Gestão de alunos e matrículas
- **Payments** — Processamento de pagamentos
- **BFF** — Backend for Frontend (API Gateway)

Todos os serviços se comunicam de forma desacoplada, utilizando **RabbitMQ** quando necessário.

---

## ✅ 5. Funcionalidades Principais

- 🔐 Autenticação e autorização com JWT
- 🧑‍🎓 Gestão de alunos e matrículas
- 📚 Cadastro e consumo de cursos
- 💳 Processamento de pagamentos
- 📡 APIs documentadas via Swagger
- 🧪 Seed automático para ambiente de testes

---

## 🚀 6. Executando o Projeto

O AcademyIO pode ser executado de **três formas diferentes**, dependendo do seu objetivo.

---

## 🐳 6.1 Rodando Localmente com Docker Compose (Build Local)

### Pré-requisitos

- Docker
- Docker Compose

### Passos

1. Clone o repositório
2. Na raiz do projeto, execute:

```
docker compose up --build
```

Isso irá construir e subir todos os serviços e o RabbitMQ.

### Portas e Swagger

| Serviço | Porta | Swagger |
|------|------|--------|
| Auth | 5001 | http://localhost:5001/swagger |
| Courses | 5002 | http://localhost:5002/swagger |
| Payments | 5003 | http://localhost:5003/swagger |
| Students | 5004 | http://localhost:5004/swagger |
| BFF | 5000 | http://localhost:5000/swagger |

Para parar os serviços:

```
docker compose down
```

---

## 📦 6.2 Rodando com Imagens Publicadas no Docker Hub

### 🔗 Imagens Oficiais no Docker Hub

As imagens do projeto estão disponíveis publicamente no Docker Hub:

- Auth: https://hub.docker.com/r/brenodev29/academyio/tags?name=auth
- BFF: https://hub.docker.com/r/brenodev29/academyio/tags?name=bff
- Students: https://hub.docker.com/r/brenodev29/academyio/tags?name=students
- Payments: https://hub.docker.com/r/brenodev29/academyio/tags?name=payments
- Courses: https://hub.docker.com/r/brenodev29/academyio/tags?name=courses

### 📥 Download das Imagens (docker pull)

```bash
docker pull brenodev29/academyio:auth
docker pull brenodev29/academyio:bff
docker pull brenodev29/academyio:students
docker pull brenodev29/academyio:payments
docker pull brenodev29/academyio:courses
```



Essa opção dispensa build local.

### Pré-requisitos

- Docker
- Docker Compose

### Passos

1. Clone o repositório
2. Ajuste o `docker-compose.yml` para usar as imagens:

- `brenodev29/academyio:auth`
- `brenodev29/academyio:courses`
- `brenodev29/academyio:payments`
- `brenodev29/academyio:students`
- `brenodev29/academyio:bff`

3. Execute:

```
docker compose up -d
```

As portas e URLs do Swagger são as mesmas do modo local.

---

## ☸️ 6.3 Rodando com Kubernetes (Minikube)

### Pré-requisitos

- Minikube
- kubectl
- Imagens publicadas no Docker Hub

### Passo a Passo

1. Inicie o Minikube:

```
minikube start
```

2. Acesse a pasta `k8s`

3. Crie o namespace:

```
kubectl apply -f 00-namespace.yaml
```

4. Crie secrets e configmaps:

```
kubectl apply -f 00-secrets.yaml
kubectl apply -f config-messagebus.yaml
```

5. Suba o RabbitMQ:

```
kubectl apply -f 01-rabbitmq-deployment.yaml
```

6. Suba as APIs:

```
kubectl apply -f auth-deployment.yaml
kubectl apply -f courses-deployment.yaml
kubectl apply -f payments-deployment.yaml
kubectl apply -f students-deployment.yaml
kubectl apply -f bff-deployment.yaml
```

### Acessando as APIs (Port-forward)

```
kubectl port-forward svc/academyio-auth 5001:80 -n academyio
kubectl port-forward svc/academyio-courses 5002:80 -n academyio
kubectl port-forward svc/academyio-payments 5003:80 -n academyio
kubectl port-forward svc/academyio-students 5004:80 -n academyio
kubectl port-forward svc/academyio-bff 5000:80 -n academyio
```

---

## 👤 7. Usuários para Teste

**Administrador**
- Email: admin@domain.com
- Senha: Admin123!

**Aluno**
- Email: student@domain.com
- Senha: Student123!

---

## 📁 8. Considerações Finais

Este projeto foi desenvolvido com **finalidade acadêmica**, como parte da avaliação do **módulo Arquitetura, Modelagem e Qualidade de Software**, especificamente no **eixo de DevOps para Desenvolvedores**, do MBA **DevXpert Full Stack .NET**.

A solução proposta demonstra, de forma prática e aplicada, os principais conceitos estudados ao longo do módulo, incluindo:

- Containerização de aplicações com **Docker**;
- Orquestração de serviços com **Docker Compose** e **Kubernetes (Minikube)**;
- Padronização de ambientes por meio de imagens versionadas no **Docker Hub**;
- Aplicação de boas práticas de **arquitetura de software**, como separação de responsabilidades, escalabilidade e desacoplamento;
- Uso de **mensageria** para comunicação entre serviços.


