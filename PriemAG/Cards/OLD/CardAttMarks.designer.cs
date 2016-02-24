namespace Priem
{
    public partial class CardAttMarks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardAttMarks));
            this.dgvExams = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMark = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbAvg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSubjects = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvExams
            // 
            this.dgvExams.AllowUserToAddRows = false;
            this.dgvExams.AllowUserToDeleteRows = false;
            this.dgvExams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvExams.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExams.Location = new System.Drawing.Point(12, 110);
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.ReadOnly = true;
            this.dgvExams.RowHeadersVisible = false;
            this.dgvExams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExams.Size = new System.Drawing.Size(403, 296);
            this.dgvExams.TabIndex = 3;
            this.dgvExams.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExams_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Оценка";
            // 
            // tbMark
            // 
            this.tbMark.Location = new System.Drawing.Point(61, 69);
            this.tbMark.Name = "tbMark";
            this.tbMark.Size = new System.Drawing.Size(57, 20);
            this.tbMark.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(124, 67);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(119, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Выберите предмет:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(296, 412);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(119, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbAvg
            // 
            this.tbAvg.Enabled = false;
            this.tbAvg.Location = new System.Drawing.Point(364, 49);
            this.tbAvg.Name = "tbAvg";
            this.tbAvg.Size = new System.Drawing.Size(51, 20);
            this.tbAvg.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Ср. балл";
            // 
            // cbSubjects
            // 
            this.cbSubjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSubjects.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbSubjects.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSubjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubjects.FormattingEnabled = true;
            this.cbSubjects.Location = new System.Drawing.Point(12, 32);
            this.cbSubjects.Name = "cbSubjects";
            this.cbSubjects.Size = new System.Drawing.Size(231, 21);
            this.cbSubjects.TabIndex = 114;
            // 
            // CardAttMarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 447);
            this.Controls.Add(this.cbSubjects);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbAvg);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbMark);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvExams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CardAttMarks";
            this.Text = "Оценки аттестата";
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExams;       
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMark;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbAvg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSubjects;
    }
}