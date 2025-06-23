<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GroupPreferentialSorter
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnSort = New System.Windows.Forms.Button()
        Me.domGroupAmount = New System.Windows.Forms.DomainUpDown()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.txtbxDisplay = New System.Windows.Forms.TextBox()
        Me.txtbxInclusionExclusion = New System.Windows.Forms.TextBox()
        Me.lblGroupAmount = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnSort
        '
        Me.btnSort.Location = New System.Drawing.Point(305, 54)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(261, 117)
        Me.btnSort.TabIndex = 0
        Me.btnSort.Text = "Sort"
        Me.btnSort.UseVisualStyleBackColor = True
        '
        'domGroupAmount
        '
        Me.domGroupAmount.Location = New System.Drawing.Point(36, 80)
        Me.domGroupAmount.Name = "domGroupAmount"
        Me.domGroupAmount.Size = New System.Drawing.Size(131, 31)
        Me.domGroupAmount.TabIndex = 1
        Me.domGroupAmount.Text = "1"
        Me.domGroupAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnHelp
        '
        Me.btnHelp.Location = New System.Drawing.Point(36, 415)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(131, 109)
        Me.btnHelp.TabIndex = 3
        Me.btnHelp.Text = "Help"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(36, 277)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(131, 109)
        Me.btnImport.TabIndex = 4
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(36, 129)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(131, 109)
        Me.btnExport.TabIndex = 5
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'txtbxDisplay
        '
        Me.txtbxDisplay.Location = New System.Drawing.Point(663, 54)
        Me.txtbxDisplay.Multiline = True
        Me.txtbxDisplay.Name = "txtbxDisplay"
        Me.txtbxDisplay.ReadOnly = True
        Me.txtbxDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtbxDisplay.Size = New System.Drawing.Size(384, 450)
        Me.txtbxDisplay.TabIndex = 6
        '
        'txtbxInclusionExclusion
        '
        Me.txtbxInclusionExclusion.Location = New System.Drawing.Point(305, 202)
        Me.txtbxInclusionExclusion.Multiline = True
        Me.txtbxInclusionExclusion.Name = "txtbxInclusionExclusion"
        Me.txtbxInclusionExclusion.ReadOnly = True
        Me.txtbxInclusionExclusion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtbxInclusionExclusion.Size = New System.Drawing.Size(261, 302)
        Me.txtbxInclusionExclusion.TabIndex = 7
        '
        'lblGroupAmount
        '
        Me.lblGroupAmount.AutoSize = True
        Me.lblGroupAmount.Location = New System.Drawing.Point(31, 41)
        Me.lblGroupAmount.Name = "lblGroupAmount"
        Me.lblGroupAmount.Size = New System.Drawing.Size(150, 25)
        Me.lblGroupAmount.TabIndex = 8
        Me.lblGroupAmount.Text = "Group Amount"
        '
        'GroupPreferentialSorter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1128, 558)
        Me.Controls.Add(Me.lblGroupAmount)
        Me.Controls.Add(Me.txtbxInclusionExclusion)
        Me.Controls.Add(Me.txtbxDisplay)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.domGroupAmount)
        Me.Controls.Add(Me.btnSort)
        Me.Name = "GroupPreferentialSorter"
        Me.Text = "Group Preferential Sorter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSort As Button
    Friend WithEvents domGroupAmount As DomainUpDown
    Friend WithEvents btnHelp As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents btnExport As Button
    Friend WithEvents txtbxDisplay As TextBox
    Friend WithEvents txtbxInclusionExclusion As TextBox
    Friend WithEvents lblGroupAmount As Label
End Class
