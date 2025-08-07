# Plano de Testes

Este documento descreve o plano de testes para o projeto Space Invaders, delineando as estratégias, tipos de testes e abordagens para garantir a qualidade e o funcionamento correto da aplicação.

## 1. Estratégia de Testes

A estratégia de testes visa cobrir as principais funcionalidades do jogo, desde a lógica de negócio até a interação com a interface do usuário. Dada a natureza do projeto, uma combinação de testes manuais e, quando aplicável, testes automatizados será utilizada.

## 2. Tipos de Testes

### 2.1. Testes Funcionais

*   **Objetivo**: Verificar se as funcionalidades do jogo operam de acordo com os requisitos definidos no Product Backlog e nos Requisitos de Engenharia.
*   **Cobertura**: Inclui o movimento do jogador, disparo de lasers, comportamento dos alienígenas, sistema de pontuação, gerenciamento de vidas, telas de menu, fim de jogo e placares.
*   **Abordagem**: Principalmente testes manuais, simulando a interação do usuário com o jogo.

### 2.2. Testes de Unidade

*   **Objetivo**: Isolar e testar componentes individuais do código (métodos, classes) para garantir que funcionem conforme o esperado.
*   **Cobertura**: Lógica de negócio em `Services` (ex: `ScoreService`, `PlayerService`), modelos (`Models`), e lógica de apresentação em `ViewModels`.
*   **Ferramentas Potenciais**: Frameworks de teste como NUnit ou xUnit, e bibliotecas de mocking como Moq (se a complexidade justificar).
*   **Abordagem**: Testes automatizados para garantir a correção da lógica interna.

### 2.3. Testes de Integração

*   **Objetivo**: Verificar a interação entre diferentes módulos ou componentes do sistema.
*   **Cobertura**: Integração entre `Services` e o banco de dados (Entity Framework Core com PostgreSQL), interação entre `ViewModels` e `Models`, e o fluxo de dados entre as camadas.
*   **Abordagem**: Testes automatizados para validar a comunicação entre os componentes.

### 2.4. Testes de Interface do Usuário (UI)

*   **Objetivo**: Assegurar que a interface do usuário se comporta corretamente, exibe os elementos visuais como esperado e responde às interações do usuário.
*   **Cobertura**: Navegação entre telas, exibição de sprites, atualização de HUD (pontuação, vidas), e responsividade dos controles.
*   **Abordagem**: Principalmente testes manuais, com validação visual e de interação.

### 2.5. Testes de Áudio

*   **Objetivo**: Verificar se os efeitos sonoros e músicas são reproduzidos corretamente em momentos apropriados do jogo.
*   **Cobertura**: Disparos, explosões, sons de colisão, música de fundo, etc.
*   **Abordagem**: Testes manuais durante a jogabilidade.

## 3. Casos de Teste (Exemplos)

Os casos de teste detalhados seriam desenvolvidos com base nos requisitos funcionais e não funcionais. Alguns exemplos incluem:

*   **RF01 - Controle da Nave**: Testar o movimento da nave para a esquerda e direita, e verificar se ela não ultrapassa os limites da tela.
*   **RF02 - Disparo de Lasers**: Testar o disparo do laser, sua trajetória e o desaparecimento ao atingir um alvo ou o topo da tela.
*   **RF03 - Pontuação por Inimigo**: Destruir diferentes tipos de alienígenas e verificar se a pontuação é incrementada corretamente.
*   **RF07 - Condição de Fim de Jogo**: Simular a perda de todas as vidas e o alcance dos alienígenas à base para verificar a transição para a tela de fim de jogo.

## 4. Ambiente de Testes

Os testes serão realizados no ambiente de desenvolvimento local, garantindo que o banco de dados PostgreSQL (via Docker) esteja em execução e que todas as dependências do projeto estejam instaladas e configuradas corretamente.
