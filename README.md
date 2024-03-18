# DesafioS4E

No desenvolvimento foram utilizados o Visual Studio 2022 com VB.Net e NetFramework 4.6. E para o banco de dados foi utilizado o SQL Server 2008.

A solução está dividida em dois projetos, o DesafioS4EDb que é responsável por toda parte do CRUD no banco de dados e o outro projeto DesafioS4ESite que é o Site e a API.

Dentro do diretório Arquivos estão localizados o script SQL para criação do banco de dados, além de um backup zerado caso prefira restaurar. Será necessário ajustar a string de conexão que fica na classe DesafioS4EDb.SQL.Configuracao com os dados da instância do SQL.

Para auxiliar no teste da API, foi implementado o Swagger e também dentro do diretório de arquivos existe um arquivo Json de configuração para importação no Postman.
