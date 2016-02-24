namespace Priem
{
    partial class Decriptor
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
            this.tbPersonName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbVedNum = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPersonName
            // 
            this.tbPersonName.Location = new System.Drawing.Point(6, 19);
            this.tbPersonName.Multiline = true;
            this.tbPersonName.Name = "tbPersonName";
            this.tbPersonName.ReadOnly = true;
            this.tbPersonName.Size = new System.Drawing.Size(262, 47);
            this.tbPersonName.TabIndex = 0;
            this.tbPersonName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Считайте штрихкод";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbPersonName);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 72);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ведомость";
            // 
            // tbVedNum
            // 
            this.tbVedNum.Location = new System.Drawing.Point(12, 34);
            this.tbVedNum.Name = "tbVedNum";
            this.tbVedNum.Size = new System.Drawing.Size(101, 20);
            this.tbVedNum.TabIndex = 5;
            this.tbVedNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbVedNum_KeyDown);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(211, 143);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Decriptor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 178);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbVedNum);
            this.Controls.Add(this.btnClose);
            this.Name = "Decriptor";
            this.Text = "Расшифровка";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPersonName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbVedNum;
        private System.Windows.Forms.Button btnClose;
    }
}