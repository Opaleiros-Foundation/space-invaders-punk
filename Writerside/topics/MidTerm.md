# Apresentação Parcial (MidTerm)

**Projeto:** Versão Inicial do Jogo "Space Invaders"

**Data da Apresentação:** Semana 4 do Módulo

## Escopo da Entrega

Para a avaliação parcial, o projeto deve implementar um subconjunto dos requisitos funcionais, focando na mecânica central do jogo. A tabela abaixo detalha o que precisa ser entregue.

| ID do Requisito | Funcionalidade | Detalhes da Entrega Parcial |
| --- | --- | --- |
| **RF01** | Controle da Nave | A nave do jogador deve se mover horizontalmente. |
| **RF02** | Disparo de Lasers | O jogador deve ser capaz de disparar projéteis para cima. |
| **RF04** | Geração de Inimigos | Os alienígenas devem ser exibidos na tela, mas podem permanecer estáticos (sem movimento). |
| **RF08** | Barreiras de Proteção | As barreiras devem ser exibidas e podem ser destruídas pelos tiros do jogador. |
| **RF13** | Dano em Inimigos | Alienígenas devem ser removidos ao serem atingidos por um tiro. |
| **RF16** | Exibição de Pontuação | A pontuação deve ser visível na tela. |
| **RF17** | Incremento de Pontuação | A pontuação deve ser atualizada sempre que um inimigo for destruído. |
| **RF19** | Tela Inicial | O jogo deve apresentar uma tela inicial simples. |
| **RF20** | Efeitos Sonoros | Deve haver sons para as ações principais (tiro, destruição). |

### Lógica de Jogo Simplificada

- **Condição de Vitória:** Para esta entrega, o jogo termina quando o jogador atingir **500 pontos**.
- **Armazenamento:** A pontuação é mantida apenas em memória durante a sessão de jogo.
- **Inimigos:** O movimento complexo dos inimigos (descida, aumento de velocidade) não é necessário nesta fase.
