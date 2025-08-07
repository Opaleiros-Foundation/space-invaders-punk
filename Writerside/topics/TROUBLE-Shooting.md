# Solução de Problemas

Este documento fornece soluções para problemas comuns que podem surgir durante a configuração, compilação ou execução do projeto.

## 1. Problemas de Conexão com o Banco de Dados (PostgreSQL)

### Problema: A aplicação não consegue se conectar ao banco de dados.

**Possíveis Causas e Soluções:**

*   **Contêiner Docker não está rodando**: O banco de dados PostgreSQL é executado em um contêiner Docker. Certifique-se de que ele está ativo.
    *   **Solução**: Abra um terminal no diretório raiz do projeto (`capstone-programming-3`) e execute:
        ```bash
        docker-compose up -d db
        ```
        Verifique o status com `docker ps`.

*   **Porta 5432 já em uso**: Outro serviço pode estar utilizando a porta padrão do PostgreSQL.
    *   **Solução**: Verifique qual processo está usando a porta 5432 (ex: `sudo lsof -i :5432` no Linux/macOS, ou `netstat -ano | findstr :5432` no Windows) e encerre-o, ou altere a porta no `docker-compose.yml` e na string de conexão da aplicação.

*   **Credenciais incorretas**: O nome de usuário, senha ou nome do banco de dados na string de conexão da aplicação podem estar incorretos.
    *   **Solução**: Verifique as variáveis de ambiente no `docker-compose.yml` (`POSTGRES_USER`, `POSTGRES_PASSWORD`, `POSTGRES_DB`) e compare com a string de conexão utilizada na aplicação.

*   **Volume de dados corrompido**: Em casos raros, o volume de dados do Docker pode estar corrompido.
    *   **Solução**: Pare o contêiner (`docker-compose down`), remova o volume (`docker volume rm capstone-programming-3_db_data`) e inicie o contêiner novamente (`docker-compose up -d db`). **Atenção**: Isso apagará todos os dados do banco de dados.

## 2. Problemas de Áudio no Linux

### Problema: O jogo não reproduz sons no Linux.

**Possíveis Causas e Soluções:**

*   **`mpg123` não está instalado**: O `SoundService` do projeto utiliza o utilitário `mpg123` para reprodução de áudio em ambientes Linux.
    *   **Solução**: Instale o `mpg123` no seu sistema. Para sistemas baseados em Debian/Ubuntu:
        ```bash
        sudo apt-get update
        sudo apt-get install mpg123
        ```
        Para outras distribuições, consulte a documentação do seu gerenciador de pacotes.

*   **Caminho do arquivo de áudio incorreto**: O jogo não consegue encontrar os arquivos de som.
    *   **Solução**: Verifique se os arquivos `.mp3` estão na pasta `SpaceInvaders/Assets/sounds/` e se os caminhos em `Constants/SoundPaths.cs` estão corretos.

## 3. Problemas de Compilação e Execução do Projeto

### Problema: O projeto não compila ou não executa.

**Possíveis Causas e Soluções:**

*   **SDK do .NET ausente ou versão incorreta**: O projeto requer o .NET SDK 9.0 (ou superior).
    *   **Solução**: Baixe e instale a versão correta do .NET SDK em [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).

*   **Dependências não restauradas**: As bibliotecas e pacotes NuGet podem não ter sido baixados.
    *   **Solução**: No diretório `SpaceInvaders/SpaceInvaders`, execute `dotnet restore`.

*   **Templates do Uno Platform não instalados**: Se você estiver criando um novo projeto ou tiver problemas relacionados ao Uno Platform.
    *   **Solução**: Execute `dotnet new install Uno.Templates`.

*   **Erros de código**: Erros de sintaxe ou lógica no seu código.
    *   **Solução**: Verifique as mensagens de erro no terminal ou na IDE e corrija o código. Utilize o comando `dotnet build` para verificar erros de compilação.

*   **Caminho do projeto incorreto ao executar**: Ao usar `dotnet run`, o caminho para o `.csproj` deve estar correto.
    *   **Solução**: Certifique-se de que você está no diretório `SpaceInvaders/SpaceInvaders` ou especifique o caminho completo: `dotnet run --project SpaceInvaders/SpaceInvaders.csproj -f net9.0-desktop`.
