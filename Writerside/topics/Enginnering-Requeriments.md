# Engenharia de Requisitos

## Requisitos Funcionais

| ID | Requisito | Descrição                                                                                                         | Prioridade |
| --- | --- |-------------------------------------------------------------------------------------------------------------------| --- |
| RF01 | Controle da Nave | O sistema deve permitir que o usuário controle uma nave, movimentando-a horizontalmente com as setas direcionais. | Alta |
| RF02 | Disparo de Lasers | O usuário deve conseguir disparar lasers verticalmente com a barra de espaço.                                     | Alta |
| RF03 | Pontuação por Inimigo | O sistema deve atribuir pontos ao usuário quando um laser acerta um inimigo, com base na [tabela de inimigos](#tabela-de-inimigos). | Alta |
| RF04 | Geração de Inimigos | Inimigos (tipos 1, 2, 3) devem aparecer na tela e se mover sem sair do quadro de jogo.                            | Alta |
| RF05 | Inimigo Especial | Um alienígena vermelho especial deve cruzar a tela periodicamente, oferecendo pontos variáveis.                   | Média |
| RF06 | Sistema de Vidas | O usuário começa com 3 vidas e perde uma ao ser atingido ou tocar em um alienígena.                               | Alta |
| RF07 | Condição de Fim de Jogo | O jogo termina se o jogador perder todas as vidas ou se os alienígenas alcançarem a base.                         | Alta |
| RF08 | Barreiras de Proteção | O sistema deve ter 4 blocos de proteção que se degradam com o dano.                                               | Alta |
| RF09 | Aumento de Dificuldade | A velocidade de movimento e de tiro dos alienígenas deve aumentar conforme eles descem.                           | Média |
| RF10 | Ataque Inimigo | Apenas o tipo de alienígena que vale 40 pontos pode atirar.                                                       | Média |
| RF11 | Recarga de Tiro | O usuário só pode disparar novamente após o tiro anterior atingir um alvo ou sair da tela.                        | Alta |
| RF12 | Comportamento dos Inimigos | Os alienígenas não devem colidir com as barreiras de proteção.                                                    | Baixa |
| RF13 | Dano em Inimigos | O usuário deve conseguir destruir naves alienígenas com um único tiro.                                            | Alta |
| RF14 | Novas Ondas | Uma nova onda de inimigos deve aparecer após a anterior ser destruída, com dificuldade aumentada.                 | Alta |
| RF15 | Vida Extra | O jogador ganha uma vida extra a cada 1000 pontos.                                                                | Média |
| RF16 | Exibição de Pontuação | A pontuação deve ser exibida no canto superior esquerdo da tela.                                                  | Alta |
| RF17 | Incremento de Pontuação | A pontuação deve ser incrementada de acordo com a [tabela de inimigos](#tabela-de-inimigos).                      | Alta |
| RF18 | Tela de Fim de Jogo | Ao final do jogo, deve ser possível salvar a pontuação com um apelido ou jogar novamente.                         | Alta |
| RF19 | Tela Inicial | O jogo deve ter uma tela inicial com opções para "Novo Jogo", "Placares" e "Controles".                           | Alta |
| RF20 | Efeitos Sonoros | O sistema deve ter um som representativo para cada ação principal do jogo.                                        | Média |
| RF21 | Salvamento de Placar | As informações do painel de pontuação devem ser salvas em um arquivo de texto.                                    | Alta |

## Tabela de Inimigos

| Tipo de Inimigo | Pontuação | Observações |
| --- | --- | --- |
| Inimigo Tipo 1 | 10 | Inimigo padrão, não atira. |
| Inimigo Tipo 2 | 20 | Inimigo padrão, não atira. |
| Inimigo Tipo 3 | 40 | Inimigo que pode atirar. |
| Alienígena Vermelho Especial | 50-250 (variável) | Cruza a tela periodicamente. |

## Requisitos Não Funcionais

### 1. Plataforma

*   **RNF01**: O jogo deve ser uma aplicação de desktop, construída com a Uno Platform para garantir a portabilidade.

### 2. Tecnologia

*   **RNF02**: A linguagem de programação será C#.
*   **RNF03**: A interface do usuário (UI) será definida usando XAML.
*   **RNF04**: Nenhuma game engine externa (ex: Unity, Godot) será utilizada.

### 3. Som

*   **RNF05**: Cada ação significativa no jogo (disparo, destruição de inimigo, morte do jogador, etc.) deve ser acompanhada por um efeito sonoro correspondente.
