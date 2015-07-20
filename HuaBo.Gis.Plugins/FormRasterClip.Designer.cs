namespace HuaBo.Gis.Plugins
{
    partial class FormRasterClip
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_cmbDataDataset = new System.Windows.Forms.ComboBox();
            this.m_cmbDataDatasource = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.m_labelResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_numerX = new System.Windows.Forms.NumericUpDown();
            this.m_numerY = new System.Windows.Forms.NumericUpDown();
            this.m_btnStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numerX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numerY)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_cmbDataDataset);
            this.groupBox1.Controls.Add(this.m_cmbDataDatasource);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 109);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据";
            // 
            // m_cmbDataDataset
            // 
            this.m_cmbDataDataset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbDataDataset.FormattingEnabled = true;
            this.m_cmbDataDataset.Location = new System.Drawing.Point(101, 63);
            this.m_cmbDataDataset.Name = "m_cmbDataDataset";
            this.m_cmbDataDataset.Size = new System.Drawing.Size(121, 20);
            this.m_cmbDataDataset.TabIndex = 3;
            // 
            // m_cmbDataDatasource
            // 
            this.m_cmbDataDatasource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbDataDatasource.FormattingEnabled = true;
            this.m_cmbDataDatasource.Location = new System.Drawing.Point(101, 26);
            this.m_cmbDataDatasource.Name = "m_cmbDataDatasource";
            this.m_cmbDataDatasource.Size = new System.Drawing.Size(121, 20);
            this.m_cmbDataDatasource.TabIndex = 2;
            this.m_cmbDataDatasource.SelectedIndexChanged += new System.EventHandler(this.m_cmbDataDatasource_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据集";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据源";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(50, 226);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(225, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // m_labelResult
            // 
            this.m_labelResult.AutoSize = true;
            this.m_labelResult.Location = new System.Drawing.Point(3, 233);
            this.m_labelResult.Name = "m_labelResult";
            this.m_labelResult.Size = new System.Drawing.Size(41, 12);
            this.m_labelResult.TabIndex = 6;
            this.m_labelResult.Text = "进度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "竖";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "横";
            // 
            // m_numerX
            // 
            this.m_numerX.Location = new System.Drawing.Point(103, 138);
            this.m_numerX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_numerX.Name = "m_numerX";
            this.m_numerX.Size = new System.Drawing.Size(120, 21);
            this.m_numerX.TabIndex = 8;
            this.m_numerX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_numerY
            // 
            this.m_numerY.Location = new System.Drawing.Point(104, 175);
            this.m_numerY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_numerY.Name = "m_numerY";
            this.m_numerY.Size = new System.Drawing.Size(120, 21);
            this.m_numerY.TabIndex = 9;
            this.m_numerY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_btnStart
            // 
            this.m_btnStart.Location = new System.Drawing.Point(50, 279);
            this.m_btnStart.Name = "m_btnStart";
            this.m_btnStart.Size = new System.Drawing.Size(175, 23);
            this.m_btnStart.TabIndex = 10;
            this.m_btnStart.Text = "开始";
            this.m_btnStart.UseVisualStyleBackColor = true;
            this.m_btnStart.Click += new System.EventHandler(this.m_btnStart_Click);
            // 
            // FormRasterClip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 335);
            this.Controls.Add(this.m_btnStart);
            this.Controls.Add(this.m_numerY);
            this.Controls.Add(this.m_numerX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_labelResult);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormRasterClip";
            this.Text = "FormRasterClip";
            this.Load += new System.EventHandler(this.FormRasterClip_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numerX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numerY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox m_cmbDataDataset;
        private System.Windows.Forms.ComboBox m_cmbDataDatasource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label m_labelResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown m_numerX;
        private System.Windows.Forms.NumericUpDown m_numerY;
        private System.Windows.Forms.Button m_btnStart;
    }
}