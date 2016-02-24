namespace Priem
{
    partial class AddMark
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
            this.tbMark = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMark
            // 
            this.tbMark.Location = new System.Drawing.Point(7, 12);
            this.tbMark.Name = "tbMark";
            this.tbMark.Size = new System.Drawing.Size(101, 20);
            this.tbMark.TabIndex = 0;
            this.tbMark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMark_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(29, 36);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(56, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // AddMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(115, 60);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbMark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddMark";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMark;
        private System.Windows.Forms.Button btnOk;
    }
}