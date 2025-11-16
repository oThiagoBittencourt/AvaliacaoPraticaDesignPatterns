# AvaliacaoPraticaDesignPatterns
Projeto demonstrando uso de **SOLID** e **Design Patterns** em C#

# 📖 Sobre o Projeto

Este repositório contém a implementação de quatro problemas independentes, cada um exigindo o uso de padrões de projeto e princípios de design.

Cada módulo inclui um arquivo .txt, justificando a escolha de cada padrão.

---

# Questão 1 – Estratégia para Algoritmos de Risco (0,5)

### Contexto

Uma empresa financeira precisa calcular diferentes métricas de risco (**VaR**, **Expected Shortfall**, **Stress Testing**) que podem mudar em tempo de execução.

### Problemas Resolvidos

* Algoritmos intercambiáveis dinamicamente
* Compartilhamento de contexto financeiro complexo
* Troca de estratégias sem expor implementação ao cliente

### Padrão Utilizado

**Strategy Pattern**

Permite que cálculos de risco sejam trocados em tempo real, mantendo baixo acoplamento e alta extensibilidade.

### Estrutura

* `IRiskStrategy` → Interface comum
* `ValueAtRiskStrategy`, `ExpectedShortfallStrategy`, `StressTestingStrategy` → Implementações
* `RiskContext` → Armazena parâmetros financeiros
* `RiskCalculator` → Composição + polimorfismo para troca dinâmica

---

# Questão 2 – Adapter para Integração com Legado Bancário (0,5)

### Contexto

A empresa integra-se a um sistema legado com interface incompatível (`SistemaBancarioLegado`).

### Problemas Resolvidos

* Conversão bidirecional moderno ↔ legado
* Tratamento de obrigatórios do legado
* Conversão de moedas (USD=1, EUR=2, BRL=3)

### Padrão Utilizado

**Adapter Pattern (Two-Way Adapter)**

Permite que o sistema moderno interaja com o legado sem modificar nenhum dos dois.

### Estrutura

* `IProcessadorTransacoes` → interface moderna
* `TwoWayBancarioAdapter` → converte chamadas em tempo real
* Tratamento de campos obrigatórios e normalização de dados

---

# Questão 3 – Máquina de Estados para Usina Nuclear (0,25)

### Contexto

Modelagem de estados críticos da usina:
`DESLIGADA`, `OPERACAO_NORMAL`, `ALERTA_AMARELO`, `ALERTA_VERMELHO`, `EMERGENCIA`, além do estado especial `MANUTENCAO`.

### Problemas Resolvidos

* Transições com validações complexas
* Prevenção de transições circulares
* Regras estritas para emergência
* Sobrescrita de comportamento em modo manutenção

### Padrão Utilizado

**State Pattern**

Encapsula o comportamento de cada estado e garante segurança nas transições.

### Estrutura

* `IUsinaState` → interface comum
* Estados concretos implementam suas regras
* `StateMachineUsina` → controla transições com segurança

---

# Questão 4 – Cadeia de Validação NF-e (0,25)

### Contexto

O sistema precisa validar NF-e em uma cadeia, com regras flexíveis e rollback.

### Problemas Resolvidos

* Validadores especializados
* Execução condicional
* **Circuit Breaker** após 3 falhas
* Timeout individual
* Rollback para validadores que modificam o documento
* Respeito às regras:

  * Validadores **3** e **5** só executam se anteriores passarem
  * Validador **4** precisa desfazer operações se subsequentes falharem

### Padrões Utilizados

* **Chain of Responsibility** para a cadeia de execução
* **Command/Transactional Validator** para rollback
* **Circuit Breaker** para parar falhas repetidas
* **Template Method** (parcial) para padronizar estrutura comum

### Estrutura

* `IValidator` → interface base
* Validadores:

  1. Schema XML
  2. Certificado digital
  3. Regras fiscais
  4. Banco de dados (com rollback)
  5. Consulta SEFAZ
* `ValidationPipeline` → controla execução, circuit breaker e timeouts

---

# Estrutura do Repositório

```
/
│── Questao1_Risco/
│   ├── Strategies/
│   ├── Context/
│   └── RiskCalculator.cs
│
│── Questao2_Legado/
│   ├── Adapter/
│   └── Models/
│
│── Questao3_Usina/
│   ├── States/
│   └── StateMachineUsina.cs
│
│── Questao4_ValidacaoNFe/
│   ├── Validators/
│   ├── Pipeline/
│   └── Models/
│
└── README.md
```

---

# Como Executar

```
dotnet build
dotnet run
```

Cada questão possui um `Program.cs` independente para testes.

---

# Princípios SOLID Aplicados

* **S**ingle Responsibility – cada classe tem responsabilidade única
* **O**pen/Closed – novos algoritmos, estados e validadores podem ser adicionados sem alterar código existente
* **L**iskov Substitution – todas as interfaces permitem substituição segura
* **I**nterface Segregation – interfaces pequenas e específicas
* **D**ependency Inversion – classes dependem de abstrações
