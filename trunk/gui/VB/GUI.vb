Public Class GUI
    Function vbRunMotor(ByVal msg As String) As Integer
        MsgBox("vbRunMotor | " & msg)
    End Function

    Private Sub AxShockwaveFlash1_FlashCall(ByVal sender As Object, ByVal e As AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent) Handles AxShockwaveFlash1.FlashCall
        'MsgBox(e.request.ToString())
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
                MsgBox(fArgs(0) & " | " & fArgs(1), , funcName)
                If CInt(fArgs(0)) = 1 Then               'motor 1
                    If CInt(fArgs(1)) > 0 Then
                        Form1.txtTx.Text = 22 & Hex(127 * (Math.Abs(CInt(fArgs(1))) / 100)) 'up
                    Else
                        Form1.txtTx.Text = 23 & Hex(127 * (Math.Abs(CInt(fArgs(1))) / 100)) 'down
                    End If
                ElseIf CInt(fArgs(0)) = 2 Then           'motor 2
                    If CInt(fArgs(1)) > 0 Then
                        Form1.txtTx.Text = 24 & Hex(127 * (Math.Abs(CInt(fArgs(1))) / 100)) 'up
                    Else
                        Form1.txtTx.Text = 25 & Hex(127 * (Math.Abs(CInt(fArgs(1))) / 100)) 'down
                    End If
                ElseIf CInt(fArgs(0)) = 2 Then
                    If CInt(fArgs(1)) > 0 Then
                        Form1.txtTx.Text = 26 & Hex(127 * (Math.Abs(CInt(fArgs(1))) / 100)) 'up
                    Else
                        Form1.txtTx.Text = 27 & Hex(127 * (Math.Abs(CInt(fArgs(1))) / 100)) 'down
                    End If
                End If
                    Form1.btnTx.PerformClick()
                    'next line to be kept under scrutiny: might not work
                    'Form1.moRS232.Write("0x2" & fArgs(0)) ' 0x21 is motor 1, 22 is motor 2, 23 is motor 3
            Case "vbShowRS232"
                    Form1.Show()
            Case Else
                    MsgBox(funcName)
        End Select

        'MsgBox(funcName & ": " & elem.ToString())

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Form1.btnOpenCom_Click()
        Dim XMLizedData As String
        XMLizedData = "<invoke name=""vbColorFoot"" returntype=""xml"">" _
                    & "<arguments>" _
                        & "<array>" _
                            & "<property id=""0""><number>50</number></property>" _
                            & "<property id=""1""><number>50</number></property>" _
                            & "<property id=""2""><number>34</number></property>" _
                            & "<property id=""3""><number>35</number></property>" _
                            & "<property id=""4""><number>26</number></property>" _
                            & "<property id=""5""><number>17</number></property>" _
                            & "<property id=""6""><number>18</number></property>" _
                            & "<property id=""7""><number>29</number></property>" _
                            & "<property id=""8""><number>9</number></property>" _
                            & "<property id=""9""><number>8</number></property>" _
                            & "<property id=""10""><number>7</number></property>" _
                            & "<property id=""11""><number>23</number></property>" _
                            & "<property id=""12""><number>15</number></property>" _
                            & "<property id=""13""><number>24</number></property>" _
                            & "<property id=""14""><number>33</number></property>" _
                            & "<property id=""15""><number>32</number></property>" _
                            & "<property id=""16""><number>21</number></property>" _
                            & "<property id=""17""><number>10</number></property>" _
                        & "</array>" _
                        & "</arguments></invoke>" '<string>hello</string><string>world</string>
        MsgBox(XMLizedData)
        Me.AxShockwaveFlash1.CallFunction(XMLizedData)
    End Sub

    Private Sub GUI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Form1.Show()
        Form1.btnOpenCom.PerformClick()

    End Sub
End Class