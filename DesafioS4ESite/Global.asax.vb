Imports System.Web.Http
Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' É acionado quando o aplicativo é iniciado
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
End Class