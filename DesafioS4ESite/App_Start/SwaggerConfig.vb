Imports System.Web.Http
Imports WebActivatorEx
Imports DesafioS4ESite
Imports Swashbuckle.Application
Imports DesafioS4ESite.DesafioS4ESite

<Assembly: PreApplicationStartMethod(GetType(SwaggerConfig), "Register")>

Namespace DesafioS4ESite
    Public Class SwaggerConfig
        Public Shared Sub Register()
            Dim thisAssembly = GetType(SwaggerConfig).Assembly

            GlobalConfiguration.Configuration.
                EnableSwagger(Function(c)
                                  c.SingleApiVersion("v1", "Desafio S4E API")
                              End Function).
                EnableSwaggerUi(Function(c)
                                End Function)
        End Sub
    End Class
End Namespace
