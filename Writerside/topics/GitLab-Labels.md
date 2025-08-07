# Labels do GitLab

Este documento descreve as labels utilizadas no GitLab para organizar e categorizar as issues e merge requests do projeto Capstone Space Invaders. Mesmo sendo um projeto individual, a utilização dessas labels ajuda na organização e no acompanhamento do progresso.

## Categorias de Labels

### 1. Tipo de Trabalho (Type)
Para classificar a natureza da tarefa.

| Title (Nome da Label) | Description (Descrição) | Color (Hex Code) |
| :-------------------- | :---------------------- | :--------------- |
| `Type::Feature` | Nova funcionalidade a ser implementada. | `#0066CC` (Azul) |
| `Type::Bug` | Defeito ou comportamento inesperado que precisa ser corrigido. | `#CC0000` (Vermelho Escuro) |
| `Type::Enhancement` | Melhoria em uma funcionalidade existente. | `#FF9900` (Laranja) |
| `Type::Refactor` | Reestruturação de código sem alteração de funcionalidade. | `#9933CC` (Roxo) |
| `Type::Documentation` | Tarefa relacionada à criação ou atualização de documentação. | `#009933` (Verde Escuro) |
| `Type::Technical Debt` | Dívida técnica a ser resolvida. | `#666666` (Cinza Escuro) |

### 2. Status do Fluxo (Workflow)
Para indicar o progresso da tarefa.

| Title (Nome da Label) | Description (Descrição) | Color (Hex Code) |
| :-------------------- | :---------------------- | :--------------- |
| `Workflow::To Do` | Tarefa pronta para ser iniciada. | `#CCCCCC` (Cinza Claro) |
| `Workflow::In Progress` | Tarefa em desenvolvimento ativo. | `#FFFF00` (Amarelo) |
| `Workflow::Done` | Tarefa concluída e verificada. | `#00CC00` (Verde Claro) |

### 3. Prioridade (Priority)
Para indicar a urgência ou importância da tarefa.

| Title (Nome da Label) | Description (Descrição) | Color (Hex Code) |
| :-------------------- | :---------------------- | :--------------- |
| `Priority::High` | Tarefa de alta prioridade, deve ser feita o mais rápido possível. | `#FF0000` (Vermelho) |
| `Priority::Medium` | Tarefa de prioridade média, importante mas não urgente. | `#FFCC00` (Amarelo Alaranjado) |
| `Priority::Low` | Tarefa de baixa prioridade, pode ser feita quando houver tempo. | `#3399FF` (Azul Claro) |

### 4. Componente/Área (Component)
Para identificar a parte do sistema afetada.

| Title (Nome da Label) | Description (Descrição) | Color (Hex Code) |
| :-------------------- | :---------------------- | :--------------- |
| `Component::Gameplay` | Relacionado à lógica central do jogo e mecânicas. | `#663399` (Roxo Escuro) |
| `Component::UI` | Relacionado à interface do usuário (menus, HUD, elementos visuais). | `#FF66B2` (Rosa) |
| `Component::Audio` | Relacionado a sons, música e efeitos sonoros. | `#00CC99` (Verde Água) |
| `Component::Persistence` | Relacionado a salvamento, carregamento de dados e placares. | `#996633` (Marrom) |
| `Component::Build/Deploy` | Relacionado a configurações de build, scripts de automação, etc. | `#333333` (Preto) |
