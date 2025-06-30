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
        Me.lblGroupAmount = New System.Windows.Forms.Label()
        Me.txtbxInclusionExclusion = New System.Windows.Forms.TextBox()
        Me.txtbxDisplay = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.domGroupAmount = New System.Windows.Forms.DomainUpDown()
        Me.btnSort = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblGroupAmount
        '
        Me.lblGroupAmount.AutoSize = True
        Me.lblGroupAmount.Location = New System.Drawing.Point(69, 49)
        Me.lblGroupAmount.Name = "lblGroupAmount"
        Me.lblGroupAmount.Size = New System.Drawing.Size(150, 25)
        Me.lblGroupAmount.TabIndex = 16
        Me.lblGroupAmount.Text = "Group Amount"
        '
        'txtbxInclusionExclusion
        '
        Me.txtbxInclusionExclusion.Location = New System.Drawing.Point(343, 210)
        Me.txtbxInclusionExclusion.Multiline = True
        Me.txtbxInclusionExclusion.Name = "txtbxInclusionExclusion"
        Me.txtbxInclusionExclusion.ReadOnly = True
        Me.txtbxInclusionExclusion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtbxInclusionExclusion.Size = New System.Drawing.Size(261, 302)
        Me.txtbxInclusionExclusion.TabIndex = 15
        '
        'txtbxDisplay
        '
        Me.txtbxDisplay.Location = New System.Drawing.Point(701, 62)
        Me.txtbxDisplay.Multiline = True
        Me.txtbxDisplay.Name = "txtbxDisplay"
        Me.txtbxDisplay.ReadOnly = True
        Me.txtbxDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtbxDisplay.Size = New System.Drawing.Size(384, 450)
        Me.txtbxDisplay.TabIndex = 14
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(74, 137)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(131, 109)
        Me.btnExport.TabIndex = 13
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(74, 285)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(131, 109)
        Me.btnImport.TabIndex = 12
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.Location = New System.Drawing.Point(74, 423)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(131, 109)
        Me.btnHelp.TabIndex = 11
        Me.btnHelp.Text = "Help"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'domGroupAmount
        '
        Me.domGroupAmount.Location = New System.Drawing.Point(74, 88)
        Me.domGroupAmount.Name = "domGroupAmount"
        Me.domGroupAmount.Size = New System.Drawing.Size(131, 31)
        Me.domGroupAmount.TabIndex = 10
        Me.domGroupAmount.Text = "1"
        Me.domGroupAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSort
        '
        Me.btnSort.Location = New System.Drawing.Point(343, 62)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(261, 117)
        Me.btnSort.TabIndex = 9
        Me.btnSort.Text = "Sort"
        Me.btnSort.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1154, 581)
        Me.Controls.Add(Me.lblGroupAmount)
        Me.Controls.Add(Me.txtbxInclusionExclusion)
        Me.Controls.Add(Me.txtbxDisplay)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.domGroupAmount)
        Me.Controls.Add(Me.btnSort)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblGroupAmount As Label
    Friend WithEvents txtbxInclusionExclusion As TextBox
    Friend WithEvents txtbxDisplay As TextBox
    Friend WithEvents btnExport As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents btnHelp As Button
    Friend WithEvents domGroupAmount As DomainUpDown
    Friend WithEvents btnSort As Button
End Class
