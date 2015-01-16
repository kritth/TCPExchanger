namespace DataRetriever
{
    partial class FocusApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FocusApp));
            this.bg_box = new System.Windows.Forms.PictureBox();
            this.close_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bg_box)).BeginInit();
            this.SuspendLayout();
            // 
            // bg_box
            // 
            this.bg_box.Image = ((System.Drawing.Image)(resources.GetObject("bg_box.Image")));
            this.bg_box.Location = new System.Drawing.Point(-3, -2);
            this.bg_box.Name = "bg_box";
            this.bg_box.Size = new System.Drawing.Size(404, 59);
            this.bg_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bg_box.TabIndex = 4;
            this.bg_box.TabStop = false;
            // 
            // close_label
            // 
            this.close_label.AutoSize = true;
            this.close_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.close_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.close_label.Location = new System.Drawing.Point(360, -1);
            this.close_label.Name = "close_label";
            this.close_label.Size = new System.Drawing.Size(15, 18);
            this.close_label.TabIndex = 5;
            this.close_label.Text = "x";
            this.close_label.Click += new System.EventHandler(this.close_label_Click);
            // 
            // FocusApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(400, 56);
            this.Controls.Add(this.close_label);
            this.Controls.Add(this.bg_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FocusApp";
            this.Text = "FocusApp";
            this.TransparencyKey = System.Drawing.SystemColors.ControlDarkDark;
            ((System.ComponentModel.ISupportInitialize)(this.bg_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox bg_box;
        private System.Windows.Forms.Label close_label;
    }
}