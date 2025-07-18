# Arquitetura

## Technology Stack

A pilha de tecnologia para este projeto foi escolhida para focar em tecnologias .NET modernas e garantir a portabilidade da aplicação de desktop.

- **Linguagem de Programação**: **C#**
  - Uma linguagem robusta, orientada a objetos, ideal para o desenvolvimento de aplicações complexas como jogos.
- **Plataforma**: **Uno Platform**
  - Utilizada para construir a aplicação de desktop. Garante que a base de código possa ser potencialmente portada para outras plataformas (Web, Mobile) no futuro.
- **Definição de UI**: **XAML**
  - Usado para definir de forma declarativa a interface do usuário, separando a lógica da apresentação.
- **Persistência de Dados**: **Arquivo de Texto (.txt)**
  - Para o salvamento dos placares, será utilizado um arquivo de texto simples, seguindo os requisitos do projeto.

## Wireframe

A seguir, uma representação textual das principais telas do jogo.

### 1. Tela Inicial

```
+----------------------------------------+
|                                        |
|             CATSTEROIDS                |
|         (Space Invaders Clone)         |
|                                        |
|           [ Iniciar Jogo ]             |
|           [   Placares   ]             |
|           [  Controles   ]             |
|                                        |
+----------------------------------------+
```

### 2. Tela de Jogo

```
+----------------------------------------+
| PONTOS: 00420         VIDAS: ❤️ ❤️      |
|----------------------------------------|
|                                        |
|                👾👾👾👾👾               |
|                   | (laser do jogador) |
|             👾👾 👾👾👾               |
|                                        |
|     -=-*     ---    -**-    ---        |
|      * (laser inimigo)                 |
|                                        |
|            🤖                          |
+----------------------------------------+
```

### 3. Tela de Fim de Jogo

```
+----------------------------------------+
|                                        |
|               FIM DE JOGO              |
|                                        |
|         Sua pontuação: 01230           |
|                                        |
|   Apelido: [___________] [ Salvar ]    |
|                                        |
|           [ Jogar Novamente ]          |
|           [  Voltar ao Menu ]          |
|                                        |
+----------------------------------------+
```

## Database Schema

> Isso é apenas um exemplo básico. 

O "banco de dados" do projeto será um simples arquivo de texto chamado `scores.txt` para armazenar os placares. A estrutura será baseada em valores separados por vírgula (CSV), facilitando a leitura e a escrita.

**Arquivo**: `scores.txt`

**Formato**: Cada linha representará uma pontuação salva.

```
Nickname,Score
```

**Exemplo de Conteúdo:**

```
Player1,1550
GamerX,980
CatLover,2100
```

