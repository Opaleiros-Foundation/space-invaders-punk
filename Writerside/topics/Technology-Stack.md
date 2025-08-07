# Pilha de Tecnologia (Technology Stack)

A pilha de tecnologia do projeto Space Invaders foi cuidadosamente selecionada para garantir um desenvolvimento eficiente, performance adequada e a capacidade de construir uma aplicação de desktop moderna e multiplataforma. Abaixo estão os principais componentes da tecnologia stack:

## 1. Linguagem de Programação: C#

*   **Descrição**: C# é uma linguagem de programação moderna, orientada a objetos e desenvolvida pela Microsoft. É amplamente utilizada para construir uma vasta gama de aplicações, incluindo desktop, web, mobile e jogos.
*   **Motivo da Escolha**: Sua robustez, ecossistema maduro (.NET), forte tipagem e recursos avançados tornam-na ideal para o desenvolvimento de lógicas de jogo complexas e aplicações de alto desempenho.

## 2. Plataforma: Uno Platform

*   **Descrição**: Uno Platform é uma plataforma de código aberto que permite construir aplicações nativas para WebAssembly, iOS, Android, macOS, Linux e Windows a partir de uma única base de código C# e XAML.
*   **Motivo da Escolha**: Garante a portabilidade da aplicação de desktop para diferentes sistemas operacionais, além de oferecer a flexibilidade para futuras expansões para outras plataformas, se necessário.

## 3. Interface de Usuário (UI): XAML

*   **Descrição**: XAML (Extensible Application Markup Language) é uma linguagem de marcação declarativa utilizada para definir interfaces de usuário em aplicações baseadas em .NET, especialmente com tecnologias como WPF, UWP e Uno Platform.
*   **Motivo da Escolha**: Permite uma separação clara entre o design da interface e a lógica de negócio (seguindo o padrão MVVM), facilitando o desenvolvimento e a manutenção da UI.

## 4. Persistência de Dados: Entity Framework Core com PostgreSQL

*   **Entity Framework Core (EF Core)**:
    *   **Descrição**: É um ORM (Object-Relational Mapper) leve, extensível e multiplataforma da Microsoft. Ele permite que desenvolvedores .NET trabalhem com um banco de dados usando objetos .NET, eliminando a necessidade da maior parte do código de acesso a dados que eles geralmente precisariam escrever.
    *   **Motivo da Escolha**: Simplifica a interação com o banco de dados, permitindo o uso de objetos C# para gerenciar os placares, além de oferecer recursos como migrações para controle de versão do esquema do banco de dados.
*   **PostgreSQL**:
    *   **Descrição**: É um sistema de gerenciamento de banco de dados relacional de código aberto, robusto e altamente extensível, conhecido por sua confiabilidade e conformidade com padrões.
    *   **Motivo da Escolha**: Oferece um banco de dados confiável e performático para armazenar os placares do jogo, sendo uma escolha popular em ambientes de desenvolvimento e produção.

## 5. Áudio: NAudio

*   **Descrição**: NAudio é uma biblioteca de áudio de código aberto para .NET, que fornece funcionalidades para reprodução, gravação e processamento de áudio.
*   **Motivo da Escolha**: Permite a manipulação e reprodução de efeitos sonoros e músicas no jogo, contribuindo para uma experiência imersiva. A implementação considera a compatibilidade com diferentes sistemas operacionais, incluindo Linux (via `mpg123`).

## 6. Controle de Versão: Git

*   **Descrição**: Git é um sistema de controle de versão distribuído, amplamente utilizado para rastrear mudanças no código-fonte durante o desenvolvimento de software.
*   **Motivo da Escolha**: Facilita o gerenciamento do código, o histórico de alterações e a colaboração (mesmo em projetos individuais, ajuda na organização e no versionamento de entregas).
