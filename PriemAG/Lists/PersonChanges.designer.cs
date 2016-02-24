namespace Priem
{
    partial class PersonChangesList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonChangesList));
            this.dgvChanges = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChanges)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvChanges
            // 
            this.dgvChanges.AllowUserToAddRows = false;
            this.dgvChanges.AllowUserToDeleteRows = false;
            this.dgvChanges.AllowUserToResizeRows = false;
            this.dgvChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChanges.Location = new System.Drawing.Point(12, 57);
            this.dgvChanges.MultiSelect = false;
            this.dgvChanges.Name = "dgvChanges";
            this.dgvChanges.ReadOnly = true;
            this.dgvChanges.RowHeadersVisible = false;
            this.dgvChanges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChanges.Size = new System.Drawing.Size(628, 289);
            this.dgvChanges.TabIndex = 0;
            this.dgvChanges.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChanges_CellDoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(565, 352);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(9, 15);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(33, 13);
            this.lblDateFrom.TabIndex = 14;
            this.lblDateFrom.Text = "Дата";
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(183, 35);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(19, 13);
            this.lblDateTo.TabIndex = 13;
            this.lblDateTo.Text = "по";
            // 
            // dtDateTo
            // 
            this.dtDateTo.Checked = false;
            this.dtDateTo.Location = new System.Drawing.Point(208, 31);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.ShowCheckBox = true;
            this.dtDateTo.Size = new System.Drawing.Size(136, 20);
            this.dtDateTo.TabIndex = 12;
            this.dtDateTo.Value = new System.DateTime(2007, 11, 23, 0, 0, 0, 0);
            // 
            // dtDateFrom
            // 
            this.dtDateFrom.Checked = false;
            this.dtDateFrom.Location = new System.Drawing.Point(31, 31);
            this.dtDateFrom.Name = "dtDateFrom";
            this.dtDateFrom.ShowCheckBox = true;
            this.dtDateFrom.Size = new System.Drawing.Size(136, 20);
            this.dtDateFrom.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "с";
            // 
            // PersonChangesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 387);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.dtDateTo);
            this.Controls.Add(this.dtDateFrom);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvChanges);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PersonChangesList";
            this.Text = "Изменения персональных данных абитуриентов";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PersonChanges_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChanges)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvChanges;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDateFrom;
        private System.Windows.Forms.Label label1;
    }
}