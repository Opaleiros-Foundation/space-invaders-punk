# Product Backlog

Este documento detalha o Product Backlog do projeto de recriação do Space Invaders, apresentado em formato de User Stories. Cada User Story inclui sua descrição detalhada, critérios de aceitação, labels de categorização e a release à qual pertence.

## User Stories

### 1. Tela Inicial e Fluxo do Jogo

*   **User Story:** Como um **jogador**, eu quero **ver uma tela inicial clara** para que eu possa **escolher entre iniciar um novo jogo, ver placares ou consultar os controles**.
    *   **Descrição:** A tela inicial deve ser a primeira interface apresentada ao usuário ao iniciar o jogo. Ela deve conter botões ou opções claras para iniciar uma nova partida, acessar a tela de placares e visualizar as instruções de controle do jogo. O design deve ser intuitivo e fácil de navegar.
    *   **Critérios de Aceitação:**
        *   [ ] Ao iniciar o jogo, a tela inicial é exibida automaticamente.
        *   [ ] A tela inicial contém um botão ou opção claramente identificada para "Iniciar Novo Jogo".
        *   [ ] A tela inicial contém um botão ou opção claramente identificada para "Ver Placares".
        *   [ ] A tela inicial contém um botão ou opção claramente identificada para "Ver Controles".
        *   [ ] Clicar em "Iniciar Novo Jogo" inicia uma nova partida (mesmo que o jogo ainda não tenha funcionalidade completa).
        *   [ ] Clicar em "Ver Placares" leva à tela de placares (mesmo que vazia).
        *   [ ] Clicar em "Ver Controles" leva à tela de instruções de controle.
        *   [ ] O layout da tela inicial é limpo e fácil de entender.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::UI`, `Workflow::To Do`
    *   **(Release 1)**

*   **User Story:** Como um **jogador**, eu quero **ver uma tela de fim de jogo** para que eu possa **salvar minha pontuação, jogar novamente ou retornar ao menu principal**.
    *   **Descrição:** Após o jogo terminar (seja por perda de vidas ou alienígenas alcançando a base), uma tela de fim de jogo deve ser exibida. Esta tela deve permitir ao jogador registrar sua pontuação, iniciar uma nova partida ou voltar para a tela inicial.
    *   **Critérios de Aceitação:**
        *   [ ] Ao final do jogo, a tela de fim de jogo é exibida.
        *   [ ] A tela de fim de jogo permite ao jogador inserir um apelido para salvar a pontuação.
        *   [ ] A tela de fim de jogo contém uma opção para "Jogar Novamente".
        *   [ ] A tela de fim de jogo contém uma opção para "Retornar ao Menu Principal".
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::UI`, `Workflow::To Do`
    *   **(Future Release)**

*   **User Story:** Como um **jogador**, eu quero **que o jogo termine automaticamente** para que eu saiba **quando perdi todas as vidas ou quando os alienígenas alcançaram a base**.
    *   **Descrição:** O jogo deve monitorar continuamente as condições de derrota. Assim que uma das condições for satisfeita, o jogo deve parar e transicionar para a tela de fim de jogo.
    *   **Critérios de Aceitação:**
        *   [ ] O jogo termina quando o jogador perde todas as vidas.
        *   [ ] O jogo termina quando o bloco de alienígenas atinge a linha inferior da tela.
        *   [ ] Ao término do jogo, a tela de fim de jogo é acionada.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

### 2. Jogabilidade Principal (Nave do Jogador)

*   **User Story:** Como um **jogador**, eu quero **mover minha nave horizontalmente** para que eu possa **desviar de ataques e posicionar-me para atirar nos inimigos**.
    *   **Descrição:** A nave do jogador deve ser controlável horizontalmente (esquerda e direita) através de inputs do teclado (ex: setas ou A/D). A movimentação deve ser fluida e responsiva, permitindo ao jogador desviar de projéteis inimigos e alinhar-se com os alvos.
    *   **Critérios de Aceitação:**
        *   [ ] A nave do jogador se move para a esquerda ao pressionar a tecla designada (ex: seta esquerda ou 'A').
        *   [ ] A nave do jogador se move para a direita ao pressionar a tecla designada (ex: seta direita ou 'D').
        *   [ ] A nave do jogador não pode sair dos limites horizontais da tela (paredes invisíveis).
        *   [ ] A movimentação da nave é suave e sem travamentos.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Release 1)**

*   **User Story:** Como um **jogador**, eu quero **disparar lasers da minha nave** para que eu possa **destruir os alienígenas**.
    *   **Descrição:** O jogador deve ser capaz de disparar um projétil (laser) verticalmente para cima a partir da nave, utilizando um input do teclado (ex: barra de espaço).
    *   **Critérios de Aceitação:**
        *   [ ] Ao pressionar a tecla de disparo, um laser é gerado na posição da nave e se move para cima.
        *   [ ] O laser desaparece ao atingir o topo da tela ou um inimigo/barreira.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Release 1)**

*   **User Story:** Como um **jogador**, eu quero **que meu laser recarregue após cada disparo** para que eu possa **gerenciar meus ataques de forma estratégica**.
    *   **Descrição:** O jogador só deve ser capaz de disparar um novo laser após o laser anterior ter saído da tela ou atingido algo.
    *   **Critérios de Aceitação:**
        *   [ ] O jogador não pode disparar um novo laser enquanto um laser anterior estiver ativo na tela.
        *   [ ] Um novo laser pode ser disparado assim que o laser anterior desaparecer.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

### 3. Inimigos e Interação

*   **User Story:** Como um **jogador**, eu quero **ver os alienígenas aparecerem na tela** para que eu saiba **quais inimigos preciso destruir**.
    *   **Descrição:** Múltiplas ondas de alienígenas devem ser posicionadas na parte superior da tela. Para a Release 1, eles podem permanecer estáticos.
    *   **Critérios de Aceitação:**
        *   [ ] Um conjunto de alienígenas (diferentes tipos) é exibido na parte superior da tela ao iniciar o jogo.
        *   [ ] Os alienígenas são visíveis e distinguíveis.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Release 1)**

*   **User Story:** Como um **jogador**, eu quero **destruir os alienígenas com um único tiro** para que eu possa **limpar a tela e avançar no jogo**.
    *   **Descrição:** Quando um laser do jogador atinge um alienígena, o alienígena deve ser removido da tela.
    *   **Critérios de Aceitação:**
        *   [ ] Um alienígena desaparece da tela ao ser atingido por um laser do jogador.
        *   [ ] O laser do jogador desaparece ao atingir um alienígena.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Release 1)**

*   **User Story:** Como um **jogador**, eu quero **que os alienígenas se movam em bloco** para que o jogo **simule o comportamento clássico do Space Invaders**.
    *   **Descrição:** Os alienígenas devem se mover em um bloco coeso, da esquerda para a direita, descendo uma linha e invertendo a direção ao atingir a borda da tela.
    *   **Critérios de Aceitação:**
        *   [ ] Os alienígenas se movem horizontalmente em conjunto.
        *   [ ] Ao atingir a borda da tela, o bloco de alienígenas desce uma linha.
        *   [ ] A direção do movimento horizontal do bloco é invertida após descer.
    *   **Labels:** `Type::Feature`, `Priority::Medium`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

*   **User Story:** Como um **jogador**, eu quero **que a dificuldade do jogo aumente gradualmente** para que eu **seja desafiado à medida que avanço**.
    *   **Descrição:** A velocidade de movimento dos alienígenas e a frequência de seus disparos devem aumentar a cada vez que o bloco desce.
    *   **Critérios de Aceitação:**
        *   [ ] A velocidade de movimento dos alienígenas aumenta após cada descida do bloco.
        *   [ ] A frequência de disparo dos alienígenas aumenta após cada descida do bloco.
    *   **Labels:** `Type::Feature`, `Priority::Medium`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

*   **User Story:** Como um **jogador**, eu quero **que apenas certos alienígenas atirem** para que eu possa **identificar as ameaças principais**.
    *   **Descrição:** Apenas o alienígena de 40 pontos (Tipo 3) deve ser capaz de disparar projéteis.
    *   **Critérios de Aceitação:**
        *   [ ] Somente alienígenas do Tipo 3 disparam projéteis.
        *   [ ] Projéteis inimigos se movem para baixo.
    *   **Labels:** `Type::Feature`, `Priority::Medium`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

*   **User Story:** Como um **jogador**, eu quero **ver um alienígena especial aparecer periodicamente** para que eu possa **ter a chance de ganhar pontos extras**.
    *   **Descrição:** Um alienígena vermelho especial deve aparecer periodicamente no topo da tela, movendo-se horizontalmente e oferecendo uma pontuação variável ao ser destruído.
    *   **Critérios de Aceitação:**
        *   [ ] Um alienígena especial aparece aleatoriamente no topo da tela.
        *   [ ] O alienígena especial se move horizontalmente.
        *   [ ] Destruir o alienígena especial concede uma pontuação variável.
    *   **Labels:** `Type::Feature`, `Priority::Medium`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

*   **User Story:** Como um **jogador**, eu quero **que os alienígenas não colidam com as barreiras** para que a **mecânica de proteção funcione corretamente**.
    *   **Descrição:** Os alienígenas devem passar por cima ou desviar das barreiras de proteção, sem interagir com elas diretamente.
    *   **Critérios de Aceitação:**
        *   [ ] Alienígenas não colidem com as barreiras de proteção.
        *   [ ] Alienígenas não são impedidos de se mover pelas barreiras.
    *   **Labels:** `Type::Feature`, `Priority::Low`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

*   **User Story:** Como um **jogador**, eu quero **que novas ondas de inimigos apareçam** para que o jogo **continue após eu destruir uma onda completa**.
    *   **Descrição:** Após todos os alienígenas de uma onda serem destruídos, uma nova onda deve ser gerada, potencialmente com dificuldade aumentada.
    *   **Critérios de Aceitação:**
        *   [ ] Uma nova onda de alienígenas é gerada após a destruição da onda anterior.
        *   [ ] A nova onda pode apresentar maior dificuldade (ex: mais alienígenas, maior velocidade inicial).
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

### 4. Barreiras de Proteção

*   **User Story:** Como um **jogador**, eu quero **ter barreiras de proteção na tela** para que eu possa **me proteger dos tiros inimigos**.
    *   **Descrição:** Haverá 4 blocos de proteção (escudos) localizados acima da nave do jogador. Eles devem se degradar visualmente ao serem atingidos e desaparecer após sofrerem uma certa quantidade de dano.
    *   **Critérios de Aceitação:**
        *   [ ] 4 barreiras de proteção são exibidas na tela.
        *   [ ] As barreiras bloqueiam projéteis (do jogador e inimigos).
        *   [ ] As barreiras mudam de aparência (degradam) ao serem atingidas.
        *   [ ] As barreiras desaparecem após sofrerem dano suficiente.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Release 1)**

### 5. Pontuação e Vidas

*   **User Story:** Como um **jogador**, eu quero **ver minha pontuação atualizada na tela** para que eu possa **acompanhar meu desempenho**.
    *   **Descrição:** A pontuação atual do jogador deve ser exibida de forma clara e visível no canto superior esquerdo da tela durante o jogo.
    *   **Critérios de Aceitação:**
        *   [ ] A pontuação é exibida no canto superior esquerdo da tela.
        *   [ ] A pontuação é atualizada em tempo real.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::UI`, `Workflow::To Do`
    *   **(Release 1)**

*   **User Story:** Como um **jogador**, eu quero **ganhar pontos ao destruir inimigos** para que eu possa **aumentar minha pontuação total**.
    *   **Descrição:** A pontuação do jogador deve ser incrementada ao destruir um alienígena, com valores diferentes para cada tipo de inimigo.
    *   **Critérios de Aceitação:**
        *   [ ] Destruir um alienígena Tipo 1 adiciona 10 pontos.
        *   [ ] Destruir um alienígena Tipo 2 adiciona 20 pontos.
        *   [ ] Destruir um alienígena Tipo 3 adiciona 40 pontos.
        *   [ ] Destruir o alienígena especial adiciona uma pontuação variável (50-250).
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Release 1)**

*   **User Story:** Como um **jogador**, eu quero **ter um sistema de vidas** para que eu possa **saber quantas vezes posso ser atingido antes do fim do jogo**.
    *   **Descrição:** O jogador começa com um número fixo de vidas (ex: 3) e perde uma vida ao ser atingido por um projétil inimigo ou colidir com um alienígena.
    *   **Critérios de Aceitação:**
        *   [ ] O jogador começa com 3 vidas.
        *   [ ] Uma vida é perdida quando o jogador é atingido por um projétil inimigo.
        *   [ ] Uma vida é perdida quando o jogador colide com um alienígena.
        *   [ ] O número de vidas restantes é exibido na tela.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

*   **User Story:** Como um **jogador**, eu quero **ganhar uma vida extra ao atingir uma certa pontuação** para que eu **seja recompensado por um bom desempenho**.
    *   **Descrição:** O jogador deve ganhar uma vida extra a cada 1000 pontos, até um máximo de 6 vidas.
    *   **Critérios de Aceitação:**
        *   [ ] Uma vida extra é concedida a cada 1000 pontos.
        *   [ ] O número máximo de vidas é 6.
    *   **Labels:** `Type::Feature`, `Priority::Medium`, `Component::Gameplay`, `Workflow::To Do`
    *   **(Future Release)**

### 6. Áudio e Persistência

*   **User Story:** Como um **jogador**, eu quero **ouvir efeitos sonoros para as ações do jogo** para que a **experiência de jogo seja mais imersiva e responsiva**.
    *   **Descrição:** Cada ação significativa no jogo (disparo do jogador, destruição de inimigo, morte do jogador, etc.) deve ser acompanhada por um efeito sonoro correspondente.
    *   **Critérios de Aceitação:**
        *   [ ] Um som é reproduzido ao disparar um laser.
        *   [ ] Um som é reproduzido ao destruir um inimigo.
        *   [ ] Um som é reproduzido ao ser atingido.
        *   [ ] Outros sons relevantes (ex: fim de jogo) são reproduzidos.
    *   **Labels:** `Type::Feature`, `Priority::Medium`, `Component::Audio`, `Workflow::To Do`
    *   **(Release 1)**

*   **User Story:** Como um **jogador**, eu quero **que minhas pontuações mais altas sejam salvas** para que eu possa **competir comigo mesmo e com outros (hipoteticamente)**.
    *   **Descrição:** As informações do placar (pontuação e apelido) devem ser salvas em um arquivo de texto para persistência entre as sessões de jogo.
    *   **Critérios de Aceitação:**
        *   [ ] As pontuações são salvas em um arquivo de texto.
        *   [ ] As pontuações salvas podem ser carregadas e exibidas na tela de placares.
        *   [ ] O arquivo de placares é atualizado corretamente com novas pontuações.
    *   **Labels:** `Type::Feature`, `Priority::High`, `Component::Persistence`, `Workflow::To Do`
    *   **(Future Release)**
