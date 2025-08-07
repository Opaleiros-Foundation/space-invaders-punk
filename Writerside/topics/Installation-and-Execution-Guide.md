# Guia de Instalação e Execução

Este guia detalha os passos necessários para configurar o ambiente de desenvolvimento, instalar as dependências e executar o projeto Space Invaders.

## 1. Pré-requisitos

Certifique-se de que os seguintes softwares estejam instalados em sua máquina:

*   **SDK do .NET 9.0 (ou superior)**: Essencial para compilar e executar aplicações .NET. Você pode baixá-lo em [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
*   **Docker Desktop (ou Docker Engine)**: Necessário para rodar o banco de dados PostgreSQL. Disponível em [docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop).
*   **Visual Studio 2022 (ou Visual Studio Code ou Rider)**: IDEs recomendadas para desenvolvimento C# e Uno Platform. Embora não sejam estritamente obrigatórias, facilitam muito o processo.
    *   **Visual Studio**: Certifique-se de ter as cargas de trabalho "Desenvolvimento para desktop com .NET" e "Desenvolvimento multiplataforma de interface do usuário .NET" instaladas.
*   **Templates do Uno Platform**: Para criar e gerenciar projetos Uno Platform, você precisará instalar os templates. Execute o seguinte comando no terminal:

    ```bash
    dotnet new install Uno.Templates
    ```

    Para mais detalhes, consulte a documentação oficial: [platform.uno/docs/articles/get-started.html](https://platform.uno/docs/articles/get-started.html)

## 2. Configuração do Banco de Dados (PostgreSQL com Docker)

O projeto utiliza PostgreSQL para persistência de dados (placares), gerenciado via Docker para simplificar a configuração.

1.  **Navegue até o diretório raiz do projeto** no seu terminal (onde o arquivo `docker-compose.yml` está localizado):

2.  **Inicie o serviço do banco de dados** usando Docker Compose:

    ```bash
    docker-compose up -d db
    ```

    Este comando irá baixar a imagem do PostgreSQL (se ainda não tiver), criar e iniciar um contêiner de banco de dados configurado para o projeto. O banco de dados estará acessível na porta `5432`.

3.  **Verifique se o contêiner está rodando** (opcional):

    ```bash
    docker ps
    ```

    Você deverá ver um contêiner chamado `capstone-programming-3-db-1` (ou similar) na lista.

## 3. Configuração e Compilação do Projeto

1.  **Navegue até o diretório do projeto SpaceInvaders**:

    ```bash
    cd SpaceInvaders/SpaceInvaders
    ```

2.  **Restaure as dependências do projeto**:

    ```bash
    dotnet restore
    ```

3.  **Compile o projeto**:

    ```bash
    dotnet build
    ```

    Este comando irá compilar o código-fonte e verificar se há erros.

## 4. Execução do Projeto

Após a compilação bem-sucedida e com o banco de dados rodando, você pode executar a aplicação.

1.  **Execute a aplicação desktop**:

    ```bash
    dotnet run --project SpaceInvaders/SpaceInvaders.csproj -f net9.0-desktop
    ```

    A aplicação Space Invaders deverá ser iniciada em uma nova janela.

### Nota sobre Áudio no Linux

Se você estiver executando o projeto em um ambiente Linux, certifique-se de ter o `mpg123` instalado para a reprodução de áudio. O `SoundService` do projeto utiliza este utilitário externo para garantir a compatibilidade de áudio no Linux.

Para instalar o `mpg123` (exemplo para sistemas baseados em Debian/Ubuntu):

```bash
sudo apt-get update
sudo apt-get install mpg123
```

Com esses passos, você estará pronto para desenvolver e testar o projeto Space Invaders em sua máquina.
