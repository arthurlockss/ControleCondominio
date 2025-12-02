# Projeto - Arquitetura em Microsserviços: Controle de Condomínios

[cite_start]Este projeto foi desenvolvido como parte da avaliação da disciplina de Arquitetura de Software[cite: 240]. [cite_start]O objetivo é construir o back-end de uma aplicação distribuída utilizando a arquitetura de microsserviços, demonstrando comunicação síncrona, validação de dados distribuída e orquestração de transações[cite: 242].

---

## 1. Documento de Requisitos

### a. Propósito do Sistema
O sistema tem como propósito gerenciar as informações de um condomínio de forma desacoplada. [cite_start]Ele permite o cadastro de moradores, unidades habitacionais e o lançamento de taxas condominiais, garantindo a integridade financeira e cadastral através de integrações em tempo real entre os serviços[cite: 251].

### b. Usuários
* [cite_start]**Administrador (Síndico/Zeladoria):** Responsável por cadastrar unidades, registrar moradores e lançar cobranças[cite: 252].

### c. [cite_start]Requisitos Funcionais (RF) [cite: 253]
Abaixo estão listadas as funcionalidades e regras de negócio implementadas:

* **[RF01] Manter Moradores:** Cadastro, consulta e atualização de moradores e seus saldos devedores.
* **[RF02] Manter Residências:** Cadastro e consulta de unidades habitacionais.
* **[RF03] Regra de Negócio (Bloqueio de Inadimplência):** O sistema **não deve permitir** o cadastro de uma residência vinculada a um morador que possua dívidas ativas (ValorDivida > 0).
* **[RF04] Lançar Taxas:** Cadastro de cobranças associadas a uma residência.
* [cite_start]**[RF05] Integração de Consulta (Residência -> Morador):** Ao consultar uma residência, o sistema deve buscar automaticamente o nome e telefone do morador no serviço de Moradores[cite: 246].
* [cite_start]**[RF06] Validação de Lançamento (Taxas -> Residência):** Antes de lançar uma taxa, o sistema deve validar na API de Residências se a unidade existe e está ativa[cite: 272].
* [cite_start]**[RF07] Atualização Financeira (Taxas -> Morador):** Após lançar uma taxa, o sistema deve atualizar automaticamente a dívida do morador responsável na API de Moradores[cite: 273].

---

## [cite_start]2. Descritivo Técnico [cite: 254]

A solução utiliza **.NET 8** e **SQLite**, composta por três microsserviços independentes que se comunicam via **HTTP/REST**.

### Estrutura dos Microsserviços

#### 1. Moradores.API
* **Função:** Gerenciamento de dados pessoais e financeiros.
* **Dados:** Tabela `Moradores` (Id, Nome, CPF, Telefone, ValorDivida).
* **Papel:** Atua como servidor de dados para os outros serviços.

#### 2. Residencias.API
* **Função:** Gerenciamento das unidades físicas.
* **Dados:** Tabela `Residencias` (Id, Numero, Bloco, IdMoradorResponsavel).
* **Integrações:**
    * Consome `Moradores.API` (GET) para exibir o nome do responsável.
    * Consome `Moradores.API` (GET) para validar se o morador possui dívidas antes do cadastro (Regra de Negócio).

#### 3. Taxas.API (Orquestrador)
* **Função:** Gerenciamento financeiro e cobranças.
* **Dados:** Tabela `Taxas` (Id, Valor, Descricao, IdResidencia).
* **Integrações:**
    * Consome `Residencias.API` (GET) para validar a unidade.
    * Consome `Moradores.API` (PUT) para somar o valor da taxa na dívida do morador.

---

## 3. Como Executar o Projeto

### Pré-requisitos
* Visual Studio 2022+ ou VS Code.
* .NET SDK 8.0.

### Passo a Passo
1.  **Banco de Dados:**
    * Caso seja a primeira execução ou tenha limpado os dados, abra o **Console do Gerenciador de Pacotes** no Visual Studio.
    * Execute `Update-Database` para cada projeto individualmente (`Moradores.API`, `Residencias.API`, `Taxas.API`).

2.  **Inicialização:**
    * Configure a solução para **"Vários Projetos de Inicialização"**.
    * Defina a ação **"Iniciar"** para os três projetos simultaneamente.

3.  **Configuração de URLs:**
    * Certifique-se de que as URLs no `Program.cs` de `Residencias.API` e `Taxas.API` correspondem às portas locais onde os serviços estão rodando (ex: `https://localhost:7042/`).

---

## 4. Roteiro de Testes (Demonstração)

Siga esta ordem para validar todos os requisitos na apresentação:

### Cenário A: Cadastro com Sucesso
1.  **Moradores (POST):** Cadastre um morador "João" (Dívida inicia em 0).
    * *Resultado:* Sucesso (200).
2.  **Residências (POST):** Cadastre uma residência para o "João".
    * *Resultado:* Sucesso (200) -> O sistema validou que ele não tem dívida.
3.  **Residências (GET {id}):** Busque a casa criada.
    * *Resultado:* O JSON retorna os dados da casa + o nome "João" (Integração de busca).

### Cenário B: Fluxo Financeiro (Taxa e Dívida)
1.  **Taxas (POST):** Lance uma taxa de R$ 500,00 para a casa do João.
    * *Resultado:* Sucesso (200).
2.  **Moradores (GET {id}):** Busque o cadastro do João novamente.
    * *Resultado:* O campo `valorDivida` agora é **500.00** (Integração de alteração).

### Cenário C: Regra de Bloqueio (Inadimplência)
1.  **Residências (POST):** Tente cadastrar uma **nova/segunda** casa para o "João" (que agora deve R$ 500,00).
    * *Resultado:* **Erro 400 (Bad Request)**.
    * *Mensagem:* "Cadastro negado: O morador João possui dívida de R$ 500,00."