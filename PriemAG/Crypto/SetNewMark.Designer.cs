namespace Priem
{
    partial class SetNewMark
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetNewMark));
            this.lblAdd = new System.Windows.Forms.Label();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbExam = new System.Windows.Forms.TextBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.btnRightAll = new System.Windows.Forms.Button();
            this.btnLeftAll = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvRight = new System.Windows.Forms.DataGridView();
            this.dgvLeft = new System.Windows.Forms.DataGridView();
            this.lblCountLeft = new System.Windows.Forms.Label();
            this.lblCountRight = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.ForeColor = System.Drawing.Color.Red;
            this.lblAdd.Location = new System.Drawing.Point(291, 36);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(116, 13);
            this.lblAdd.TabIndex = 83;
            this.lblAdd.Text = "ДОПОЛНИТЕЛЬНАЯ";
            this.lblAdd.Visible = false;
            // 
            // tbDate
            // 
            this.tbDate.Enabled = false;
            this.tbDate.Location = new System.Drawing.Point(190, 33);
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new System.Drawing.Size(95, 20);
            this.tbDate.TabIndex = 76;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Экзамен";
            // 
            // tbExam
            // 
            this.tbExam.Enabled = false;
            this.tbExam.Location = new System.Drawing.Point(11, 33);
            this.tbExam.Name = "tbExam";
            this.tbExam.Size = new System.Drawing.Size(173, 20);
            this.tbExam.TabIndex = 74;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(187, 17);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(33, 13);
            this.lblNum.TabIndex = 73;
            this.lblNum.Text = "Дата";
            // 
            // btnRightAll
            // 
            this.btnRightAll.Location = new System.Drawing.Point(347, 426);
            this.btnRightAll.Name = "btnRightAll";
            this.btnRightAll.Size = new System.Drawing.Size(33, 23);
            this.btnRightAll.TabIndex = 72;
            this.btnRightAll.Text = ">>";
            this.btnRightAll.UseVisualStyleBackColor = true;
            this.btnRightAll.Click += new System.EventHandler(this.btnRightAll_Click);
            // 
            // btnLeftAll
            // 
            this.btnLeftAll.Location = new System.Drawing.Point(327, 376);
            this.btnLeftAll.Name = "btnLeftAll";
            this.btnLeftAll.Size = new System.Drawing.Size(33, 23);
            this.btnLeftAll.TabIndex = 69;
            this.btnLeftAll.Text = "<<";
            this.btnLeftAll.UseVisualStyleBackColor = true;
            this.btnLeftAll.Click += new System.EventHandler(this.btnLeftAll_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(347, 167);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(33, 23);
            this.btnRight.TabIndex = 70;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(327, 117);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(33, 23);
            this.btnLeft.TabIndex = 71;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(394, 506);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(144, 23);
            this.btnOk.TabIndex = 67;
            this.btnOk.Text = "Подтвердить и закрыть";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(544, 506);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 23);
            this.btnCancel.TabIndex = 68;
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
            this.dgvRight.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRight.Location = new System.Drawing.Point(394, 59);
            this.dgvRight.MultiSelect = false;
            this.dgvRight.Name = "dgvRight";
            this.dgvRight.Size = new System.Drawing.Size(300, 422);
            this.dgvRight.TabIndex = 65;
            this.dgvRight.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRight_CellDoubleClick);
            // 
            // dgvLeft
            // 
            this.dgvLeft.AllowUserToAddRows = false;
            this.dgvLeft.AllowUserToDeleteRows = false;
            this.dgvLeft.AllowUserToResizeRows = false;
            this.dgvLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dgvLeft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeft.Location = new System.Drawing.Point(11, 59);
            this.dgvLeft.MultiSelect = false;
            this.dgvLeft.Name = "dgvLeft";
            this.dgvLeft.Size = new System.Drawing.Size(300, 422);
            this.dgvLeft.TabIndex = 66;
            // 
            // lblCountLeft
            // 
            this.lblCountLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCountLeft.AutoSize = true;
            this.lblCountLeft.Location = new System.Drawing.Point(113, 484);
            this.lblCountLeft.Name = "lblCountLeft";
            this.lblCountLeft.Size = new System.Drawing.Size(0, 13);
            this.lblCountLeft.TabIndex = 84;
            // 
            // lblCountRight
            // 
            this.lblCountRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountRight.AutoSize = true;
            this.lblCountRight.Location = new System.Drawing.Point(534, 484);
            this.lblCountRight.Name = "lblCountRight";
            this.lblCountRight.Size = new System.Drawing.Size(0, 13);
            this.lblCountRight.TabIndex = 85;
            // 
            // SetNewMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 541);
            this.Controls.Add(this.lblCountRight);
            this.Controls.Add(this.lblCountLeft);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.tbDate);
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
            this.Name = "SetNewMark";
            this.Text = "Изменение оценки";
            this.Resize += new System.EventHandler(this.SetNewMark_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.TextBox tbDate;
        protected System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbExam;
        protected System.Windows.Forms.Label lblNum;
        protected System.Windows.Forms.Button btnRightAll;
        protected System.Windows.Forms.Button btnLeftAll;
        protected System.Windows.Forms.Button btnRight;
        protected System.Windows.Forms.Button btnLeft;
        protected System.Windows.Forms.Button btnOk;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.DataGridView dgvRight;
        protected System.Windows.Forms.DataGridView dgvLeft;
        protected System.Windows.Forms.Label lblCountLeft;
        protected System.Windows.Forms.Label lblCountRight;
    }
}