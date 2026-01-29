# Guia de Contribuicao

Obrigado pelo interesse em contribuir com o **AcademyIO**! Este documento fornece diretrizes para contribuicoes ao projeto.

## Indice

- [Codigo de Conduta](#codigo-de-conduta)
- [Como Contribuir](#como-contribuir)
- [Ambiente de Desenvolvimento](#ambiente-de-desenvolvimento)
- [Padroes de Codigo](#padroes-de-codigo)
- [Commits e Pull Requests](#commits-e-pull-requests)
- [Reportando Bugs](#reportando-bugs)

---

## Codigo de Conduta

Este projeto segue um codigo de conduta que todos os contribuidores devem respeitar. Seja respeitoso, inclusivo e colaborativo.

---

## Como Contribuir

### 1. Fork o Repositorio

```bash
# Clone seu fork
git clone https://github.com/SEU_USUARIO/MBA05_Academy.git
cd MBA05_Academy
```

### 2. Crie uma Branch

```bash
# Crie uma branch para sua feature/fix
git checkout -b feature/nome-da-feature
# ou
git checkout -b fix/nome-do-bug
```

### 3. Faca suas Alteracoes

- Siga os padroes de codigo do projeto
- Adicione testes quando aplicavel
- Atualize a documentacao se necessario

### 4. Commit e Push

```bash
git add .
git commit -m "feat: descricao da alteracao"
git push origin feature/nome-da-feature
```

### 5. Abra um Pull Request

- Descreva claramente as alteracoes
- Referencie issues relacionadas
- Aguarde revisao do mantenedor

---

## Ambiente de Desenvolvimento

### Pre-requisitos

- .NET 8.0 SDK
- Node.js 18+ e npm
- Docker e Docker Compose
- IDE recomendada: Visual Studio 2022 ou VS Code

### Configuracao Local

```bash
# Backend - Restaurar dependencias
dotnet restore

# Frontend - Instalar dependencias
cd src/Front-End
npm install

# Subir infraestrutura (RabbitMQ)
docker compose up rabbit -d

# Executar servicos individualmente
dotnet run --project src/services/AcademyIO.Auth.API
dotnet run --project src/services/AcademyIO.Courses.API
# ... outros servicos
```

### Variaveis de Ambiente

Copie o arquivo `.env.example` para `.env` e configure as variaveis necessarias:

```bash
cp .env.example .env
```

---

## Padroes de Codigo

### C# / .NET

- Siga as convencoes de nomenclatura do .NET
- Use `PascalCase` para classes, metodos e propriedades publicas
- Use `camelCase` para variaveis locais e parametros
- Prefixe interfaces com `I` (ex: `ICourseRepository`)
- Mantenha metodos pequenos e focados
- Documente metodos publicos com XML comments

### Angular / TypeScript

- Siga o Angular Style Guide oficial
- Use `kebab-case` para nomes de arquivos
- Use `PascalCase` para classes e interfaces
- Use `camelCase` para variaveis e funcoes
- Organize imports alfabeticamente

### Estrutura de Pastas

```
src/
├── building-blocks/     # Bibliotecas compartilhadas
├── services/            # Microsservicos
├── api-gateways/        # API Gateway (BFF)
└── Front-End/           # Aplicacao Angular
```

---

## Commits e Pull Requests

### Conventional Commits

Use o padrao [Conventional Commits](https://www.conventionalcommits.org/):

```
<tipo>[escopo opcional]: <descricao>

[corpo opcional]

[rodape opcional]
```

#### Tipos de Commit

| Tipo | Descricao |
|------|-----------|
| `feat` | Nova funcionalidade |
| `fix` | Correcao de bug |
| `docs` | Alteracao em documentacao |
| `style` | Formatacao (sem alteracao de codigo) |
| `refactor` | Refatoracao de codigo |
| `test` | Adicao ou correcao de testes |
| `chore` | Tarefas de manutencao |

#### Exemplos

```bash
feat(courses): adicionar endpoint de listagem paginada
fix(auth): corrigir validacao de token expirado
docs(readme): atualizar instrucoes de instalacao
refactor(payments): extrair logica de validacao
```

### Pull Request

- Titulo claro e descritivo
- Descricao das alteracoes realizadas
- Screenshots (se houver alteracoes visuais)
- Checklist de verificacao:
  - [ ] Codigo segue os padroes do projeto
  - [ ] Testes adicionados/atualizados
  - [ ] Documentacao atualizada
  - [ ] Build passa sem erros

---

## Reportando Bugs

### Antes de Reportar

1. Verifique se o bug ja foi reportado nas Issues
2. Tente reproduzir em ambiente limpo
3. Colete informacoes relevantes

### Template de Bug Report

```markdown
## Descricao do Bug
[Descricao clara e concisa do bug]

## Passos para Reproduzir
1. Va para '...'
2. Clique em '...'
3. Observe o erro '...'

## Comportamento Esperado
[O que deveria acontecer]

## Comportamento Atual
[O que esta acontecendo]

## Screenshots
[Se aplicavel]

## Ambiente
- OS: [ex: Windows 11]
- .NET Version: [ex: 8.0.100]
- Docker Version: [ex: 24.0.5]
```

---

## Duvidas

Se tiver duvidas sobre como contribuir, abra uma Issue com a tag `question` ou entre em contato com o mantenedor.

**Obrigado por contribuir!**
