namespace Priem
{
    partial class ProtocolCard
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
            this.chbInostr = new System.Windows.Forms.CheckBox();
            this.lblHeaderText = new System.Windows.Forms.Label();
            this.epError = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblTotalRight = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.chbFilter = new System.Windows.Forms.CheckBox();
            this.chbEnable = new System.Windows.Forms.CheckBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblTotalLeft = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.tbNum = new System.Windows.Forms.TextBox();
            this.btnRightAll = new System.Windows.Forms.Button();
            this.btnLeftAll = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvRight = new System.Windows.Forms.DataGridView();
            this.dgvLeft = new System.Windows.Forms.DataGridView();
            this.cbHeaders = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // chbInostr
            // 
            this.chbInostr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbInostr.AutoSize = true;
            this.chbInostr.Location = new System.Drawing.Point(413, 54);
            this.chbInostr.Name = "chbInostr";
            this.chbInostr.Size = new System.Drawing.Size(89, 17);
            this.chbInostr.TabIndex = 61;
            this.chbInostr.Text = "Иностранцы";
            this.chbInostr.UseVisualStyleBackColor = true;
            this.chbInostr.Visible = false;
            // 
            // lblHeaderText
            // 
            this.lblHeaderText.AutoSize = true;
            this.lblHeaderText.Location = new System.Drawing.Point(410, 10);
            this.lblHeaderText.Name = "lblHeaderText";
            this.lblHeaderText.Size = new System.Drawing.Size(0, 13);
            this.lblHeaderText.TabIndex = 60;
            // 
            // epError
            // 
            this.epError.ContainerControl = this;
            // 
            // lblTotalRight
            // 
            this.lblTotalRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalRight.Location = new System.Drawing.Point(623, 402);
            this.lblTotalRight.Name = "lblTotalRight";
            this.lblTotalRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalRight.Size = new System.Drawing.Size(76, 16);
            this.lblTotalRight.TabIndex = 59;
            this.lblTotalRight.Text = "Всего";
            this.lblTotalRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(215, 454);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(115, 23);
            this.btnDelete.TabIndex = 58;
            this.btnDelete.Text = "Удалить протокол";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // chbFilter
            // 
            this.chbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chbFilter.AutoSize = true;
            this.chbFilter.Location = new System.Drawing.Point(389, 421);
            this.chbFilter.Name = "chbFilter";
            this.chbFilter.Size = new System.Drawing.Size(314, 17);
            this.chbFilter.TabIndex = 56;
            this.chbFilter.Text = "Отфильтровать абитуриентов с проверенными данными";
            this.chbFilter.UseVisualStyleBackColor = true;
            this.chbFilter.Visible = false;
            this.chbFilter.CheckedChanged += new System.EventHandler(this.chbFilter_CheckedChanged);
            // 
            // chbEnable
            // 
            this.chbEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbEnable.AutoSize = true;
            this.chbEnable.Location = new System.Drawing.Point(12, 421);
            this.chbEnable.Name = "chbEnable";
            this.chbEnable.Size = new System.Drawing.Size(279, 17);
            this.chbEnable.TabIndex = 57;
            this.chbEnable.Text = "Внести всех выбранных абитуриентов в протокол";
            this.chbEnable.UseVisualStyleBackColor = true;
            this.chbEnable.CheckedChanged += new System.EventHandler(this.chbEnable_CheckedChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(183, 28);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(132, 20);
            this.dtpDate.TabIndex = 55;
            // 
            // lblTotalLeft
            // 
            this.lblTotalLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalLeft.AutoSize = true;
            this.lblTotalLeft.Location = new System.Drawing.Point(13, 402);
            this.lblTotalLeft.Name = "lblTotalLeft";
            this.lblTotalLeft.Size = new System.Drawing.Size(37, 13);
            this.lblTotalLeft.TabIndex = 53;
            this.lblTotalLeft.Text = "Всего";
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(13, 31);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(41, 13);
            this.lblNum.TabIndex = 54;
            this.lblNum.Text = "Номер";
            // 
            // tbNum
            // 
            this.tbNum.Location = new System.Drawing.Point(60, 28);
            this.tbNum.Name = "tbNum";
            this.tbNum.ReadOnly = true;
            this.tbNum.Size = new System.Drawing.Size(104, 20);
            this.tbNum.TabIndex = 52;
            // 
            // btnRightAll
            // 
            this.btnRightAll.Location = new System.Drawing.Point(350, 286);
            this.btnRightAll.Name = "btnRightAll";
            this.btnRightAll.Size = new System.Drawing.Size(33, 23);
            this.btnRightAll.TabIndex = 49;
            this.btnRightAll.Text = ">>";
            this.btnRightAll.UseVisualStyleBackColor = true;
            this.btnRightAll.Click += new System.EventHandler(this.btnRightAll_Click);
            // 
            // btnLeftAll
            // 
            this.btnLeftAll.Location = new System.Drawing.Point(330, 236);
            this.btnLeftAll.Name = "btnLeftAll";
            this.btnLeftAll.Size = new System.Drawing.Size(33, 23);
            this.btnLeftAll.TabIndex = 48;
            this.btnLeftAll.Text = "<<";
            this.btnLeftAll.UseVisualStyleBackColor = true;
            this.btnLeftAll.Click += new System.EventHandler(this.btnLeftAll_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(350, 152);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(33, 23);
            this.btnRight.TabIndex = 51;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(330, 102);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(33, 23);
            this.btnLeft.TabIndex = 50;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreview.Location = new System.Drawing.Point(12, 454);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(175, 23);
            this.btnPreview.TabIndex = 45;
            this.btnPreview.Text = "Предварительный просмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(388, 454);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(150, 23);
            this.btnOk.TabIndex = 46;
            this.btnOk.Text = "Подтвердить и закрыть";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(553, 454);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 23);
            this.btnCancel.TabIndex = 47;
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
            this.dgvRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRight.Location = new System.Drawing.Point(399, 73);
            this.dgvRight.MultiSelect = false;
            this.dgvRight.Name = "dgvRight";
            this.dgvRight.Size = new System.Drawing.Size(300, 319);
            this.dgvRight.TabIndex = 43;
            this.dgvRight.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRight_CellDoubleClick);
            // 
            // dgvLeft
            // 
            this.dgvLeft.AllowUserToAddRows = false;
            this.dgvLeft.AllowUserToDeleteRows = false;
            this.dgvLeft.AllowUserToResizeRows = false;
            this.dgvLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dgvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeft.Location = new System.Drawing.Point(15, 73);
            this.dgvLeft.MultiSelect = false;
            this.dgvLeft.Name = "dgvLeft";
            this.dgvLeft.Size = new System.Drawing.Size(300, 319);
            this.dgvLeft.TabIndex = 44;
            this.dgvLeft.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLeft_CellContentClick);
            // 
            // cbHeaders
            // 
            this.cbHeaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeaders.FormattingEnabled = true;
            this.cbHeaders.Location = new System.Drawing.Point(413, 28);
            this.cbHeaders.Name = "cbHeaders";
            this.cbHeaders.Size = new System.Drawing.Size(283, 21);
            this.cbHeaders.TabIndex = 62;
            this.cbHeaders.Visible = false;
            // 
            // ProtocolCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 486);
            this.Controls.Add(this.cbHeaders);
            this.Controls.Add(this.chbInostr);
            this.Controls.Add(this.lblHeaderText);
            this.Controls.Add(this.lblTotalRight);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.chbFilter);
            this.Controls.Add(this.chbEnable);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblTotalLeft);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.tbNum);
            this.Controls.Add(this.btnRightAll);
            this.Controls.Add(this.btnLeftAll);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvRight);
            this.Controls.Add(this.dgvLeft);
            this.Name = "ProtocolCard";
            this.Text = "Protocol";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Protocol_FormClosed);
            this.Resize += new System.EventHandler(this.Protocol_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.CheckBox chbInostr;
        protected System.Windows.Forms.Label lblHeaderText;
        protected System.Windows.Forms.ErrorProvider epError;
        protected System.Windows.Forms.Label lblTotalRight;
        protected System.Windows.Forms.Button btnDelete;
        protected System.Windows.Forms.CheckBox chbFilter;
        protected System.Windows.Forms.CheckBox chbEnable;
        protected System.Windows.Forms.DateTimePicker dtpDate;
        protected System.Windows.Forms.Label lblTotalLeft;
        protected System.Windows.Forms.Label lblNum;
        protected System.Windows.Forms.TextBox tbNum;
        protected System.Windows.Forms.Button btnRightAll;
        protected System.Windows.Forms.Button btnLeftAll;
        protected System.Windows.Forms.Button btnRight;
        protected System.Windows.Forms.Button btnLeft;
        protected System.Windows.Forms.Button btnPreview;
        protected System.Windows.Forms.Button btnOk;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.DataGridView dgvRight;
        protected System.Windows.Forms.DataGridView dgvLeft;
        protected System.Windows.Forms.ComboBox cbHeaders;
    }
}