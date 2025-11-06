namespace UMFST.MIP.Variant2
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
            this.btnImportData = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.dgvWorkOrders = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl.SuspendLayout();
            this.dgvWorkOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImportData
            // 
            this.btnImportData.Location = new System.Drawing.Point(23, 29);
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(182, 55);
            this.btnImportData.TabIndex = 0;
            this.btnImportData.Text = "Import data";
            this.btnImportData.UseVisualStyleBackColor = true;
            this.btnImportData.Click += new System.EventHandler(this.btnImportData_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.dgvWorkOrders);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 90);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1131, 674);
            this.tabControl.TabIndex = 1;
            // 
            // dgvWorkOrders
            // 
            this.dgvWorkOrders.Controls.Add(this.dataGridView1);
            this.dgvWorkOrders.Location = new System.Drawing.Point(8, 39);
            this.dgvWorkOrders.Name = "dgvWorkOrders";
            this.dgvWorkOrders.Padding = new System.Windows.Forms.Padding(3);
            this.dgvWorkOrders.Size = new System.Drawing.Size(1115, 627);
            this.dgvWorkOrders.TabIndex = 0;
            this.dgvWorkOrders.Text = "work orders";
            this.dgvWorkOrders.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1115, 627);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(43, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 776);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnImportData);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.tabControl.ResumeLayout(false);
            this.dgvWorkOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportData;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage dgvWorkOrders;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

