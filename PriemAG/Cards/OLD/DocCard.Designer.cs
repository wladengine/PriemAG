namespace Priem
{
    partial class DocCard
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
            this.chlbFile = new System.Windows.Forms.CheckedListBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chlbFile
            // 
            this.chlbFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chlbFile.BackColor = System.Drawing.SystemColors.Control;
            this.chlbFile.CheckOnClick = true;
            this.chlbFile.FormattingEnabled = true;
            this.chlbFile.HorizontalScrollbar = true;
            this.chlbFile.Location = new System.Drawing.Point(12, 12);
            this.chlbFile.Name = "chlbFile";
            this.chlbFile.Size = new System.Drawing.Size(371, 199);
            this.chlbFile.TabIndex = 9;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFile.Location = new System.Drawing.Point(12, 228);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(100, 23);
            this.btnOpenFile.TabIndex = 10;
            this.btnOpenFile.Text = "Открыть файл";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(283, 228);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DocCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 263);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.chlbFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DocCard";
            this.Text = "Документы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocCard_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.CheckedListBox chlbFile;
        protected System.Windows.Forms.Button btnOpenFile;
        protected System.Windows.Forms.Button btnClose;
    }
}