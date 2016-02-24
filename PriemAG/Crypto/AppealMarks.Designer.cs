namespace Priem
{
    partial class AppealMarks
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
            this.lblIsLoad = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            this.dgvMarks = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblExam = new System.Windows.Forms.Label();
            this.lblStudyBasis = new System.Windows.Forms.Label();
            this.lblFaculty = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIsLoad
            // 
            this.lblIsLoad.AutoSize = true;
            this.lblIsLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIsLoad.ForeColor = System.Drawing.Color.Red;
            this.lblIsLoad.Location = new System.Drawing.Point(385, 93);
            this.lblIsLoad.Name = "lblIsLoad";
            this.lblIsLoad.Size = new System.Drawing.Size(0, 20);
            this.lblIsLoad.TabIndex = 41;
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAdd.Location = new System.Drawing.Point(21, 93);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(0, 20);
            this.lblAdd.TabIndex = 40;
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
            this.dgvMarks.Location = new System.Drawing.Point(21, 131);
            this.dgvMarks.MultiSelect = false;
            this.dgvMarks.Name = "dgvMarks";
            this.dgvMarks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMarks.Size = new System.Drawing.Size(727, 311);
            this.dgvMarks.TabIndex = 35;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(21, 476);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(151, 36);
            this.btnLoad.TabIndex = 44;
            this.btnLoad.Text = "Загрузить оценки";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(226, 445);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 13);
            this.lblTotal.TabIndex = 43;
            this.lblTotal.Text = "Всего:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(673, 483);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(272, 445);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 13);
            this.lblCount.TabIndex = 45;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDate.Location = new System.Drawing.Point(22, 110);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(234, 18);
            this.lblDate.TabIndex = 52;
            this.lblDate.Text = "Дата проведения экзамена: ";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblExam
            // 
            this.lblExam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExam.AutoSize = true;
            this.lblExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExam.Location = new System.Drawing.Point(22, 83);
            this.lblExam.Name = "lblExam";
            this.lblExam.Size = new System.Drawing.Size(87, 18);
            this.lblExam.TabIndex = 51;
            this.lblExam.Text = "Экзамен: ";
            this.lblExam.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStudyBasis
            // 
            this.lblStudyBasis.AutoSize = true;
            this.lblStudyBasis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStudyBasis.Location = new System.Drawing.Point(20, 51);
            this.lblStudyBasis.Name = "lblStudyBasis";
            this.lblStudyBasis.Size = new System.Drawing.Size(164, 20);
            this.lblStudyBasis.TabIndex = 50;
            this.lblStudyBasis.Text = "Основа обучения: ";
            // 
            // lblFaculty
            // 
            this.lblFaculty.AutoSize = true;
            this.lblFaculty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFaculty.Location = new System.Drawing.Point(20, 20);
            this.lblFaculty.Name = "lblFaculty";
            this.lblFaculty.Size = new System.Drawing.Size(113, 20);
            this.lblFaculty.TabIndex = 49;
            this.lblFaculty.Text = "Факультет: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(563, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 58;
            this.label1.Text = "доп";
            // 
            // AppealMarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 518);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblExam);
            this.Controls.Add(this.lblStudyBasis);
            this.Controls.Add(this.lblFaculty);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblIsLoad);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.dgvMarks);
            this.Name = "AppealMarks";
            this.Text = "Аппеляционная ведомость";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIsLoad;
        private System.Windows.Forms.Label lblAdd;
        public System.Windows.Forms.DataGridView dgvMarks;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblExam;
        private System.Windows.Forms.Label lblStudyBasis;
        private System.Windows.Forms.Label lblFaculty;
        private System.Windows.Forms.Label label1;
    }
}