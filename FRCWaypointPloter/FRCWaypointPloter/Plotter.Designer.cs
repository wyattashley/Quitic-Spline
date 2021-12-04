namespace FRCWaypointPloter
{
    partial class Plotter
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Points", System.Windows.Forms.HorizontalAlignment.Left);
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.TeamName = new System.Windows.Forms.PictureBox();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.xInput = new System.Windows.Forms.NumericUpDown();
            this.yInput = new System.Windows.Forms.NumericUpDown();
            this.addButton = new System.Windows.Forms.Button();
            this.values = new System.Windows.Forms.ListView();
            this.Points = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.exportButton = new System.Windows.Forms.Button();
            this.Export = new System.Windows.Forms.SaveFileDialog();
            this.PurePursuitEnabled = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.TeamName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yInput)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(5)))));
            this.labelX.Location = new System.Drawing.Point(359, 318);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(26, 17);
            this.labelX.TabIndex = 2;
            this.labelX.Text = "X: ";
            this.labelX.UseMnemonic = false;
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(5)))));
            this.labelY.Location = new System.Drawing.Point(478, 318);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(20, 17);
            this.labelY.TabIndex = 3;
            this.labelY.Text = "Y:";
            // 
            // TeamName
            // 
            this.TeamName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(49)))));
            this.TeamName.Image = global::FRCWaypointPloter.Properties.Resources.Team2137_New_Brand_Embroidered_Beenie_Hat1;
            this.TeamName.ImageLocation = "";
            this.TeamName.Location = new System.Drawing.Point(12, 12);
            this.TeamName.Margin = new System.Windows.Forms.Padding(0);
            this.TeamName.Name = "TeamName";
            this.TeamName.Size = new System.Drawing.Size(300, 83);
            this.TeamName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.TeamName.TabIndex = 0;
            this.TeamName.TabStop = false;
            // 
            // mainChart
            // 
            this.mainChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(49)))));
            chartArea1.BackImage = "C:\\Users\\Wyatt\\Downloads\\Feild Image Crop.png";
            chartArea1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea1.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Scaled;
            chartArea1.CursorX.AutoScroll = false;
            chartArea1.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.CursorY.AutoScroll = false;
            chartArea1.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea1);
            this.mainChart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mainChart.Location = new System.Drawing.Point(324, 12);
            this.mainChart.Name = "mainChart";
            this.mainChart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            this.mainChart.Series.Add(series1);
            this.mainChart.Size = new System.Drawing.Size(554, 300);
            this.mainChart.TabIndex = 4;
            this.mainChart.Text = "mainChart";
            this.mainChart.Click += new System.EventHandler(this.addPoint);
            this.mainChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainChart_MouseMove);
            // 
            // xInput
            // 
            this.xInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(49)))));
            this.xInput.DecimalPlaces = 2;
            this.xInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(5)))));
            this.xInput.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.xInput.Location = new System.Drawing.Point(391, 318);
            this.xInput.Maximum = new decimal(new int[] {
            504375,
            0,
            0,
            262144});
            this.xInput.Name = "xInput";
            this.xInput.Size = new System.Drawing.Size(61, 20);
            this.xInput.TabIndex = 6;
            this.xInput.ValueChanged += new System.EventHandler(this.Input_ValueChanged);
            // 
            // yInput
            // 
            this.yInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(49)))));
            this.yInput.DecimalPlaces = 2;
            this.yInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(5)))));
            this.yInput.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.yInput.Location = new System.Drawing.Point(504, 318);
            this.yInput.Maximum = new decimal(new int[] {
            504375,
            0,
            0,
            262144});
            this.yInput.Name = "yInput";
            this.yInput.Size = new System.Drawing.Size(61, 20);
            this.yInput.TabIndex = 7;
            this.yInput.ValueChanged += new System.EventHandler(this.Input_ValueChanged);
            // 
            // addButton
            // 
            this.addButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.addButton.Location = new System.Drawing.Point(587, 318);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(40, 23);
            this.addButton.TabIndex = 8;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addPoint);
            // 
            // values
            // 
            this.values.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(49)))));
            this.values.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Points});
            this.values.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.values.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(5)))));
            listViewGroup1.Header = "Points";
            listViewGroup1.Name = "Points";
            this.values.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.values.HideSelection = false;
            this.values.Location = new System.Drawing.Point(884, 12);
            this.values.Name = "values";
            this.values.Size = new System.Drawing.Size(146, 408);
            this.values.TabIndex = 10;
            this.values.UseCompatibleStateImageBehavior = false;
            this.values.View = System.Windows.Forms.View.Details;
            // 
            // Points
            // 
            this.Points.Text = "Points";
            this.Points.Width = 136;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(803, 318);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 11;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // Export
            // 
            this.Export.DefaultExt = "output.csv";
            this.Export.FileName = "output.csv";
            // 
            // PurePursuitEnabled
            // 
            this.PurePursuitEnabled.AutoSize = true;
            this.PurePursuitEnabled.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold);
            this.PurePursuitEnabled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(5)))));
            this.PurePursuitEnabled.Location = new System.Drawing.Point(13, 99);
            this.PurePursuitEnabled.Name = "PurePursuitEnabled";
            this.PurePursuitEnabled.Size = new System.Drawing.Size(182, 21);
            this.PurePursuitEnabled.TabIndex = 12;
            this.PurePursuitEnabled.Text = "PurePursuit Calculation";
            this.PurePursuitEnabled.UseVisualStyleBackColor = true;
            // 
            // Plotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1042, 450);
            this.Controls.Add(this.PurePursuitEnabled);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.values);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.yInput);
            this.Controls.Add(this.xInput);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(this.TeamName);
            this.Name = "Plotter";
            this.Text = "Plotter";
            ((System.ComponentModel.ISupportInitialize)(this.TeamName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox TeamName;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.NumericUpDown xInput;
        private System.Windows.Forms.NumericUpDown yInput;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView values;
        private System.Windows.Forms.ColumnHeader Points;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.SaveFileDialog Export;
        private System.Windows.Forms.CheckBox PurePursuitEnabled;
    }
}

