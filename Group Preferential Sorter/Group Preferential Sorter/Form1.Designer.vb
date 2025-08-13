<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.lblGroupAmount = New System.Windows.Forms.Label()
        Me.txtbxDisplay = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnSort = New System.Windows.Forms.Button()
        Me.numUpDown = New System.Windows.Forms.NumericUpDown()
        Me.btnFun = New System.Windows.Forms.Button()
        Me.pnlSideLeft = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        CType(Me.numUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSideLeft.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblGroupAmount
        '
        Me.lblGroupAmount.AutoSize = True
        Me.lblGroupAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.lblGroupAmount.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblGroupAmount.Location = New System.Drawing.Point(59, 62)
        Me.lblGroupAmount.Name = "lblGroupAmount"
        Me.lblGroupAmount.Size = New System.Drawing.Size(154, 26)
        Me.lblGroupAmount.TabIndex = 16
        Me.lblGroupAmount.Text = "Group Amount"
        '
        'txtbxDisplay
        '
        Me.txtbxDisplay.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.txtbxDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtbxDisplay.Location = New System.Drawing.Point(723, 103)
        Me.txtbxDisplay.Multiline = True
        Me.txtbxDisplay.Name = "txtbxDisplay"
        Me.txtbxDisplay.ReadOnly = True
        Me.txtbxDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtbxDisplay.Size = New System.Drawing.Size(384, 450)
        Me.txtbxDisplay.TabIndex = 14
        '
        'btnExport
        '
        Me.btnExport.BackgroundImage = CType(resources.GetObject("btnExport.BackgroundImage"), System.Drawing.Image)
        Me.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnExport.Location = New System.Drawing.Point(64, 180)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(119, 107)
        Me.btnExport.TabIndex = 13
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.BackgroundImage = CType(resources.GetObject("btnImport.BackgroundImage"), System.Drawing.Image)
        Me.btnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnImport.Location = New System.Drawing.Point(64, 310)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(119, 107)
        Me.btnImport.TabIndex = 12
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.BackgroundImage = CType(resources.GetObject("btnHelp.BackgroundImage"), System.Drawing.Image)
        Me.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnHelp.Location = New System.Drawing.Point(64, 446)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(119, 107)
        Me.btnHelp.TabIndex = 11
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnSort
        '
        Me.btnSort.BackColor = System.Drawing.Color.Black
        Me.btnSort.ForeColor = System.Drawing.Color.White
        Me.btnSort.Location = New System.Drawing.Point(344, 264)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(261, 117)
        Me.btnSort.TabIndex = 9
        Me.btnSort.Text = "Sort"
        Me.btnSort.UseVisualStyleBackColor = False
        '
        'numUpDown
        '
        Me.numUpDown.BackColor = System.Drawing.Color.Black
        Me.numUpDown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.numUpDown.ForeColor = System.Drawing.Color.White
        Me.numUpDown.Location = New System.Drawing.Point(64, 112)
        Me.numUpDown.Name = "numUpDown"
        Me.numUpDown.Size = New System.Drawing.Size(119, 27)
        Me.numUpDown.TabIndex = 17
        '
        'btnFun
        '
        Me.btnFun.BackColor = System.Drawing.Color.Black
        Me.btnFun.ForeColor = System.Drawing.Color.White
        Me.btnFun.Location = New System.Drawing.Point(390, 446)
        Me.btnFun.Name = "btnFun"
        Me.btnFun.Size = New System.Drawing.Size(171, 37)
        Me.btnFun.TabIndex = 18
        Me.btnFun.Text = "Fun Button"
        Me.btnFun.UseVisualStyleBackColor = False
        '
        'pnlSideLeft
        '
        Me.pnlSideLeft.BackColor = System.Drawing.Color.DimGray
        Me.pnlSideLeft.Controls.Add(Me.btnHelp)
        Me.pnlSideLeft.Controls.Add(Me.btnImport)
        Me.pnlSideLeft.Controls.Add(Me.btnExport)
        Me.pnlSideLeft.Controls.Add(Me.numUpDown)
        Me.pnlSideLeft.Controls.Add(Me.lblGroupAmount)
        Me.pnlSideLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSideLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlSideLeft.Name = "pnlSideLeft"
        Me.pnlSideLeft.Size = New System.Drawing.Size(254, 581)
        Me.pnlSideLeft.TabIndex = 19
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(279, 25)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(600, 63)
        Me.lblTitle.TabIndex = 20
        Me.lblTitle.Text = "Group Preferntial Sorter"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1154, 581)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnSort)
        Me.Controls.Add(Me.pnlSideLeft)
        Me.Controls.Add(Me.btnFun)
        Me.Controls.Add(Me.txtbxDisplay)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.numUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSideLeft.ResumeLayout(False)
        Me.pnlSideLeft.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblGroupAmount As Label
    Friend WithEvents txtbxDisplay As TextBox
    Friend WithEvents btnExport As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents btnHelp As Button
    Friend WithEvents btnSort As Button
    Friend WithEvents numUpDown As NumericUpDown
    Friend WithEvents btnFun As Button
    Friend WithEvents pnlSideLeft As Panel
    Friend WithEvents lblTitle As Label
End Class
