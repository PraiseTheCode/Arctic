namespace Arctic
{
    partial class Arctic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Arctic));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabV = new System.Windows.Forms.TabPage();
            this.btnSavePlotV = new System.Windows.Forms.Button();
            this.plot = new NPlot.Windows.PlotSurface2D();
            this.btnClearV = new System.Windows.Forms.Button();
            this.btnShowV = new System.Windows.Forms.Button();
            this.btnLoadV = new System.Windows.Forms.Button();
            this.tabBVR = new System.Windows.Forms.TabPage();
            this.plotBVRV = new NPlot.Windows.PlotSurface2D();
            this.plotBVRR = new NPlot.Windows.PlotSurface2D();
            this.plotBVRB = new NPlot.Windows.PlotSurface2D();
            this.btnSavePlotBVR = new System.Windows.Forms.Button();
            this.btnClearBVR = new System.Windows.Forms.Button();
            this.btnShowBVR = new System.Windows.Forms.Button();
            this.btnLoadR_BVR = new System.Windows.Forms.Button();
            this.btnLoadV_BVR = new System.Windows.Forms.Button();
            this.btnLoadB_BVR = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabIVStokes = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.plotIVV = new NPlot.Windows.PlotSurface2D();
            this.plotIVI = new NPlot.Windows.PlotSurface2D();
            this.btnSaveIV = new System.Windows.Forms.Button();
            this.btnClearIV = new System.Windows.Forms.Button();
            this.btnShowIV = new System.Windows.Forms.Button();
            this.btnLoadVV = new System.Windows.Forms.Button();
            this.btnLoadIV = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInc = new System.Windows.Forms.TextBox();
            this.txtSP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtkT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPsi = new System.Windows.Forms.TextBox();
            this.txtKsi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBeta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdFi = new System.Windows.Forms.TextBox();
            this.txtFi1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_SavePars = new System.Windows.Forms.Button();
            this.btnSaveLC = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMC = new System.Windows.Forms.TextBox();
            this.txtGen = new System.Windows.Forms.TextBox();
            this.txtPop = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnLC = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new ExtendedDotNET.Controls.Progress.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.tabV.SuspendLayout();
            this.tabBVR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabIVStokes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabV);
            this.tabControl1.Controls.Add(this.tabBVR);
            this.tabControl1.Controls.Add(this.tabIVStokes);
            this.tabControl1.Location = new System.Drawing.Point(210, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(562, 361);
            this.tabControl1.TabIndex = 0;
            // 
            // tabV
            // 
            this.tabV.Controls.Add(this.btnSavePlotV);
            this.tabV.Controls.Add(this.plot);
            this.tabV.Controls.Add(this.btnClearV);
            this.tabV.Controls.Add(this.btnShowV);
            this.tabV.Controls.Add(this.btnLoadV);
            this.tabV.Location = new System.Drawing.Point(4, 22);
            this.tabV.Name = "tabV";
            this.tabV.Padding = new System.Windows.Forms.Padding(3);
            this.tabV.Size = new System.Drawing.Size(554, 335);
            this.tabV.TabIndex = 0;
            this.tabV.Text = "IStokes  V";
            this.tabV.UseVisualStyleBackColor = true;
            // 
            // btnSavePlotV
            // 
            this.btnSavePlotV.Enabled = false;
            this.btnSavePlotV.Location = new System.Drawing.Point(447, 6);
            this.btnSavePlotV.Name = "btnSavePlotV";
            this.btnSavePlotV.Size = new System.Drawing.Size(101, 23);
            this.btnSavePlotV.TabIndex = 79;
            this.btnSavePlotV.Text = "Save plot";
            this.btnSavePlotV.UseVisualStyleBackColor = true;
            this.btnSavePlotV.Click += new System.EventHandler(this.btnSavePlotV_Click);
            // 
            // plot
            // 
            this.plot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plot.AutoScaleAutoGeneratedAxes = false;
            this.plot.AutoScaleTitle = false;
            this.plot.BackColor = System.Drawing.SystemColors.Window;
            this.plot.DateTimeToolTip = false;
            this.plot.Legend = null;
            this.plot.LegendZOrder = -1;
            this.plot.Location = new System.Drawing.Point(6, 35);
            this.plot.Name = "plot";
            this.plot.RightMenu = null;
            this.plot.ShowCoordinates = true;
            this.plot.Size = new System.Drawing.Size(545, 295);
            this.plot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plot.TabIndex = 78;
            this.plot.Text = "plotSurface2D1";
            this.plot.Title = "";
            this.plot.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plot.XAxis1 = null;
            this.plot.XAxis2 = null;
            this.plot.YAxis1 = null;
            this.plot.YAxis2 = null;
            // 
            // btnClearV
            // 
            this.btnClearV.Enabled = false;
            this.btnClearV.Location = new System.Drawing.Point(339, 6);
            this.btnClearV.Name = "btnClearV";
            this.btnClearV.Size = new System.Drawing.Size(102, 23);
            this.btnClearV.TabIndex = 77;
            this.btnClearV.Text = "Clear plot";
            this.btnClearV.UseVisualStyleBackColor = true;
            this.btnClearV.Click += new System.EventHandler(this.btnClearV_Click);
            // 
            // btnShowV
            // 
            this.btnShowV.Enabled = false;
            this.btnShowV.Location = new System.Drawing.Point(231, 6);
            this.btnShowV.Name = "btnShowV";
            this.btnShowV.Size = new System.Drawing.Size(102, 23);
            this.btnShowV.TabIndex = 76;
            this.btnShowV.Text = "Show";
            this.btnShowV.UseVisualStyleBackColor = true;
            this.btnShowV.Click += new System.EventHandler(this.btnShowV_Click);
            // 
            // btnLoadV
            // 
            this.btnLoadV.Location = new System.Drawing.Point(6, 6);
            this.btnLoadV.Name = "btnLoadV";
            this.btnLoadV.Size = new System.Drawing.Size(219, 23);
            this.btnLoadV.TabIndex = 75;
            this.btnLoadV.Text = "Load observed V lightcurve";
            this.btnLoadV.UseVisualStyleBackColor = true;
            this.btnLoadV.Click += new System.EventHandler(this.btnLoadV_Click);
            // 
            // tabBVR
            // 
            this.tabBVR.Controls.Add(this.plotBVRV);
            this.tabBVR.Controls.Add(this.plotBVRR);
            this.tabBVR.Controls.Add(this.plotBVRB);
            this.tabBVR.Controls.Add(this.btnSavePlotBVR);
            this.tabBVR.Controls.Add(this.btnClearBVR);
            this.tabBVR.Controls.Add(this.btnShowBVR);
            this.tabBVR.Controls.Add(this.btnLoadR_BVR);
            this.tabBVR.Controls.Add(this.btnLoadV_BVR);
            this.tabBVR.Controls.Add(this.btnLoadB_BVR);
            this.tabBVR.Controls.Add(this.pictureBox1);
            this.tabBVR.Location = new System.Drawing.Point(4, 22);
            this.tabBVR.Name = "tabBVR";
            this.tabBVR.Padding = new System.Windows.Forms.Padding(3);
            this.tabBVR.Size = new System.Drawing.Size(554, 335);
            this.tabBVR.TabIndex = 1;
            this.tabBVR.Text = "IStokes B + V + R";
            this.tabBVR.UseVisualStyleBackColor = true;
            // 
            // plotBVRV
            // 
            this.plotBVRV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plotBVRV.AutoScaleAutoGeneratedAxes = false;
            this.plotBVRV.AutoScaleTitle = false;
            this.plotBVRV.BackColor = System.Drawing.SystemColors.Window;
            this.plotBVRV.DateTimeToolTip = false;
            this.plotBVRV.Legend = null;
            this.plotBVRV.LegendZOrder = -1;
            this.plotBVRV.Location = new System.Drawing.Point(226, 106);
            this.plotBVRV.Name = "plotBVRV";
            this.plotBVRV.RightMenu = null;
            this.plotBVRV.ShowCoordinates = true;
            this.plotBVRV.Size = new System.Drawing.Size(327, 117);
            this.plotBVRV.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotBVRV.TabIndex = 85;
            this.plotBVRV.Text = "plotSurface2D3";
            this.plotBVRV.Title = "";
            this.plotBVRV.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotBVRV.XAxis1 = null;
            this.plotBVRV.XAxis2 = null;
            this.plotBVRV.YAxis1 = null;
            this.plotBVRV.YAxis2 = null;
            // 
            // plotBVRR
            // 
            this.plotBVRR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plotBVRR.AutoScaleAutoGeneratedAxes = false;
            this.plotBVRR.AutoScaleTitle = false;
            this.plotBVRR.BackColor = System.Drawing.SystemColors.Window;
            this.plotBVRR.DateTimeToolTip = false;
            this.plotBVRR.Legend = null;
            this.plotBVRR.LegendZOrder = -1;
            this.plotBVRR.Location = new System.Drawing.Point(226, 215);
            this.plotBVRR.Name = "plotBVRR";
            this.plotBVRR.RightMenu = null;
            this.plotBVRR.ShowCoordinates = true;
            this.plotBVRR.Size = new System.Drawing.Size(327, 117);
            this.plotBVRR.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotBVRR.TabIndex = 84;
            this.plotBVRR.Text = "plotSurface2D2";
            this.plotBVRR.Title = "";
            this.plotBVRR.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotBVRR.XAxis1 = null;
            this.plotBVRR.XAxis2 = null;
            this.plotBVRR.YAxis1 = null;
            this.plotBVRR.YAxis2 = null;
            // 
            // plotBVRB
            // 
            this.plotBVRB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plotBVRB.AutoScaleAutoGeneratedAxes = false;
            this.plotBVRB.AutoScaleTitle = false;
            this.plotBVRB.BackColor = System.Drawing.SystemColors.Window;
            this.plotBVRB.DateTimeToolTip = false;
            this.plotBVRB.Legend = null;
            this.plotBVRB.LegendZOrder = -1;
            this.plotBVRB.Location = new System.Drawing.Point(226, 0);
            this.plotBVRB.Name = "plotBVRB";
            this.plotBVRB.RightMenu = null;
            this.plotBVRB.ShowCoordinates = true;
            this.plotBVRB.Size = new System.Drawing.Size(327, 117);
            this.plotBVRB.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotBVRB.TabIndex = 83;
            this.plotBVRB.Text = "plotSurface2D1";
            this.plotBVRB.Title = "";
            this.plotBVRB.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotBVRB.XAxis1 = null;
            this.plotBVRB.XAxis2 = null;
            this.plotBVRB.YAxis1 = null;
            this.plotBVRB.YAxis2 = null;
            // 
            // btnSavePlotBVR
            // 
            this.btnSavePlotBVR.Enabled = false;
            this.btnSavePlotBVR.Location = new System.Drawing.Point(6, 125);
            this.btnSavePlotBVR.Name = "btnSavePlotBVR";
            this.btnSavePlotBVR.Size = new System.Drawing.Size(214, 23);
            this.btnSavePlotBVR.TabIndex = 81;
            this.btnSavePlotBVR.Text = "Save plot";
            this.btnSavePlotBVR.UseVisualStyleBackColor = true;
            this.btnSavePlotBVR.Click += new System.EventHandler(this.btnSavePlotBVR_Click);
            // 
            // btnClearBVR
            // 
            this.btnClearBVR.Enabled = false;
            this.btnClearBVR.Location = new System.Drawing.Point(114, 96);
            this.btnClearBVR.Name = "btnClearBVR";
            this.btnClearBVR.Size = new System.Drawing.Size(106, 23);
            this.btnClearBVR.TabIndex = 80;
            this.btnClearBVR.Text = "Clear plot";
            this.btnClearBVR.UseVisualStyleBackColor = true;
            this.btnClearBVR.Click += new System.EventHandler(this.btnClearBVR_Click);
            // 
            // btnShowBVR
            // 
            this.btnShowBVR.Enabled = false;
            this.btnShowBVR.Location = new System.Drawing.Point(6, 96);
            this.btnShowBVR.Name = "btnShowBVR";
            this.btnShowBVR.Size = new System.Drawing.Size(102, 23);
            this.btnShowBVR.TabIndex = 79;
            this.btnShowBVR.Text = "Show plot";
            this.btnShowBVR.UseVisualStyleBackColor = true;
            this.btnShowBVR.Click += new System.EventHandler(this.btnShowBVR_Click);
            // 
            // btnLoadR_BVR
            // 
            this.btnLoadR_BVR.Location = new System.Drawing.Point(6, 67);
            this.btnLoadR_BVR.Name = "btnLoadR_BVR";
            this.btnLoadR_BVR.Size = new System.Drawing.Size(214, 23);
            this.btnLoadR_BVR.TabIndex = 78;
            this.btnLoadR_BVR.Text = "Load observed R lightcurve";
            this.btnLoadR_BVR.UseVisualStyleBackColor = true;
            this.btnLoadR_BVR.Click += new System.EventHandler(this.btnLoadR_BVR_Click);
            // 
            // btnLoadV_BVR
            // 
            this.btnLoadV_BVR.Location = new System.Drawing.Point(6, 38);
            this.btnLoadV_BVR.Name = "btnLoadV_BVR";
            this.btnLoadV_BVR.Size = new System.Drawing.Size(214, 23);
            this.btnLoadV_BVR.TabIndex = 77;
            this.btnLoadV_BVR.Text = "Load observed V lightcurve";
            this.btnLoadV_BVR.UseVisualStyleBackColor = true;
            this.btnLoadV_BVR.Click += new System.EventHandler(this.btnLoadV_BVR_Click);
            // 
            // btnLoadB_BVR
            // 
            this.btnLoadB_BVR.Location = new System.Drawing.Point(6, 9);
            this.btnLoadB_BVR.Name = "btnLoadB_BVR";
            this.btnLoadB_BVR.Size = new System.Drawing.Size(214, 23);
            this.btnLoadB_BVR.TabIndex = 76;
            this.btnLoadB_BVR.Text = "Load observed B lightcurve";
            this.btnLoadB_BVR.UseVisualStyleBackColor = true;
            this.btnLoadB_BVR.Click += new System.EventHandler(this.btnLoadB_BVR_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Arctic.Properties.Resources._7_2_polar_bear_free_download_png_thumb;
            this.pictureBox1.Location = new System.Drawing.Point(6, 154);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(214, 178);
            this.pictureBox1.TabIndex = 82;
            this.pictureBox1.TabStop = false;
            // 
            // tabIVStokes
            // 
            this.tabIVStokes.Controls.Add(this.pictureBox2);
            this.tabIVStokes.Controls.Add(this.plotIVV);
            this.tabIVStokes.Controls.Add(this.plotIVI);
            this.tabIVStokes.Controls.Add(this.btnSaveIV);
            this.tabIVStokes.Controls.Add(this.btnClearIV);
            this.tabIVStokes.Controls.Add(this.btnShowIV);
            this.tabIVStokes.Controls.Add(this.btnLoadVV);
            this.tabIVStokes.Controls.Add(this.btnLoadIV);
            this.tabIVStokes.Location = new System.Drawing.Point(4, 22);
            this.tabIVStokes.Name = "tabIVStokes";
            this.tabIVStokes.Size = new System.Drawing.Size(554, 335);
            this.tabIVStokes.TabIndex = 2;
            this.tabIVStokes.Text = "I+VStokes V";
            this.tabIVStokes.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Arctic.Properties.Resources.Без_имени_2;
            this.pictureBox2.Location = new System.Drawing.Point(215, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(336, 118);
            this.pictureBox2.TabIndex = 86;
            this.pictureBox2.TabStop = false;
            // 
            // plotIVV
            // 
            this.plotIVV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plotIVV.AutoScaleAutoGeneratedAxes = false;
            this.plotIVV.AutoScaleTitle = false;
            this.plotIVV.BackColor = System.Drawing.SystemColors.Window;
            this.plotIVV.DateTimeToolTip = false;
            this.plotIVV.Legend = null;
            this.plotIVV.LegendZOrder = -1;
            this.plotIVV.Location = new System.Drawing.Point(278, 124);
            this.plotIVV.Name = "plotIVV";
            this.plotIVV.RightMenu = null;
            this.plotIVV.ShowCoordinates = true;
            this.plotIVV.Size = new System.Drawing.Size(275, 208);
            this.plotIVV.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotIVV.TabIndex = 85;
            this.plotIVV.Text = "plotSurface2D5";
            this.plotIVV.Title = "";
            this.plotIVV.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotIVV.XAxis1 = null;
            this.plotIVV.XAxis2 = null;
            this.plotIVV.YAxis1 = null;
            this.plotIVV.YAxis2 = null;
            // 
            // plotIVI
            // 
            this.plotIVI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plotIVI.AutoScaleAutoGeneratedAxes = false;
            this.plotIVI.AutoScaleTitle = false;
            this.plotIVI.BackColor = System.Drawing.SystemColors.Window;
            this.plotIVI.DateTimeToolTip = false;
            this.plotIVI.Legend = null;
            this.plotIVI.LegendZOrder = -1;
            this.plotIVI.Location = new System.Drawing.Point(6, 124);
            this.plotIVI.Name = "plotIVI";
            this.plotIVI.RightMenu = null;
            this.plotIVI.ShowCoordinates = true;
            this.plotIVI.Size = new System.Drawing.Size(275, 208);
            this.plotIVI.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotIVI.TabIndex = 84;
            this.plotIVI.Text = "plotSurface2D4";
            this.plotIVI.Title = "";
            this.plotIVI.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotIVI.XAxis1 = null;
            this.plotIVI.XAxis2 = null;
            this.plotIVI.YAxis1 = null;
            this.plotIVI.YAxis2 = null;
            // 
            // btnSaveIV
            // 
            this.btnSaveIV.Enabled = false;
            this.btnSaveIV.Location = new System.Drawing.Point(6, 95);
            this.btnSaveIV.Name = "btnSaveIV";
            this.btnSaveIV.Size = new System.Drawing.Size(203, 23);
            this.btnSaveIV.TabIndex = 82;
            this.btnSaveIV.Text = "Save plot";
            this.btnSaveIV.UseVisualStyleBackColor = true;
            this.btnSaveIV.Click += new System.EventHandler(this.btnSaveIV_Click);
            // 
            // btnClearIV
            // 
            this.btnClearIV.Enabled = false;
            this.btnClearIV.Location = new System.Drawing.Point(114, 66);
            this.btnClearIV.Name = "btnClearIV";
            this.btnClearIV.Size = new System.Drawing.Size(95, 23);
            this.btnClearIV.TabIndex = 81;
            this.btnClearIV.Text = "Clear plot";
            this.btnClearIV.UseVisualStyleBackColor = true;
            this.btnClearIV.Click += new System.EventHandler(this.btnClearIV_Click);
            // 
            // btnShowIV
            // 
            this.btnShowIV.Enabled = false;
            this.btnShowIV.Location = new System.Drawing.Point(6, 66);
            this.btnShowIV.Name = "btnShowIV";
            this.btnShowIV.Size = new System.Drawing.Size(102, 23);
            this.btnShowIV.TabIndex = 80;
            this.btnShowIV.Text = "Show plot";
            this.btnShowIV.UseVisualStyleBackColor = true;
            this.btnShowIV.Click += new System.EventHandler(this.btnShowIV_Click);
            // 
            // btnLoadVV
            // 
            this.btnLoadVV.Location = new System.Drawing.Point(6, 37);
            this.btnLoadVV.Name = "btnLoadVV";
            this.btnLoadVV.Size = new System.Drawing.Size(203, 23);
            this.btnLoadVV.TabIndex = 78;
            this.btnLoadVV.Text = "Load V Stokes V";
            this.btnLoadVV.UseVisualStyleBackColor = true;
            this.btnLoadVV.Click += new System.EventHandler(this.btnLoadVV_Click);
            // 
            // btnLoadIV
            // 
            this.btnLoadIV.Location = new System.Drawing.Point(6, 9);
            this.btnLoadIV.Name = "btnLoadIV";
            this.btnLoadIV.Size = new System.Drawing.Size(203, 23);
            this.btnLoadIV.TabIndex = 77;
            this.btnLoadIV.Text = "Load I Stokes V";
            this.btnLoadIV.UseVisualStyleBackColor = true;
            this.btnLoadIV.Click += new System.EventHandler(this.btnLoadIV_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInc);
            this.groupBox1.Controls.Add(this.txtSP);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtB);
            this.groupBox1.Controls.Add(this.txtkT);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 65);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set star pars";
            // 
            // txtInc
            // 
            this.txtInc.Location = new System.Drawing.Point(89, 39);
            this.txtInc.Name = "txtInc";
            this.txtInc.Size = new System.Drawing.Size(33, 20);
            this.txtInc.TabIndex = 63;
            this.txtInc.Text = "90";
            // 
            // txtSP
            // 
            this.txtSP.Location = new System.Drawing.Point(153, 15);
            this.txtSP.Name = "txtSP";
            this.txtSP.Size = new System.Drawing.Size(33, 20);
            this.txtSP.TabIndex = 57;
            this.txtSP.Text = "5,1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(132, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "SP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Inc";
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(25, 15);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(34, 20);
            this.txtB.TabIndex = 59;
            this.txtB.Text = "26";
            // 
            // txtkT
            // 
            this.txtkT.Location = new System.Drawing.Point(89, 15);
            this.txtkT.Name = "txtkT";
            this.txtkT.Size = new System.Drawing.Size(33, 20);
            this.txtkT.TabIndex = 60;
            this.txtkT.Text = "12";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 62;
            this.label11.Text = "B";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(69, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "kT";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPsi);
            this.groupBox2.Controls.Add(this.txtKsi);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtBeta);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtdFi);
            this.groupBox2.Controls.Add(this.txtFi1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(9, 249);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 70);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Best fit";
            // 
            // txtPsi
            // 
            this.txtPsi.Enabled = false;
            this.txtPsi.Location = new System.Drawing.Point(96, 14);
            this.txtPsi.Name = "txtPsi";
            this.txtPsi.Size = new System.Drawing.Size(33, 20);
            this.txtPsi.TabIndex = 71;
            // 
            // txtKsi
            // 
            this.txtKsi.Enabled = false;
            this.txtKsi.Location = new System.Drawing.Point(32, 39);
            this.txtKsi.Name = "txtKsi";
            this.txtKsi.Size = new System.Drawing.Size(33, 20);
            this.txtKsi.TabIndex = 69;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Psi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Ksi";
            // 
            // txtBeta
            // 
            this.txtBeta.Enabled = false;
            this.txtBeta.Location = new System.Drawing.Point(32, 14);
            this.txtBeta.Name = "txtBeta";
            this.txtBeta.Size = new System.Drawing.Size(33, 20);
            this.txtBeta.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 70;
            this.label1.Text = "Beta";
            // 
            // txtdFi
            // 
            this.txtdFi.Enabled = false;
            this.txtdFi.Location = new System.Drawing.Point(155, 39);
            this.txtdFi.Name = "txtdFi";
            this.txtdFi.Size = new System.Drawing.Size(33, 20);
            this.txtdFi.TabIndex = 73;
            // 
            // txtFi1
            // 
            this.txtFi1.Enabled = false;
            this.txtFi1.Location = new System.Drawing.Point(96, 39);
            this.txtFi1.Name = "txtFi1";
            this.txtFi1.Size = new System.Drawing.Size(33, 20);
            this.txtFi1.TabIndex = 71;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(136, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "dFi";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 72;
            this.label5.Text = "Fi1";
            // 
            // btn_SavePars
            // 
            this.btn_SavePars.Enabled = false;
            this.btn_SavePars.Location = new System.Drawing.Point(9, 352);
            this.btn_SavePars.Name = "btn_SavePars";
            this.btn_SavePars.Size = new System.Drawing.Size(193, 21);
            this.btn_SavePars.TabIndex = 73;
            this.btn_SavePars.Text = "Save pars";
            this.btn_SavePars.UseVisualStyleBackColor = true;
            this.btn_SavePars.Click += new System.EventHandler(this.btn_SavePars_Click);
            // 
            // btnSaveLC
            // 
            this.btnSaveLC.Enabled = false;
            this.btnSaveLC.Location = new System.Drawing.Point(9, 325);
            this.btnSaveLC.Name = "btnSaveLC";
            this.btnSaveLC.Size = new System.Drawing.Size(193, 21);
            this.btnSaveLC.TabIndex = 72;
            this.btnSaveLC.Text = "Save LCs";
            this.btnSaveLC.UseVisualStyleBackColor = true;
            this.btnSaveLC.Click += new System.EventHandler(this.btnSaveLC_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtMC);
            this.groupBox3.Controls.Add(this.txtGen);
            this.groupBox3.Controls.Add(this.txtPop);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Location = new System.Drawing.Point(9, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(193, 122);
            this.groupBox3.TabIndex = 74;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Optimization";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(58, 95);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 76;
            this.label13.Text = "Points";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(71, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 78;
            this.label12.Text = "and";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 76;
            this.label9.Text = "Generations";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 75;
            this.label7.Text = "Population";
            // 
            // txtMC
            // 
            this.txtMC.Location = new System.Drawing.Point(100, 92);
            this.txtMC.Name = "txtMC";
            this.txtMC.Size = new System.Drawing.Size(33, 20);
            this.txtMC.TabIndex = 77;
            this.txtMC.Text = "500";
            // 
            // txtGen
            // 
            this.txtGen.Location = new System.Drawing.Point(139, 37);
            this.txtGen.Name = "txtGen";
            this.txtGen.Size = new System.Drawing.Size(33, 20);
            this.txtGen.TabIndex = 77;
            this.txtGen.Text = "50";
            // 
            // txtPop
            // 
            this.txtPop.Location = new System.Drawing.Point(100, 37);
            this.txtPop.Name = "txtPop";
            this.txtPop.Size = new System.Drawing.Size(33, 20);
            this.txtPop.TabIndex = 76;
            this.txtPop.Text = "100";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(9, 72);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(155, 17);
            this.radioButton2.TabIndex = 75;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Monte-Carlo + Nelder-Mead";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(135, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Genetic + Nelder-Mead";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnLC
            // 
            this.btnLC.Enabled = false;
            this.btnLC.Location = new System.Drawing.Point(9, 206);
            this.btnLC.Name = "btnLC";
            this.btnLC.Size = new System.Drawing.Size(193, 37);
            this.btnLC.TabIndex = 75;
            this.btnLC.Text = "To strive, to seek, to find, \r\nand not to yield!";
            this.btnLC.UseVisualStyleBackColor = true;
            this.btnLC.Click += new System.EventHandler(this.btnLC_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBar1
            // 
            this.progressBar1.BarOffset = 1;
            this.progressBar1.Caption = "Progress";
            this.progressBar1.CaptionColor = System.Drawing.Color.Black;
            this.progressBar1.CaptionMode = ExtendedDotNET.Controls.Progress.ProgressCaptionMode.None;
            this.progressBar1.CaptionShadowColor = System.Drawing.Color.White;
            this.progressBar1.ChangeByMouse = false;
            this.progressBar1.DashSpace = 0;
            this.progressBar1.DashWidth = 5;
            this.progressBar1.Edge = ExtendedDotNET.Controls.Progress.ProgressBarEdge.None;
            this.progressBar1.EdgeColor = System.Drawing.Color.LightGray;
            this.progressBar1.EdgeLightColor = System.Drawing.Color.LightGray;
            this.progressBar1.EdgeWidth = 0;
            this.progressBar1.FloodPercentage = 0.2F;
            this.progressBar1.FloodStyle = ExtendedDotNET.Controls.Progress.ProgressFloodStyle.Standard;
            this.progressBar1.Invert = false;
            this.progressBar1.Location = new System.Drawing.Point(446, 12);
            this.progressBar1.MainColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Orientation = ExtendedDotNET.Controls.Progress.ProgressBarDirection.Horizontal;
            this.progressBar1.ProgressBackColor = System.Drawing.Color.Transparent;
            this.progressBar1.ProgressBarStyle = ExtendedDotNET.Controls.Progress.ProgressStyle.Solid;
            this.progressBar1.SecondColor = System.Drawing.Color.LightSeaGreen;
            this.progressBar1.Shadow = false;
            this.progressBar1.ShadowOffset = 1;
            this.progressBar1.Size = new System.Drawing.Size(322, 16);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 80;
            this.progressBar1.TextAntialias = false;
            this.progressBar1.Value = 0;
            // 
            // Arctic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 380);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnLC);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_SavePars);
            this.Controls.Add(this.btnSaveLC);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Arctic";
            this.Text = "ARCtic polars";
            this.tabControl1.ResumeLayout(false);
            this.tabV.ResumeLayout(false);
            this.tabBVR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabIVStokes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBVR;
        private System.Windows.Forms.TabPage tabIVStokes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInc;
        private System.Windows.Forms.TextBox txtSP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtkT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPsi;
        private System.Windows.Forms.TextBox txtKsi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBeta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdFi;
        private System.Windows.Forms.TextBox txtFi1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_SavePars;
        private System.Windows.Forms.Button btnSaveLC;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMC;
        private System.Windows.Forms.TextBox txtGen;
        private System.Windows.Forms.TextBox txtPop;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnLC;
        private System.Windows.Forms.TabPage tabV;
        private System.Windows.Forms.Button btnSavePlotV;
        private NPlot.Windows.PlotSurface2D plot;
        private System.Windows.Forms.Button btnClearV;
        private System.Windows.Forms.Button btnShowV;
        private System.Windows.Forms.Button btnLoadV;
        private System.Windows.Forms.Button btnSavePlotBVR;
        private System.Windows.Forms.Button btnClearBVR;
        private System.Windows.Forms.Button btnShowBVR;
        private System.Windows.Forms.Button btnLoadR_BVR;
        private System.Windows.Forms.Button btnLoadV_BVR;
        private System.Windows.Forms.Button btnLoadB_BVR;
        private System.Windows.Forms.PictureBox pictureBox1;
        private NPlot.Windows.PlotSurface2D plotBVRV;
        private NPlot.Windows.PlotSurface2D plotBVRR;
        private NPlot.Windows.PlotSurface2D plotBVRB;
        private System.Windows.Forms.Button btnLoadIV;
        private NPlot.Windows.PlotSurface2D plotIVV;
        private NPlot.Windows.PlotSurface2D plotIVI;
        private System.Windows.Forms.Button btnSaveIV;
        private System.Windows.Forms.Button btnClearIV;
        private System.Windows.Forms.Button btnShowIV;
        private System.Windows.Forms.Button btnLoadVV;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public ExtendedDotNET.Controls.Progress.ProgressBar progressBar1;
    }
}

