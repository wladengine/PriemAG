namespace Priem
{
    partial class CardExam
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbExamName = new System.Windows.Forms.ComboBox();
            this.chbIsAdd = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(254, 117);
            // 
            // btnSaveChange
            // 
            this.btnSaveChange.Location = new System.Drawing.Point(12, 116);
            // 
            // btnSaveAsNew
            // 
            this.btnSaveAsNew.Location = new System.Drawing.Point(117, 116);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Название экзамена";
            // 
            // cbExamName
            // 
            this.cbExamName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExamName.FormattingEnabled = true;
            this.cbExamName.Location = new System.Drawing.Point(4, 34);
            this.cbExamName.Name = "cbExamName";
            this.cbExamName.Size = new System.Drawing.Size(331, 21);
            this.cbExamName.TabIndex = 34;
            // 
            // chbIsAdd
            // 
            this.chbIsAdd.AutoSize = true;
            this.chbIsAdd.Location = new System.Drawing.Point(4, 75);
            this.chbIsAdd.Name = "chbIsAdd";
            this.chbIsAdd.Size = new System.Drawing.Size(114, 17);
            this.chbIsAdd.TabIndex = 36;
            this.chbIsAdd.Text = "Дополнительный";
            this.chbIsAdd.UseVisualStyleBackColor = true;
            // 
            // CardExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 151);
            this.Controls.Add(this.chbIsAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbExamName);
            this.Name = "CardExam";
            this.Text = "ExamCard";
            this.Controls.SetChildIndex(this.btnSaveChange, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnSaveAsNew, 0);
            this.Controls.SetChildIndex(this.cbExamName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.chbIsAdd, 0);
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbExamName;
        private System.Windows.Forms.CheckBox chbIsAdd;
    }
}