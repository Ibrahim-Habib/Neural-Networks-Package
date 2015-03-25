namespace Neural_Network_Package
{
    partial class MainWindowGUI
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
            this.LMSButton = new System.Windows.Forms.Button();
            this.BPButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LMSButton
            // 
            this.LMSButton.Location = new System.Drawing.Point(99, 70);
            this.LMSButton.Name = "LMSButton";
            this.LMSButton.Size = new System.Drawing.Size(159, 36);
            this.LMSButton.TabIndex = 0;
            this.LMSButton.Text = "Least Mean Square Algorithm";
            this.LMSButton.UseVisualStyleBackColor = true;
            this.LMSButton.Click += new System.EventHandler(this.LMSButton_Click);
            // 
            // BPButton
            // 
            this.BPButton.Location = new System.Drawing.Point(121, 112);
            this.BPButton.Name = "BPButton";
            this.BPButton.Size = new System.Drawing.Size(115, 99);
            this.BPButton.TabIndex = 1;
            this.BPButton.Text = "Back-propagation Learning Algorithm";
            this.BPButton.UseVisualStyleBackColor = true;
            this.BPButton.Click += new System.EventHandler(this.BPButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tw Cen MT", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Neural Networks Package";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWindowGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 309);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BPButton);
            this.Controls.Add(this.LMSButton);
            this.Name = "MainWindowGUI";
            this.Text = "Main Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LMSButton;
        private System.Windows.Forms.Button BPButton;
        private System.Windows.Forms.Label label1;
    }
}