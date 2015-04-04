namespace Neural_Network_Package
{
    partial class Multi_Layer_Perceptron_GUI
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
            this.trainButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.HiddenLayersCountComboBox = new System.Windows.Forms.ComboBox();
            this.numOfNeuronGridView = new System.Windows.Forms.DataGridView();
            this.Hidden_Layer_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NeuronsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.etaTextBOx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.slopeTextBox = new System.Windows.Forms.TextBox();
            this.initialWeightTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numOfIterationsTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numOfNeuronGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // trainButton
            // 
            this.trainButton.Location = new System.Drawing.Point(31, 267);
            this.trainButton.Name = "trainButton";
            this.trainButton.Size = new System.Drawing.Size(140, 40);
            this.trainButton.TabIndex = 0;
            this.trainButton.Text = "Start Training";
            this.trainButton.UseVisualStyleBackColor = true;
            this.trainButton.Click += new System.EventHandler(this.trainButton_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(237, 267);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(138, 40);
            this.testButton.TabIndex = 1;
            this.testButton.Text = "Start Testing";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // HiddenLayersCountComboBox
            // 
            this.HiddenLayersCountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HiddenLayersCountComboBox.FormattingEnabled = true;
            this.HiddenLayersCountComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.HiddenLayersCountComboBox.Location = new System.Drawing.Point(320, 33);
            this.HiddenLayersCountComboBox.Name = "HiddenLayersCountComboBox";
            this.HiddenLayersCountComboBox.Size = new System.Drawing.Size(121, 21);
            this.HiddenLayersCountComboBox.TabIndex = 2;
            this.HiddenLayersCountComboBox.SelectedIndexChanged += new System.EventHandler(this.HiddenLayersCountComboBox_SelectedIndexChanged);
            // 
            // numOfNeuronGridView
            // 
            this.numOfNeuronGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.numOfNeuronGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hidden_Layer_Index,
            this.NeuronsCount});
            this.numOfNeuronGridView.Location = new System.Drawing.Point(264, 73);
            this.numOfNeuronGridView.Name = "numOfNeuronGridView";
            this.numOfNeuronGridView.Size = new System.Drawing.Size(243, 168);
            this.numOfNeuronGridView.TabIndex = 3;
            // 
            // Hidden_Layer_Index
            // 
            this.Hidden_Layer_Index.HeaderText = "Hidden Layer Index";
            this.Hidden_Layer_Index.Name = "Hidden_Layer_Index";
            // 
            // NeuronsCount
            // 
            this.NeuronsCount.HeaderText = "Number of Neurons";
            this.NeuronsCount.Name = "NeuronsCount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hidden Layers Count";
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(22, 22);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(94, 23);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "<<";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // etaTextBOx
            // 
            this.etaTextBOx.Location = new System.Drawing.Point(141, 82);
            this.etaTextBOx.Name = "etaTextBOx";
            this.etaTextBOx.Size = new System.Drawing.Size(78, 20);
            this.etaTextBOx.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Learning Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Slope (a)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Initial Weight";
            // 
            // slopeTextBox
            // 
            this.slopeTextBox.Location = new System.Drawing.Point(141, 124);
            this.slopeTextBox.Name = "slopeTextBox";
            this.slopeTextBox.Size = new System.Drawing.Size(78, 20);
            this.slopeTextBox.TabIndex = 10;
            // 
            // initialWeightTextBox
            // 
            this.initialWeightTextBox.Location = new System.Drawing.Point(141, 165);
            this.initialWeightTextBox.Name = "initialWeightTextBox";
            this.initialWeightTextBox.Size = new System.Drawing.Size(78, 20);
            this.initialWeightTextBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Number Of Iterations";
            // 
            // numOfIterationsTextBox
            // 
            this.numOfIterationsTextBox.Location = new System.Drawing.Point(141, 202);
            this.numOfIterationsTextBox.Name = "numOfIterationsTextBox";
            this.numOfIterationsTextBox.Size = new System.Drawing.Size(76, 20);
            this.numOfIterationsTextBox.TabIndex = 13;
            // 
            // Multi_Layer_Perceptron_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 327);
            this.Controls.Add(this.numOfIterationsTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.initialWeightTextBox);
            this.Controls.Add(this.slopeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.etaTextBOx);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numOfNeuronGridView);
            this.Controls.Add(this.HiddenLayersCountComboBox);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.trainButton);
            this.Name = "Multi_Layer_Perceptron_GUI";
            this.Text = "Multi_Layer_Perceptron_GUI";
            ((System.ComponentModel.ISupportInitialize)(this.numOfNeuronGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button trainButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.ComboBox HiddenLayersCountComboBox;
        private System.Windows.Forms.DataGridView numOfNeuronGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hidden_Layer_Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn NeuronsCount;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TextBox etaTextBOx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox slopeTextBox;
        private System.Windows.Forms.TextBox initialWeightTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox numOfIterationsTextBox;
    }
}