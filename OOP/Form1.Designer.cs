namespace OOP
{
    partial class Form1
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
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.ResolutionNumeric = new System.Windows.Forms.NumericUpDown();
            this.LabelUnit = new System.Windows.Forms.Label();
            this.TextLabel = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.WorldView = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize) (this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.ResolutionNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.WorldView)).BeginInit();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.label1);
            this.MainSplitContainer.Panel1.Controls.Add(this.ResolutionNumeric);
            this.MainSplitContainer.Panel1.Controls.Add(this.LabelUnit);
            this.MainSplitContainer.Panel1.Controls.Add(this.TextLabel);
            this.MainSplitContainer.Panel1.Controls.Add(this.labelCount);
            this.MainSplitContainer.Panel1.Controls.Add(this.StopButton);
            this.MainSplitContainer.Panel1.Controls.Add(this.startButton);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.AutoScroll = true;
            this.MainSplitContainer.Panel2.Controls.Add(this.WorldView);
            this.MainSplitContainer.Size = new System.Drawing.Size(1403, 603);
            this.MainSplitContainer.SplitterDistance = 172;
            this.MainSplitContainer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resolution";
            // 
            // ResolutionNumeric
            // 
            this.ResolutionNumeric.Location = new System.Drawing.Point(15, 167);
            this.ResolutionNumeric.Maximum = new decimal(new int[] {10, 0, 0, 0});
            this.ResolutionNumeric.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.ResolutionNumeric.Name = "ResolutionNumeric";
            this.ResolutionNumeric.Size = new System.Drawing.Size(120, 20);
            this.ResolutionNumeric.TabIndex = 4;
            this.ResolutionNumeric.Value = new decimal(new int[] {5, 0, 0, 0});
            this.ResolutionNumeric.ValueChanged += new System.EventHandler(this.ResolutionNumeric_ValueChanged);
            // 
            // LabelUnit
            // 
            this.LabelUnit.AutoSize = true;
            this.LabelUnit.Location = new System.Drawing.Point(12, 233);
            this.LabelUnit.Name = "LabelUnit";
            this.LabelUnit.Size = new System.Drawing.Size(103, 13);
            this.LabelUnit.TabIndex = 2;
            this.LabelUnit.Text = "Unit Info will be here";
            // 
            // TextLabel
            // 
            this.TextLabel.AutoSize = true;
            this.TextLabel.Location = new System.Drawing.Point(12, 199);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(86, 13);
            this.TextLabel.TabIndex = 2;
            this.TextLabel.Text = "Number of Units:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(122, 199);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(13, 13);
            this.labelCount.TabIndex = 3;
            this.labelCount.Text = "0";
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(12, 79);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(140, 61);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(140, 61);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // WorldView
            // 
            this.WorldView.Location = new System.Drawing.Point(0, 0);
            this.WorldView.Name = "WorldView";
            this.WorldView.Size = new System.Drawing.Size(1000, 1000);
            this.WorldView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.WorldView.TabIndex = 1;
            this.WorldView.TabStop = false;
            this.WorldView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WorldView_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 603);
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "UserInterface";
            this.Text = "UserInterface";
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel1.PerformLayout();
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.ResolutionNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.WorldView)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.PictureBox WorldView;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label LabelUnit;
        private System.Windows.Forms.Label TextLabel;
        private System.Windows.Forms.NumericUpDown ResolutionNumeric;
        private System.Windows.Forms.Label label1;
    }
}

