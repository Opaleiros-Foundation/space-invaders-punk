# Design de Interface

O design da interface do usuário (UI) do projeto Space Invaders foi concebido para ser intuitivo, funcional e fiel à experiência clássica do jogo, ao mesmo tempo em que incorpora as capacidades da Uno Platform e XAML.

## 1. Princípios de Design

Os seguintes princípios guiaram o desenvolvimento da UI:

*   **Clareza**: As informações e opções devem ser apresentadas de forma clara e compreensível para o usuário.
*   **Simplicidade**: Evitar elementos desnecessários para manter o foco na jogabilidade e nas funcionalidades essenciais.
*   **Responsividade**: Embora seja uma aplicação desktop, a estrutura XAML permite uma adaptação flexível a diferentes resoluções de tela.
*   **Fidelidade ao Original**: Manter a estética e a sensação do jogo Space Invaders clássico, especialmente nos elementos visuais da jogabilidade.

## 2. Tecnologia de UI: XAML

A interface do usuário é definida declarativamente utilizando **XAML (Extensible Application Markup Language)**. O XAML permite uma separação limpa entre o design visual da UI e a lógica de negócio (implementada nos ViewModels), seguindo o padrão MVVM.

Todos os arquivos XAML que definem as telas e componentes da UI estão localizados na pasta `SpaceInvaders/Presentation`.

## 3. Telas Principais da Interface

O jogo é composto por diversas telas que guiam o usuário através da experiência:

### 3.1. Tela Inicial (`GameStartPage.xaml`)

*   **Propósito**: Ponto de entrada do jogo, oferecendo opções para o usuário.
*   **Elementos**: Título do jogo, botões para "Iniciar Jogo", "Placares" e "Controles".

### 3.2. Tela de Jogo (`MainPage.xaml`)

*   **Propósito**: Onde a ação principal do jogo acontece.
*   **Elementos**: Área de jogo com a nave do jogador, alienígenas, projéteis e barreiras. HUD (Head-Up Display) exibindo a pontuação atual e o número de vidas restantes.

### 3.3. Tela de Fim de Jogo (`GameOver.xaml`)

*   **Propósito**: Exibida após o término de uma partida.
*   **Elementos**: Mensagem de "Fim de Jogo", pontuação final do jogador, campo para inserir apelido e salvar a pontuação, botões para "Jogar Novamente" e "Voltar ao Menu Principal".

### 3.4. Tela de Placares (`ScorePage.xaml`)

*   **Propósito**: Exibir as pontuações mais altas salvas.
*   **Elementos**: Lista das pontuações, com apelido do jogador e pontuação. Botão para retornar ao menu principal.

### 3.5. Tela de Controles (`ControllersPage.xaml`)

*   **Propósito**: Informar o usuário sobre os comandos do jogo.
*   **Elementos**: Descrição dos controles de movimento e disparo da nave.

## 4. Estilos e Recursos Visuais

O projeto utiliza estilos e recursos visuais definidos em arquivos XAML separados (como `AppStyles.xaml` e `GameOverStyles.xaml` na pasta `SpaceInvaders/Styles`) para garantir consistência visual e facilitar a manutenção do design. Os assets visuais (sprites, backgrounds) estão organizados na pasta `SpaceInvaders/Assets`.