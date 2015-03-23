namespace Neural_Network_Package
{
    partial class ConfusionMatrixForm
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
            this.ConfusionMatrixGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.overallAccuracyTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ConfusionMatrixGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ConfusionMatrixGridView
            // 
            this.ConfusionMatrixGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfusionMatrixGridView.Location = new System.Drawing.Point(2, 3);
            this.ConfusionMatrixGridView.Name = "ConfusionMatrixGridView";
            this.ConfusionMatrixGridView.ReadOnly = true;
            this.ConfusionMatrixGridView.Size = new System.Drawing.Size(343, 257);
            this.ConfusionMatrixGridView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(361, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Overall Accuracy";
            // 
            // overallAccuracyTextBox
            // 
            this.overallAccuracyTextBox.Location = new System.Drawing.Point(361, 62);
            this.overallAccuracyTextBox.Name = "overallAccuracyTextBox";
            this.overallAccuracyTextBox.ReadOnly = true;
            this.overallAccuracyTextBox.Size = new System.Drawing.Size(85, 20);
            this.overallAccuracyTextBox.TabIndex = 2;
            // 
            // ConfusionMatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 262);
            this.Controls.Add(this.overallAccuracyTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConfusionMatrixGridView);
            this.Name = "ConfusionMatrixForm";
            this.Text = "Confusion Matrix";
            ((System.ComponentModel.ISupportInitialize)(this.ConfusionMatrixGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ConfusionMatrixGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox overallAccuracyTextBox;

    }
}