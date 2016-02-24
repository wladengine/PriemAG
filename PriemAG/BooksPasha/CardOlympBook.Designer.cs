namespace Priem
{
    partial class CardOlympBook
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
            this.cbOlympType = new System.Windows.Forms.ComboBox();
            this.cbOlympSubject = new System.Windows.Forms.ComboBox();
            this.cbOlympLevel = new System.Windows.Forms.ComboBox();
            this.cbOlympName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(517, 150);
            // 
            // btnSaveChange
            // 
            this.btnSaveChange.Location = new System.Drawing.Point(12, 149);
            // 
            // btnSaveAsNew
            // 
            this.btnSaveAsNew.Location = new System.Drawing.Point(380, 149);
            // 
            // cbOlympType
            // 
            this.cbOlympType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOlympType.FormattingEnabled = true;
            this.cbOlympType.Location = new System.Drawing.Point(133, 17);
            this.cbOlympType.Name = "cbOlympType";
            this.cbOlympType.Size = new System.Drawing.Size(465, 21);
            this.cbOlympType.TabIndex = 25;
            // 
            // cbOlympSubject
            // 
            this.cbOlympSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOlympSubject.FormattingEnabled = true;
            this.cbOlympSubject.Location = new System.Drawing.Point(133, 77);
            this.cbOlympSubject.Name = "cbOlympSubject";
            this.cbOlympSubject.Size = new System.Drawing.Size(465, 21);
            this.cbOlympSubject.TabIndex = 26;
            // 
            // cbOlympLevel
            // 
            this.cbOlympLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOlympLevel.FormattingEnabled = true;
            this.cbOlympLevel.Location = new System.Drawing.Point(133, 106);
            this.cbOlympLevel.Name = "cbOlympLevel";
            this.cbOlympLevel.Size = new System.Drawing.Size(465, 21);
            this.cbOlympLevel.TabIndex = 27;
            // 
            // cbOlympName
            // 
            this.cbOlympName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOlympName.FormattingEnabled = true;
            this.cbOlympName.Location = new System.Drawing.Point(133, 47);
            this.cbOlympName.Name = "cbOlympName";
            this.cbOlympName.Size = new System.Drawing.Size(465, 21);
            this.cbOlympName.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 127;
            this.label5.Text = "Название олимпиады";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Уровень олимпиады";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 125;
            this.label2.Text = "Предмет олимпиады";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 124;
            this.label1.Text = "Вид олимпиады";
            // 
            // CardOlympBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 184);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbOlympName);
            this.Controls.Add(this.cbOlympLevel);
            this.Controls.Add(this.cbOlympSubject);
            this.Controls.Add(this.cbOlympType);
            this.Name = "CardOlympBook";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.cbOlympType, 0);
            this.Controls.SetChildIndex(this.btnSaveChange, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnSaveAsNew, 0);
            this.Controls.SetChildIndex(this.cbOlympSubject, 0);
            this.Controls.SetChildIndex(this.cbOlympLevel, 0);
            this.Controls.SetChildIndex(this.cbOlympName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbOlympType;
        private System.Windows.Forms.ComboBox cbOlympSubject;
        private System.Windows.Forms.ComboBox cbOlympLevel;
        private System.Windows.Forms.ComboBox cbOlympName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}