Public Class Form1
	Inherits System.Windows.Forms.Form


	'// Private members
   Private miComPort As Integer
   Friend WithEvents btnOpenCom As System.Windows.Forms.Button
	Friend WithEvents btnCloseCom As System.Windows.Forms.Button
	Friend WithEvents btnTx As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents txtTx As System.Windows.Forms.TextBox
	Friend WithEvents txtRx As System.Windows.Forms.TextBox
	Friend WithEvents btnRx As System.Windows.Forms.Button
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents txtBytes2Read As System.Windows.Forms.TextBox
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents optCom2 As System.Windows.Forms.RadioButton
	Friend WithEvents optCom1 As System.Windows.Forms.RadioButton
	Friend WithEvents txtTimeout As System.Windows.Forms.TextBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents txtBaudrate As System.Windows.Forms.TextBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents chkAutorx As System.Windows.Forms.CheckBox
	Friend WithEvents chkRTS As System.Windows.Forms.CheckBox
	Friend WithEvents chkDTR As System.Windows.Forms.CheckBox
	Friend WithEvents btnExit As System.Windows.Forms.Button
    Public WithEvents moRS232 As Rs232
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Private mlTicks As Long
    Private Delegate Sub CommEventUpdate(ByVal source As Rs232, ByVal mask As Rs232.EventMasks)

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Private components As System.ComponentModel.IContainer


    'Required by the Windows Form Designer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents linkAuthor As System.Windows.Forms.LinkLabel
    Friend WithEvents lbHex As System.Windows.Forms.ListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboStatusLine As System.Windows.Forms.ComboBox
    Friend WithEvents btnCheck As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbAsync As System.Windows.Forms.ListBox
    Friend WithEvents lblAsync As System.Windows.Forms.Label
    Friend WithEvents chkEvents As System.Windows.Forms.CheckBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents chkAddCR As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPortNum As System.Windows.Forms.TextBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents btnInBuffer As System.Windows.Forms.Button
    Friend WithEvents lblInBuffer As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkRTS = New System.Windows.Forms.CheckBox
        Me.btnTx = New System.Windows.Forms.Button
        Me.txtTimeout = New System.Windows.Forms.TextBox
        Me.txtTx = New System.Windows.Forms.TextBox
        Me.chkAutorx = New System.Windows.Forms.CheckBox
        Me.btnOpenCom = New System.Windows.Forms.Button
        Me.txtBaudrate = New System.Windows.Forms.TextBox
        Me.btnRx = New System.Windows.Forms.Button
        Me.txtBytes2Read = New System.Windows.Forms.TextBox
        Me.chkDTR = New System.Windows.Forms.CheckBox
        Me.lbAsync = New System.Windows.Forms.ListBox
        Me.chkEvents = New System.Windows.Forms.CheckBox
        Me.chkAddCR = New System.Windows.Forms.CheckBox
        Me.txtPortNum = New System.Windows.Forms.TextBox
        Me.btnTest = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtRx = New System.Windows.Forms.TextBox
        Me.optCom1 = New System.Windows.Forms.RadioButton
        Me.btnCloseCom = New System.Windows.Forms.Button
        Me.optCom2 = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.linkAuthor = New System.Windows.Forms.LinkLabel
        Me.lbHex = New System.Windows.Forms.ListBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cboStatusLine = New System.Windows.Forms.ComboBox
        Me.btnCheck = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblAsync = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.btnInBuffer = New System.Windows.Forms.Button
        Me.lblInBuffer = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkRTS
        '
        Me.chkRTS.Checked = True
        Me.chkRTS.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRTS.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkRTS.Location = New System.Drawing.Point(212, 90)
        Me.chkRTS.Name = "chkRTS"
        Me.chkRTS.Size = New System.Drawing.Size(63, 19)
        Me.chkRTS.TabIndex = 3
        Me.chkRTS.Text = "RTS"
        Me.ToolTip1.SetToolTip(Me.chkRTS, "Set state of RTS Line")
        '
        'btnTx
        '
        Me.btnTx.Enabled = False
        Me.btnTx.Location = New System.Drawing.Point(216, 134)
        Me.btnTx.Name = "btnTx"
        Me.btnTx.Size = New System.Drawing.Size(60, 17)
        Me.btnTx.TabIndex = 7
        Me.btnTx.Text = "Tx"
        Me.ToolTip1.SetToolTip(Me.btnTx, "Sends data to COM Port")
        '
        'txtTimeout
        '
        Me.txtTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTimeout.Location = New System.Drawing.Point(69, 24)
        Me.txtTimeout.Name = "txtTimeout"
        Me.txtTimeout.Size = New System.Drawing.Size(49, 21)
        Me.txtTimeout.TabIndex = 3
        Me.txtTimeout.Text = "1500"
        Me.ToolTip1.SetToolTip(Me.txtTimeout, "COM Port timeout in ms")
        '
        'txtTx
        '
        Me.txtTx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTx.Location = New System.Drawing.Point(7, 134)
        Me.txtTx.Name = "txtTx"
        Me.txtTx.Size = New System.Drawing.Size(200, 21)
        Me.txtTx.TabIndex = 6
        Me.txtTx.Text = "ATZ"
        Me.ToolTip1.SetToolTip(Me.txtTx, "Type data you want to send")
        '
        'chkAutorx
        '
        Me.chkAutorx.Location = New System.Drawing.Point(4, 699)
        Me.chkAutorx.Name = "chkAutorx"
        Me.chkAutorx.Size = New System.Drawing.Size(185, 15)
        Me.chkAutorx.TabIndex = 13
        Me.chkAutorx.Text = "Automatically receive bytes"
        Me.ToolTip1.SetToolTip(Me.chkAutorx, "After a Tx tries to Rx bytes")
        '
        'btnOpenCom
        '
        Me.btnOpenCom.Location = New System.Drawing.Point(210, 19)
        Me.btnOpenCom.Name = "btnOpenCom"
        Me.btnOpenCom.Size = New System.Drawing.Size(95, 27)
        Me.btnOpenCom.TabIndex = 1
        Me.btnOpenCom.Text = "Open COM Port"
        Me.ToolTip1.SetToolTip(Me.btnOpenCom, "Initializes and Open COM port")
        '
        'txtBaudrate
        '
        Me.txtBaudrate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBaudrate.Location = New System.Drawing.Point(69, 60)
        Me.txtBaudrate.Name = "txtBaudrate"
        Me.txtBaudrate.Size = New System.Drawing.Size(49, 21)
        Me.txtBaudrate.TabIndex = 5
        Me.txtBaudrate.Text = "115200"
        Me.ToolTip1.SetToolTip(Me.txtBaudrate, "COM Port Baudrate")
        '
        'btnRx
        '
        Me.btnRx.Enabled = False
        Me.btnRx.Location = New System.Drawing.Point(4, 661)
        Me.btnRx.Name = "btnRx"
        Me.btnRx.Size = New System.Drawing.Size(58, 19)
        Me.btnRx.TabIndex = 10
        Me.btnRx.Text = "Rx"
        Me.ToolTip1.SetToolTip(Me.btnRx, "Reads COM Buffer")
        '
        'txtBytes2Read
        '
        Me.txtBytes2Read.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBytes2Read.Location = New System.Drawing.Point(241, 662)
        Me.txtBytes2Read.Name = "txtBytes2Read"
        Me.txtBytes2Read.Size = New System.Drawing.Size(65, 21)
        Me.txtBytes2Read.TabIndex = 12
        Me.txtBytes2Read.Text = "250"
        Me.ToolTip1.SetToolTip(Me.txtBytes2Read, "Bytes to read from COM buffer (this number effects also CommEvent)")
        '
        'chkDTR
        '
        Me.chkDTR.Checked = True
        Me.chkDTR.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDTR.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkDTR.Location = New System.Drawing.Point(260, 90)
        Me.chkDTR.Name = "chkDTR"
        Me.chkDTR.Size = New System.Drawing.Size(63, 19)
        Me.chkDTR.TabIndex = 4
        Me.chkDTR.Text = "DTR"
        Me.ToolTip1.SetToolTip(Me.chkDTR, "Set state of DTR Line")
        '
        'lbAsync
        '
        Me.lbAsync.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbAsync.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbAsync.Location = New System.Drawing.Point(7, 774)
        Me.lbAsync.Name = "lbAsync"
        Me.lbAsync.Size = New System.Drawing.Size(306, 54)
        Me.lbAsync.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.lbAsync, "Async method sequence")
        '
        'chkEvents
        '
        Me.chkEvents.Enabled = False
        Me.chkEvents.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkEvents.Location = New System.Drawing.Point(182, 699)
        Me.chkEvents.Name = "chkEvents"
        Me.chkEvents.Size = New System.Drawing.Size(124, 19)
        Me.chkEvents.TabIndex = 29
        Me.chkEvents.Text = "Enable events"
        Me.ToolTip1.SetToolTip(Me.chkEvents, "Enables notification events (CommEvent)")
        '
        'chkAddCR
        '
        Me.chkAddCR.Location = New System.Drawing.Point(216, 158)
        Me.chkAddCR.Name = "chkAddCR"
        Me.chkAddCR.Size = New System.Drawing.Size(79, 13)
        Me.chkAddCR.TabIndex = 31
        Me.chkAddCR.Text = "Add CR"
        Me.ToolTip1.SetToolTip(Me.chkAddCR, "Add a CR at the end of Tx Message")
        '
        'txtPortNum
        '
        Me.txtPortNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPortNum.Location = New System.Drawing.Point(132, 36)
        Me.txtPortNum.Name = "txtPortNum"
        Me.txtPortNum.Size = New System.Drawing.Size(49, 21)
        Me.txtPortNum.TabIndex = 6
        Me.txtPortNum.Text = "1"
        Me.ToolTip1.SetToolTip(Me.txtPortNum, "Enter port number")
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(132, 64)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(49, 17)
        Me.btnTest.TabIndex = 7
        Me.btnTest.Text = "Test"
        Me.ToolTip1.SetToolTip(Me.btnTest, "Test port availability")
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(236, 647)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 14)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Bytes to read"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(69, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Timeout (ms)"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(69, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "BaudRate"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 164)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 14)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Received Data"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(7, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Data to Tx"
        '
        'txtRx
        '
        Me.txtRx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRx.Location = New System.Drawing.Point(7, 178)
        Me.txtRx.Multiline = True
        Me.txtRx.Name = "txtRx"
        Me.txtRx.Size = New System.Drawing.Size(305, 44)
        Me.txtRx.TabIndex = 9
        '
        'optCom1
        '
        Me.optCom1.Location = New System.Drawing.Point(10, 18)
        Me.optCom1.Name = "optCom1"
        Me.optCom1.Size = New System.Drawing.Size(64, 26)
        Me.optCom1.TabIndex = 0
        Me.optCom1.Text = "COM &1"
        '
        'btnCloseCom
        '
        Me.btnCloseCom.Enabled = False
        Me.btnCloseCom.Location = New System.Drawing.Point(211, 51)
        Me.btnCloseCom.Name = "btnCloseCom"
        Me.btnCloseCom.Size = New System.Drawing.Size(95, 27)
        Me.btnCloseCom.TabIndex = 2
        Me.btnCloseCom.Text = "Close COM Port"
        '
        'optCom2
        '
        Me.optCom2.Location = New System.Drawing.Point(10, 38)
        Me.optCom2.Name = "optCom2"
        Me.optCom2.Size = New System.Drawing.Size(66, 26)
        Me.optCom2.TabIndex = 1
        Me.optCom2.Text = "COM &5"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.btnTest)
        Me.GroupBox1.Controls.Add(Me.txtPortNum)
        Me.GroupBox1.Controls.Add(Me.txtTimeout)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtBaudrate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.optCom2)
        Me.GroupBox1.Controls.Add(Me.optCom1)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(198, 99)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "COM Setup"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(132, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 14)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Port check"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(251, 836)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(62, 23)
        Me.btnExit.TabIndex = 14
        Me.btnExit.Text = "&Close"
        '
        'linkAuthor
        '
        Me.linkAuthor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.linkAuthor.Location = New System.Drawing.Point(7, 844)
        Me.linkAuthor.Name = "linkAuthor"
        Me.linkAuthor.Size = New System.Drawing.Size(82, 12)
        Me.linkAuthor.TabIndex = 15
        Me.linkAuthor.TabStop = True
        Me.linkAuthor.Text = "Contact Author"
        '
        'lbHex
        '
        Me.lbHex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbHex.Location = New System.Drawing.Point(8, 242)
        Me.lbHex.Name = "lbHex"
        Me.lbHex.Size = New System.Drawing.Size(305, 392)
        Me.lbHex.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 226)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(114, 14)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Received Data (Hex)"
        '
        'cboStatusLine
        '
        Me.cboStatusLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatusLine.Location = New System.Drawing.Point(79, 661)
        Me.cboStatusLine.Name = "cboStatusLine"
        Me.cboStatusLine.Size = New System.Drawing.Size(97, 21)
        Me.cboStatusLine.Sorted = True
        Me.cboStatusLine.TabIndex = 18
        '
        'btnCheck
        '
        Me.btnCheck.Enabled = False
        Me.btnCheck.Location = New System.Drawing.Point(181, 663)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(43, 19)
        Me.btnCheck.TabIndex = 19
        Me.btnCheck.Text = "Check"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(80, 645)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 14)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Status line"
        '
        'lblAsync
        '
        Me.lblAsync.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAsync.Location = New System.Drawing.Point(7, 759)
        Me.lblAsync.Name = "lblAsync"
        Me.lblAsync.Size = New System.Drawing.Size(83, 14)
        Me.lblAsync.TabIndex = 26
        Me.lblAsync.Text = "Async flow"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel1.Location = New System.Drawing.Point(119, 844)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(100, 12)
        Me.LinkLabel1.TabIndex = 30
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "www.codeworks.it"
        '
        'btnInBuffer
        '
        Me.btnInBuffer.Location = New System.Drawing.Point(4, 728)
        Me.btnInBuffer.Name = "btnInBuffer"
        Me.btnInBuffer.Size = New System.Drawing.Size(151, 23)
        Me.btnInBuffer.TabIndex = 32
        Me.btnInBuffer.Text = "Bytes on Rx buffer"
        '
        'lblInBuffer
        '
        Me.lblInBuffer.BackColor = System.Drawing.Color.Khaki
        Me.lblInBuffer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInBuffer.Location = New System.Drawing.Point(166, 729)
        Me.lblInBuffer.Name = "lblInBuffer"
        Me.lblInBuffer.Size = New System.Drawing.Size(139, 19)
        Me.lblInBuffer.TabIndex = 33
        Me.lblInBuffer.Text = "?"
        Me.lblInBuffer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(320, 862)
        Me.Controls.Add(Me.lblInBuffer)
        Me.Controls.Add(Me.btnInBuffer)
        Me.Controls.Add(Me.chkAddCR)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.chkEvents)
        Me.Controls.Add(Me.lblAsync)
        Me.Controls.Add(Me.lbAsync)
        Me.Controls.Add(Me.txtBytes2Read)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnCheck)
        Me.Controls.Add(Me.cboStatusLine)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lbHex)
        Me.Controls.Add(Me.linkAuthor)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.chkDTR)
        Me.Controls.Add(Me.chkRTS)
        Me.Controls.Add(Me.chkAutorx)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnRx)
        Me.Controls.Add(Me.txtRx)
        Me.Controls.Add(Me.txtTx)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnTx)
        Me.Controls.Add(Me.btnCloseCom)
        Me.Controls.Add(Me.btnOpenCom)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VB.Net Serial comunication example"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region




    Private Sub btnOpenCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenCom.Click
        moRS232 = New Rs232()
        Try
            '// Setup parameters
            With moRS232
                .Port = miComPort
                .BaudRate = Int32.Parse(txtBaudrate.Text)
                .DataBit = 8
                .StopBit = Rs232.DataStopBit.StopBit_1
                .Parity = Rs232.DataParity.Parity_None
                .Timeout = Int32.Parse(txtTimeout.Text)
            End With
            '// Initializes port
            moRS232.Open()
            '// Set state of RTS / DTS
            moRS232.Dtr = (chkDTR.CheckState = CheckState.Checked)
            moRS232.Rts = (chkRTS.CheckState = CheckState.Checked)
            If chkEvents.Checked Then moRS232.EnableEvents()
            chkEvents.Enabled = True
        Catch Ex As Exception
            MessageBox.Show(Ex.Message, "Connection Error", MessageBoxButtons.OK)
        Finally
            btnCloseCom.Enabled = moRS232.IsOpen
            btnOpenCom.Enabled = Not moRS232.IsOpen
            btnTx.Enabled = moRS232.IsOpen
            btnRx.Enabled = moRS232.IsOpen
            btnCheck.Enabled = moRS232.IsOpen
        End Try
        Me.Timer1.Enabled = True
        btnRx.PerformClick()
    End Sub

    Private Sub btnCloseCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseCom.Click
        chkEvents.Enabled = False
        moRS232.Close()
        btnCloseCom.Enabled = moRS232.IsOpen
        btnOpenCom.Enabled = Not moRS232.IsOpen
        btnTx.Enabled = moRS232.IsOpen
        btnRx.Enabled = moRS232.IsOpen
        btnCheck.Enabled = moRS232.IsOpen
        Me.Timer1.Enabled = False
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTx.Click
        'MsgBox("Tx ran")
        Dim sTx As String
        '----------------------
        '// Clear Tx/Rx Buffers
        moRS232.PurgeBuffer(Rs232.PurgeBuffers.TxClear Or Rs232.PurgeBuffers.RXClear)
        sTx = txtTx.Text
        If chkAddCR.Checked Then sTx += ControlChars.Cr

        moRS232.Write(sTx)
        'moRS232.Write(Chr(2) & Chr(2) & Chr(73) & Chr(48) & Chr(121) & Chr(3))
        '// Clears Rx textbox
        txtRx.Text = String.Empty
        txtRx.Refresh()
        lbHex.Items.Clear()
        If chkAutorx.Checked Then Button1_Click(Nothing, Nothing)
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not moRS232 Is Nothing Then
            '// Disables Events if active
            moRS232.DisableEvents()
            If moRS232.IsOpen Then moRS232.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRx.Click
        Try
            moRS232.Read(Int32.Parse(txtBytes2Read.Text))

            txtRx.Text = moRS232.InputStreamString
            txtRx.ForeColor = Color.Black
            txtRx.BackColor = Color.White
            '// Fills listbox with hex values
            Dim aBytes As Byte() = moRS232.InputStream
            Dim iPnt As Int32
            Dim highNibble, lowNibble As Double
            Dim currentSensorVal, smallSensorData(12), mediumSensorData(4), largeSensorData(2), flexSensorData(3) As Integer
            Dim startIndex As Integer = 0

            Dim loop_ As Integer = 0

            While loop_ <> 1
                If aBytes.Length > (56 + 115) Then
                    If aBytes(startIndex) = &HA2 Then 'changed UA
                        loop_ = 1
                    End If
                    startIndex = startIndex + 1
                End If

            End While


            For iPnt = startIndex - 1 To startIndex + 54 'aBytes.Length - 1
                lbHex.Items.Add(iPnt.ToString & ControlChars.Tab & String.Format("0x{0}", aBytes(iPnt).ToString("X")))
                'MsgBox(aBytes(iPnt))
                'sensorData(iPnt)
                highNibble = Val("&h" & aBytes(iPnt).ToString("X").Substring(0, 1))
                lowNibble = Val("&h" & aBytes(iPnt).ToString("X").Substring(1))
                If (highNibble = &HA) Then 'aBytes(iPnt) = &HA1
                    'MsgBox(aBytes(iPnt).ToString("X").Substring(1))
                    'MsgBox(highNibble & lowNibble)
                    If (lowNibble > 0 And lowNibble < &HD) Then
                        currentSensorVal = Convert.ToInt32(Val("&H" & aBytes(iPnt + 1).ToString("X") & aBytes(iPnt + 2).ToString("X")))
                        iPnt = iPnt + 2
                        smallSensorData(CInt(lowNibble)) = CInt(50 * currentSensorVal / 4096)
                        'MsgBox(smallSensorData(CInt(lowNibble)))
                    End If
                ElseIf (highNibble = &HB) Then
                    ' MsgBox(highNibble & lowNibble)
                    If (lowNibble > 0 And lowNibble < &H5) Then
                        currentSensorVal = Convert.ToInt32(Val("&H" & aBytes(iPnt + 1).ToString("X") & aBytes(iPnt + 2).ToString("X")))
                        iPnt = iPnt + 2
                        mediumSensorData(CInt(lowNibble)) = CInt(50 * currentSensorVal / 4096)
                        'MsgBox(mediumSensorData(CInt(lowNibble)))
                    End If
                ElseIf (highNibble = &HC) Then
                    'MsgBox(highNibble & lowNibble)
                    If (lowNibble > 0 And lowNibble < &H3) Then
                        currentSensorVal = Convert.ToInt32(Val("&H" & aBytes(iPnt + 1).ToString("X") & aBytes(iPnt + 2).ToString("X")))
                        iPnt = iPnt + 2
                        largeSensorData(CInt(lowNibble)) = CInt(50 * currentSensorVal / 4096)
                        'MsgBox(largeSensorData(CInt(lowNibble)))
                    End If
                ElseIf (highNibble = &HD) Then
                    'MsgBox(highNibble & lowNibble)
                    If (lowNibble > 0 And lowNibble < &H4) Then
                        currentSensorVal = Convert.ToInt32(Val("&H" & aBytes(iPnt + 1).ToString("X") & aBytes(iPnt + 2).ToString("X")))
                        iPnt = iPnt + 2
                        flexSensorData(CInt(lowNibble)) = CInt(50 * currentSensorVal / 4096)
                        'MsgBox(largeSensorData(CInt(lowNibble)))
                    End If
                End If
            Next

            'have all data
            'MsgBox("clicked")
            updateFlashFoot(smallSensorData, mediumSensorData, largeSensorData, flexSensorData)

        Catch Ex As Exception
            txtRx.BackColor = Color.Red
            txtRx.ForeColor = Color.White
            txtRx.Text = "Error occurred " & Ex.Message & "  data fetched: " & moRS232.InputStreamString
        End Try



        'MsgBox("ouch")
    End Sub
    Function updateFlashFoot(ByVal smallVals() As Integer, ByVal mediumVals() As Integer, ByVal largeVals() As Integer, ByVal flexVals() As Integer) As Integer
        Dim XMLizedData As String
        'If (smallVals(1) > 10) Then
        ' smallVals(1) = 10
        ' End If
        XMLizedData = "<invoke name=""vbColorFoot"" returntype=""xml"">" _
                    & "<arguments>" _
                        & "<array>" _
                            & "<property id=""0""><number>" & largeVals(1) & "</number></property>" _
                            & "<property id=""1""><number>" & largeVals(2) & "</number></property>" _
                            & "<property id=""2""><number>" & mediumVals(1) & "</number></property>" _
                            & "<property id=""3""><number>" & mediumVals(2) & "</number></property>" _
                            & "<property id=""4""><number>" & mediumVals(3) & "</number></property>" _
                            & "<property id=""5""><number>" & mediumVals(4) & "</number></property>" _
                            & "<property id=""6""><number>" & smallVals(1) & "</number></property>" _
                            & "<property id=""7""><number>" & smallVals(2) & "</number></property>" _
                            & "<property id=""8""><number>" & smallVals(3) & "</number></property>" _
                            & "<property id=""9""><number>" & smallVals(4) & "</number></property>" _
                            & "<property id=""10""><number>" & smallVals(5) & "</number></property>" _
                            & "<property id=""11""><number>" & smallVals(6) & "</number></property>" _
                            & "<property id=""12""><number>" & smallVals(7) & "</number></property>" _
                            & "<property id=""13""><number>" & smallVals(8) & "</number></property>" _
                            & "<property id=""14""><number>" & smallVals(9) & "</number></property>" _
                            & "<property id=""15""><number>" & smallVals(10) & "</number></property>" _
                            & "<property id=""16""><number>" & smallVals(11) & "</number></property>" _
                            & "<property id=""17""><number>" & smallVals(12) & "</number></property>" _
                               & "<property id=""17""><number>" & flexVals(1) / 2 & "</number></property>" _
                             & "<property id=""17""><number>" & flexVals(1) & "</number></property>" _
                              & "<property id=""17""><number>" & flexVals(1) / 2 & "</number></property>" _
                               & "<property id=""17""><number>" & flexVals(2) / 2 & "</number></property>" _
                                & "<property id=""17""><number>" & flexVals(2) & "</number></property>" _
                                 & "<property id=""17""><number>" & flexVals(2) / 2 & "</number></property>" _
                                  & "<property id=""17""><number>" & flexVals(3) / 2 & "</number></property>" _
                                   & "<property id=""17""><number>" & flexVals(3) & "</number></property>" _
                                    & "<property id=""17""><number>" & flexVals(3) / 2 & "</number></property>" _
                        & "</array>" _
                        & "</arguments></invoke>" '<string>hello</string><string>world</string>
        'MsgBox(XMLizedData)
        GUI.AxShockwaveFlash1.CallFunction(XMLizedData)
    End Function


    Private Sub chkDTR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDTR.Click
        If Not (moRS232 Is Nothing) Then
            moRS232.Dtr = chkDTR.CheckState = CheckState.Checked
        End If
    End Sub

    Private Sub chkRTS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRTS.Click
        If Not (moRS232 Is Nothing) Then
            moRS232.Rts = chkRTS.CheckState = CheckState.Checked
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    Private Sub optCom1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCom1.CheckedChanged, optCom2.CheckedChanged
        If (sender Is optCom1) Then
            miComPort = 1
        Else
            miComPort = CInt(Me.optCom2.Text.Substring(4))
        End If
    End Sub

    Private Sub linkAuthor_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkAuthor.LinkClicked
        System.Diagnostics.Process.Start("mailto:corrado@mvps.org")
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '===================================================
        '
        '	Description	:	Fills form items
        '	Created			:	28/02/2002 - 10:33:20
        '
        '						*Parameters Info*
        '
        '	Notes				:
        '===================================================

        Dim no As Integer = CInt(InputBox("Enter COM Port #", , "5").ToString)
        optCom2.Text = "COM " & no
        miComPort = no
        optCom2.Checked = True

        'Me.btnOpenCom.PerformClick()

        cboStatusLine.Items.Add("CTS")
        cboStatusLine.Items.Add("DSR")
        cboStatusLine.Items.Add("RI")
        cboStatusLine.Items.Add("CD")

    End Sub

    Private Sub btnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheck.Click
        '===================================================
        '												
        '	Description	:	Check passed status line
        '	Created			:	28/02/2002 - 10:35:54
        '
        '						*Parameters Info*
        '
        '	Notes				:
        '===================================================
        If Not moRS232 Is Nothing Then
            Dim bState As Boolean
            Select Case cboStatusLine.Text
                Case "CTS"
                    bState = moRS232.CheckLineStatus(Rs232.ModemStatusBits.ClearToSendOn)
                Case "DSR"
                    bState = moRS232.CheckLineStatus(Rs232.ModemStatusBits.DataSetReadyOn)
                Case "RI"
                    bState = moRS232.CheckLineStatus(Rs232.ModemStatusBits.RingIndicatorOn)
                Case "CD"
                    bState = moRS232.CheckLineStatus(Rs232.ModemStatusBits.CarrierDetect)
            End Select
            MessageBox.Show("Selected line is " & IIf(bState, "On", "Off").ToString, "Check line status")
        End If
    End Sub

    Private Sub btnAsyncTx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '// Clears Rx textbox
        lbAsync.Items.Clear()
        txtRx.Text = String.Empty
        txtRx.Refresh()
        lbHex.Items.Clear()
        mlTicks = DateTime.Now.Ticks
        'moRS232.AsyncWrite(Chr(4) & Chr(3) & Chr(0) & Chr(0) & Chr(0) & Chr(16) & Chr(68) & Chr(83))
        lbAsync.Items.Add("Tx Started at ticks: " & mlTicks.ToString)
        If chkAutorx.Checked Then btnAsync_Click(Nothing, Nothing)
    End Sub

    Private Sub btnAsync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            lbHex.Text = String.Empty
            lbHex.Refresh()
            Dim lTicks As Long = DateTime.Now.Ticks
            lbAsync.Items.Add("Rx Started at ticks: " & lTicks.ToString & " (" & (lTicks - mlTicks).ToString & ")")
        Catch Ex As Exception
            txtRx.BackColor = Color.Red
            txtRx.ForeColor = Color.White
            txtRx.Text = "Error occurred " & Ex.Message & "  data fetched: " & moRS232.InputStreamString
        End Try
    End Sub

    Private Sub moRS232_CommEvent(ByVal source As Rs232, ByVal Mask As Rs232.EventMasks) Handles moRS232.CommEvent
        '===================================================
        '												©2003 www.codeworks.it All rights reserved
        '
        '	Description	:	Events raised when a comunication event occurs
        '	Created			:	15/07/03 - 15:13:46
        '	Author			:	Corrado Cavalli
        '
        '						*Parameters Info*
        '
        '	Notes				:	
        '===================================================
        Debug.Assert(Me.InvokeRequired = False)

        Dim iPnt As Int32, sBuf As String, Buffer() As Byte
        Debug.Assert(Me.InvokeRequired = False)
        lbAsync.Items.Add("Mask: " & Mask.ToString)
        If (Mask And Rs232.EventMasks.RxChar) > 0 Then
            lbHex.Items.Add("Received data: " & source.InputStreamString)
            Buffer = source.InputStream
            For iPnt = 0 To Buffer.Length - 1
                lbHex.Items.Add(iPnt.ToString & ControlChars.Tab & String.Format("0x{0}", Buffer(iPnt).ToString("X")))
                'MsgBox(Buffer(iPnt).ToString)
            Next
            lbHex.SelectedIndex = lbHex.Items.Count - 1
        End If
        lbAsync.SelectedIndex = lbAsync.Items.Count - 1
    End Sub

#Region "UI update routine"
#End Region

    Private Sub chkEvents_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEvents.CheckedChanged
        '===================================================
        '												©2003 www.codeworks.it All rights reserved
        '
        '	Description	:	Set state of notification events
        '	Created			:	16/07/03 - 9:44:05
        '	Author			:	Corrado Cavalli
        '
        '						*Parameters Info*
        '
        '	Notes				:
        '===================================================
        If Not moRS232 Is Nothing Then
            If txtBytes2Read.Text.Length = 0 Then
                moRS232.RxBufferThreshold = 1
            Else
                moRS232.RxBufferThreshold = Int32.Parse(txtBytes2Read.Text)
            End If
            If chkEvents.Checked Then
                moRS232.EnableEvents()
            Else
                moRS232.DisableEvents()
            End If
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("http://www.codeworks.it/net/index.htm")
    End Sub


    Private Sub lbAsync_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbAsync.SelectedIndexChanged

    End Sub

    Private Sub lbAsync_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAsync.DoubleClick
        lbAsync.Items.Clear()
    End Sub


    Private Sub lbHex_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbHex.DoubleClick
        lbHex.Items.Clear()
    End Sub

    Private Sub txtRx_BackColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRx.BackColorChanged

    End Sub

    Private Sub txtRx_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRx.DoubleClick
        txtRx.Text = String.Empty
    End Sub


    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        Try
            If Rs232.IsPortAvailable(Int32.Parse(txtPortNum.Text)) Then
                MessageBox.Show("Port available", "Port test", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Port NOT available", "Port test", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Port test failed", "Port test", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnInBuffer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInBuffer.Click
        lblInBuffer.Text = moRS232.InBufferCount.ToString()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'MsgBox("WHAT!")
        Me.btnRx.PerformClick()
    End Sub

    Private Sub txtRx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRx.TextChanged

    End Sub
End Class

