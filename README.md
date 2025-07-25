# BlogApp

Este é um aplicativo de demonstração de blog desenvolvido com **.NET MAUI**, projetado para oferecer uma experiência de visualização de posts de blog em diversas plataformas (Android, iOS, Windows, macOS). Ele apresenta uma arquitetura em camadas clara, seguindo princípios de design que promovem a manutenibilidade e escalabilidade.

---

## Visão Geral do Projeto

O **BlogApp** permite aos usuários:

* Visualizar uma lista de posts de blog disponíveis.
* Clicar em um post para ver seus detalhes completos.

A arquitetura do projeto foi cuidadosamente planejada para separar as responsabilidades, tornando o desenvolvimento mais organizado e o código mais testável.

---

## Estrutura de Camadas

O projeto está organizado nas seguintes camadas:

* **`BlogApp.Core`**:
    * Contém entidades de domínio (`Models`), constantes (`Constants`), definições de exceções (`Exceptions`), interfaces (`Interfaces`) para serviços e repositórios, e conversores (`Converters`) para tratamento de dados. É o coração da lógica de negócios agnóstica à plataforma.
* **`BlogApp.Data`**:
    * Responsável pela configuração do banco de dados (provavelmente SQLite, dada a natureza do MAUI) e pelo `DatabaseContext`. Gerencia as operações de baixo nível com o armazenamento de dados.
* **`BlogApp.Repositories`**:
    * Define e implementa a lógica para acesso e manipulação de dados. Abstrai a fonte de dados subjacente (como o `DatabaseContext`), fornecendo métodos para interagir com as entidades de blog (ex: `GetBlogPosts()`, `GetBlogPostById()`).
* **`BlogApp.Services`**:
    * Contém a lógica de negócios de alto nível. Utiliza os repositórios para orquestrar operações complexas, aplicar regras de negócio e preparar dados para a camada de apresentação. Ex: `BlogService` que pode chamar `BlogRepository` para obter dados e processá-los.
* **`BlogApp` (Projeto Principal)**:
    * O projeto da interface do usuário `.NET MAUI`. Contém as Views (páginas), ViewModels (que interagem com os Services) e a lógica específica da plataforma para renderização e interação do usuário. É a camada que o usuário final vê e interage.

---

## Tecnologias Utilizadas

* **.NET MAUI**: Framework para desenvolvimento de aplicativos multiplataforma nativos.
* **C#**: Linguagem de programação principal.
* **SQLite**: (Provável) Banco de dados local para armazenamento de dados do blog.
* **MVVM (Model-View-ViewModel)**: Padrão de arquitetura para separar a UI da lógica de negócios.

---

## Como Configurar e Rodar o Projeto

Siga os passos abaixo para colocar o BlogApp em funcionamento na sua máquina local.

### Pré-requisitos

* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (versão 17.3 ou superior) com a workload de desenvolvimento de interface do usuário para **.NET Multi-plataform App UI development** instalada.
* SDKs de plataforma alvo (Android SDK, iOS SDK, Windows App SDK) instalados e configurados.

### Passos de Configuração

1.  **Clone o Repositório:**
    ```bash
    git clone [https://github.com/igorferreirasampaio/BlogApp.git](https://github.com/igorferreirasampaio/BlogApp.git)
    cd BlogApp
    ```
    *(Substitua `igorferreirasampaio/BlogApp.git` pelo caminho real do seu repositório no GitHub)*

2.  **Abra no Visual Studio:**
    * Abra a solução `BlogApp.sln` no Visual Studio 2022.

3.  **Restaurar Pacotes NuGet:**
    * O Visual Studio deve restaurar automaticamente os pacotes NuGet. Se não, clique com o botão direito na solução no Gerenciador de Soluções e selecione **"Restaurar Pacotes NuGet"**.

4.  **Configuração do Banco de Dados (se aplicável):**
    * Verifique o projeto `BlogApp.Data` para quaisquer configurações de caminho de banco de dados ou strings de conexão. Se estiver usando SQLite, o banco de dados geralmente é criado e preenchido na primeira execução, ou você pode precisar de um script de inicialização.

5.  **Executar o Projeto:**
    * Selecione o projeto `BlogApp` como o projeto de inicialização.
    * Escolha o emulador/dispositivo/plataforma desejado (Android, iOS, Windows, etc.) na barra de ferramentas do Visual Studio.
    * Pressione **F5** ou clique no botão **"Executar"** para compilar e implantar o aplicativo.

---

## Contato

* **Seu Nome/GitHub:** [SeuPerfilGitHub](https://github.com/igorferreirasampaio)
* **Email:** [seu.email@example.com](mailto:igorferreirasampao@gmail.com)
