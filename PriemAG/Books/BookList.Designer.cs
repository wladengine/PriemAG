namespace Priem
{
    partial class BookList
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
            this.lblCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCard
            // 
            this.btnCard.Location = new System.Drawing.Point(490, 343);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(12, 372);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 343);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(490, 372);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(250, 348);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(35, 13);
            this.lblCount.TabIndex = 24;
            this.lblCount.Text = "label1";
            // 
            // BookList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 407);
            this.Controls.Add(this.lblCount);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "BookList";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.lblCount, 0);
            this.Controls.SetChildIndex(this.btnCard, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label lblCount;
    }
}