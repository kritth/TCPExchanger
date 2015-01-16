namespace TCP_Exchanger
{
    partial class PortScanner
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
            this.label1 = new System.Windows.Forms.Label();
            this.ip_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.start_box = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.end_box = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.start_btn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.olv_list = new BrightIdeasSoftware.ObjectListView();
            this.port_column = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.status_column = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.add_btn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.portScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.olv_list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // ip_box
            // 
            this.ip_box.Location = new System.Drawing.Point(15, 50);
            this.ip_box.Name = "ip_box";
            this.ip_box.Size = new System.Drawing.Size(213, 20);
            this.ip_box.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start";
            // 
            // start_box
            // 
            this.start_box.Location = new System.Drawing.Point(47, 92);
            this.start_box.Name = "start_box";
            this.start_box.Size = new System.Drawing.Size(65, 20);
            this.start_box.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "End";
            // 
            // end_box
            // 
            this.end_box.Location = new System.Drawing.Point(165, 92);
            this.end_box.Name = "end_box";
            this.end_box.Size = new System.Drawing.Size(63, 20);
            this.end_box.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Port search range";
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(12, 271);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(75, 23);
            this.start_btn.TabIndex = 7;
            this.start_btn.Text = "Start scan";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(175, 271);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // olv_list
            // 
            this.olv_list.AllColumns.Add(this.port_column);
            this.olv_list.AllColumns.Add(this.status_column);
            this.olv_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.port_column,
            this.status_column});
            this.olv_list.Location = new System.Drawing.Point(13, 118);
            this.olv_list.Name = "olv_list";
            this.olv_list.ShowGroups = false;
            this.olv_list.Size = new System.Drawing.Size(237, 147);
            this.olv_list.TabIndex = 9;
            this.olv_list.UseCompatibleStateImageBehavior = false;
            this.olv_list.View = System.Windows.Forms.View.Details;
            this.olv_list.SelectedIndexChanged += new System.EventHandler(this.olv_list_SelectedIndexChanged);
            // 
            // port_column
            // 
            this.port_column.AspectName = "remote_port";
            this.port_column.CellPadding = null;
            this.port_column.Text = "Port number";
            this.port_column.Width = 134;
            // 
            // status_column
            // 
            this.status_column.AspectName = "status";
            this.status_column.CellPadding = null;
            this.status_column.Text = "Status";
            this.status_column.Width = 93;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // add_btn
            // 
            this.add_btn.Enabled = false;
            this.add_btn.Location = new System.Drawing.Point(94, 271);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(75, 23);
            this.add_btn.TabIndex = 10;
            this.add_btn.Text = "Add";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Enabled = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portScannerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(264, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // portScannerToolStripMenuItem
            // 
            this.portScannerToolStripMenuItem.Enabled = false;
            this.portScannerToolStripMenuItem.Name = "portScannerToolStripMenuItem";
            this.portScannerToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.portScannerToolStripMenuItem.Text = "Port Scanner";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Enabled = false;
            this.statusStrip1.Location = new System.Drawing.Point(0, 301);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(264, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PortScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 323);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.add_btn);
            this.Controls.Add(this.olv_list);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.end_box);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.start_box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ip_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PortScanner";
            this.ShowInTaskbar = false;
            this.Text = "PortScanner";
            ((System.ComponentModel.ISupportInitialize)(this.olv_list)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ip_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox start_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox end_box;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button button2;
        private BrightIdeasSoftware.ObjectListView olv_list;
        private BrightIdeasSoftware.OLVColumn port_column;
        private BrightIdeasSoftware.OLVColumn status_column;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem portScannerToolStripMenuItem;
    }
}