namespace Priem
{
    partial class CardExamName
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAcronym = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNamePad = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(254, 163);
            // 
            // btnSaveChange
            // 
            this.btnSaveChange.Location = new System.Drawing.Point(12, 162);
            // 
            // btnSaveAsNew
            // 
            this.btnSaveAsNew.Location = new System.Drawing.Point(117, 162);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 34);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(323, 20);
            this.tbName.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Сокращение";
            // 
            // tbAcronym
            // 
            this.tbAcronym.Location = new System.Drawing.Point(12, 79);
            this.tbAcronym.Name = "tbAcronym";
            this.tbAcronym.Size = new System.Drawing.Size(323, 20);
            this.tbAcronym.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Название в род. падеже";
            // 
            // tbNamePad
            // 
            this.tbNamePad.Location = new System.Drawing.Point(12, 125);
            this.tbNamePad.Name = "tbNamePad";
            this.tbNamePad.Size = new System.Drawing.Size(323, 20);
            this.tbNamePad.TabIndex = 29;
            // 
            // CardExamName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 197);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbNamePad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbAcronym);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
            this.Name = "CardExamName";
            this.Text = "CardExamName";
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnSaveChange, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnSaveAsNew, 0);
            this.Controls.SetChildIndex(this.tbAcronym, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tbNamePad, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAcronym;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNamePad;
    }
}