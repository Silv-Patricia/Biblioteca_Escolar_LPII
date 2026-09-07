# 📚 Sistema de Biblioteca Escolar

Uma aplicação de console robusta desenvolvida em C# para o gerenciamento completo do acervo, usuários e empréstimos de uma biblioteca escolar. Este projeto foi construído com forte ênfase em **Programação Orientada a Objetos (POO)**, aplicando conceitos avançados de arquitetura limpa e separação de responsabilidades.

## ✨ Funcionalidades

* **Gestão de Usuários:** Cadastro seguro com validação de campos obrigatórios e garantia de unicidade de matrícula. Suporte à atualização de dados (e-mail).
* **Gestão de Acervo (Polimorfismo):** Cadastro dinâmico de `Livros` e `Revistas`, com regras específicas de negócio (prazos e multas diferenciados) herdadas de uma classe abstrata comum.
* **Empréstimos Inteligentes:** Validação rigorosa de regras de negócio (limite máximo de 3 empréstimos ativos por usuário, bloqueio de itens já emprestados).
* **Simulador de Empréstimos e Devoluções:** Módulo de testes integrado e sobrecarga de construtores que permite simular datas customizadas tanto para a retirada quanto para devoluções futuras, projetando cálculos de prazos e multas para casos de uso atípicos.
* **Simulador de Devolução:** Módulo de testes integrado que permite simular devoluções em datas futuras para projetar cálculos de multas sem afetar o banco de dados em memória.
* **Relatórios e Consultas:** Geração de tabelas formatadas e dinâmicas (utilizando LINQ) para listagem de acervo, usuários, empréstimos ativos e consolidação financeira das multas.

## 🚀 Tecnologias e Conceitos Utilizados

**Linguagem & Framework:**
* C#
* .NET 8.0 (Console Application)

**Engenharia de Software & Boas Práticas:**
* **POO Avançada:** Uso de Classes Abstratas, Interfaces (`IEmprestavel`), Herança e Polimorfismo.
* **Encapsulamento Restrito:** Proteção do estado interno dos objetos (ex: as datas e o status de disponibilidade só mudam através de métodos controlados).
* **Separação de Responsabilidades (SRP):**
  * `Menu.cs`: Responsável exclusivo pela camada de interface (I/O, validações de *TryParse*, tratamento de strings vazias).
  * `Biblioteca.cs`: Atua como a camada de serviço/controlador, gerenciando as listas e o fluxo das regras de negócio.
  * `Program.cs`: Foca apenas no laço principal de execução e roteamento.
* **Design Patterns & Clean Code:** Uso extensivo de *Early Return* (retorno antecipado) para evitar aninhamento de `if/else`, *Delay Declaration* de variáveis para otimizar memória e reaproveitamento de código (DRY).

---

## ⚙️ Como Executar o Projeto

Como a aplicação foi desenvolvida em .NET, é necessário ter o [SDK do .NET](https://dotnet.microsoft.com/pt-br/download) instalado em sua máquina.

1. **Clone este repositório:**
   ```bash
   git clone https://github.com/Silv-Patricia/Biblioteca_Escolar_LPII.git
   cd biblioteca-escolar
   ```

2. **Compile e execute a aplicação:**
   Na raiz do projeto (onde está localizado o arquivo `Biblioteca_Escolar.csproj`), execute o seguinte comando no terminal:
   ```bash
   dotnet run
   ```

---

## 📁 Estrutura do Projeto

* `Program.cs`: Ponto de entrada da aplicação, contendo o loop do menu principal.
* `Menu.cs`: Classe utilitária estática para entrada e saída de dados no console, garantindo validações anti-falhas.
* `Biblioteca.cs`: Classe central que armazena os dados em memória (Listas) e orquestra as operações principais (Cadastros, Consultas, Empréstimos, Relatórios).
* `Usuario.cs`: Entidade de domínio representando os leitores.
* `MaterialBiblioteca.cs` (e classes filhas `Livro` e `Revista`): Entidades de domínio do acervo com regras de polimorfismo.
* `Emprestimo.cs`: Classe associativa que une Usuários e Materiais, responsável pelos cálculos de datas, atrasos e multas.

---

## 👨‍💻 Autores

**[Valdir Neto](https://github.com/valdirneto34)** & **[Patrícia Silva](https://github.com/Silv-Patricia)**
Estudantes de Sistemas de Informação no IFMG-SJE.
