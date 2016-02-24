namespace Priem
{
    partial class DisEntryViewList
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
            this.chbIsForeign = new System.Windows.Forms.CheckBox();
            this.btnCancelView = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.dgvViews = new System.Windows.Forms.DataGridView();
            this.btnPrintOrder = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.chbIsSecond = new System.Windows.Forms.CheckBox();
            this.chbIsReduced = new System.Windows.Forms.CheckBox();
            this.chbIsParallel = new System.Windows.Forms.CheckBox();
            this.cbStudyForm = new System.Windows.Forms.ComboBox();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.cbLicenseProgram = new System.Windows.Forms.ComboBox();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.chbIsListener = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViews)).BeginInit();
            this.SuspendLayout();
            // 
            // chbIsForeign
            // 
            this.chbIsForeign.AutoSize = true;
            this.chbIsForeign.Location = new System.Drawing.Point(139, 482);
            this.chbIsForeign.Name = "chbIsForeign";
            this.chbIsForeign.Size = new System.Drawing.Size(112, 17);
            this.chbIsForeign.TabIndex = 103;
            this.chbIsForeign.Text = "для иностранцев";
            this.chbIsForeign.UseVisualStyleBackColor = true;
            // 
            // btnCancelView
            // 
            this.btnCancelView.Location = new System.Drawing.Point(206, 509);
            this.btnCancelView.Name = "btnCancelView";
            this.btnCancelView.Size = new System.Drawing.Size(148, 23);
            this.btnCancelView.TabIndex = 102;
            this.btnCancelView.Text = "Отменить представление";
            this.btnCancelView.UseVisualStyleBackColor = true;
            this.btnCancelView.Click += new System.EventHandler(this.btnCancelView_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.Location = new System.Drawing.Point(230, 416);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(121, 52);
            this.btnCreate.TabIndex = 99;
            this.btnCreate.Text = "Создать представление на отчисление";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // dgvViews
            // 
            this.dgvViews.AllowUserToAddRows = false;
            this.dgvViews.AllowUserToDeleteRows = false;
            this.dgvViews.AllowUserToResizeRows = false;
            this.dgvViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViews.Location = new System.Drawing.Point(12, 255);
            this.dgvViews.Name = "dgvViews";
            this.dgvViews.ReadOnly = true;
            this.dgvViews.Size = new System.Drawing.Size(339, 154);
            this.dgvViews.TabIndex = 98;
            // 
            // btnPrintOrder
            // 
            this.btnPrintOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintOrder.Location = new System.Drawing.Point(12, 509);
            this.btnPrintOrder.Name = "btnPrintOrder";
            this.btnPrintOrder.Size = new System.Drawing.Size(121, 27);
            this.btnPrintOrder.TabIndex = 90;
            this.btnPrintOrder.Text = "Печать приказа";
            this.btnPrintOrder.UseVisualStyleBackColor = true;
            this.btnPrintOrder.Click += new System.EventHandler(this.btnPrintOrder_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Location = new System.Drawing.Point(12, 415);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(121, 40);
            this.btnPrint.TabIndex = 104;
            this.btnPrint.Text = "Печать представления";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateOrder.Location = new System.Drawing.Point(12, 476);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(121, 27);
            this.btnCreateOrder.TabIndex = 105;
            this.btnCreateOrder.Text = "Отчислить";
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // chbIsSecond
            // 
            this.chbIsSecond.AutoSize = true;
            this.chbIsSecond.Location = new System.Drawing.Point(217, 109);
            this.chbIsSecond.Name = "chbIsSecond";
            this.chbIsSecond.Size = new System.Drawing.Size(137, 17);
            this.chbIsSecond.TabIndex = 162;
            this.chbIsSecond.Text = "для лиц, имеющих ВО";
            this.chbIsSecond.UseVisualStyleBackColor = true;
            // 
            // chbIsReduced
            // 
            this.chbIsReduced.AutoSize = true;
            this.chbIsReduced.Location = new System.Drawing.Point(12, 109);
            this.chbIsReduced.Name = "chbIsReduced";
            this.chbIsReduced.Size = new System.Drawing.Size(95, 17);
            this.chbIsReduced.TabIndex = 161;
            this.chbIsReduced.Text = "сокращенная";
            this.chbIsReduced.UseVisualStyleBackColor = true;
            // 
            // chbIsParallel
            // 
            this.chbIsParallel.AutoSize = true;
            this.chbIsParallel.Location = new System.Drawing.Point(113, 109);
            this.chbIsParallel.Name = "chbIsParallel";
            this.chbIsParallel.Size = new System.Drawing.Size(98, 17);
            this.chbIsParallel.TabIndex = 160;
            this.chbIsParallel.Text = "параллельная";
            this.chbIsParallel.UseVisualStyleBackColor = true;
            // 
            // cbStudyForm
            // 
            this.cbStudyForm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyForm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyForm.FormattingEnabled = true;
            this.cbStudyForm.Location = new System.Drawing.Point(12, 151);
            this.cbStudyForm.Name = "cbStudyForm";
            this.cbStudyForm.Size = new System.Drawing.Size(170, 21);
            this.cbStudyForm.TabIndex = 159;
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyBasis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(12, 72);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(156, 21);
            this.cbStudyBasis.TabIndex = 158;
            // 
            // cbLicenseProgram
            // 
            this.cbLicenseProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbLicenseProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbLicenseProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseProgram.FormattingEnabled = true;
            this.cbLicenseProgram.Location = new System.Drawing.Point(12, 196);
            this.cbLicenseProgram.Name = "cbLicenseProgram";
            this.cbLicenseProgram.Size = new System.Drawing.Size(342, 21);
            this.cbLicenseProgram.TabIndex = 157;
            // 
            // cbFaculty
            // 
            this.cbFaculty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFaculty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(12, 25);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(342, 21);
            this.cbFaculty.TabIndex = 156;
            // 
            // chbIsListener
            // 
            this.chbIsListener.AutoSize = true;
            this.chbIsListener.Location = new System.Drawing.Point(12, 232);
            this.chbIsListener.Name = "chbIsListener";
            this.chbIsListener.Size = new System.Drawing.Size(80, 17);
            this.chbIsListener.TabIndex = 155;
            this.chbIsListener.Text = "слушатели";
            this.chbIsListener.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 154;
            this.label1.Text = "Направление (Направление)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 153;
            this.label2.Text = "Факультет";
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(9, 56);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(94, 13);
            this.lblLanguage.TabIndex = 152;
            this.lblLanguage.Text = "Основа обучения";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 151;
            this.label3.Text = "Форма обучения";
            // 
            // DisEntryViewList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 544);
            this.Controls.Add(this.chbIsSecond);
            this.Controls.Add(this.chbIsReduced);
            this.Controls.Add(this.chbIsParallel);
            this.Controls.Add(this.cbStudyForm);
            this.Controls.Add(this.cbStudyBasis);
            this.Controls.Add(this.cbLicenseProgram);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.chbIsListener);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCreateOrder);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.chbIsForeign);
            this.Controls.Add(this.btnCancelView);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.dgvViews);
            this.Controls.Add(this.btnPrintOrder);
            this.Name = "DisEntryViewList";
            this.Text = "Представление на отчисление";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisEntryViewList_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbIsForeign;
        private System.Windows.Forms.Button btnCancelView;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.DataGridView dgvViews;
        private System.Windows.Forms.Button btnPrintOrder;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.CheckBox chbIsSecond;
        private System.Windows.Forms.CheckBox chbIsReduced;
        private System.Windows.Forms.CheckBox chbIsParallel;
        private System.Windows.Forms.ComboBox cbStudyForm;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.ComboBox cbLicenseProgram;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.CheckBox chbIsListener;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label label3;

    }
}