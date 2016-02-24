namespace Priem
{
    partial class SelectVed
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
            this.btnOK = new System.Windows.Forms.Button();
            this.tbVedNum = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbVedName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbVedType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbVedNum
            // 
            this.tbVedNum.Location = new System.Drawing.Point(12, 25);
            this.tbVedNum.Name = "tbVedNum";
            this.tbVedNum.Size = new System.Drawing.Size(75, 20);
            this.tbVedNum.TabIndex = 0;
            this.tbVedNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbVedNum_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(211, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbVedName);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 72);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ведомость";
            // 
            // tbVedName
            // 
            this.tbVedName.Location = new System.Drawing.Point(6, 19);
            this.tbVedName.Multiline = true;
            this.tbVedName.Name = "tbVedName";
            this.tbVedName.ReadOnly = true;
            this.tbVedName.Size = new System.Drawing.Size(262, 47);
            this.tbVedName.TabIndex = 0;
            this.tbVedName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Считайте любой штрихкод для определения ведомости";
            // 
            // cbVedType
            // 
            this.cbVedType.FormattingEnabled = true;
            this.cbVedType.Location = new System.Drawing.Point(140, 139);
            this.cbVedType.Name = "cbVedType";
            this.cbVedType.Size = new System.Drawing.Size(146, 21);
            this.cbVedType.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Оценки за какую часть";
            // 
            // SelectVed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 206);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbVedType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbVedNum);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SelectVed";
            this.Text = "Ведомость";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbVedNum;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbVedName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbVedType;
        private System.Windows.Forms.Label label2;
    }
}