namespace EasyInstall
{
    partial class MainWindow
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnplus = new System.Windows.Forms.Button();
            this.btnminus = new System.Windows.Forms.Button();
            this.btninstall = new System.Windows.Forms.Button();
            this.btnedit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(492, 283);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnplus
            // 
            this.btnplus.Location = new System.Drawing.Point(44, 328);
            this.btnplus.Name = "btnplus";
            this.btnplus.Size = new System.Drawing.Size(60, 60);
            this.btnplus.TabIndex = 1;
            this.btnplus.Text = "Plus";
            this.btnplus.UseVisualStyleBackColor = true;
            this.btnplus.Click += new System.EventHandler(this.btnplus_Click);
            // 
            // btnminus
            // 
            this.btnminus.Location = new System.Drawing.Point(122, 328);
            this.btnminus.Name = "btnminus";
            this.btnminus.Size = new System.Drawing.Size(60, 60);
            this.btnminus.TabIndex = 2;
            this.btnminus.Text = "Minus";
            this.btnminus.UseVisualStyleBackColor = true;
            this.btnminus.Click += new System.EventHandler(this.btnminus_Click);
            // 
            // btninstall
            // 
            this.btninstall.Location = new System.Drawing.Point(357, 328);
            this.btninstall.Name = "btninstall";
            this.btninstall.Size = new System.Drawing.Size(60, 60);
            this.btninstall.TabIndex = 3;
            this.btninstall.Text = "Install";
            this.btninstall.UseVisualStyleBackColor = true;
            this.btninstall.Click += new System.EventHandler(this.btninstall_Click);
            // 
            // btnedit
            // 
            this.btnedit.Location = new System.Drawing.Point(433, 328);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(60, 60);
            this.btnedit.TabIndex = 4;
            this.btnedit.Text = "Edit";
            this.btnedit.UseVisualStyleBackColor = true;
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 418);
            this.Controls.Add(this.btnedit);
            this.Controls.Add(this.btninstall);
            this.Controls.Add(this.btnminus);
            this.Controls.Add(this.btnplus);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainWindow";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnplus;
        private System.Windows.Forms.Button btnminus;
        private System.Windows.Forms.Button btninstall;
        private System.Windows.Forms.Button btnedit;
    }
}