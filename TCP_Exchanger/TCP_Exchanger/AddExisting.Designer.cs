namespace TCP_Exchanger
{
    partial class AddExisting
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
            this.olv_list = new BrightIdeasSoftware.ObjectListView();
            this.ip_column = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.port_column = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.status_column = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label1 = new System.Windows.Forms.Label();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.next_btn = new System.Windows.Forms.Button();
            this.close_btn = new System.Windows.Forms.Button();
            this.minimize_btn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addExistingConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.olv_list)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // olv_list
            // 
            this.olv_list.AllColumns.Add(this.ip_column);
            this.olv_list.AllColumns.Add(this.port_column);
            this.olv_list.AllColumns.Add(this.status_column);
            this.olv_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ip_column,
            this.port_column,
            this.status_column});
            this.olv_list.Location = new System.Drawing.Point(12, 59);
            this.olv_list.Name = "olv_list";
            this.olv_list.ShowGroups = false;
            this.olv_list.Size = new System.Drawing.Size(375, 178);
            this.olv_list.TabIndex = 0;
            this.olv_list.UseCompatibleStateImageBehavior = false;
            this.olv_list.View = System.Windows.Forms.View.Details;
            this.olv_list.SelectedIndexChanged += new System.EventHandler(this.olv_list_SelectedIndexChanged);
            // 
            // ip_column
            // 
            this.ip_column.AspectName = "Address";
            this.ip_column.CellPadding = null;
            this.ip_column.Text = "IP Address";
            this.ip_column.Width = 178;
            // 
            // port_column
            // 
            this.port_column.AspectName = "Port";
            this.port_column.CellPadding = null;
            this.port_column.Text = "Port";
            // 
            // status_column
            // 
            this.status_column.AspectName = "State";
            this.status_column.CellPadding = null;
            this.status_column.Text = "Status";
            this.status_column.Width = 101;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select one connection from the list below";
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(337, 243);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(49, 23);
            this.cancel_btn.TabIndex = 2;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // next_btn
            // 
            this.next_btn.Location = new System.Drawing.Point(288, 243);
            this.next_btn.Name = "next_btn";
            this.next_btn.Size = new System.Drawing.Size(43, 23);
            this.next_btn.TabIndex = 3;
            this.next_btn.Text = "Next";
            this.next_btn.UseVisualStyleBackColor = true;
            this.next_btn.Click += new System.EventHandler(this.next_btn_Click);
            // 
            // close_btn
            // 
            this.close_btn.Location = new System.Drawing.Point(364, -5);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(22, 22);
            this.close_btn.TabIndex = 4;
            this.close_btn.Text = "x";
            this.close_btn.UseVisualStyleBackColor = true;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // minimize_btn
            // 
            this.minimize_btn.Location = new System.Drawing.Point(337, -5);
            this.minimize_btn.Name = "minimize_btn";
            this.minimize_btn.Size = new System.Drawing.Size(22, 22);
            this.minimize_btn.TabIndex = 5;
            this.minimize_btn.Text = "-";
            this.minimize_btn.UseVisualStyleBackColor = true;
            this.minimize_btn.Click += new System.EventHandler(this.minimize_btn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Enabled = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addExistingConnectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(399, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addExistingConnectionToolStripMenuItem
            // 
            this.addExistingConnectionToolStripMenuItem.Enabled = false;
            this.addExistingConnectionToolStripMenuItem.Name = "addExistingConnectionToolStripMenuItem";
            this.addExistingConnectionToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.addExistingConnectionToolStripMenuItem.Text = "Add Existing connection";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Enabled = false;
            this.statusStrip1.Location = new System.Drawing.Point(0, 278);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(399, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // AddExisting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 300);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.minimize_btn);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.next_btn);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.olv_list);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AddExisting";
            this.ShowInTaskbar = false;
            this.Text = "AddExisting";
            ((System.ComponentModel.ISupportInitialize)(this.olv_list)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olv_list;
        private BrightIdeasSoftware.OLVColumn ip_column;
        private BrightIdeasSoftware.OLVColumn port_column;
        private BrightIdeasSoftware.OLVColumn status_column;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Button next_btn;
        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.Button minimize_btn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addExistingConnectionToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}