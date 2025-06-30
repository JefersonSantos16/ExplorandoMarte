# 🚀 Explorando Marte

Este projeto é a solução para o desafio técnico "Explorando Marte", no qual sondas (rovers) devem ser controladas sobre um planalto em Marte, recebendo comandos de movimentação e rotação. O sistema simula a movimentação e valida limites e conflitos de posição entre as sondas.

---

## ✅ Descrição resumida do problema

A NASA enviou sondas a Marte que devem explorar um planalto retangular. Cada sonda:
- Recebe uma posição inicial (X, Y, direção).
- Recebe uma sequência de comandos (`L`, `R`, `M`) que indicam rotação ou movimento.
- Deve evitar sair dos limites do planalto ou ocupar uma posição já ocupada por outra sonda.

A aplicação:
- Lê a entrada do console.
- Executa os comandos.
- Valida limites e colisões.
- Exibe as posições finais das sondas.

---

## ⚙️ Como configurar e rodar o projeto

### Pré-requisitos
- Visual Studio 2015 ou superior
- .NET Framework 4.5.2
- NUnit (já instalado no projeto de testes)

### Estrutura de projetos
- `ExplorandoMarte.Domain` → Entidades, enums e exceções.
- `ExplorandoMarte.App` → Serviços de aplicação.
- `ExplorandoMarte.Tests` → Testes unitários com NUnit.
- `ExplorandoMarte` → Aplicação de console (entrada do usuário).

### Como rodar
1. Abra o Visual Studio.
2. Defina o projeto `ExplorandoMarte` como projeto de inicialização.
3. Pressione **F5** para executar.
4. Siga as instruções no console para digitar dimensões do planalto, posição inicial e comandos.

---

## 🧱 Decisões de projeto

- Utilizamos orientação a objetos para separar responsabilidades.
- Implementamos validações de limites e colisões.
- Usamos testes unitários para validar cenários de movimentação e erro.
- Aplicação simples via console para facilitar testes e entrada manual.

---

## 🧩 Padrões de projeto utilizados

### 🔹 Command Pattern
Cada comando (`L`, `R`, `M`) foi implementado como uma classe separada (`RotateLeftCommand`, `RotateRightCommand`, `MoveCommand`) que implementa a interface `ICommand`. Isso facilita a extensão do sistema para novos comandos sem modificar o código existente (OCP - Open/Closed Principle).

### 🔹 Factory Pattern
A classe `CommandFactory` é responsável por criar instâncias dos comandos com base no caractere informado (`L`, `R`, `M`), encapsulando a lógica de instanciamento e tornando o código mais limpo.

---

## 🐞 Debugging no Visual Studio

Durante o desenvolvimento, utilizei o depurador do Visual Studio:

- **Breakpoints** foram colocados em métodos como `MoveForward`, `ExecuteCommands`, `AddRover`.
- **Inspeção de variáveis** foi usada para verificar se as posições estavam dentro dos limites.
- **Step Into (F11)** e **Step Over (F10)** ajudaram a entender o fluxo exato dos comandos.

---

## 🔁 Pipeline de CI (Integração Contínua)

O projeto inclui um workflow GitHub Actions:

### Arquivo: `.github/workflows/ci.yml`

```yaml
name: .NET Framework CI

on: [push, pull_request]

jobs:
  build-and-test:
    runs-on: windows-latest

    steps:
    - uses: actions/checkout@v2
    - name: Instalar dependências
      run: nuget restore ExplorandoMarte.sln
    - name: Build
      run: msbuild ExplorandoMarte.sln
    - name: Testes
      run: |
        vstest.console.exe ExplorandoMarte.Tests\bin\Debug\ExplorandoMarte.Tests.dll
```

### Como verificar os resultados
- Vá até a aba **"Actions"** no repositório no GitHub.
- Clique no workflow mais recente para ver os logs e status dos testes.

---

## ✨ Destaques adicionais

- Todos os testes unitários estão implementados com NUnit, cobrindo casos de sucesso, erro e exceções.
- O código está pronto para ser estendido com novos tipos de comandos ou diferentes regras de movimentação.
- O código segue princípios SOLID, com ênfase em responsabilidade única (SRP) e abstração por interfaces.

---

## 🧪 Exemplo de entrada e saída

**Entrada:**
```
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
```

**Saída esperada:**
```
1 3 N
5 1 E
```

---

## 👨‍💻 Autor
[Jeferson Santos](https://github.com/JefersonSantos16)
