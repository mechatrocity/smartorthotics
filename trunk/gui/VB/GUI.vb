Public Class GUI
    Function vbRunMotor(ByVal msg As String) As Integer
        MsgBox("vbRunMotor | " & msg)
    End Function

    Private Sub AxShockwaveFlash1_FlashCall(ByVal sender As Object, ByVal e As AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent) Handles AxShockwaveFlash1.FlashCall
        MsgBox(e.request.ToString())
        Dim reqXml As New MSXML2.DOMDocument
        reqXml.loadXML(e.request)

        Dim elem As MSXML2.IXMLDOMElement
        elem = DirectCast(reqXml.selectSingleNode("invoke"), MSXML2.IXMLDOMElement)

        Dim funcName As String
        funcName = DirectCast(elem.getAttribute("name"), String)

        elem = DirectCast(elem.selectSingleNode("arguments"), MSXML2.IXMLDOMElement)

        'Dim le As Integer
        Dim fArgs() As String
        ReDim fArgs(elem.childNodes.length)
        Dim r As Integer
        For r = 0 To (elem.childNodes.length - 1)
            fArgs(r) = elem.childNodes.item(r).text
        Next

        Dim ret As Object
        Select Case funcName
            Case "vbRunMotor"
                'ret = cargarXml(fArgs)
                MsgBox(fArgs(0) & " | " & fArgs(1) & " | " & fArgs(2), , funcName)
            Case Else
                MsgBox(funcName)
        End Select

        'MsgBox(funcName & ": " & elem.ToString())

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.AxShockwaveFlash1.CallFunction("<invoke name=""vbColorFoot"" returntype=""xml""><arguments><string>hello</string><string>world</string></arguments></invoke>")
    End Sub
End Class