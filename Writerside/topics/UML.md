# Diagramas UML

Os Diagramas de Linguagem de Modelagem Unificada (UML) são ferramentas essenciais para visualizar, especificar, construir e documentar os artefatos de um sistema de software. Eles fornecem uma representação gráfica da estrutura e do comportamento do sistema, facilitando a compreensão e a comunicação entre os membros da equipe.

## 1. Diagramas Estruturais

Os diagramas estruturais focam na visualização da estrutura estática do sistema, seus componentes e como eles se relacionam.

### 1.1. Diagrama de Estrutura do Projeto

Este diagrama ilustra a organização das principais pastas do projeto Space Invaders e a relação entre elas, oferecendo uma visão de alto nível da arquitetura do código.

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

    folder "Factories" {
        [AlienFactory]
        [WaveFactory]
        folder "Strategies" {
            [StandardFormationStrategy]
            [RandomFormationStrategy]
        }
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
Factories --> Models : cria
@enduml
```

### 1.2. Diagrama de Classes: Domínio Principal

Este diagrama detalha as classes centrais da jogabilidade, seus atributos e relacionamentos, fornecendo uma visão estrutural do modelo de domínio do jogo.

```plantuml
@startuml
skinparam classAttributeIconStyle hidden
title Diagrama de Classes: Domínio Principal

abstract class Actor {
    + X: double
    + Y: double
    + Health: int
    + SpritePath: string
    + CheckCollision(other: Actor): bool
}

class Player {
    + Score: int
    + Lives: int
    + CanShoot: bool
    + Shoot(): void
}

abstract class Alien {
    + ScoreValue: int
}

class Projectile {
    + Speed: int
    + Damage: int
    + Move(): void
}

class Shield {
    + MaxHealth: int
}

class Weapon {
    + Damage: int
    + FireRate: double
}

Actor <|-- Player
Actor <|-- Alien
Actor <|-- Projectile
Actor <|-- Shield

Player "1" -- "1" Weapon : has >
Player "1" -- "*" Projectile : shoots >
Alien "1" -- "1" Weapon : has >
Alien "1" -- "*" Projectile : shoots >
@enduml
```

### 1.3. Diagrama de Classes: Factories e Strategies

Este diagrama foca em como os objetos do jogo são criados, ilustrando o uso dos padrões de design Factory e Strategy.

```plantuml
@startuml
skinparam classAttributeIconStyle hidden
title Diagrama de Classes: Factories e Strategies

class AlienFactory {
    + CreateAlien(alienType: AlienType): Alien
}

class WaveFactory <<static>> {
    + GenerateWave(level: int): List<AlienType>
}

interface IWaveFormationStrategy {
    + CreateWave(level: int): List<AlienType>
}

class StandardFormationStrategy implements IWaveFormationStrategy
class RandomFormationStrategy implements IWaveFormationStrategy

note right of WaveFactory
  Usa uma estratégia (Standard ou Random)
  para gerar uma lista de 'AlienType'
  baseado no nível do jogo.
end note

abstract class "Alien" as Alien
enum "AlienType" as AlienType

AlienFactory ..> Alien : cria
WaveFactory .right.> IWaveFormationStrategy : usa
IWaveFormationStrategy <|.. StandardFormationStrategy
IWaveFormationStrategy <|.. RandomFormationStrategy
WaveFactory ..> AlienType
@enduml
```

### 1.4. Diagrama de Classes: Serviços e Persistência

Este diagrama ilustra como os serviços da aplicação interagem com a camada de persistência de dados e outros serviços externos.

```plantuml
@startuml
skinparam classAttributeIconStyle hidden
title Diagrama de Classes: Serviços e Persistência

class SpaceInvadersDbContext {
    + Players: DbSet<Player>
    + Scores: DbSet<Score>
    + OnModelCreating(modelBuilder: ModelBuilder): void
}

class Score {
    + PlayerScore: int
    + DateAchieved: DateTime
}

class Player {
    + Name: string
}

interface IScoreService {
  + AddScoreAsync(score: Score): Task<Score>
  + GetTopScoresAsync(count: int): Task<List<Score>>
}

class ScoreService implements IScoreService

interface ISoundService {
  + Volume: float
  + PlaySound(soundPath: string): void
}

class SoundService implements ISoundService

Player "1" -- "*" Score : tem >
ScoreService ..> SpaceInvadersDbContext : acessa
ScoreService ..> Score : manipula
SoundService ..> "NAudio" : usa
@enduml
```

### 1.5. Diagrama de Componentes

Este diagrama ilustra a estrutura dos componentes de software do projeto e suas dependências, fornecendo uma visão de alto nível de como as diferentes partes do sistema se interligam.

```plantuml
@startuml
skinparam componentStyle rectangle

component "SpaceInvaders.UI" as UI {
  [Presentation Layer]
}

component "SpaceInvaders.Application" as App {
  [ViewModels]
  [Models]
  [Services]
  [Factories]
  [Constants]
  [Assets]
}

component "SpaceInvaders.Data" as Data {
  [DbContext]
  [Migrations]
}

component "PostgreSQL DB" as DB
component "NAudio Library" as NAudio
component "mpg123 (Linux Audio Player)" as Mpg123

UI --> App : uses
App --> Data : accesses
Data --> DB : connects to
App --> NAudio : uses
App --> Mpg123 : invokes (Linux)

@enduml
```

## 2. Diagramas Comportamentais

Os diagramas comportamentais focam na visualização da dinâmica do sistema, mostrando como os objetos interagem e como o sistema responde a eventos.

### 2.1. Diagrama de Sequência: Jogador Atira em Alienígena e Pontuação é Atualizada

Este diagrama ilustra a sequência de interações quando o jogador dispara um projétil que atinge um alienígena, resultando na atualização da pontuação.

```plantuml
@startuml
actor Player
participant "Game Logic" as GL
participant Projectile
participant Alien
participant ScoreService
participant SpaceInvadersDbContext as DB

Player -> GL : Disparar Laser
activate GL
GL -> Projectile : Criar(laser)
activate Projectile
Projectile --> GL : Laser Criado
deactivate Projectile

loop Game Loop
  GL -> Projectile : Mover()
  activate Projectile
  Projectile --> GL : Posição Atualizada
  deactivate Projectile

  GL -> Projectile : CheckCollision(Alien)
  activate Projectile
  Projectile -> Alien : CheckCollision(Projectile)
  activate Alien
  Alien --> Projectile : Colisão Detectada (true)
  deactivate Alien
  Projectile --> GL : Colisão Detectada (true)
  deactivate Projectile

  alt Colisão com Alienígena
    GL -> Alien : TakeDamage(Projectile.Damage)
    activate Alien
    Alien --> GL : Alienígena Destruído
    deactivate Alien

    GL -> ScoreService : AddScoreAsync(scoreValue)
    activate ScoreService
    ScoreService -> DB : Add(score)
    activate DB
    DB --> ScoreService : Score Adicionado
    deactivate DB
    ScoreService --> GL : Pontuação Atualizada
    deactivate ScoreService
  end
end

deactivate GL
@enduml
```

### 2.2. Diagrama de Sequência: Fim de Jogo e Salvamento de Pontuação

Este diagrama ilustra a sequência de interações que ocorrem quando o jogo termina e o jogador opta por salvar sua pontuação.

```plantuml
@startuml
actor User
participant "Game Logic" as GL
participant "GameOverViewModel" as GOVM
participant "PlayerService" as PS
participant "ScoreService" as SS
participant "SpaceInvadersDbContext" as DB

GL -> GOVM : Navegar para Tela de Fim de Jogo
activate GOVM

User -> GOVM : Inserir Nome do Jogador
User -> GOVM : Clicar em "Salvar Pontuação"

GOVM -> PS : AddPlayerAsync(player) ou UpdatePlayerAsync(player)
activate PS
PS -> DB : Adicionar/Atualizar Jogador
activate DB
DB --> PS : Jogador Salvo
deactivate DB
PS --> GOVM : Jogador Processado
deactivate PS

GOVM -> SS : AddScoreAsync(new Score)
activate SS
SS -> DB : Add(score)
activate DB
DB --> SS : Score Adicionado
deactivate DB
SS --> GOVM : Pontuação Salva
deactivate SS

GOVM --> User : Exibir Mensagem de Confirmação
deactivate GOVM
@enduml
```

### 2.3. Diagrama de Casos de Uso

Este diagrama descreve as funcionalidades do sistema do ponto de vista do usuário, mostrando como os atores interagem com o sistema.

```plantuml
@startuml
left to right direction
actor Jogador
actor Administrador

rectangle "Sistema Space Invaders" {
  usecase "Iniciar Jogo" as UC1
  usecase "Mover Nave" as UC2
  usecase "Disparar Laser" as UC3
  usecase "Destruir Alienígena" as UC4
  usecase "Atualizar Pontuação" as UC5
  usecase "Gerenciar Vidas" as UC6
  usecase "Exibir Tela de Fim de Jogo" as UC7
  usecase "Salvar Pontuação" as UC8
  usecase "Visualizar Placares" as UC9
  usecase "Visualizar Controles" as UC10
  usecase "Gerenciar Alienígenas" as UC11
  usecase "Gerenciar Barreiras" as UC12
  usecase "Reproduzir Efeitos Sonoros" as UC13
  usecase "Sair do Jogo" as UC14
}

Jogador --> UC1
Jogador --> UC2
Jogador --> UC3
Jogador --> UC4
Jogador --> UC5
Jogador --> UC6
Jogador --> UC7
Jogador --> UC8
Jogador --> UC9
Jogador --> UC10
Jogador --> UC13
Jogador --> UC14

UC4 ..> UC5 : include
UC6 ..> UC7 : include
UC8 ..> UC9 : include

Administrador --> UC11
Administrador --> UC12

@enduml
```

### 2.4. Diagrama de Atividades: Fluxo Principal do Jogo

Este diagrama representa o fluxo principal de atividades do jogo, desde o início da partida até o seu término.

```plantuml
@startuml
start

:Exibir Tela Inicial;

switch (Opção selecionada)
case (Iniciar Jogo)
  :Inicializar Jogo;
  :Carregar Assets;
  :Gerar Primeira Onda de Alienígenas
(usando WaveFactory);
  :Exibir Tela de Jogo;

  while (Jogo Ativo) is (true)
    :Processar Input do Jogador;
    :Atualizar Posição da Nave;
    :Gerenciar Projéteis do Jogador;
    :Gerenciar Alienígenas (Movimento, Disparo);
    :Gerenciar Colisões (Nave, Projéteis, Alienígenas, Barreiras);
    :Atualizar Pontuação e Vidas;

    if (Todos os alienígenas foram destruídos) then (sim)
        :Avançar Nível;
        :Gerar Nova Onda de Alienígenas
(usando WaveFactory);
    endif

    if (Condição de Fim de Jogo) then (true)
      break
    endif
  end while (false)

  :Exibir Tela de Fim de Jogo;
  switch (Opção selecionada na Tela de Fim de Jogo)
  case (Salvar Pontuação)
    :Solicitar Nome do Jogador;
    :Salvar Pontuação no Banco de Dados;
  case (Jogar Novamente)
    :Reiniciar Jogo;
  case (Voltar ao Menu Principal)
    :Retornar à Tela Inicial;
  endswitch
case (Visualizar Placares)
  :Exibir Tela de Placares;
  :Retornar à Tela Inicial;
case (Visualizar Controles)
  :Exibir Tela de Controles;
  :Retornar à Tela Inicial;
case (Sair do Jogo)
  :Encerrar Aplicação;
endswitch

stop
@enduml
```

