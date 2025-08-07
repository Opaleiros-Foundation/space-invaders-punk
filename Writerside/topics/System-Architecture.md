# Arquitetura do Sistema

A arquitetura do sistema do projeto Space Invaders foi projetada para ser modular, escalável e de fácil manutenção, seguindo princípios de design que promovem a separação de responsabilidades e a clareza do código. Esta seção oferece uma visão abrangente da estrutura técnica do jogo, das tecnologias empregadas e dos padrões de design que guiaram seu desenvolvimento.

## 1. Visão Geral

O sistema é construído sobre a **Uno Platform** e **C#**, utilizando **XAML** para a interface do usuário e **Entity Framework Core** com **PostgreSQL** para persistência de dados. A aplicação segue o padrão **Model-View-ViewModel (MVVM)**, que é fundamental para a organização e testabilidade do código.

## 2. Componentes da Arquitetura

Para uma compreensão mais aprofundada dos diferentes aspectos da arquitetura, consulte os seguintes documentos:

*   **Padrões de Design**: Detalha os padrões de design aplicados no projeto, como MVVM, Factory Method (implícito) e Singleton (implícito), explicando como eles contribuem para a estrutura do código.

*   **Diagramas UML**: Apresenta diagramas que visualizam a estrutura do projeto, incluindo o diagrama de pacotes (PlantUML) e discute outros tipos de diagramas UML relevantes para a modelagem do sistema.

*   **Modelo de Dados**: Descreve a estrutura das informações persistidas pelo jogo, com foco nas entidades principais (como `Score`) e no mecanismo de persistência (Entity Framework Core com PostgreSQL).

*   **Design de Interface**: Aborda os princípios de design da interface do usuário, as tecnologias de UI (XAML) e as telas principais do jogo, garantindo uma experiência intuitiva e fiel ao original.

*   **Pilha de Tecnologia**: Lista e descreve todas as tecnologias e ferramentas essenciais utilizadas no desenvolvimento do projeto, incluindo C#, Uno Platform, XAML, Entity Framework Core, PostgreSQL, NAudio e Git.

Esta estrutura arquitetural visa proporcionar um ambiente de desenvolvimento robusto e um produto final de alta qualidade.
