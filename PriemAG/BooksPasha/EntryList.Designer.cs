namespace Priem
{
    partial class EntryList
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
            this.label11 = new System.Windows.Forms.Label();
            this.cbStudyLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.tbPlanNumSearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbStudyForm = new System.Windows.Forms.ComboBox();
            this.btnLoadEntry = new System.Windows.Forms.Button();
            this.dgvEntry = new System.Windows.Forms.DataGridView();
            this.cbIsSecond = new System.Windows.Forms.ComboBox();
            this.cbIsParallel = new System.Windows.Forms.ComboBox();
            this.cbIsReduced = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(366, 445);
            // 
            // btnCard
            // 
            this.btnCard.Location = new System.Drawing.Point(968, 448);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(12, 477);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 448);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(968, 477);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 13);
            this.label11.TabIndex = 95;
            this.label11.Text = "Уровень программы";
            // 
            // cbStudyLevel
            // 
            this.cbStudyLevel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyLevel.FormattingEnabled = true;
            this.cbStudyLevel.Location = new System.Drawing.Point(12, 29);
            this.cbStudyLevel.Name = "cbStudyLevel";
            this.cbStudyLevel.Size = new System.Drawing.Size(161, 21);
            this.cbStudyLevel.TabIndex = 94;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "Факультет";
            // 
            // cbFaculty
            // 
            this.cbFaculty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFaculty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(200, 29);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(329, 21);
            this.cbFaculty.TabIndex = 86;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Основа обучения";
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyBasis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(12, 82);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(161, 21);
            this.cbStudyBasis.TabIndex = 96;
            // 
            // tbPlanNumSearch
            // 
            this.tbPlanNumSearch.Location = new System.Drawing.Point(12, 146);
            this.tbPlanNumSearch.Name = "tbPlanNumSearch";
            this.tbPlanNumSearch.Size = new System.Drawing.Size(262, 20);
            this.tbPlanNumSearch.TabIndex = 99;
            this.tbPlanNumSearch.TextChanged += new System.EventHandler(this.tbPlanNumSearch_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 13);
            this.label4.TabIndex = 98;
            this.label4.Text = "Поиск по образовательной программе";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "Форма обучения";
            // 
            // cbStudyForm
            // 
            this.cbStudyForm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyForm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyForm.FormattingEnabled = true;
            this.cbStudyForm.Location = new System.Drawing.Point(198, 82);
            this.cbStudyForm.Name = "cbStudyForm";
            this.cbStudyForm.Size = new System.Drawing.Size(161, 21);
            this.cbStudyForm.TabIndex = 100;
            // 
            // btnLoadEntry
            // 
            this.btnLoadEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadEntry.Location = new System.Drawing.Point(864, 448);
            this.btnLoadEntry.Name = "btnLoadEntry";
            this.btnLoadEntry.Size = new System.Drawing.Size(75, 52);
            this.btnLoadEntry.TabIndex = 105;
            this.btnLoadEntry.Text = "Обновить данные";
            this.btnLoadEntry.UseVisualStyleBackColor = true;
            // 
            // dgvEntry
            // 
            this.dgvEntry.AllowUserToAddRows = false;
            this.dgvEntry.AllowUserToDeleteRows = false;
            this.dgvEntry.AllowUserToResizeRows = false;
            this.dgvEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntry.Location = new System.Drawing.Point(12, 172);
            this.dgvEntry.MultiSelect = false;
            this.dgvEntry.Name = "dgvEntry";
            this.dgvEntry.ReadOnly = true;
            this.dgvEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEntry.Size = new System.Drawing.Size(1031, 270);
            this.dgvEntry.TabIndex = 106;
            // 
            // cbIsSecond
            // 
            this.cbIsSecond.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbIsSecond.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIsSecond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsSecond.FormattingEnabled = true;
            this.cbIsSecond.Location = new System.Drawing.Point(789, 29);
            this.cbIsSecond.Name = "cbIsSecond";
            this.cbIsSecond.Size = new System.Drawing.Size(110, 21);
            this.cbIsSecond.TabIndex = 107;
            // 
            // cbIsParallel
            // 
            this.cbIsParallel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbIsParallel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIsParallel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsParallel.FormattingEnabled = true;
            this.cbIsParallel.Location = new System.Drawing.Point(673, 29);
            this.cbIsParallel.Name = "cbIsParallel";
            this.cbIsParallel.Size = new System.Drawing.Size(110, 21);
            this.cbIsParallel.TabIndex = 108;
            // 
            // cbIsReduced
            // 
            this.cbIsReduced.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbIsReduced.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIsReduced.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsReduced.FormattingEnabled = true;
            this.cbIsReduced.Location = new System.Drawing.Point(557, 29);
            this.cbIsReduced.Name = "cbIsReduced";
            this.cbIsReduced.Size = new System.Drawing.Size(110, 21);
            this.cbIsReduced.TabIndex = 109;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(554, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 110;
            this.label3.Text = "Сокращенные";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(786, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 111;
            this.label5.Text = "Для лис с ВО";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(670, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 112;
            this.label7.Text = "Параллельные";
            // 
            // EntryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 512);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbIsReduced);
            this.Controls.Add(this.cbIsParallel);
            this.Controls.Add(this.cbIsSecond);
            this.Controls.Add(this.dgvEntry);
            this.Controls.Add(this.btnLoadEntry);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbStudyForm);
            this.Controls.Add(this.tbPlanNumSearch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbStudyBasis);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbStudyLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFaculty);
            this.Name = "EntryList";
            this.Text = "EntryList";
            this.Controls.SetChildIndex(this.cbFaculty, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbStudyLevel, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.cbStudyBasis, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbPlanNumSearch, 0);
            this.Controls.SetChildIndex(this.cbStudyForm, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.btnLoadEntry, 0);
            this.Controls.SetChildIndex(this.dgvEntry, 0);
            this.Controls.SetChildIndex(this.lblCount, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.Controls.SetChildIndex(this.btnCard, 0);
            this.Controls.SetChildIndex(this.cbIsSecond, 0);
            this.Controls.SetChildIndex(this.cbIsParallel, 0);
            this.Controls.SetChildIndex(this.cbIsReduced, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbStudyLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.TextBox tbPlanNumSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbStudyForm;
        private System.Windows.Forms.Button btnLoadEntry;
        private System.Windows.Forms.DataGridView dgvEntry;
        private System.Windows.Forms.ComboBox cbIsSecond;
        private System.Windows.Forms.ComboBox cbIsParallel;
        private System.Windows.Forms.ComboBox cbIsReduced;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}