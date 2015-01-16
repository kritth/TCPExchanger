namespace DataRetriever
{
    partial class Option
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Option));
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.host_list_box = new System.Windows.Forms.CheckedListBox();
            this.host_box = new System.Windows.Forms.TextBox();
            this.add_host_btn = new System.Windows.Forms.PictureBox();
            this.host_remove_btn = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cancel_btn = new System.Windows.Forms.Button();
            this.save_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.close_to_tray_btn = new System.Windows.Forms.CheckBox();
            this.min_to_tray_btn = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.interval_box = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.close_btn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.add_host_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.host_remove_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host list";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Enabled = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(494, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Enabled = false;
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Enabled = false;
            this.statusStrip1.Location = new System.Drawing.Point(0, 213);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(494, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // host_list_box
            // 
            this.host_list_box.FormattingEnabled = true;
            this.host_list_box.Location = new System.Drawing.Point(15, 88);
            this.host_list_box.Name = "host_list_box";
            this.host_list_box.Size = new System.Drawing.Size(177, 109);
            this.host_list_box.TabIndex = 3;
            // 
            // host_box
            // 
            this.host_box.Location = new System.Drawing.Point(15, 60);
            this.host_box.Name = "host_box";
            this.host_box.Size = new System.Drawing.Size(177, 20);
            this.host_box.TabIndex = 4;
            // 
            // add_host_btn
            // 
            this.add_host_btn.Image = ((System.Drawing.Image)(resources.GetObject("add_host_btn.Image")));
            this.add_host_btn.Location = new System.Drawing.Point(220, 60);
            this.add_host_btn.Name = "add_host_btn";
            this.add_host_btn.Size = new System.Drawing.Size(22, 22);
            this.add_host_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.add_host_btn.TabIndex = 5;
            this.add_host_btn.TabStop = false;
            this.toolTip1.SetToolTip(this.add_host_btn, "Add host");
            this.add_host_btn.Click += new System.EventHandler(this.add_host_btn_Click);
            // 
            // host_remove_btn
            // 
            this.host_remove_btn.Image = ((System.Drawing.Image)(resources.GetObject("host_remove_btn.Image")));
            this.host_remove_btn.Location = new System.Drawing.Point(220, 88);
            this.host_remove_btn.Name = "host_remove_btn";
            this.host_remove_btn.Size = new System.Drawing.Size(22, 22);
            this.host_remove_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.host_remove_btn.TabIndex = 6;
            this.host_remove_btn.TabStop = false;
            this.toolTip1.SetToolTip(this.host_remove_btn, "Remove selected");
            this.host_remove_btn.Click += new System.EventHandler(this.host_remove_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(415, 183);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(67, 23);
            this.cancel_btn.TabIndex = 7;
            this.cancel_btn.Text = "Done";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(318, 183);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(91, 23);
            this.save_btn.TabIndex = 8;
            this.save_btn.Text = "Save changes";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(258, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Additional option";
            // 
            // close_to_tray_btn
            // 
            this.close_to_tray_btn.AutoSize = true;
            this.close_to_tray_btn.Location = new System.Drawing.Point(261, 62);
            this.close_to_tray_btn.Name = "close_to_tray_btn";
            this.close_to_tray_btn.Size = new System.Drawing.Size(201, 17);
            this.close_to_tray_btn.TabIndex = 17;
            this.close_to_tray_btn.Text = "Press close to minimize to system tray";
            this.close_to_tray_btn.UseVisualStyleBackColor = true;
            // 
            // min_to_tray_btn
            // 
            this.min_to_tray_btn.AutoSize = true;
            this.min_to_tray_btn.Location = new System.Drawing.Point(261, 85);
            this.min_to_tray_btn.Name = "min_to_tray_btn";
            this.min_to_tray_btn.Size = new System.Drawing.Size(133, 17);
            this.min_to_tray_btn.TabIndex = 18;
            this.min_to_tray_btn.Text = "Minimize to system tray";
            this.min_to_tray_btn.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Reconnection interval";
            // 
            // interval_box
            // 
            this.interval_box.Location = new System.Drawing.Point(400, 107);
            this.interval_box.Name = "interval_box";
            this.interval_box.Size = new System.Drawing.Size(42, 20);
            this.interval_box.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(470, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "s";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // close_btn
            // 
            this.close_btn.Location = new System.Drawing.Point(460, -4);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(22, 22);
            this.close_btn.TabIndex = 24;
            this.close_btn.Text = "x";
            this.close_btn.UseVisualStyleBackColor = true;
            this.close_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 235);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.interval_box);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.min_to_tray_btn);
            this.Controls.Add(this.close_to_tray_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.host_remove_btn);
            this.Controls.Add(this.add_host_btn);
            this.Controls.Add(this.host_box);
            this.Controls.Add(this.host_list_box);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Option";
            this.ShowInTaskbar = false;
            this.Text = "Option";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.add_host_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.host_remove_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckedListBox host_list_box;
        private System.Windows.Forms.TextBox host_box;
        private System.Windows.Forms.PictureBox add_host_btn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox host_remove_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox close_to_tray_btn;
        private System.Windows.Forms.CheckBox min_to_tray_btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox interval_box;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button close_btn;
    }
}