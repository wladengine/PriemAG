namespace Priem
{
    partial class AllAbitList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllAbitList));
            this.label4 = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFIO = new System.Windows.Forms.TextBox();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.dgvAbitList = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbitList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.Location = new System.Drawing.Point(330, 471);
            // 
            // btnCard
            // 
            this.btnCard.Location = new System.Drawing.Point(12, 471);
            this.btnCard.Visible = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(462, 471);
            this.btnRemove.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(543, 471);
            this.btnAdd.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(642, 471);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "ФИО";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 26);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(39, 13);
            this.lblSearch.TabIndex = 18;
            this.lblSearch.Text = "Поиск";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Номер";
            // 
            // tbFIO
            // 
            this.tbFIO.Location = new System.Drawing.Point(225, 23);
            this.tbFIO.Name = "tbFIO";
            this.tbFIO.Size = new System.Drawing.Size(184, 20);
            this.tbFIO.TabIndex = 15;
            this.tbFIO.TextChanged += new System.EventHandler(this.tbFIO_TextChanged);
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(58, 23);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(143, 20);
            this.tbNumber.TabIndex = 14;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            // 
            // dgvAbitList
            // 
            this.dgvAbitList.AllowUserToAddRows = false;
            this.dgvAbitList.AllowUserToDeleteRows = false;
            this.dgvAbitList.AllowUserToOrderColumns = true;
            this.dgvAbitList.AllowUserToResizeRows = false;
            this.dgvAbitList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAbitList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAbitList.Location = new System.Drawing.Point(12, 59);
            this.dgvAbitList.MultiSelect = false;
            this.dgvAbitList.Name = "dgvAbitList";
            this.dgvAbitList.ReadOnly = true;
            this.dgvAbitList.Size = new System.Drawing.Size(705, 406);
            this.dgvAbitList.TabIndex = 13;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Location = new System.Drawing.Point(105, 471);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 40;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // AllAbitList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 506);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbFIO);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.dgvAbitList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AllAbitList";
            this.Text = "Список абитуриентов с заявлениями на другие факультеты ";
            this.Controls.SetChildIndex(this.dgvAbitList, 0);
            this.Controls.SetChildIndex(this.btnCard, 0);
            this.Controls.SetChildIndex(this.tbNumber, 0);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.Controls.SetChildIndex(this.tbFIO, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblSearch, 0);
            this.Controls.SetChildIndex(this.lblCount, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbitList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFIO;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.DataGridView dgvAbitList;
        private System.Windows.Forms.Button btnPrint;
    }
}