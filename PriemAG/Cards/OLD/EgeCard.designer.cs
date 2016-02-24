namespace Priem
{
    partial class EgeCard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EgeCard));
            this.label1 = new System.Windows.Forms.Label();
            this.dgvExams = new System.Windows.Forms.DataGridView();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.epErrorInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.chbNewFIO = new System.Windows.Forms.CheckBox();
            this.tbSurname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPrintNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbSecondName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.tbFBSComment = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.tbFBSStatus = new System.Windows.Forms.TextBox();
            this.lblIsImported = new System.Windows.Forms.Label();
            this.btnSetStatusPasha = new System.Windows.Forms.Button();
            this.tbCommentFBSPasha = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrorInput)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(257, 614);
            // 
            // btnSaveChange
            // 
            this.btnSaveChange.Location = new System.Drawing.Point(14, 614);
            // 
            // btnSaveAsNew
            // 
            this.btnSaveAsNew.Location = new System.Drawing.Point(209, 551);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "№ свидетельства";
            // 
            // dgvExams
            // 
            this.dgvExams.AllowUserToAddRows = false;
            this.dgvExams.AllowUserToDeleteRows = false;
            this.dgvExams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvExams.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExams.Location = new System.Drawing.Point(14, 271);
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.RowHeadersVisible = false;
            this.dgvExams.Size = new System.Drawing.Size(326, 303);
            this.dgvExams.TabIndex = 7;
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(145, 8);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(149, 20);
            this.tbNumber.TabIndex = 0;
            // 
            // epErrorInput
            // 
            this.epErrorInput.ContainerControl = this;
            // 
            // chbNewFIO
            // 
            this.chbNewFIO.AutoSize = true;
            this.chbNewFIO.Location = new System.Drawing.Point(14, 106);
            this.chbNewFIO.Name = "chbNewFIO";
            this.chbNewFIO.Size = new System.Drawing.Size(259, 30);
            this.chbNewFIO.TabIndex = 3;
            this.chbNewFIO.Text = "Ввести полностью ФИО , если не совпадает \r\nс ФИО в паспорте (буквы Е и Ё различаю" +
                "тся)";
            this.chbNewFIO.UseVisualStyleBackColor = true;
            this.chbNewFIO.CheckedChanged += new System.EventHandler(this.chbNewFIO_CheckedChanged);
            // 
            // tbSurname
            // 
            this.tbSurname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSurname.Enabled = false;
            this.tbSurname.Location = new System.Drawing.Point(14, 162);
            this.tbSurname.Name = "tbSurname";
            this.tbSurname.Size = new System.Drawing.Size(102, 20);
            this.tbSurname.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Фамилия";
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(244, 36);
            this.tbYear.MaxLength = 4;
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(50, 20);
            this.tbYear.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Год выдачи свидетельства";
            // 
            // tbPrintNumber
            // 
            this.tbPrintNumber.Location = new System.Drawing.Point(164, 62);
            this.tbPrintNumber.Name = "tbPrintNumber";
            this.tbPrintNumber.Size = new System.Drawing.Size(130, 20);
            this.tbPrintNumber.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Типографский номер";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(45, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(248, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Обязателен для свидетельств после 2009 года";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Enabled = false;
            this.tbName.Location = new System.Drawing.Point(122, 162);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(85, 20);
            this.tbName.TabIndex = 5;
            // 
            // tbSecondName
            // 
            this.tbSecondName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSecondName.Enabled = false;
            this.tbSecondName.Location = new System.Drawing.Point(213, 162);
            this.tbSecondName.Name = "tbSecondName";
            this.tbSecondName.Size = new System.Drawing.Size(127, 20);
            this.tbSecondName.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Имя";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(210, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Отчество";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(12, 216);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(65, 13);
            this.label47.TabIndex = 89;
            this.label47.Text = "Ответ ФБС";
            // 
            // tbFBSComment
            // 
            this.tbFBSComment.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tbFBSComment.Enabled = false;
            this.tbFBSComment.Location = new System.Drawing.Point(86, 213);
            this.tbFBSComment.Multiline = true;
            this.tbFBSComment.Name = "tbFBSComment";
            this.tbFBSComment.ReadOnly = true;
            this.tbFBSComment.Size = new System.Drawing.Size(179, 31);
            this.tbFBSComment.TabIndex = 88;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(12, 191);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(69, 13);
            this.label45.TabIndex = 87;
            this.label45.Text = "Статус ФБС";
            // 
            // tbFBSStatus
            // 
            this.tbFBSStatus.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tbFBSStatus.Enabled = false;
            this.tbFBSStatus.Location = new System.Drawing.Point(86, 188);
            this.tbFBSStatus.Name = "tbFBSStatus";
            this.tbFBSStatus.ReadOnly = true;
            this.tbFBSStatus.Size = new System.Drawing.Size(177, 20);
            this.tbFBSStatus.TabIndex = 86;
            // 
            // lblIsImported
            // 
            this.lblIsImported.AutoSize = true;
            this.lblIsImported.ForeColor = System.Drawing.Color.Red;
            this.lblIsImported.Location = new System.Drawing.Point(11, 255);
            this.lblIsImported.Name = "lblIsImported";
            this.lblIsImported.Size = new System.Drawing.Size(105, 13);
            this.lblIsImported.TabIndex = 90;
            this.lblIsImported.Text = "Загружено из ФБС";
            // 
            // btnSetStatusPasha
            // 
            this.btnSetStatusPasha.Location = new System.Drawing.Point(109, 587);
            this.btnSetStatusPasha.Name = "btnSetStatusPasha";
            this.btnSetStatusPasha.Size = new System.Drawing.Size(120, 23);
            this.btnSetStatusPasha.TabIndex = 91;
            this.btnSetStatusPasha.Text = "Проверено вручную";
            this.btnSetStatusPasha.UseVisualStyleBackColor = true;
            this.btnSetStatusPasha.Click += new System.EventHandler(this.btnSetStatusPasha_Click);
            // 
            // tbCommentFBSPasha
            // 
            this.tbCommentFBSPasha.Location = new System.Drawing.Point(109, 616);
            this.tbCommentFBSPasha.Name = "tbCommentFBSPasha";
            this.tbCommentFBSPasha.Size = new System.Drawing.Size(129, 20);
            this.tbCommentFBSPasha.TabIndex = 92;
            this.tbCommentFBSPasha.Visible = false;
            // 
            // EgeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 642);
            this.Controls.Add(this.tbCommentFBSPasha);
            this.Controls.Add(this.btnSetStatusPasha);
            this.Controls.Add(this.lblIsImported);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.tbFBSComment);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.tbFBSStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbSecondName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPrintNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSurname);
            this.Controls.Add(this.chbNewFIO);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EgeCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сертификат ЕГЭ";
            this.Controls.SetChildIndex(this.btnSaveAsNew, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dgvExams, 0);
            this.Controls.SetChildIndex(this.tbNumber, 0);
            this.Controls.SetChildIndex(this.chbNewFIO, 0);
            this.Controls.SetChildIndex(this.tbSurname, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.tbYear, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbPrintNumber, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.tbSecondName, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.tbFBSStatus, 0);
            this.Controls.SetChildIndex(this.label45, 0);
            this.Controls.SetChildIndex(this.tbFBSComment, 0);
            this.Controls.SetChildIndex(this.label47, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnSaveChange, 0);
            this.Controls.SetChildIndex(this.lblIsImported, 0);
            this.Controls.SetChildIndex(this.btnSetStatusPasha, 0);
            this.Controls.SetChildIndex(this.tbCommentFBSPasha, 0);
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrorInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvExams;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.ErrorProvider epErrorInput;
        private System.Windows.Forms.CheckBox chbNewFIO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSurname;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPrintNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSecondName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox tbFBSComment;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox tbFBSStatus;
        private System.Windows.Forms.Label lblIsImported;
        private System.Windows.Forms.Button btnSetStatusPasha;
        private System.Windows.Forms.TextBox tbCommentFBSPasha;

    }
}