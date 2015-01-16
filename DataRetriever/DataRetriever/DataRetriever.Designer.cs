namespace DataRetriever
{
    partial class DataRetriever
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataRetriever));
            this.olv_list = new BrightIdeasSoftware.ObjectListView();
            this.name_column = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ip_column = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.status_column = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.dataRetrieverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.close_btn = new System.Windows.Forms.Button();
            this.minimize_btn = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.olv_focus = new BrightIdeasSoftware.ObjectListView();
            this.name_column_2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ip_column_2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.status_column_2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.add_to_focus = new System.Windows.Forms.Button();
            this.remove_from_focus = new System.Windows.Forms.Button();
            this.flush_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.olv_list)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_focus)).BeginInit();
            this.SuspendLayout();
            // 
            // olv_list
            // 
            this.olv_list.AllColumns.Add(this.name_column);
            this.olv_list.AllColumns.Add(this.ip_column);
            this.olv_list.AllColumns.Add(this.status_column);
            this.olv_list.CheckBoxes = true;
            this.olv_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name_column,
            this.ip_column,
            this.status_column});
            this.olv_list.Location = new System.Drawing.Point(15, 74);
            this.olv_list.Name = "olv_list";
            this.olv_list.Size = new System.Drawing.Size(338, 253);
            this.olv_list.TabIndex = 3;
            this.olv_list.UseCompatibleStateImageBehavior = false;
            this.olv_list.View = System.Windows.Forms.View.Details;
            // 
            // name_column
            // 
            this.name_column.AspectName = "name";
            this.name_column.CellPadding = null;
            this.name_column.Text = "Name";
            this.name_column.Width = 125;
            // 
            // ip_column
            // 
            this.ip_column.AspectName = "ip";
            this.ip_column.CellPadding = null;
            this.ip_column.Text = "IP Address";
            this.ip_column.Width = 113;
            // 
            // status_column
            // 
            this.status_column.AspectName = "status";
            this.status_column.CellPadding = null;
            this.status_column.Text = "Status";
            this.status_column.Width = 94;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Connection available";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(750, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.optionToolStripMenuItem.Text = "Option";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exit_btn_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Enabled = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 360);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(750, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip2.Enabled = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataRetrieverToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(750, 24);
            this.menuStrip2.TabIndex = 7;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // dataRetrieverToolStripMenuItem
            // 
            this.dataRetrieverToolStripMenuItem.Name = "dataRetrieverToolStripMenuItem";
            this.dataRetrieverToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.dataRetrieverToolStripMenuItem.Text = "Data Retriever";
            // 
            // close_btn
            // 
            this.close_btn.Location = new System.Drawing.Point(712, -4);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(22, 22);
            this.close_btn.TabIndex = 8;
            this.close_btn.Text = "x";
            this.close_btn.UseVisualStyleBackColor = true;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // minimize_btn
            // 
            this.minimize_btn.Location = new System.Drawing.Point(684, -4);
            this.minimize_btn.Name = "minimize_btn";
            this.minimize_btn.Size = new System.Drawing.Size(22, 22);
            this.minimize_btn.TabIndex = 9;
            this.minimize_btn.Text = "-";
            this.minimize_btn.UseVisualStyleBackColor = true;
            this.minimize_btn.Click += new System.EventHandler(this.minimize_btn_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Your program is minimized to system tray. Double-click on the icon to restore the" +
    " session.";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Data Retriever";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Focus group";
            // 
            // olv_focus
            // 
            this.olv_focus.AllColumns.Add(this.name_column_2);
            this.olv_focus.AllColumns.Add(this.ip_column_2);
            this.olv_focus.AllColumns.Add(this.status_column_2);
            this.olv_focus.CheckBoxes = true;
            this.olv_focus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name_column_2,
            this.ip_column_2,
            this.status_column_2});
            this.olv_focus.Location = new System.Drawing.Point(396, 74);
            this.olv_focus.Name = "olv_focus";
            this.olv_focus.Size = new System.Drawing.Size(338, 253);
            this.olv_focus.TabIndex = 11;
            this.olv_focus.UseCompatibleStateImageBehavior = false;
            this.olv_focus.View = System.Windows.Forms.View.Details;
            // 
            // name_column_2
            // 
            this.name_column_2.AspectName = "name";
            this.name_column_2.CellPadding = null;
            this.name_column_2.Text = "Name";
            this.name_column_2.Width = 130;
            // 
            // ip_column_2
            // 
            this.ip_column_2.AspectName = "ip";
            this.ip_column_2.CellPadding = null;
            this.ip_column_2.Text = "IP Address";
            this.ip_column_2.Width = 104;
            // 
            // status_column_2
            // 
            this.status_column_2.AspectName = "status";
            this.status_column_2.CellPadding = null;
            this.status_column_2.Text = "Status";
            this.status_column_2.Width = 98;
            // 
            // add_to_focus
            // 
            this.add_to_focus.Location = new System.Drawing.Point(359, 125);
            this.add_to_focus.Name = "add_to_focus";
            this.add_to_focus.Size = new System.Drawing.Size(31, 23);
            this.add_to_focus.TabIndex = 12;
            this.add_to_focus.Text = ">>";
            this.add_to_focus.UseVisualStyleBackColor = true;
            this.add_to_focus.Click += new System.EventHandler(this.add_to_focus_Click);
            // 
            // remove_from_focus
            // 
            this.remove_from_focus.Location = new System.Drawing.Point(359, 248);
            this.remove_from_focus.Name = "remove_from_focus";
            this.remove_from_focus.Size = new System.Drawing.Size(31, 23);
            this.remove_from_focus.TabIndex = 13;
            this.remove_from_focus.Text = "<<";
            this.remove_from_focus.UseVisualStyleBackColor = true;
            this.remove_from_focus.Click += new System.EventHandler(this.remove_from_focus_Click);
            // 
            // flush_btn
            // 
            this.flush_btn.Location = new System.Drawing.Point(619, 333);
            this.flush_btn.Name = "flush_btn";
            this.flush_btn.Size = new System.Drawing.Size(119, 23);
            this.flush_btn.TabIndex = 14;
            this.flush_btn.Text = "Flush all connections";
            this.flush_btn.UseVisualStyleBackColor = true;
            this.flush_btn.Click += new System.EventHandler(this.flush_btn_Click);
            // 
            // DataRetriever
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 382);
            this.Controls.Add(this.flush_btn);
            this.Controls.Add(this.remove_from_focus);
            this.Controls.Add(this.add_to_focus);
            this.Controls.Add(this.olv_focus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minimize_btn);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.olv_list);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DataRetriever";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.olv_list)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_focus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olv_list;
        private BrightIdeasSoftware.OLVColumn name_column;
        private BrightIdeasSoftware.OLVColumn ip_column;
        private BrightIdeasSoftware.OLVColumn status_column;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem dataRetrieverToolStripMenuItem;
        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.Button minimize_btn;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private BrightIdeasSoftware.ObjectListView olv_focus;
        private BrightIdeasSoftware.OLVColumn name_column_2;
        private BrightIdeasSoftware.OLVColumn ip_column_2;
        private BrightIdeasSoftware.OLVColumn status_column_2;
        private System.Windows.Forms.Button add_to_focus;
        private System.Windows.Forms.Button remove_from_focus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button flush_btn;
    }
}

