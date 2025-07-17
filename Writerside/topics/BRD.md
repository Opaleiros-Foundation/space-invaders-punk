# BRD

## 1. Introdução

### 1.1. Objetivo do Projeto
O objetivo deste projeto é desenvolver uma recriação do clássico jogo "Space Invaders" como uma aplicação de desktop. O projeto servirá como avaliação final para a disciplina de Programação 3, focando na aplicação prática de conceitos de Programação Orientada a Objetos (OOP), Estruturas de Dados e manipulação de eventos em C#.

### 1.2. Escopo
O projeto será desenvolvido individualmente, utilizando C# e a Uno Platform, sem o auxílio de qualquer motor de jogo (game engine). A interface e a lógica do jogo serão construídas do zero, com entregas parciais e uma entrega final.

---

## 2. Requisitos Funcionais

### 2.1. Tela Inicial
- O jogo deve apresentar uma tela inicial com as seguintes opções:
    - **Iniciar um novo jogo:** Começa uma nova partida.
    - **Ver a tabela com os placares:** Exibe a lista de pontuações salvas.
    - **Ver os controles do jogo:** Mostra as instruções de como jogar.

### 2.2. Jogabilidade

#### 2.2.1. Nave do Jogador
- O jogador controla uma nave localizada na parte inferior da tela.
- A nave pode se mover horizontalmente (esquerda e direita).
- A nave pode disparar um projétil (laser) para cima.
- O jogador só pode ter um projétil ativo na tela por vez.

#### 2.2.2. Inimigos (Alienígenas)
- Múltiplas ondas de alienígenas são posicionadas na parte superior da tela.
- Os alienígenas se movem em um bloco coeso, da esquerda para a direita.
- Ao atingir a borda da tela, o bloco de alienígenas desce uma linha e inverte sua direção de movimento.
- A velocidade de movimento dos alienígenas e a frequência de seus disparos aumentam a cada vez que o bloco desce.
- Apenas o alienígena de 40 pontos pode atirar.
- Um alienígena especial (vermelho) aparece periodicamente no topo da tela, movendo-se horizontalmente e oferecendo uma pontuação variável.

#### 2.2.3. Barreiras de Proteção
- Haverá 4 blocos de proteção (escudos) localizados acima da nave do jogador.
- Os escudos servem para bloquear tiros, tanto do jogador quanto dos alienígenas.
- Os escudos se degradam visualmente (mudando de cor de branco para chumbo) ao serem atingidos e desaparecem após sofrerem uma certa quantidade de dano.

#### 2.2.4. Sistema de Pontuação e Vidas
- O jogador começa com um número fixo de vidas.
- A pontuação é incrementada ao destruir um alienígena, com valores diferentes para cada tipo:
    - Alienígena tipo 1: 10 pontos
    - Alienígena tipo 2: 20 pontos
    - Alienígena tipo 3: 40 pontos
    - Alienígena especial: Pontuação misteri00 pontos, o jogador ganha uma vida extra, até um máximo de 6 vidas.
- A pontuação atual é sempre visível no canto superior esquerdo da tela.

### 2.3. Fim de Jogo
- O jogo termina se osa (???)
- A cada 10uma das seguintes condições for atendida:
    - O jogador perde todas as suas vidas.
    - O bloco de alienígenas alcança a parte inferior da tela (posição do jogador).
- Após o fim do jogo, o jogador tem a opção de:
    - Salvar sua pontuação, associando-a a um apelido.
    - Voltar a jogar.
    - Retornar à tela inicial.

### 2.4. Persistência de Dados
- As informações do placar (pontuação e apelido) devem ser salvas em um arquivo de texto.

---

## 3. Requisitos Não-Funcionais

### 3.1. Plataforma
- O jogo deve ser uma aplicação de desktop, construída com a Uno Platform para garantir a portabilidade.

### 3.2. Tecnologia
- A linguagem de programação será C#.
- A interface do usuário (UI) será definida usando XAML.
- Nenhuma game engine externa (ex: Unity, Godot) será utilizada.

### 3.3. Som
- Cada ação significativa no jogo (disparo, destruição de inimigo, morte do jogador, etc.) deve ser acompanhada por um efeito sonoro correspondente.

---

## 4. Fases do Projeto

### 4.1. Entrega Intermediária (Midterm - Semana 4)
- **Escopo Reduzido:**
    - Alienígenas não precisam se mover.
    - O jogador deve ser capaz de se mover e atirar.
    - Os blocos de proteção devem ser destrutíveis pelo jogador.
    - A pontuação deve ser atualizada ao destruir um alienígena.
    - O jogo termina quando o jogador atinge 500 pontos.
    - A pontuação é mantida apenas em memória (perdida ao fechar o jogo).
    - Deve haver uma tela inicial e sons para as ações.

### 4.2. Entrega Final
- Implementação de todos os requisitos funcionais e não-funcionais descritos neste documento.
