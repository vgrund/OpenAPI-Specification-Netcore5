[![.NET](https://github.com/vgrund/OpenAPI-Specification-Netcore5/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/vgrund/OpenAPI-Specification-Netcore5/actions/workflows/dotnet.yml)

Gerando a OpenApi Spec automaticamente a partir do código
=========

A solução proposta utiliza o [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) para gerar a 
[OpenApi Spec](https://spec.openapis.org/oas/v3.1.0).

Configurando a aplicação
-------------

### Classe Startup.cs

A configuração do Swashbuckle começa na classe Startup.cs.

* Linhas 25 a 40 - Registro do Swagger Generator e a especificação de um ou mais documentos.
* Linhas 27 a 38 - Especificação de um ou mais documentos com as informações básicas da API.
* Linha 39 - Define que vamos utilizar *annotations* para documentar os recursos.
* Linhas 63 e 64 - Insere os middlewares que expoem o arquivo json da spec e a interface do Swagger no endpoint especificado.

### Controllers

Nas controllers usamos *Annotations* para documentar diversas informações relevantes como: "Status-codes de response, response body, request body, operation id, além de outras informações". Essas informações são muito importantes e irão aparecer na OpenApi Specification.

* Linhas 14 e 15 - Define o tipo de conteúdo que todos os recursos recebem e retornam.
* Linhas 36 a 42 - Definição de informações relevantes do recurso.
  * OperationId - Conforme definido na [OpenApi Spec](https://spec.openapis.org/oas/v3.1.0): *"Unique string used to identify the operation."*
  * Summary - Conforme definido na [OpenApi Spec](https://spec.openapis.org/oas/v3.1.0): *"A short summary of what the operation does.."*
  * Description - Conforme definido na [OpenApi Spec](https://spec.openapis.org/oas/v3.1.0): *"A verbose explanation of the operation behavior."*
  * Tags - Conforme definido na [OpenApi Spec](https://spec.openapis.org/oas/v3.1.0): *"A list of tags for API documentation control. Tags can be used for logical grouping of operations by resources or any other qualifier."*
  * Linha 42 - Definição dos possíveis status-codes e o formato do response body

Como gerar a OpenApi Spec
-------------

Após toda a configuração é possível acessar uma interface que representa a Spec (como no [Swagger Editor](https://editor.swagger.io/)) através do endpoint *"/swagger"*.

Podemos automatizar a exportação da Spec para um arquivo Json em tempo de compilação com o [Swashbuckle CLI](https://github.com/domaindrivendev/Swashbuckle.AspNetCore#swashbuckleaspnetcorecli)

## Swashbuckle CLI
Extraído da doc https://github.com/domaindrivendev/Swashbuckle.AspNetCore#swashbuckleaspnetcorecli

Seguindo a explicação abaixo, aparecerá uma instrução em seu .csproj que gera o arquivo toda vez que a aplicação for compilada (Veja em Users.csproj).

```
<Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="dotnet tool restore" />
    <Exec Command="dotnet swagger tofile --output openapispec.json $(OutputPath)\$(AssemblyName).dll v1 " />
</Target>
```


Swashbuckle CLI

https://github.com/domaindrivendev/Swashbuckle.AspNetCore#swashbuckleaspnetcorecli




#### Using the tool with the .NET Core 2.1 SDK

1. Install as a [global tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-global-tool)

    ```
    dotnet tool install -g --version 6.1.2 Swashbuckle.AspNetCore.Cli
    ```

2. Verify that the tool was installed correctly

    ```
    swagger tofile --help
    ```

3. Generate a Swagger/ OpenAPI document from your application's startup assembly

	```
	swagger tofile --output [output] [startupassembly] [swaggerdoc]
	```

	Where ...
	* [output] is the relative path where the Swagger JSON will be output to
	* [startupassembly] is the relative path to your application's startup assembly
	* [swaggerdoc] is the name of the swagger document you want to retrieve, as configured in your startup class

#### Using the tool with the .NET Core 3.0 SDK or later

1. In your project root, create a tool manifest file:

    ```
    dotnet new tool-manifest
    ```

2. Install as a [local tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-local-tool)

    ```
    dotnet tool install --version 6.1.2 Swashbuckle.AspNetCore.Cli
    ```

3. Verify that the tool was installed correctly

    ```
    dotnet swagger tofile --help
    ```

4. Generate a Swagger / OpenAPI document from your application's startup assembly

	```
	dotnet swagger tofile --output [output] [startupassembly] [swaggerdoc]
	```

	Where ...
	* [output] is the relative path where the Swagger JSON will be output to
	* [startupassembly] is the relative path to your application's startup assembly
	* [swaggerdoc] is the name of the swagger document you want to retrieve, as configured in your startup class

### Use the CLI Tool with a Custom Host Configuration

Out-of-the-box, the tool will execute in the context of a "default" web host. However, in some cases you may want to bring your own host environment, for example if you've configured a custom DI container such as Autofac. For this scenario, the Swashbuckle CLI tool exposes a convention-based hook for your application.

That is, if your application contains a class that meets either of the following naming conventions, then that class will be used to provide a host for the CLI tool to run in.
