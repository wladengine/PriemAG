namespace Priem
{
    partial class LoadMarks
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
            this.btnPrintVed = new System.Windows.Forms.Button();
            this.lblIsLoad = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblExam = new System.Windows.Forms.Label();
            this.lblStudyBasis = new System.Windows.Forms.Label();
            this.lblFaculty = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvMarks = new System.Windows.Forms.DataGridView();
            this.btnDoubleLanguage = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.lblAdd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrintVed
            // 
            this.btnPrintVed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintVed.Location = new System.Drawing.Point(524, 519);
            this.btnPrintVed.Name = "btnPrintVed";
            this.btnPrintVed.Size = new System.Drawing.Size(136, 23);
            this.btnPrintVed.TabIndex = 53;
            this.btnPrintVed.Text = "Печать ведомости";
            this.btnPrintVed.UseVisualStyleBackColor = true;
            this.btnPrintVed.Click += new System.EventHandler(this.btnPrintVed_Click);
            // 
            // lblIsLoad
            // 
            this.lblIsLoad.AutoSize = true;
            this.lblIsLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIsLoad.ForeColor = System.Drawing.Color.Red;
            this.lblIsLoad.Location = new System.Drawing.Point(378, 89);
            this.lblIsLoad.Name = "lblIsLoad";
            this.lblIsLoad.Size = new System.Drawing.Size(0, 20);
            this.lblIsLoad.TabIndex = 51;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(262, 476);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 13);
            this.lblCount.TabIndex = 50;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(216, 476);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 13);
            this.lblTotal.TabIndex = 49;
            this.lblTotal.Text = "Всего:";
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDate.Location = new System.Drawing.Point(12, 102);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(234, 18);
            this.lblDate.TabIndex = 48;
            this.lblDate.Text = "Дата проведения экзамена: ";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblExam
            // 
            this.lblExam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExam.AutoSize = true;
            this.lblExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExam.Location = new System.Drawing.Point(12, 75);
            this.lblExam.Name = "lblExam";
            this.lblExam.Size = new System.Drawing.Size(87, 18);
            this.lblExam.TabIndex = 47;
            this.lblExam.Text = "Экзамен: ";
            this.lblExam.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStudyBasis
            // 
            this.lblStudyBasis.AutoSize = true;
            this.lblStudyBasis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStudyBasis.Location = new System.Drawing.Point(10, 43);
            this.lblStudyBasis.Name = "lblStudyBasis";
            this.lblStudyBasis.Size = new System.Drawing.Size(164, 20);
            this.lblStudyBasis.TabIndex = 46;
            this.lblStudyBasis.Text = "Основа обучения: ";
            // 
            // lblFaculty
            // 
            this.lblFaculty.AutoSize = true;
            this.lblFaculty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFaculty.Location = new System.Drawing.Point(10, 12);
            this.lblFaculty.Name = "lblFaculty";
            this.lblFaculty.Size = new System.Drawing.Size(113, 20);
            this.lblFaculty.TabIndex = 45;
            this.lblFaculty.Text = "Факультет: ";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(666, 519);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
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
            this.dgvMarks.Location = new System.Drawing.Point(14, 123);
            this.dgvMarks.MultiSelect = false;
            this.dgvMarks.Name = "dgvMarks";
            this.dgvMarks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMarks.Size = new System.Drawing.Size(727, 350);
            this.dgvMarks.TabIndex = 41;
            // 
            // btnDoubleLanguage
            // 
            this.btnDoubleLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoubleLanguage.Location = new System.Drawing.Point(292, 504);
            this.btnDoubleLanguage.Name = "btnDoubleLanguage";
            this.btnDoubleLanguage.Size = new System.Drawing.Size(60, 36);
            this.btnDoubleLanguage.TabIndex = 56;
            this.btnDoubleLanguage.Text = "Двуяз";
            this.btnDoubleLanguage.UseVisualStyleBackColor = true;
            this.btnDoubleLanguage.Visible = false;
            this.btnDoubleLanguage.Click += new System.EventHandler(this.btnDoubleLanguage_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(367, 505);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(151, 36);
            this.btnLoad.TabIndex = 55;
            this.btnLoad.Text = "Загрузить оценки";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Visible = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(524, 492);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(217, 23);
            this.btnView.TabIndex = 43;
            this.btnView.Text = "Предварительный просмотр";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAdd.Location = new System.Drawing.Point(520, 102);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(41, 20);
            this.lblAdd.TabIndex = 57;
            this.lblAdd.Text = "доп";
            // 
            // LoadMarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 555);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.btnDoubleLanguage);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnPrintVed);
            this.Controls.Add(this.lblIsLoad);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblExam);
            this.Controls.Add(this.lblStudyBasis);
            this.Controls.Add(this.lblFaculty);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.dgvMarks);
            this.Name = "LoadMarks";
            this.Text = "LoadMarks";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrintVed;
        private System.Windows.Forms.Label lblIsLoad;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblExam;
        private System.Windows.Forms.Label lblStudyBasis;
        private System.Windows.Forms.Label lblFaculty;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.DataGridView dgvMarks;
        private System.Windows.Forms.Button btnDoubleLanguage;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label lblAdd;
    }
}