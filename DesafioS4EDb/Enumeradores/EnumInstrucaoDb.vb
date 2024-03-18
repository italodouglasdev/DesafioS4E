
Namespace Enumeradores

    ''' <summary>
    ''' Enumerador utilizado para informar o tipo de consulta que será gerado [Consultar], [Incluir] ou [Remover]
    ''' </summary>
    Public Enum EnumInstrucaoDb

        ''' <summary>
        ''' Obtém um registro
        ''' </summary>
        Consultar = 0

        ''' <summary>
        ''' Inclui um registro
        ''' </summary>
        Incluir = 1

        ''' <summary>
        ''' Atualiza um registro
        ''' </summary>
        Remover = 2

    End Enum

End Namespace

