namespace Priem
{
    partial class CardExamInEntry
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
            this.cbExam = new System.Windows.Forms.ComboBox();
            this.chbIsProfil = new System.Windows.Forms.CheckBox();
            this.tbEgeMin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chbToAllStudyBasis = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(262, 163);
            // 
            // btnSaveChange
            // 
            this.btnSaveChange.Location = new System.Drawing.Point(12, 163);
            // 
            // btnSaveAsNew
            // 
            this.btnSaveAsNew.Location = new System.Drawing.Point(118, 163);
            // 
            // cbExam
            // 
            this.cbExam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExam.FormattingEnabled = true;
            this.cbExam.Location = new System.Drawing.Point(12, 32);
            this.cbExam.Name = "cbExam";
            this.cbExam.Size = new System.Drawing.Size(331, 21);
            this.cbExam.TabIndex = 29;
            // 
            // chbIsProfil
            // 
            this.chbIsProfil.AutoSize = true;
            this.chbIsProfil.Location = new System.Drawing.Point(12, 59);
            this.chbIsProfil.Name = "chbIsProfil";
            this.chbIsProfil.Size = new System.Drawing.Size(92, 17);
            this.chbIsProfil.TabIndex = 30;
            this.chbIsProfil.Text = "Профильный";
            this.chbIsProfil.UseVisualStyleBackColor = true;
            // 
            // tbEgeMin
            // 
            this.tbEgeMin.Location = new System.Drawing.Point(12, 123);
            this.tbEgeMin.Name = "tbEgeMin";
            this.tbEgeMin.Size = new System.Drawing.Size(100, 20);
            this.tbEgeMin.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Минимальная планка ЕГЭ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Экзамен";
            // 
            // chbToAllStudyBasis
            // 
            this.chbToAllStudyBasis.AutoSize = true;
            this.chbToAllStudyBasis.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbToAllStudyBasis.Location = new System.Drawing.Point(215, 75);
            this.chbToAllStudyBasis.Name = "chbToAllStudyBasis";
            this.chbToAllStudyBasis.Size = new System.Drawing.Size(128, 30);
            this.chbToAllStudyBasis.TabIndex = 35;
            this.chbToAllStudyBasis.Text = "размножить \r\nна основы обучения";
            this.chbToAllStudyBasis.UseVisualStyleBackColor = true;
            // 
            // CardExamInEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 198);
            this.Controls.Add(this.chbToAllStudyBasis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbEgeMin);
            this.Controls.Add(this.chbIsProfil);
            this.Controls.Add(this.cbExam);
            this.Name = "CardExamInEntry";
            this.Text = "CardExamInEntry";
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnSaveAsNew, 0);
            this.Controls.SetChildIndex(this.btnSaveChange, 0);
            this.Controls.SetChildIndex(this.cbExam, 0);
            this.Controls.SetChildIndex(this.chbIsProfil, 0);
            this.Controls.SetChildIndex(this.tbEgeMin, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.chbToAllStudyBasis, 0);
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbExam;
        private System.Windows.Forms.CheckBox chbIsProfil;
        private System.Windows.Forms.TextBox tbEgeMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbToAllStudyBasis;
    }
}