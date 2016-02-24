namespace Priem
{
    partial class VedExamLists
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VedExamLists));
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvVed = new System.Windows.Forms.DataGridView();
            this.btnLists = new System.Windows.Forms.Button();
            this.chbAll = new System.Windows.Forms.CheckBox();
            this.chbPrintChecked = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSpeciality = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.cbLicenseProgram = new System.Windows.Forms.ComboBox();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.cbStudyForm = new System.Windows.Forms.ComboBox();
            this.cbCompetition = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVed)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(260, 484);
            // 
            // btnCard
            // 
            this.btnCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCard.Location = new System.Drawing.Point(12, 509);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(409, 399);
            this.btnRemove.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(409, 370);
            this.btnAdd.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(502, 509);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Location = new System.Drawing.Point(129, 509);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(55, 23);
            this.btnPrint.TabIndex = 27;
            this.btnPrint.Text = "В Word";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgvVed
            // 
            this.dgvVed.AllowUserToAddRows = false;
            this.dgvVed.AllowUserToDeleteRows = false;
            this.dgvVed.AllowUserToResizeRows = false;
            this.dgvVed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVed.Location = new System.Drawing.Point(11, 135);
            this.dgvVed.MultiSelect = false;
            this.dgvVed.Name = "dgvVed";
            this.dgvVed.ReadOnly = true;
            this.dgvVed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVed.Size = new System.Drawing.Size(566, 346);
            this.dgvVed.TabIndex = 30;
            // 
            // btnLists
            // 
            this.btnLists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLists.Location = new System.Drawing.Point(359, 500);
            this.btnLists.Name = "btnLists";
            this.btnLists.Size = new System.Drawing.Size(123, 41);
            this.btnLists.TabIndex = 31;
            this.btnLists.Text = "Печать отмеченных экз. листов";
            this.btnLists.UseVisualStyleBackColor = true;
            this.btnLists.Click += new System.EventHandler(this.btnLists_Click);
            // 
            // chbAll
            // 
            this.chbAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbAll.AutoSize = true;
            this.chbAll.Location = new System.Drawing.Point(476, 112);
            this.chbAll.Name = "chbAll";
            this.chbAll.Size = new System.Drawing.Size(101, 17);
            this.chbAll.TabIndex = 64;
            this.chbAll.Text = "Отметить всех";
            this.chbAll.UseVisualStyleBackColor = true;
            this.chbAll.CheckedChanged += new System.EventHandler(this.chbAll_CheckedChanged);
            // 
            // chbPrintChecked
            // 
            this.chbPrintChecked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbPrintChecked.AutoSize = true;
            this.chbPrintChecked.Location = new System.Drawing.Point(190, 506);
            this.chbPrintChecked.Name = "chbPrintChecked";
            this.chbPrintChecked.Size = new System.Drawing.Size(146, 30);
            this.chbPrintChecked.TabIndex = 65;
            this.chbPrintChecked.Text = "Печатать ведомость \r\nтолько для отмеченных";
            this.chbPrintChecked.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(406, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 75;
            this.label9.Text = "Основа обучения";
            // 
            // lblSpeciality
            // 
            this.lblSpeciality.AutoSize = true;
            this.lblSpeciality.Location = new System.Drawing.Point(11, 59);
            this.lblSpeciality.Name = "lblSpeciality";
            this.lblSpeciality.Size = new System.Drawing.Size(75, 13);
            this.lblSpeciality.TabIndex = 73;
            this.lblSpeciality.Text = "Направление";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 71;
            this.label5.Text = "Тип конкурса";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 67;
            this.label3.Text = "Форма обучения";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Факультет";
            // 
            // cbFaculty
            // 
            this.cbFaculty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFaculty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(14, 25);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(178, 21);
            this.cbFaculty.TabIndex = 126;
            // 
            // cbLicenseProgram
            // 
            this.cbLicenseProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbLicenseProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbLicenseProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseProgram.FormattingEnabled = true;
            this.cbLicenseProgram.Location = new System.Drawing.Point(14, 75);
            this.cbLicenseProgram.Name = "cbLicenseProgram";
            this.cbLicenseProgram.Size = new System.Drawing.Size(356, 21);
            this.cbLicenseProgram.TabIndex = 127;
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyBasis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(409, 25);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(130, 21);
            this.cbStudyBasis.TabIndex = 128;
            // 
            // cbStudyForm
            // 
            this.cbStudyForm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyForm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyForm.FormattingEnabled = true;
            this.cbStudyForm.Location = new System.Drawing.Point(225, 25);
            this.cbStudyForm.Name = "cbStudyForm";
            this.cbStudyForm.Size = new System.Drawing.Size(145, 21);
            this.cbStudyForm.TabIndex = 129;
            // 
            // cbCompetition
            // 
            this.cbCompetition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCompetition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCompetition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompetition.FormattingEnabled = true;
            this.cbCompetition.Location = new System.Drawing.Point(409, 75);
            this.cbCompetition.Name = "cbCompetition";
            this.cbCompetition.Size = new System.Drawing.Size(130, 21);
            this.cbCompetition.TabIndex = 130;
            // 
            // VedExamLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 544);
            this.Controls.Add(this.cbCompetition);
            this.Controls.Add(this.cbStudyForm);
            this.Controls.Add(this.cbStudyBasis);
            this.Controls.Add(this.cbLicenseProgram);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblSpeciality);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbPrintChecked);
            this.Controls.Add(this.chbAll);
            this.Controls.Add(this.btnLists);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgvVed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VedExamLists";
            this.Text = "Ведомость выдачи экзаменационных листов";
            this.Activated += new System.EventHandler(this.VedExamLists_Activated);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dgvVed, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnLists, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnCard, 0);
            this.Controls.SetChildIndex(this.lblCount, 0);
            this.Controls.SetChildIndex(this.chbAll, 0);
            this.Controls.SetChildIndex(this.chbPrintChecked, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.lblSpeciality, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.cbFaculty, 0);
            this.Controls.SetChildIndex(this.cbLicenseProgram, 0);
            this.Controls.SetChildIndex(this.cbStudyBasis, 0);
            this.Controls.SetChildIndex(this.cbStudyForm, 0);
            this.Controls.SetChildIndex(this.cbCompetition, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgvVed;
        private System.Windows.Forms.Button btnLists;
        private System.Windows.Forms.CheckBox chbAll;
        private System.Windows.Forms.CheckBox chbPrintChecked;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSpeciality;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.ComboBox cbLicenseProgram;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.ComboBox cbStudyForm;
        private System.Windows.Forms.ComboBox cbCompetition;
    }
}