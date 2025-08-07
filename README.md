# Projeto Capstone: Recriação de Space Invaders


## 1. Visão Geral do Projeto

Este repositório contém o código-fonte e a documentação do projeto final da disciplina de Programação 3. O objetivo é recriar o clássico jogo **Space Invaders** como uma aplicação de desktop, utilizando C# e a Uno Platform, com foco na aplicação de conceitos de Programação Orientada a Objetos, Estruturas de Dados e manipulação de eventos.

## 2. Documentação Detalhada

A documentação do projeto é mantida em duas plataformas para garantir acesso fácil e abrangente a todas as informações relevantes:

- **[Documentação Oficial (via Writerside)](https://capstone-8f3123.gitlab.io/intro.html)**: A fonte principal para a documentação técnica, guias de usuário, e detalhes da arquitetura. É gerada automaticamente a partir dos arquivos no diretório `/Writerside`.

- **[Wiki do GitLab](https://gitlab.com/jala-university1/cohort-4/oficial-pt-programa-o-3-cspr-231.ga.t2.25.m1/se-o-a/gustavo.jesus/capstone/-/wikis/home)**: Utilizada para anotações de desenvolvimento.


## 3. Resumo dos Requisitos Funcionais Chave

| ID | Requisito | Prioridade |
| --- | --- | --- |
| RF01 | Controle da Nave (Movimento Horizontal) | Alta |
| RF02 | Disparo de Lasers (Vertical) | Alta |
| RF04 | Geração e Movimento de Inimigos | Alta |
| RF06 | Sistema de Vidas e Dano | Alta |
| RF07 | Condições de Fim de Jogo | Alta |
| RF19 | Menus e Telas (Inicial, Fim de Jogo) | Alta |
| RF21 | Persistência de Placares | Alta |

## 4. Tecnologias

*   **Linguagem**: C#
*   **Plataforma**: Uno Platform (para aplicação de desktop)
*   **UI**: XAML
*   **Persistência de Dados**: Entity Framework Core com PostgreSQL
*   **Padrão de Arquitetura**: MVVM (Model-View-ViewModel)
*   **Controle de Versão**: Git

## 5. Estrutura do Projeto

O projeto está organizado nas seguintes pastas principais:

*   **`SpaceInvaders/Models`**: Contém as classes de modelo de dados, como `Player`, `Alien`, `Projectile`, `Score`, e as classes de configuração da aplicação (`AppConfig`).
*   **`SpaceInvaders/ViewModels`**: Responsável pela lógica de apresentação e interação com os modelos, incluindo `MainViewModel`, `GameOverViewModel`, `ScoreViewModel`, etc.
*   **`SpaceInvaders/Services`**: Implementa a lógica de negócio e a interação com o banco de dados, como `PlayerService`, `ScoreService`, e `SoundService`.
*   **`SpaceInvaders/Data`**: Contém o contexto do banco de dados (`SpaceInvadersDbContext`) e as migrações do Entity Framework Core.
*   **`SpaceInvaders/Presentation`**: Define a interface do usuário (UI) em XAML, incluindo as páginas (`MainPage.xaml`, `GameOver.xaml`, `ScorePage.xaml`) e o `Shell` da aplicação.
*   **`SpaceInvaders/Constants`**: Armazena constantes utilizadas no projeto, como caminhos para sons e sprites.
*   **`SpaceInvaders/Assets`**: Contém todos os recursos visuais e sonoros do jogo, como imagens, fontes e arquivos de áudio.

## Diagrama de Estrutura do Projeto

Abaixo, um diagrama que ilustra a organização das principais pastas e a relação entre elas:

```plantuml
@startuml
skinparam componentStyle rectangle

package "SpaceInvaders" {
    folder "Models" {
        [Player]
        [Alien]
        [Score]
        [AppConfig]
    }

    folder "ViewModels" {
        [MainViewModel]
        [GameOverViewModel]
        [ScoreViewModel]
    }

    folder "Services" {
        [PlayerService]
        [ScoreService]
        [SoundService]
    }

    folder "Data" {
        [SpaceInvadersDbContext]
        [Migrations]
    }

    folder "Presentation" {
        [MainPage.xaml]
        [GameOver.xaml]
        [ScorePage.xaml]
        [Shell.xaml]
    }

    folder "Constants" {
        [SoundPaths.cs]
        [SpritePaths.cs]
    }

    folder "Assets" {
        [Images]
        [Fonts]
        [Sounds]
    }
}

ViewModels --> Models : manipula
Services --> Models : interage
Services --> Data : acessa
Presentation --> ViewModels : exibe dados
@enduml
```
