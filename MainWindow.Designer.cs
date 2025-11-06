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
            this.buttonResetImport = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabWorkOrders = new System.Windows.Forms.TabPage();
            this.dataGridViewWorkOrders = new System.Windows.Forms.DataGridView();
            this.buttonExportSummary = new System.Windows.Forms.Button();
            this.tabClientsCars = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxClients = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewCars = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonFilterCars = new System.Windows.Forms.Button();
            this.textBoxYearFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxCarFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabDiagnostics = new System.Windows.Forms.TabPage();
            this.treeViewDiagnostics = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxDiagCar = new System.Windows.Forms.ComboBox();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.labelLoadingStatus = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabWorkOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkOrders)).BeginInit();
            this.tabClientsCars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabDiagnostics.SuspendLayout();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonResetImport
            // 
            this.buttonResetImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonResetImport.ForeColor = System.Drawing.Color.DarkRed;
            this.buttonResetImport.Location = new System.Drawing.Point(604, 12);
            this.buttonResetImport.Name = "buttonResetImport";
            this.buttonResetImport.Size = new System.Drawing.Size(182, 38);
            this.buttonResetImport.TabIndex = 0;
            this.buttonResetImport.Text = "Reset DB + Re-import JSON";
            this.buttonResetImport.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabWorkOrders);
            this.tabControl.Controls.Add(this.tabClientsCars);
            this.tabControl.Controls.Add(this.tabDiagnostics);
            this.tabControl.Location = new System.Drawing.Point(12, 56);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(776, 382);
            this.tabControl.TabIndex = 1;
            // 
            // tabWorkOrders
            // 
            this.tabWorkOrders.Controls.Add(this.dataGridViewWorkOrders);
            this.tabWorkOrders.Controls.Add(this.buttonExportSummary);
            this.tabWorkOrders.Location = new System.Drawing.Point(4, 22);
            this.tabWorkOrders.Name = "tabWorkOrders";
            this.tabWorkOrders.Padding = new System.Windows.Forms.Padding(3);
            this.tabWorkOrders.Size = new System.Drawing.Size(768, 356);
            this.tabWorkOrders.TabIndex = 0;
            this.tabWorkOrders.Text = "Work Orders";
            this.tabWorkOrders.UseVisualStyleBackColor = true;
            // 
            // dataGridViewWorkOrders
            // 
            this.dataGridViewWorkOrders.AllowUserToAddRows = false;
            this.dataGridViewWorkOrders.AllowUserToDeleteRows = false;
            this.dataGridViewWorkOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewWorkOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewWorkOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWorkOrders.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewWorkOrders.Name = "dataGridViewWorkOrders";
            this.dataGridViewWorkOrders.ReadOnly = true;
            this.dataGridViewWorkOrders.Size = new System.Drawing.Size(756, 315);
            this.dataGridViewWorkOrders.TabIndex = 0;
            // 
            // buttonExportSummary
            // 
            this.buttonExportSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportSummary.Location = new System.Drawing.Point(623, 327);
            this.buttonExportSummary.Name = "buttonExportSummary";
            this.buttonExportSummary.Size = new System.Drawing.Size(139, 23);
            this.buttonExportSummary.TabIndex = 1;
            this.buttonExportSummary.Text = "Export Summary";
            this.buttonExportSummary.UseVisualStyleBackColor = true;
            // 
            // tabClientsCars
            // 
            this.tabClientsCars.Controls.Add(this.splitContainer1);
            this.tabClientsCars.Controls.Add(this.panel1);
            this.tabClientsCars.Location = new System.Drawing.Point(4, 22);
            this.tabClientsCars.Name = "tabClientsCars";
            this.tabClientsCars.Padding = new System.Windows.Forms.Padding(3);
            this.tabClientsCars.Size = new System.Drawing.Size(768, 356);
            this.tabClientsCars.TabIndex = 1;
            this.tabClientsCars.Text = "Clients & Cars";
            this.tabClientsCars.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 44);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxClients);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewCars);
            this.splitContainer1.Size = new System.Drawing.Size(762, 309);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 1;
            // 
            // listBoxClients
            // 
            this.listBoxClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxClients.FormattingEnabled = true;
            this.listBoxClients.Location = new System.Drawing.Point(0, 13);
            this.listBoxClients.Name = "listBoxClients";
            this.listBoxClients.Size = new System.Drawing.Size(254, 296);
            this.listBoxClients.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clients";
            // 
            // dataGridViewCars
            // 
            this.dataGridViewCars.AllowUserToAddRows = false;
            this.dataGridViewCars.AllowUserToDeleteRows = false;
            this.dataGridViewCars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCars.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCars.Name = "dataGridViewCars";
            this.dataGridViewCars.ReadOnly = true;
            this.dataGridViewCars.Size = new System.Drawing.Size(504, 309);
            this.dataGridViewCars.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonFilterCars);
            this.panel1.Controls.Add(this.textBoxYearFilter);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxCarFilter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 41);
            this.panel1.TabIndex = 0;
            // 
            // buttonFilterCars
            // 
            this.buttonFilterCars.Location = new System.Drawing.Point(403, 8);
            this.buttonFilterCars.Name = "buttonFilterCars";
            this.buttonFilterCars.Size = new System.Drawing.Size(75, 23);
            this.buttonFilterCars.TabIndex = 4;
            this.buttonFilterCars.Text = "Filter";
            this.buttonFilterCars.UseVisualStyleBackColor = true;
            // 
            // textBoxYearFilter
            // 
            this.textBoxYearFilter.Location = new System.Drawing.Point(286, 10);
            this.textBoxYearFilter.Name = "textBoxYearFilter";
            this.textBoxYearFilter.Size = new System.Drawing.Size(100, 20);
            this.textBoxYearFilter.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Year:";
            // 
            // comboBoxCarFilter
            // 
            this.comboBoxCarFilter.FormattingEnabled = true;
            this.comboBoxCarFilter.Location = new System.Drawing.Point(82, 10);
            this.comboBoxCarFilter.Name = "comboBoxCarFilter";
            this.comboBoxCarFilter.Size = new System.Drawing.Size(149, 21);
            this.comboBoxCarFilter.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Car Make:";
            // 
            // tabDiagnostics
            // 
            this.tabDiagnostics.Controls.Add(this.treeViewDiagnostics);
            this.tabDiagnostics.Controls.Add(this.label4);
            this.tabDiagnostics.Controls.Add(this.comboBoxDiagCar);
            this.tabDiagnostics.Location = new System.Drawing.Point(4, 22);
            this.tabDiagnostics.Name = "tabDiagnostics";
            this.tabDiagnostics.Size = new System.Drawing.Size(768, 356);
            this.tabDiagnostics.TabIndex = 2;
            this.tabDiagnostics.Text = "Diagnostics";
            this.tabDiagnostics.UseVisualStyleBackColor = true;
            // 
            // treeViewDiagnostics
            // 
            this.treeViewDiagnostics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewDiagnostics.Location = new System.Drawing.Point(17, 49);
            this.treeViewDiagnostics.Name = "treeViewDiagnostics";
            this.treeViewDiagnostics.Size = new System.Drawing.Size(732, 291);
            this.treeViewDiagnostics.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Select Car:";
            // 
            // comboBoxDiagCar
            // 
            this.comboBoxDiagCar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDiagCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiagCar.FormattingEnabled = true;
            this.comboBoxDiagCar.Location = new System.Drawing.Point(80, 12);
            this.comboBoxDiagCar.Name = "comboBoxDiagCar";
            this.comboBoxDiagCar.Size = new System.Drawing.Size(669, 21);
            this.comboBoxDiagCar.TabIndex = 0;
            // 
            // panelLoading
            // 
            this.panelLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLoading.BackColor = System.Drawing.Color.Black;
            this.panelLoading.Controls.Add(this.labelLoadingStatus);
            this.panelLoading.Location = new System.Drawing.Point(0, 0);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(800, 450);
            this.panelLoading.TabIndex = 2;
            this.panelLoading.Visible = false;
            // 
            // labelLoadingStatus
            // 
            this.labelLoadingStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLoadingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoadingStatus.ForeColor = System.Drawing.Color.White;
            this.labelLoadingStatus.Location = new System.Drawing.Point(250, 205);
            this.labelLoadingStatus.Name = "labelLoadingStatus";
            this.labelLoadingStatus.Size = new System.Drawing.Size(300, 40);
            this.labelLoadingStatus.TabIndex = 0;
            this.labelLoadingStatus.Text = "Loading...";
            this.labelLoadingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonResetImport);
            this.Name = "MainWindow";
            this.Text = "Car Service Dashboard";
            this.tabControl.ResumeLayout(false);
            this.tabWorkOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkOrders)).EndInit();
            this.tabClientsCars.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabDiagnostics.ResumeLayout(false);
            this.tabDiagnostics.PerformLayout();
            this.panelLoading.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonResetImport;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabWorkOrders;
        private System.Windows.Forms.DataGridView dataGridViewWorkOrders;
        private System.Windows.Forms.TabPage tabClientsCars;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label labelLoadingStatus;
        private System.Windows.Forms.Button buttonExportSummary;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxClients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewCars;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonFilterCars;
        private System.Windows.Forms.TextBox textBoxYearFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxCarFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabDiagnostics;
        private System.Windows.Forms.TreeView treeViewDiagnostics;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxDiagCar;
    }
}