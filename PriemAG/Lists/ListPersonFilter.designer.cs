namespace Priem
{
    partial class ListPersonFilter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListPersonFilter));
            this.dgvAbitList = new System.Windows.Forms.DataGridView();
            this.tbFIO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCard = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnColumns = new System.Windows.Forms.Button();
            this.btnFilters = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnGroup = new System.Windows.Forms.Button();
            this.sfdFile = new System.Windows.Forms.SaveFileDialog();
            this.btnExcel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPersonNumber = new System.Windows.Forms.TextBox();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbitList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAbitList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAbitList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAbitList.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAbitList.Location = new System.Drawing.Point(12, 108);
            this.dgvAbitList.MultiSelect = false;
            this.dgvAbitList.Name = "dgvAbitList";
            this.dgvAbitList.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAbitList.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAbitList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAbitList.Size = new System.Drawing.Size(648, 293);
            this.dgvAbitList.TabIndex = 0;
            this.dgvAbitList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAbitList_CellContentClick);
            // 
            // tbFIO
            // 
            this.tbFIO.Location = new System.Drawing.Point(198, 68);
            this.tbFIO.Name = "tbFIO";
            this.tbFIO.Size = new System.Drawing.Size(207, 20);
            this.tbFIO.TabIndex = 4;
            this.tbFIO.TextChanged += new System.EventHandler(this.tbFIO_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Факультет";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "ФИО";
            // 
            // btnCard
            // 
            this.btnCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCard.Location = new System.Drawing.Point(12, 486);
            this.btnCard.Name = "btnCard";
            this.btnCard.Size = new System.Drawing.Size(75, 23);
            this.btnCard.TabIndex = 5;
            this.btnCard.Text = "Карточка";
            this.btnCard.UseVisualStyleBackColor = true;
            this.btnCard.Click += new System.EventHandler(this.btnCard_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 71);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(39, 13);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "Поиск";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(290, 412);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 13);
            this.lblTotal.TabIndex = 16;
            this.lblTotal.Text = "Всего:";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(330, 412);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 13);
            this.lblCount.TabIndex = 16;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(492, 487);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "В Word";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.Location = new System.Drawing.Point(492, 428);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 23);
            this.btnFile.TabIndex = 11;
            this.btnFile.Text = "В файл";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(585, 487);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnColumns
            // 
            this.btnColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColumns.Location = new System.Drawing.Point(403, 486);
            this.btnColumns.Name = "btnColumns";
            this.btnColumns.Size = new System.Drawing.Size(83, 23);
            this.btnColumns.TabIndex = 10;
            this.btnColumns.Text = "Столбцы";
            this.btnColumns.UseVisualStyleBackColor = true;
            this.btnColumns.Click += new System.EventHandler(this.btnColumns_Click);
            // 
            // btnFilters
            // 
            this.btnFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilters.Location = new System.Drawing.Point(403, 457);
            this.btnFilters.Name = "btnFilters";
            this.btnFilters.Size = new System.Drawing.Size(83, 23);
            this.btnFilters.TabIndex = 9;
            this.btnFilters.Text = "Фильтры";
            this.btnFilters.UseVisualStyleBackColor = true;
            this.btnFilters.Click += new System.EventHandler(this.btnFilters_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(585, 79);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnGroup
            // 
            this.btnGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGroup.Location = new System.Drawing.Point(403, 428);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(83, 23);
            this.btnGroup.TabIndex = 9;
            this.btnGroup.Text = "Группировки";
            this.btnGroup.UseVisualStyleBackColor = true;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // sfdFile
            // 
            this.sfdFile.Filter = "Текстовый файл|*.txt|Все файлы|*.*";
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Location = new System.Drawing.Point(492, 457);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExcel.TabIndex = 18;
            this.btnExcel.Text = "В Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Ид. номер";
            // 
            // tbPersonNumber
            // 
            this.tbPersonNumber.Location = new System.Drawing.Point(57, 68);
            this.tbPersonNumber.Name = "tbPersonNumber";
            this.tbPersonNumber.Size = new System.Drawing.Size(115, 20);
            this.tbPersonNumber.TabIndex = 20;
            this.tbPersonNumber.TextChanged += new System.EventHandler(this.tbPersonNumber_TextChanged);
            // 
            // cbFaculty
            // 
            this.cbFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFaculty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFaculty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(12, 25);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(231, 21);
            this.cbFaculty.TabIndex = 115;
            // 
            // ListPersonFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 522);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPersonNumber);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnGroup);
            this.Controls.Add(this.btnFilters);
            this.Controls.Add(this.btnColumns);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnCard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFIO);
            this.Controls.Add(this.dgvAbitList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListPersonFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список абитуриентов (по факультетам)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ListPerson_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbitList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvAbitList;
        private System.Windows.Forms.TextBox tbFIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;       
        private System.Windows.Forms.Button btnCard;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnColumns;
        private System.Windows.Forms.Button btnFilters;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.SaveFileDialog sfdFile;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPersonNumber;
        private System.Windows.Forms.ComboBox cbFaculty;
    }
}

