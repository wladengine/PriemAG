namespace Priem
{
    partial class BackUpFix
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
            this.cbStudyLevelGroup = new System.Windows.Forms.ComboBox();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbStudyLevelGroup
            // 
            this.cbStudyLevelGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyLevelGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyLevelGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyLevelGroup.FormattingEnabled = true;
            this.cbStudyLevelGroup.Location = new System.Drawing.Point(12, 31);
            this.cbStudyLevelGroup.Name = "cbStudyLevelGroup";
            this.cbStudyLevelGroup.Size = new System.Drawing.Size(163, 21);
            this.cbStudyLevelGroup.TabIndex = 125;
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyBasis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(12, 77);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(163, 21);
            this.cbStudyBasis.TabIndex = 124;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 123;
            this.label2.Text = "Уровень";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 122;
            this.label1.Text = "Основа обучения";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(55, 123);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 121;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // BackUpFix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 160);
            this.Controls.Add(this.cbStudyLevelGroup);
            this.Controls.Add(this.cbStudyBasis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Name = "BackUpFix";
            this.Text = "BackUpFix";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbStudyLevelGroup;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
    }
}