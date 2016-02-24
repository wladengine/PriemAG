namespace Priem
{
    partial class ExamsVedCard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamsVedCard));
            this.btnRightAll = new System.Windows.Forms.Button();
            this.btnLeftAll = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvRight = new System.Windows.Forms.DataGridView();
            this.dgvLeft = new System.Windows.Forms.DataGridView();
            this.lblNum = new System.Windows.Forms.Label();
            this.tbExam = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProfession = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBase = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            this.lblAddCount = new System.Windows.Forms.Label();
            this.dtPassDate = new System.Windows.Forms.DateTimePicker();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.cbStudyForm = new System.Windows.Forms.ComboBox();
            this.cbObrazProgram = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRightAll
            // 
            this.btnRightAll.Location = new System.Drawing.Point(347, 448);
            this.btnRightAll.Name = "btnRightAll";
            this.btnRightAll.Size = new System.Drawing.Size(33, 23);
            this.btnRightAll.TabIndex = 47;
            this.btnRightAll.Text = ">>";
            this.btnRightAll.UseVisualStyleBackColor = true;
            this.btnRightAll.Click += new System.EventHandler(this.btnRightAll_Click);
            // 
            // btnLeftAll
            // 
            this.btnLeftAll.Location = new System.Drawing.Point(327, 398);
            this.btnLeftAll.Name = "btnLeftAll";
            this.btnLeftAll.Size = new System.Drawing.Size(33, 23);
            this.btnLeftAll.TabIndex = 44;
            this.btnLeftAll.Text = "<<";
            this.btnLeftAll.UseVisualStyleBackColor = true;
            this.btnLeftAll.Click += new System.EventHandler(this.btnLeftAll_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(347, 174);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(33, 23);
            this.btnRight.TabIndex = 45;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(327, 124);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(33, 23);
            this.btnLeft.TabIndex = 46;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(391, 502);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(144, 23);
            this.btnOk.TabIndex = 42;
            this.btnOk.Text = "Подтвердить и закрыть";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(541, 502);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 23);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Отменить и закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvRight
            // 
            this.dgvRight.AllowUserToAddRows = false;
            this.dgvRight.AllowUserToDeleteRows = false;
            this.dgvRight.AllowUserToResizeRows = false;
            this.dgvRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRight.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRight.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRight.Location = new System.Drawing.Point(393, 112);
            this.dgvRight.MultiSelect = false;
            this.dgvRight.Name = "dgvRight";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRight.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRight.Size = new System.Drawing.Size(300, 373);
            this.dgvRight.TabIndex = 39;
            this.dgvRight.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRight_CellDoubleClick);
            // 
            // dgvLeft
            // 
            this.dgvLeft.AllowUserToAddRows = false;
            this.dgvLeft.AllowUserToDeleteRows = false;
            this.dgvLeft.AllowUserToResizeRows = false;
            this.dgvLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeft.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLeft.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLeft.Location = new System.Drawing.Point(10, 112);
            this.dgvLeft.MultiSelect = false;
            this.dgvLeft.Name = "dgvLeft";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeft.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvLeft.Size = new System.Drawing.Size(300, 373);
            this.dgvLeft.TabIndex = 40;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(9, 12);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(33, 13);
            this.lblNum.TabIndex = 49;
            this.lblNum.Text = "Дата";
            // 
            // tbExam
            // 
            this.tbExam.Enabled = false;
            this.tbExam.Location = new System.Drawing.Point(12, 75);
            this.tbExam.Name = "tbExam";
            this.tbExam.Size = new System.Drawing.Size(300, 20);
            this.tbExam.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Экзамен";
            // 
            // lblProfession
            // 
            this.lblProfession.AutoSize = true;
            this.lblProfession.Location = new System.Drawing.Point(388, 56);
            this.lblProfession.Name = "lblProfession";
            this.lblProfession.Size = new System.Drawing.Size(158, 13);
            this.lblProfession.TabIndex = 59;
            this.lblProfession.Text = "Образовательная программа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(388, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "Форма обучения";
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point(554, 9);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(94, 13);
            this.lblBase.TabIndex = 60;
            this.lblBase.Text = "Основа обучения";
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.ForeColor = System.Drawing.Color.Red;
            this.lblAdd.Location = new System.Drawing.Point(150, 31);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(116, 13);
            this.lblAdd.TabIndex = 64;
            this.lblAdd.Text = "ДОПОЛНИТЕЛЬНАЯ";
            this.lblAdd.Visible = false;
            // 
            // lblAddCount
            // 
            this.lblAddCount.AutoSize = true;
            this.lblAddCount.ForeColor = System.Drawing.Color.Red;
            this.lblAddCount.Location = new System.Drawing.Point(264, 31);
            this.lblAddCount.Name = "lblAddCount";
            this.lblAddCount.Size = new System.Drawing.Size(0, 13);
            this.lblAddCount.TabIndex = 65;
            // 
            // dtPassDate
            // 
            this.dtPassDate.Location = new System.Drawing.Point(11, 28);
            this.dtPassDate.Name = "dtPassDate";
            this.dtPassDate.Size = new System.Drawing.Size(133, 20);
            this.dtPassDate.TabIndex = 66;
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyBasis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(557, 27);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(136, 21);
            this.cbStudyBasis.TabIndex = 141;
            // 
            // cbStudyForm
            // 
            this.cbStudyForm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyForm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyForm.FormattingEnabled = true;
            this.cbStudyForm.Location = new System.Drawing.Point(391, 27);
            this.cbStudyForm.Name = "cbStudyForm";
            this.cbStudyForm.Size = new System.Drawing.Size(136, 21);
            this.cbStudyForm.TabIndex = 140;
            // 
            // cbObrazProgram
            // 
            this.cbObrazProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbObrazProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbObrazProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObrazProgram.FormattingEnabled = true;
            this.cbObrazProgram.Location = new System.Drawing.Point(391, 75);
            this.cbObrazProgram.Name = "cbObrazProgram";
            this.cbObrazProgram.Size = new System.Drawing.Size(302, 21);
            this.cbObrazProgram.TabIndex = 139;
            // 
            // ExamsVedCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 537);
            this.Controls.Add(this.cbStudyBasis);
            this.Controls.Add(this.cbStudyForm);
            this.Controls.Add(this.cbObrazProgram);
            this.Controls.Add(this.dtPassDate);
            this.Controls.Add(this.lblAddCount);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblBase);
            this.Controls.Add(this.lblProfession);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbExam);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.btnRightAll);
            this.Controls.Add(this.btnLeftAll);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvRight);
            this.Controls.Add(this.dgvLeft);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExamsVedCard";
            this.Text = "Ведомость абитуриентов на экзамен";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExamsVed_FormClosed);
            this.Resize += new System.EventHandler(this.VedExamCrypto_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnRightAll;
        protected System.Windows.Forms.Button btnLeftAll;
        protected System.Windows.Forms.Button btnRight;
        protected System.Windows.Forms.Button btnLeft;
        protected System.Windows.Forms.Button btnOk;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.DataGridView dgvRight;
        protected System.Windows.Forms.DataGridView dgvLeft;
        protected System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.TextBox tbExam;
        protected System.Windows.Forms.Label label1;       
        private System.Windows.Forms.Label lblProfession;       
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.Label lblAddCount;
        private System.Windows.Forms.DateTimePicker dtPassDate;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.ComboBox cbStudyForm;
        private System.Windows.Forms.ComboBox cbObrazProgram;

    }
}