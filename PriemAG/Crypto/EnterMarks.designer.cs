namespace Priem
{
    partial class EnterMarks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterMarks));
            this.dgvMarks = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFaculty = new System.Windows.Forms.Label();
            this.lblStudyBasis = new System.Windows.Forms.Label();
            this.lblExam = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            this.lblIsLoad = new System.Windows.Forms.Label();
            this.tbPersonVedNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVedType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).BeginInit();
            this.SuspendLayout();
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
            this.dgvMarks.Location = new System.Drawing.Point(12, 173);
            this.dgvMarks.MultiSelect = false;
            this.dgvMarks.Name = "dgvMarks";
            this.dgvMarks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMarks.Size = new System.Drawing.Size(727, 306);
            this.dgvMarks.TabIndex = 1;
            this.dgvMarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvMarks_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(12, 525);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(527, 504);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(131, 44);
            this.btnView.TabIndex = 20;
            this.btnView.Text = "Предварительный просмотр";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(664, 525);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFaculty
            // 
            this.lblFaculty.AutoSize = true;
            this.lblFaculty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFaculty.Location = new System.Drawing.Point(8, 18);
            this.lblFaculty.Name = "lblFaculty";
            this.lblFaculty.Size = new System.Drawing.Size(113, 20);
            this.lblFaculty.TabIndex = 22;
            this.lblFaculty.Text = "Факультет: ";
            // 
            // lblStudyBasis
            // 
            this.lblStudyBasis.AutoSize = true;
            this.lblStudyBasis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStudyBasis.Location = new System.Drawing.Point(8, 47);
            this.lblStudyBasis.Name = "lblStudyBasis";
            this.lblStudyBasis.Size = new System.Drawing.Size(164, 20);
            this.lblStudyBasis.TabIndex = 23;
            this.lblStudyBasis.Text = "Основа обучения: ";
            // 
            // lblExam
            // 
            this.lblExam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExam.AutoSize = true;
            this.lblExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExam.Location = new System.Drawing.Point(9, 92);
            this.lblExam.Name = "lblExam";
            this.lblExam.Size = new System.Drawing.Size(87, 18);
            this.lblExam.TabIndex = 25;
            this.lblExam.Text = "Экзамен: ";
            this.lblExam.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDate.Location = new System.Drawing.Point(9, 152);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(234, 18);
            this.lblDate.TabIndex = 26;
            this.lblDate.Text = "Дата проведения экзамена: ";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(214, 482);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 13);
            this.lblTotal.TabIndex = 30;
            this.lblTotal.Text = "Всего:";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(260, 482);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 13);
            this.lblCount.TabIndex = 31;
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAdd.Location = new System.Drawing.Point(553, 151);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(41, 20);
            this.lblAdd.TabIndex = 32;
            this.lblAdd.Text = "доп";
            // 
            // lblIsLoad
            // 
            this.lblIsLoad.AutoSize = true;
            this.lblIsLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIsLoad.ForeColor = System.Drawing.Color.Red;
            this.lblIsLoad.Location = new System.Drawing.Point(376, 95);
            this.lblIsLoad.Name = "lblIsLoad";
            this.lblIsLoad.Size = new System.Drawing.Size(0, 20);
            this.lblIsLoad.TabIndex = 34;
            // 
            // tbPersonVedNum
            // 
            this.tbPersonVedNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbPersonVedNum.Location = new System.Drawing.Point(154, 527);
            this.tbPersonVedNum.Name = "tbPersonVedNum";
            this.tbPersonVedNum.Size = new System.Drawing.Size(100, 20);
            this.tbPersonVedNum.TabIndex = 0;
            this.tbPersonVedNum.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbPersonVedNum_KeyUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 511);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Номер";
            // 
            // lblVedType
            // 
            this.lblVedType.AutoSize = true;
            this.lblVedType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVedType.Location = new System.Drawing.Point(9, 122);
            this.lblVedType.Name = "lblVedType";
            this.lblVedType.Size = new System.Drawing.Size(89, 18);
            this.lblVedType.TabIndex = 39;
            this.lblVedType.Text = "lblVedType";
            // 
            // EnterMarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 555);
            this.Controls.Add(this.lblVedType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPersonVedNum);
            this.Controls.Add(this.lblIsLoad);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblExam);
            this.Controls.Add(this.lblStudyBasis);
            this.Controls.Add(this.lblFaculty);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvMarks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnterMarks";
            this.Text = "Ввод оценок";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnterMarks_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnterMarks_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvMarks;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblFaculty;
        private System.Windows.Forms.Label lblStudyBasis;
        private System.Windows.Forms.Label lblExam;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.Label lblIsLoad;
        private System.Windows.Forms.TextBox tbPersonVedNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVedType;

    }
}