Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Configuração de Web API e serviços

        ' Rotas de Web API
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{action}"
        )

        ' Força retornar um Json em não um XML
        Dim formatters = GlobalConfiguration.Configuration.Formatters
        formatters.Remove(formatters.XmlFormatter)

    End Sub
End Module
