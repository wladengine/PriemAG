namespace Priem
{
    partial class SelectVedForLoad
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
            this.label3 = new System.Windows.Forms.Label();
            this.lblProtocolNum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbVed = new System.Windows.Forms.ComboBox();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Основа обучения";
            // 
            // lblProtocolNum
            // 
            this.lblProtocolNum.AutoSize = true;
            this.lblProtocolNum.Location = new System.Drawing.Point(12, 63);
            this.lblProtocolNum.Name = "lblProtocolNum";
            this.lblProtocolNum.Size = new System.Drawing.Size(63, 13);
            this.lblProtocolNum.TabIndex = 51;
            this.lblProtocolNum.Text = "Ведомость";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Факультет";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(175, 125);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 23);
            this.btnOK.TabIndex = 56;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbVed
            // 
            this.cbVed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVed.FormattingEnabled = true;
            this.cbVed.Location = new System.Drawing.Point(12, 79);
            this.cbVed.Name = "cbVed";
            this.cbVed.Size = new System.Drawing.Size(421, 21);
            this.cbVed.TabIndex = 57;
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(277, 37);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(156, 21);
            this.cbStudyBasis.TabIndex = 58;
            // 
            // cbFaculty
            // 
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(12, 37);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(250, 21);
            this.cbFaculty.TabIndex = 59;
            // 
            // SelectVedForLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 160);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.cbStudyBasis);
            this.Controls.Add(this.cbVed);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblProtocolNum);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SelectVedForLoad";
            this.Text = "Выбрать ведомость";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label lblProtocolNum;        
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbVed;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.ComboBox cbFaculty;
    }
}