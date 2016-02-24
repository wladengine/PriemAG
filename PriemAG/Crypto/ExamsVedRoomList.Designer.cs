namespace Priem
{
    partial class ExamsVedRoomList
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
            this.btnCreateRooms = new System.Windows.Forms.Button();
            this.btnCreateCsv = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLocked = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.lblProtocolNum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.cbExamVed = new System.Windows.Forms.ComboBox();
            this.cbExamVedRoom = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateRooms
            // 
            this.btnCreateRooms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateRooms.Location = new System.Drawing.Point(288, 114);
            this.btnCreateRooms.Name = "btnCreateRooms";
            this.btnCreateRooms.Size = new System.Drawing.Size(165, 23);
            this.btnCreateRooms.TabIndex = 79;
            this.btnCreateRooms.Text = "Разделить по помещениям";
            this.btnCreateRooms.UseVisualStyleBackColor = true;
            this.btnCreateRooms.Click += new System.EventHandler(this.btnCreateRooms_Click);
            // 
            // btnCreateCsv
            // 
            this.btnCreateCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateCsv.Location = new System.Drawing.Point(124, 455);
            this.btnCreateCsv.Name = "btnCreateCsv";
            this.btnCreateCsv.Size = new System.Drawing.Size(87, 31);
            this.btnCreateCsv.TabIndex = 78;
            this.btnCreateCsv.Text = "Создать csv";
            this.btnCreateCsv.UseVisualStyleBackColor = true;
            this.btnCreateCsv.Click += new System.EventHandler(this.btnCreateCsv_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 13);
            this.label2.TabIndex = 76;
            this.label2.Text = "Разделение по помещениям";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Основа обучения";
            // 
            // lblLocked
            // 
            this.lblLocked.AutoSize = true;
            this.lblLocked.ForeColor = System.Drawing.Color.Red;
            this.lblLocked.Location = new System.Drawing.Point(350, 69);
            this.lblLocked.Name = "lblLocked";
            this.lblLocked.Size = new System.Drawing.Size(51, 13);
            this.lblLocked.TabIndex = 69;
            this.lblLocked.Text = "Закрыта";
            this.lblLocked.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(799, 463);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 64;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(12, 143);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(862, 292);
            this.dgvList.TabIndex = 63;
            this.dgvList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvList_CellFormatting);
            // 
            // lblProtocolNum
            // 
            this.lblProtocolNum.AutoSize = true;
            this.lblProtocolNum.Location = new System.Drawing.Point(12, 50);
            this.lblProtocolNum.Name = "lblProtocolNum";
            this.lblProtocolNum.Size = new System.Drawing.Size(63, 13);
            this.lblProtocolNum.TabIndex = 60;
            this.lblProtocolNum.Text = "Ведомость";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Факультет";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(12, 470);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 23);
            this.btnDelete.TabIndex = 68;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChange.Enabled = false;
            this.btnChange.Location = new System.Drawing.Point(12, 441);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(87, 23);
            this.btnChange.TabIndex = 65;
            this.btnChange.Text = "Изменить";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbStudyBasis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(264, 24);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(136, 21);
            this.cbStudyBasis.TabIndex = 148;
            // 
            // cbFaculty
            // 
            this.cbFaculty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFaculty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(15, 24);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(226, 21);
            this.cbFaculty.TabIndex = 147;
            // 
            // cbExamVed
            // 
            this.cbExamVed.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbExamVed.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbExamVed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExamVed.FormattingEnabled = true;
            this.cbExamVed.Location = new System.Drawing.Point(15, 69);
            this.cbExamVed.Name = "cbExamVed";
            this.cbExamVed.Size = new System.Drawing.Size(421, 21);
            this.cbExamVed.TabIndex = 146;
            // 
            // cbExamVedRoom
            // 
            this.cbExamVedRoom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbExamVedRoom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbExamVedRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExamVedRoom.FormattingEnabled = true;
            this.cbExamVedRoom.Location = new System.Drawing.Point(15, 116);
            this.cbExamVedRoom.Name = "cbExamVedRoom";
            this.cbExamVedRoom.Size = new System.Drawing.Size(226, 21);
            this.cbExamVedRoom.TabIndex = 149;
            // 
            // ExamsVedRoomList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 498);
            this.Controls.Add(this.cbExamVedRoom);
            this.Controls.Add(this.cbStudyBasis);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.cbExamVed);
            this.Controls.Add(this.btnCreateRooms);
            this.Controls.Add(this.btnCreateCsv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblLocked);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.lblProtocolNum);
            this.Controls.Add(this.label1);
            this.Name = "ExamsVedRoomList";
            this.Text = "Ведомости для экзамена (помещения и csv)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnCreateRooms;
        protected System.Windows.Forms.Button btnCreateCsv;        
        protected System.Windows.Forms.Label label2;       
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLocked;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.DataGridView dgvList;       
        protected System.Windows.Forms.Label lblProtocolNum;        
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Button btnDelete;
        protected System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.ComboBox cbExamVed;
        private System.Windows.Forms.ComboBox cbExamVedRoom;
    }
}