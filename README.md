# Teste-Projtc-Slotgame
# 🎰 SlotIcons — Jogo 777

Projeto desenvolvido em **C# / .NET 8** para aprendizado de programação, orientação a objetos, Entity Framework Core e integração com MySQL.

---

## 📌 Objetivo

Criar um jogo de caça-níquel simples executado no terminal.

O jogador pode:

* Criar uma conta;
* Fazer login;
* Começar com 100 moedas;
* Girar os três slots;
* Ganhar moedas ao acertar os três símbolos;
* Perder moedas quando não acertar;
* Recarregar moedas quando ficar sem saldo;
* Sair do jogo.

O projeto possui finalidade **educacional** e não envolve dinheiro real.

---

# 🛠️ Tecnologias

* C#
* .NET 8
* Entity Framework Core
* MySQL
* MySQL Workbench
* Pomelo.EntityFrameworkCore.MySql
* Microsoft.EntityFrameworkCore.Tools

---

# 📂 Estrutura do projeto

```text
Teste_Projtc_Slotgame
│
├── Data
│   └── AppDbContext.cs
│
├── Models
│   └── Usuario.cs
│
├── Services
│   ├── UsuarioService.cs
│   └── JogoService.cs
│
└── Program.cs
```

### Responsabilidade de cada arquivo

| Arquivo             | Responsabilidade                        |
| ------------------- | --------------------------------------- |
| `Program.cs`        | Controla o fluxo principal do programa  |
| `Usuario.cs`        | Representa um usuário                   |
| `AppDbContext.cs`   | Faz a comunicação entre C# e MySQL      |
| `UsuarioService.cs` | Cadastro, login e atualização de moedas |
| `JogoService.cs`    | Regras e funcionamento do jogo          |

---

# 🗄️ Banco de dados

O banco utilizado é:

```text
game_slot
```

A tabela:

```text
usuarios
```

Foi criada **manualmente no MySQL Workbench**.

Não estamos utilizando migrations para criar a tabela.

## Tabela `usuarios`

```sql
CREATE TABLE usuarios
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100),
    usuario VARCHAR(50),
    senha VARCHAR(100),
    moedas INT DEFAULT 100
);
```

### Colunas

| Coluna    | Tipo         | Função                   |
| --------- | ------------ | ------------------------ |
| `id`      | INT          | Identificador do usuário |
| `nome`    | VARCHAR(100) | Nome do jogador          |
| `usuario` | VARCHAR(50)  | Login do jogador         |
| `senha`   | VARCHAR(100) | Senha                    |
| `moedas`  | INT          | Quantidade de moedas     |

O `id` é:

```text
AUTO_INCREMENT PRIMARY KEY
```

Portanto, o MySQL gera automaticamente o ID.

---

# 🔌 Entity Framework Core

Foram instalados:

```text
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools
Pomelo.EntityFrameworkCore.MySql
```

### Função de cada um

**Entity Framework Core**

É o ORM utilizado para trabalhar com o banco usando classes C#.

**Pomelo**

É o provider que permite ao Entity Framework Core trabalhar com MySQL.

**EF Core Tools**

Fornece ferramentas para tarefas como migrations e gerenciamento do banco durante o desenvolvimento.

Neste projeto, entretanto, a tabela foi criada manualmente no MySQL Workbench.

---

# 📄 AppDbContext.cs

O `AppDbContext` representa a conexão entre o programa e o banco de dados.

```csharp
using Microsoft.EntityFrameworkCore;
using Teste_Projtc_Slotgame.Models;

namespace Teste_Projtc_Slotgame.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            string connection =
                "server=localhost;database=game_slot;user=root;password=basico";

            optionsBuilder.UseMySql(
                connection,
                ServerVersion.AutoDetect(connection));
        }
    }
}
---

# 🔐 Login

O método:

```csharp
public Usuario? Login()
```

retorna o usuário encontrado ou `null`.

Exemplo:

```csharp
Usuario? usuario = usuarioService.Login();

if (usuario != null)
{
    jogoService.Jogar(usuario);
}
```

Isso permite passar o usuário logado para o jogo.

---

# 🎰 JogoService.cs

O `JogoService` contém a lógica do jogo.

Os símbolos utilizados são:

```text
🍒
🐔
⭐
💎
```

Eles ficam em:

```csharp
private readonly string[] _icons =
{
    "🍒",
    "🐔",
    "⭐",
    "💎"
};
```

Os símbolos são escolhidos aleatoriamente utilizando:

```csharp
Random
```

---

# 🎲 Funcionamento do jogo

A cada rodada são escolhidos três símbolos:

```csharp
string slot1 = _icons[_random.Next(_icons.Length)];
string slot2 = _icons[_random.Next(_icons.Length)];
string slot3 = _icons[_random.Next(_icons.Length)];
```

Depois eles são exibidos no terminal.

---

# 🏆 Regra de vitória

O jogador ganha quando os três símbolos são iguais:

```csharp
if (slot1 == slot2 && slot2 == slot3)
```

Nesse caso:

```text
+50 moedas
```

Caso contrário:

```text
-10 moedas
```

---

# 💰 Sistema de moedas

O jogador começa com:

```text
100 moedas
```

### Vitória

```text
+50 moedas
```

### Derrota

```text
-10 moedas
```

Exemplo:

```csharp
_usuarioService.AtualizarMoedas(usuario, 50);
```

ou:

```csharp
_usuarioService.AtualizarMoedas(usuario, -10);
```

O valor é atualizado no objeto e salvo no MySQL.

---

# 🪙 Quando o jogador fica sem moedas

Foi decidido que o jogador **não deve conseguir continuar girando quando chegar a 0 moedas**.

Por isso, a verificação deve acontecer **antes de iniciar uma nova rodada**.

---

# 🔄 Fluxo do jogo

O fluxo ficou aproximadamente assim:

```text
Início
  ↓
Menu principal
  ↓
Cadastrar ou fazer login
  ↓
Login realizado
  ↓
Entrar no jogo
  ↓
Possui moedas?
  ├── NÃO → Recarregar ou sair
  │
  └── SIM
       ↓
    Girar slots
       ↓
    3 símbolos
       ↓
    São iguais?
     ├── SIM → +50 moedas
     │
     └── NÃO → -10 moedas
       ↓
    Verificar moedas
       ↓
    Nova rodada
```

---

# ⏳ Tela de carregamento

Foi criada uma função:

```csharp
private void Load()
{
    Console.Write("Carregando ");

    for (int i = 0; i < 10; i++)
    {
        Console.Write("█");
        Thread.Sleep(150);
    }
}
```

Ela serve apenas para criar uma pequena animação de carregamento.

Essa função pertence ao:

```text
JogoService
```

porque faz parte da experiência do jogo.

---

# 🖥️ Program.cs

O `Program.cs` controla o fluxo geral da aplicação.

Também é nele que configuramos o UTF-8:

```csharp
Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
```

Isso é importante para os emojis:

```text
🎰 🍒 🐔 ⭐ 💎
```

---

# 🔗 Comunicação entre os serviços

No `Program.cs`:

```csharp
UsuarioService usuarioService = new UsuarioService();

JogoService jogoService =
    new JogoService(usuarioService);
```

O `JogoService` recebe o `UsuarioService`:

```csharp
public JogoService(UsuarioService usuarioService)
{
    _usuarioService = usuarioService;
}
```

Isso permite que o jogo atualize as moedas através do serviço de usuário.

---

# 📋 Menu principal

O programa apresenta:

```text
==== Bem Vindo ao SlotIcons! ====

Escolha uma das opções:

1 - Cadastre uma conta para jogar
2 - Logue na sua conta
3 - Sair
```

### Opção 1

Chama:

```csharp
usuarioService.Cadastrar();
```

### Opção 2

Chama:

```csharp
Usuario? usuario = usuarioService.Login();
```

Se encontrar o usuário:

```csharp
jogoService.Jogar(usuario);
```

Caso contrário:

```text
Login ou senha incorretos.
```

### Opção 3

Encerra o programa:

```csharp
return;
```

---
# 🏗️ Arquitetura simplificada

```text
             Program.cs
                  │
          ┌───────┴───────┐
          ↓               ↓
 UsuarioService      JogoService
          │               │
          ↓               ↓
     AppDbContext     Regras do jogo
          │
          ↓
        MySQL
          │
          ↓
      game_slot
          │
          ↓
      usuarios
```

---

# ⚠️ Melhorias futuras

Algumas melhorias podem ser feitas posteriormente:

* Impedir nomes de usuário duplicados;
* Validar campos vazios;
* Melhorar o tratamento de erros;
* Utilizar `int.TryParse()` no depósito de moedas;
* Criar um sistema de apostas;
* Adicionar mais símbolos;
* Criar diferentes prêmios;
* Criar histórico de partidas;
* Criar ranking;
* Adicionar menu dentro do jogo;
* Melhorar a interface do terminal;
* Criptografar/hash das senhas;
* Separar melhor algumas responsabilidades.
---

# 🎯 Resumo final

O **SlotIcons** é um jogo de caça-níquel simples feito em C# para praticar programação.

O usuário cria uma conta, faz login e recebe **100 moedas**.

Durante o jogo:

```text
3 símbolos iguais → +50 moedas
Qualquer combinação diferente → -10 moedas
```

Quando as moedas chegam a:

```text
0
```

o jogador precisa escolher entre:

```text
[1] Receber +100 moedas
[2] Sair
```

Os dados do jogador são armazenados no **MySQL**, enquanto o **Entity Framework Core** faz a comunicação entre o código C# e o banco.

A estrutura principal é:

```text
Program
   ↓
UsuarioService
   ↓
AppDbContext
   ↓
MySQL

Program
   ↓
JogoService
   ↓
UsuarioService
   ↓
AppDbContext
   ↓
MySQL
```

O projeto tem como principal objetivo servir como exercício prático para aprender **C#, orientação a objetos, banco de dados e Entity Framework Core**.
