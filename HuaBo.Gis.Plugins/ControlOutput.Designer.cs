namespace HuaBo.Gis.Plugins
{
    partial class ControlOutput
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_textEdit = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.m_textEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // m_textEdit
            // 
            this.m_textEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_textEdit.Location = new System.Drawing.Point(0, 0);
            this.m_textEdit.Name = "m_textEdit";
            this.m_textEdit.Properties.ReadOnly = true;
            this.m_textEdit.Size = new System.Drawing.Size(688, 274);
            this.m_textEdit.TabIndex = 1;
            this.m_textEdit.UseOptimizedRendering = true;
            // 
            // ControlOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_textEdit);
            this.Name = "ControlOutput";
            this.Size = new System.Drawing.Size(688, 274);
            ((System.ComponentModel.ISupportInitialize)(this.m_textEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit m_textEdit;

    }
}
