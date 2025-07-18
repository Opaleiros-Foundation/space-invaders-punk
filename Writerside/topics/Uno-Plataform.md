# Uno Plataform

A Uno Platform é uma plataforma de código aberto que permite a criação de aplicações multi-plataforma usando C# e XAML. Com ela, você pode desenvolver aplicações que rodam nativamente em WebAssembly, iOS, Android, macOS, Linux e Windows a partir de uma única base de código.

## Instalação e Setup

Para começar a desenvolver com Uno Platform, você precisará instalar algumas ferramentas e SDKs. Siga os passos abaixo para configurar seu ambiente de desenvolvimento.

### Pré-requisitos

Certifique-se de ter os seguintes pré-requisitos instalados em seu sistema:

*   **Visual Studio 2022** (Community, Professional ou Enterprise) com as seguintes cargas de trabalho:
    *   Desenvolvimento para desktop com .NET
    *   Desenvolvimento móvel com .NET
    *   Desenvolvimento multiplataforma de interface do usuário .NET
    *   Desenvolvimento Web ASP.NET e Web
*   **SDK do .NET** (geralmente incluído com o Visual Studio, mas pode ser baixado separadamente se necessário).
*   **Node.js** (necessário para o desenvolvimento WebAssembly).

### Instalação do Uno Platform Solution Templates

Os templates de solução da Uno Platform facilitam a criação de novos projetos. Você pode instalá-los via linha de comando:

```bash
dotnet new install Uno.Templates
```

### Criando um Novo Projeto Uno Platform

Após instalar os templates, você pode criar um novo projeto Uno Platform usando o Visual Studio ou a linha de comando.

#### Via Visual Studio

1.  Abra o Visual Studio 2022.
2.  Selecione "Criar um projeto".
3.  Pesquise por "Uno Platform" e escolha o template "Uno Platform App".
4.  Siga as instruções para configurar seu projeto (nome, localização, etc.).

#### Via Linha de Comando

Você pode criar um novo projeto Uno Platform a partir do terminal:

```bash
dotnet new unoapp -n MyUnoApp
```

Substitua `MyUnoApp` pelo nome desejado para o seu projeto.

### Executando o Projeto

Após criar o projeto, você pode executá-lo em diferentes plataformas. Por exemplo, para rodar a versão WebAssembly:

```bash
dotnet run -f net8.0-browser
```

Para rodar a versão Windows (se estiver no Windows):

```bash
dotnet run -f net8.0-windows
```

Para outras plataformas, você pode usar o Visual Studio para selecionar o target desejado e executar a aplicação.

## Recursos Adicionais

*   [Documentação Oficial Uno Platform](https://platform.uno/docs/)
*   [Tutoriais e Exemplos da Uno Platform](https://platform.uno/platform/)
*   [Comunidade Uno Platform (Discord, GitHub Discussions)](https://platform.uno/community/)
