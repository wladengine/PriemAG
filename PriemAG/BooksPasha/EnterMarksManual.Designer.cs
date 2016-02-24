namespace Priem
{
    partial class EnterMarksManual
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
            this.lblAdd = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblExam = new System.Windows.Forms.Label();
            this.lblStudyBasis = new System.Windows.Forms.Label();
            this.lblFaculty = new System.Windows.Forms.Label();
            this.dgvMarks = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAdd.Location = new System.Drawing.Point(12, 92);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(0, 20);
            this.lblAdd.TabIndex = 42;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(260, 479);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 13);
            this.lblCount.TabIndex = 41;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(214, 479);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 13);
            this.lblTotal.TabIndex = 40;
            this.lblTotal.Text = "Всего:";
            // 
            // lblExam
            // 
            this.lblExam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExam.AutoSize = true;
            this.lblExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExam.Location = new System.Drawing.Point(12, 77);
            this.lblExam.Name = "lblExam";
            this.lblExam.Size = new System.Drawing.Size(87, 18);
            this.lblExam.TabIndex = 38;
            this.lblExam.Text = "Экзамен: ";
            this.lblExam.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStudyBasis
            // 
            this.lblStudyBasis.AutoSize = true;
            this.lblStudyBasis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStudyBasis.Location = new System.Drawing.Point(8, 46);
            this.lblStudyBasis.Name = "lblStudyBasis";
            this.lblStudyBasis.Size = new System.Drawing.Size(164, 20);
            this.lblStudyBasis.TabIndex = 37;
            this.lblStudyBasis.Text = "Основа обучения: ";
            // 
            // lblFaculty
            // 
            this.lblFaculty.AutoSize = true;
            this.lblFaculty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFaculty.Location = new System.Drawing.Point(8, 15);
            this.lblFaculty.Name = "lblFaculty";
            this.lblFaculty.Size = new System.Drawing.Size(113, 20);
            this.lblFaculty.TabIndex = 36;
            this.lblFaculty.Text = "Факультет: ";
            // 
            // dgvMarks
            // 
            this.dgvMarks.AllowUserToAddRows = false;
            this.dgvMarks.AllowUserToDeleteRows = false;
            this.dgvMarks.AllowUserToResizeColumns = false;
            this.dgvMarks.AllowUserToResizeRows = false;
            this.dgvMarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMarks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMarks.ColumnHeadersHeight = 55;
            this.dgvMarks.Location = new System.Drawing.Point(12, 115);
            this.dgvMarks.MultiSelect = false;
            this.dgvMarks.Name = "dgvMarks";
            this.dgvMarks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMarks.Size = new System.Drawing.Size(786, 361);
            this.dgvMarks.TabIndex = 35;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(723, 511);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(12, 511);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(489, 511);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(217, 23);
            this.btnView.TabIndex = 46;
            this.btnView.Text = "Предварительный просмотр";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // EnterMarksManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 555);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblExam);
            this.Controls.Add(this.lblStudyBasis);
            this.Controls.Add(this.lblFaculty);
            this.Controls.Add(this.dgvMarks);
            this.Name = "EnterMarksManual";
            this.Text = "EnterMarksManual";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblExam;
        private System.Windows.Forms.Label lblStudyBasis;
        private System.Windows.Forms.Label lblFaculty;
        public System.Windows.Forms.DataGridView dgvMarks;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnView;
    }
}