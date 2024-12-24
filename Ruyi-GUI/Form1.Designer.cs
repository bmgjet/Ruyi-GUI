namespace Ruyi_GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Img1lbl = new System.Windows.Forms.Label();
            this.Img2lbl = new System.Windows.Forms.Label();
            this.Img1 = new System.Windows.Forms.TextBox();
            this.Img2 = new System.Windows.Forms.TextBox();
            this.SelectImg1 = new System.Windows.Forms.Button();
            this.SelectImg2 = new System.Windows.Forms.Button();
            this.SelectVideoOut = new System.Windows.Forms.Button();
            this.VideoOut = new System.Windows.Forms.TextBox();
            this.Videolbl = new System.Windows.Forms.Label();
            this.FrameRate = new System.Windows.Forms.ComboBox();
            this.Frameslbl = new System.Windows.Forms.Label();
            this.Resolutionlbl = new System.Windows.Forms.Label();
            this.Resolution = new System.Windows.Forms.ComboBox();
            this.Aspectlbl = new System.Windows.Forms.Label();
            this.AspectRatio = new System.Windows.Forms.ComboBox();
            this.Motionlbl = new System.Windows.Forms.Label();
            this.Motion = new System.Windows.Forms.ComboBox();
            this.Directionlbl = new System.Windows.Forms.Label();
            this.Direction = new System.Windows.Forms.ComboBox();
            this.LowMemoryMode = new System.Windows.Forms.CheckBox();
            this.Offloadlbl = new System.Windows.Forms.Label();
            this.GPUOffload = new System.Windows.Forms.TextBox();
            this.Samplerlbl = new System.Windows.Forms.Label();
            this.Steps = new System.Windows.Forms.TextBox();
            this.Stepslbl = new System.Windows.Forms.Label();
            this.Cfg = new System.Windows.Forms.TextBox();
            this.Cfglbl = new System.Windows.Forms.Label();
            this.Seed = new System.Windows.Forms.TextBox();
            this.Seedlbl = new System.Windows.Forms.Label();
            this.Schedulerlbl = new System.Windows.Forms.Label();
            this.Scheduler = new System.Windows.Forms.ComboBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.PlayVideo = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.AddJob = new System.Windows.Forms.Button();
            this.JobList = new System.Windows.Forms.ListBox();
            this.RemoveJob = new System.Windows.Forms.Button();
            this.RunJobs = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Batch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Weighttxt = new System.Windows.Forms.TextBox();
            this.Weightlbl = new System.Windows.Forms.Label();
            this.LoraButton = new System.Windows.Forms.Button();
            this.Loratxt = new System.Windows.Forms.TextBox();
            this.Loralbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.VideoRes = new System.Windows.Forms.ComboBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.Updates = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(257, 267);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(276, 6);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(257, 267);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // Img1lbl
            // 
            this.Img1lbl.AutoSize = true;
            this.Img1lbl.Location = new System.Drawing.Point(3, 280);
            this.Img1lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Img1lbl.Name = "Img1lbl";
            this.Img1lbl.Size = new System.Drawing.Size(78, 13);
            this.Img1lbl.TabIndex = 2;
            this.Img1lbl.Text = "Starting Image:";
            // 
            // Img2lbl
            // 
            this.Img2lbl.AutoSize = true;
            this.Img2lbl.Location = new System.Drawing.Point(273, 280);
            this.Img2lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Img2lbl.Name = "Img2lbl";
            this.Img2lbl.Size = new System.Drawing.Size(61, 13);
            this.Img2lbl.TabIndex = 3;
            this.Img2lbl.Text = "End Image:";
            // 
            // Img1
            // 
            this.Img1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Img1.Location = new System.Drawing.Point(85, 277);
            this.Img1.Margin = new System.Windows.Forms.Padding(2);
            this.Img1.Name = "Img1";
            this.Img1.ReadOnly = true;
            this.Img1.Size = new System.Drawing.Size(178, 20);
            this.Img1.TabIndex = 4;
            // 
            // Img2
            // 
            this.Img2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Img2.Location = new System.Drawing.Point(335, 277);
            this.Img2.Margin = new System.Windows.Forms.Padding(2);
            this.Img2.Name = "Img2";
            this.Img2.ReadOnly = true;
            this.Img2.Size = new System.Drawing.Size(198, 20);
            this.Img2.TabIndex = 5;
            // 
            // SelectImg1
            // 
            this.SelectImg1.Location = new System.Drawing.Point(6, 300);
            this.SelectImg1.Margin = new System.Windows.Forms.Padding(2);
            this.SelectImg1.Name = "SelectImg1";
            this.SelectImg1.Size = new System.Drawing.Size(257, 22);
            this.SelectImg1.TabIndex = 6;
            this.SelectImg1.Text = "Select";
            this.SelectImg1.UseVisualStyleBackColor = true;
            this.SelectImg1.Click += new System.EventHandler(this.SelectImg1_Click);
            // 
            // SelectImg2
            // 
            this.SelectImg2.Location = new System.Drawing.Point(276, 300);
            this.SelectImg2.Margin = new System.Windows.Forms.Padding(2);
            this.SelectImg2.Name = "SelectImg2";
            this.SelectImg2.Size = new System.Drawing.Size(257, 22);
            this.SelectImg2.TabIndex = 7;
            this.SelectImg2.Text = "Select";
            this.SelectImg2.UseVisualStyleBackColor = true;
            this.SelectImg2.Click += new System.EventHandler(this.SelectImg2_Click);
            // 
            // SelectVideoOut
            // 
            this.SelectVideoOut.Location = new System.Drawing.Point(445, 324);
            this.SelectVideoOut.Margin = new System.Windows.Forms.Padding(2);
            this.SelectVideoOut.Name = "SelectVideoOut";
            this.SelectVideoOut.Size = new System.Drawing.Size(87, 22);
            this.SelectVideoOut.TabIndex = 10;
            this.SelectVideoOut.Text = "Select";
            this.SelectVideoOut.UseVisualStyleBackColor = true;
            this.SelectVideoOut.Click += new System.EventHandler(this.SelectVideoOut_Click);
            // 
            // VideoOut
            // 
            this.VideoOut.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.VideoOut.Location = new System.Drawing.Point(78, 326);
            this.VideoOut.Margin = new System.Windows.Forms.Padding(2);
            this.VideoOut.Name = "VideoOut";
            this.VideoOut.ReadOnly = true;
            this.VideoOut.Size = new System.Drawing.Size(363, 20);
            this.VideoOut.TabIndex = 9;
            // 
            // Videolbl
            // 
            this.Videolbl.AutoSize = true;
            this.Videolbl.Location = new System.Drawing.Point(3, 329);
            this.Videolbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Videolbl.Name = "Videolbl";
            this.Videolbl.Size = new System.Drawing.Size(72, 13);
            this.Videolbl.TabIndex = 8;
            this.Videolbl.Text = "Output Video:";
            // 
            // FrameRate
            // 
            this.FrameRate.FormattingEnabled = true;
            this.FrameRate.Items.AddRange(new object[] {
            "24",
            "48",
            "72",
            "96",
            "120"});
            this.FrameRate.Location = new System.Drawing.Point(6, 372);
            this.FrameRate.Margin = new System.Windows.Forms.Padding(2);
            this.FrameRate.MaxLength = 3;
            this.FrameRate.Name = "FrameRate";
            this.FrameRate.Size = new System.Drawing.Size(80, 21);
            this.FrameRate.TabIndex = 11;
            this.FrameRate.Text = "24";
            this.FrameRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOnly);
            // 
            // Frameslbl
            // 
            this.Frameslbl.AutoSize = true;
            this.Frameslbl.Location = new System.Drawing.Point(3, 357);
            this.Frameslbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Frameslbl.Name = "Frameslbl";
            this.Frameslbl.Size = new System.Drawing.Size(74, 13);
            this.Frameslbl.TabIndex = 12;
            this.Frameslbl.Text = "Video Frames:";
            // 
            // Resolutionlbl
            // 
            this.Resolutionlbl.AutoSize = true;
            this.Resolutionlbl.Location = new System.Drawing.Point(164, 358);
            this.Resolutionlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Resolutionlbl.Name = "Resolutionlbl";
            this.Resolutionlbl.Size = new System.Drawing.Size(115, 13);
            this.Resolutionlbl.TabIndex = 14;
            this.Resolutionlbl.Text = "Generation Resolution:";
            // 
            // Resolution
            // 
            this.Resolution.FormattingEnabled = true;
            this.Resolution.Items.AddRange(new object[] {
            "384",
            "480",
            "512",
            "720",
            "768",
            "896"});
            this.Resolution.Location = new System.Drawing.Point(166, 372);
            this.Resolution.Margin = new System.Windows.Forms.Padding(2);
            this.Resolution.MaxLength = 3;
            this.Resolution.Name = "Resolution";
            this.Resolution.Size = new System.Drawing.Size(116, 21);
            this.Resolution.TabIndex = 13;
            this.Resolution.Text = "384";
            this.Resolution.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOnly);
            // 
            // Aspectlbl
            // 
            this.Aspectlbl.AutoSize = true;
            this.Aspectlbl.Location = new System.Drawing.Point(90, 358);
            this.Aspectlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Aspectlbl.Name = "Aspectlbl";
            this.Aspectlbl.Size = new System.Drawing.Size(71, 13);
            this.Aspectlbl.TabIndex = 16;
            this.Aspectlbl.Text = "Aspect Ratio:";
            // 
            // AspectRatio
            // 
            this.AspectRatio.FormattingEnabled = true;
            this.AspectRatio.Items.AddRange(new object[] {
            "21:9",
            "16:9",
            "4:3",
            "3:2",
            "1:1",
            "2:3",
            "3:4",
            "9:16",
            "9:21"});
            this.AspectRatio.Location = new System.Drawing.Point(93, 372);
            this.AspectRatio.Margin = new System.Windows.Forms.Padding(2);
            this.AspectRatio.MaxLength = 5;
            this.AspectRatio.Name = "AspectRatio";
            this.AspectRatio.Size = new System.Drawing.Size(70, 21);
            this.AspectRatio.TabIndex = 15;
            this.AspectRatio.Text = "16:9";
            this.AspectRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AspectRatio_KeyPress);
            // 
            // Motionlbl
            // 
            this.Motionlbl.AutoSize = true;
            this.Motionlbl.Location = new System.Drawing.Point(378, 358);
            this.Motionlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Motionlbl.Name = "Motionlbl";
            this.Motionlbl.Size = new System.Drawing.Size(42, 13);
            this.Motionlbl.TabIndex = 18;
            this.Motionlbl.Text = "Motion:";
            // 
            // Motion
            // 
            this.Motion.FormattingEnabled = true;
            this.Motion.Items.AddRange(new object[] {
            "auto",
            "1",
            "2",
            "3",
            "4"});
            this.Motion.Location = new System.Drawing.Point(380, 372);
            this.Motion.Margin = new System.Windows.Forms.Padding(2);
            this.Motion.MaxLength = 4;
            this.Motion.Name = "Motion";
            this.Motion.Size = new System.Drawing.Size(61, 21);
            this.Motion.TabIndex = 17;
            this.Motion.Text = "auto";
            this.Motion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberAndAuto);
            // 
            // Directionlbl
            // 
            this.Directionlbl.AutoSize = true;
            this.Directionlbl.Location = new System.Drawing.Point(282, 358);
            this.Directionlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Directionlbl.Name = "Directionlbl";
            this.Directionlbl.Size = new System.Drawing.Size(91, 13);
            this.Directionlbl.TabIndex = 20;
            this.Directionlbl.Text = "Camera Direction:";
            // 
            // Direction
            // 
            this.Direction.FormattingEnabled = true;
            this.Direction.Items.AddRange(new object[] {
            "auto",
            "static",
            "left",
            "right",
            "up",
            "down"});
            this.Direction.Location = new System.Drawing.Point(285, 372);
            this.Direction.Margin = new System.Windows.Forms.Padding(2);
            this.Direction.MaxLength = 4;
            this.Direction.Name = "Direction";
            this.Direction.Size = new System.Drawing.Size(92, 21);
            this.Direction.TabIndex = 19;
            this.Direction.Text = "auto";
            this.Direction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberAndAuto);
            // 
            // LowMemoryMode
            // 
            this.LowMemoryMode.AutoSize = true;
            this.LowMemoryMode.Location = new System.Drawing.Point(113, 425);
            this.LowMemoryMode.Margin = new System.Windows.Forms.Padding(2);
            this.LowMemoryMode.Name = "LowMemoryMode";
            this.LowMemoryMode.Size = new System.Drawing.Size(142, 17);
            this.LowMemoryMode.TabIndex = 21;
            this.LowMemoryMode.Text = "Low GPU Memory Mode";
            this.LowMemoryMode.UseVisualStyleBackColor = true;
            // 
            // Offloadlbl
            // 
            this.Offloadlbl.AutoSize = true;
            this.Offloadlbl.Location = new System.Drawing.Point(3, 403);
            this.Offloadlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Offloadlbl.Name = "Offloadlbl";
            this.Offloadlbl.Size = new System.Drawing.Size(100, 13);
            this.Offloadlbl.TabIndex = 23;
            this.Offloadlbl.Text = "GPU Offload Steps:";
            // 
            // GPUOffload
            // 
            this.GPUOffload.Location = new System.Drawing.Point(6, 422);
            this.GPUOffload.Margin = new System.Windows.Forms.Padding(2);
            this.GPUOffload.MaxLength = 2;
            this.GPUOffload.Name = "GPUOffload";
            this.GPUOffload.Size = new System.Drawing.Size(95, 20);
            this.GPUOffload.TabIndex = 24;
            this.GPUOffload.Text = "0";
            this.GPUOffload.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOnly);
            // 
            // Samplerlbl
            // 
            this.Samplerlbl.AutoSize = true;
            this.Samplerlbl.Location = new System.Drawing.Point(3, 447);
            this.Samplerlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Samplerlbl.Name = "Samplerlbl";
            this.Samplerlbl.Size = new System.Drawing.Size(89, 13);
            this.Samplerlbl.TabIndex = 25;
            this.Samplerlbl.Text = "Sampler Settings:";
            // 
            // Steps
            // 
            this.Steps.Location = new System.Drawing.Point(11, 480);
            this.Steps.Margin = new System.Windows.Forms.Padding(2);
            this.Steps.MaxLength = 2;
            this.Steps.Name = "Steps";
            this.Steps.Size = new System.Drawing.Size(95, 20);
            this.Steps.TabIndex = 27;
            this.Steps.Text = "25";
            this.Steps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOnly);
            // 
            // Stepslbl
            // 
            this.Stepslbl.AutoSize = true;
            this.Stepslbl.Location = new System.Drawing.Point(5, 463);
            this.Stepslbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Stepslbl.Name = "Stepslbl";
            this.Stepslbl.Size = new System.Drawing.Size(37, 13);
            this.Stepslbl.TabIndex = 26;
            this.Stepslbl.Text = "Steps:";
            // 
            // Cfg
            // 
            this.Cfg.Location = new System.Drawing.Point(113, 480);
            this.Cfg.Margin = new System.Windows.Forms.Padding(2);
            this.Cfg.MaxLength = 4;
            this.Cfg.Name = "Cfg";
            this.Cfg.Size = new System.Drawing.Size(95, 20);
            this.Cfg.TabIndex = 29;
            this.Cfg.Text = "7.0";
            this.Cfg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cfg_KeyPress);
            // 
            // Cfglbl
            // 
            this.Cfglbl.AutoSize = true;
            this.Cfglbl.Location = new System.Drawing.Point(110, 463);
            this.Cfglbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Cfglbl.Name = "Cfglbl";
            this.Cfglbl.Size = new System.Drawing.Size(26, 13);
            this.Cfglbl.TabIndex = 28;
            this.Cfglbl.Text = "Cfg:";
            // 
            // Seed
            // 
            this.Seed.Location = new System.Drawing.Point(215, 480);
            this.Seed.Margin = new System.Windows.Forms.Padding(2);
            this.Seed.MaxLength = 6;
            this.Seed.Name = "Seed";
            this.Seed.Size = new System.Drawing.Size(95, 20);
            this.Seed.TabIndex = 31;
            this.Seed.Text = "42";
            this.Seed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOnly);
            // 
            // Seedlbl
            // 
            this.Seedlbl.AutoSize = true;
            this.Seedlbl.Location = new System.Drawing.Point(212, 463);
            this.Seedlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Seedlbl.Name = "Seedlbl";
            this.Seedlbl.Size = new System.Drawing.Size(35, 13);
            this.Seedlbl.TabIndex = 30;
            this.Seedlbl.Text = "Seed:";
            // 
            // Schedulerlbl
            // 
            this.Schedulerlbl.AutoSize = true;
            this.Schedulerlbl.Location = new System.Drawing.Point(315, 463);
            this.Schedulerlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Schedulerlbl.Name = "Schedulerlbl";
            this.Schedulerlbl.Size = new System.Drawing.Size(58, 13);
            this.Schedulerlbl.TabIndex = 33;
            this.Schedulerlbl.Text = "Scheduler:";
            // 
            // Scheduler
            // 
            this.Scheduler.FormattingEnabled = true;
            this.Scheduler.Items.AddRange(new object[] {
            "DDIM",
            "Euler",
            "Euler A",
            "DPM++",
            "PNDM"});
            this.Scheduler.Location = new System.Drawing.Point(318, 480);
            this.Scheduler.Margin = new System.Windows.Forms.Padding(2);
            this.Scheduler.Name = "Scheduler";
            this.Scheduler.Size = new System.Drawing.Size(138, 21);
            this.Scheduler.TabIndex = 32;
            this.Scheduler.Text = "DDIM";
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(6, 513);
            this.GenerateButton.Margin = new System.Windows.Forms.Padding(2);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(544, 30);
            this.GenerateButton.TabIndex = 34;
            this.GenerateButton.Text = "Generate Video";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // PlayVideo
            // 
            this.PlayVideo.Enabled = false;
            this.PlayVideo.Location = new System.Drawing.Point(6, 548);
            this.PlayVideo.Name = "PlayVideo";
            this.PlayVideo.Size = new System.Drawing.Size(544, 24);
            this.PlayVideo.TabIndex = 35;
            this.PlayVideo.Text = "Play Video";
            this.PlayVideo.UseVisualStyleBackColor = true;
            this.PlayVideo.Click += new System.EventHandler(this.PlayVideo_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AddJob
            // 
            this.AddJob.Location = new System.Drawing.Point(556, 451);
            this.AddJob.Name = "AddJob";
            this.AddJob.Size = new System.Drawing.Size(101, 31);
            this.AddJob.TabIndex = 36;
            this.AddJob.Text = "Add As Job";
            this.AddJob.UseVisualStyleBackColor = true;
            this.AddJob.Click += new System.EventHandler(this.AddJob_Click);
            // 
            // JobList
            // 
            this.JobList.FormattingEnabled = true;
            this.JobList.Location = new System.Drawing.Point(556, 12);
            this.JobList.Name = "JobList";
            this.JobList.Size = new System.Drawing.Size(228, 433);
            this.JobList.TabIndex = 37;
            this.JobList.SelectedIndexChanged += new System.EventHandler(this.JobList_SelectedIndexChanged);
            // 
            // RemoveJob
            // 
            this.RemoveJob.Location = new System.Drawing.Point(683, 451);
            this.RemoveJob.Name = "RemoveJob";
            this.RemoveJob.Size = new System.Drawing.Size(101, 31);
            this.RemoveJob.TabIndex = 38;
            this.RemoveJob.Text = "Remove Job";
            this.RemoveJob.UseVisualStyleBackColor = true;
            this.RemoveJob.Click += new System.EventHandler(this.RemoveJob_Click);
            // 
            // RunJobs
            // 
            this.RunJobs.Location = new System.Drawing.Point(556, 493);
            this.RunJobs.Name = "RunJobs";
            this.RunJobs.Size = new System.Drawing.Size(228, 79);
            this.RunJobs.TabIndex = 39;
            this.RunJobs.Text = "Run Jobs";
            this.RunJobs.UseVisualStyleBackColor = true;
            this.RunJobs.Click += new System.EventHandler(this.RunJobs_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Batch
            // 
            this.Batch.Location = new System.Drawing.Point(465, 480);
            this.Batch.Name = "Batch";
            this.Batch.Size = new System.Drawing.Size(67, 21);
            this.Batch.TabIndex = 40;
            this.Batch.Text = "Batch";
            this.Batch.UseVisualStyleBackColor = true;
            this.Batch.Click += new System.EventHandler(this.Batch_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Updates);
            this.panel1.Controls.Add(this.Weighttxt);
            this.panel1.Controls.Add(this.Weightlbl);
            this.panel1.Controls.Add(this.LoraButton);
            this.panel1.Controls.Add(this.Loratxt);
            this.panel1.Controls.Add(this.Loralbl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.VideoRes);
            this.panel1.Controls.Add(this.Batch);
            this.panel1.Controls.Add(this.Schedulerlbl);
            this.panel1.Controls.Add(this.Scheduler);
            this.panel1.Controls.Add(this.Seed);
            this.panel1.Controls.Add(this.Seedlbl);
            this.panel1.Controls.Add(this.Cfg);
            this.panel1.Controls.Add(this.Cfglbl);
            this.panel1.Controls.Add(this.Steps);
            this.panel1.Controls.Add(this.Stepslbl);
            this.panel1.Controls.Add(this.Samplerlbl);
            this.panel1.Controls.Add(this.GPUOffload);
            this.panel1.Controls.Add(this.Offloadlbl);
            this.panel1.Controls.Add(this.LowMemoryMode);
            this.panel1.Controls.Add(this.Directionlbl);
            this.panel1.Controls.Add(this.Direction);
            this.panel1.Controls.Add(this.Motionlbl);
            this.panel1.Controls.Add(this.Motion);
            this.panel1.Controls.Add(this.Aspectlbl);
            this.panel1.Controls.Add(this.AspectRatio);
            this.panel1.Controls.Add(this.Resolutionlbl);
            this.panel1.Controls.Add(this.Resolution);
            this.panel1.Controls.Add(this.Frameslbl);
            this.panel1.Controls.Add(this.FrameRate);
            this.panel1.Controls.Add(this.SelectVideoOut);
            this.panel1.Controls.Add(this.VideoOut);
            this.panel1.Controls.Add(this.Videolbl);
            this.panel1.Controls.Add(this.SelectImg2);
            this.panel1.Controls.Add(this.SelectImg1);
            this.panel1.Controls.Add(this.Img2);
            this.panel1.Controls.Add(this.Img1);
            this.panel1.Controls.Add(this.Img2lbl);
            this.panel1.Controls.Add(this.Img1lbl);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 507);
            this.panel1.TabIndex = 41;
            // 
            // Weighttxt
            // 
            this.Weighttxt.Location = new System.Drawing.Point(330, 422);
            this.Weighttxt.Margin = new System.Windows.Forms.Padding(2);
            this.Weighttxt.MaxLength = 4;
            this.Weighttxt.Name = "Weighttxt";
            this.Weighttxt.Size = new System.Drawing.Size(43, 20);
            this.Weighttxt.TabIndex = 47;
            this.Weighttxt.Text = "1.0";
            this.Weighttxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cfg_KeyPress);
            // 
            // Weightlbl
            // 
            this.Weightlbl.AutoSize = true;
            this.Weightlbl.Location = new System.Drawing.Point(282, 426);
            this.Weightlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Weightlbl.Name = "Weightlbl";
            this.Weightlbl.Size = new System.Drawing.Size(44, 13);
            this.Weightlbl.TabIndex = 46;
            this.Weightlbl.Text = "Weight:";
            // 
            // LoraButton
            // 
            this.LoraButton.Location = new System.Drawing.Point(445, 422);
            this.LoraButton.Margin = new System.Windows.Forms.Padding(2);
            this.LoraButton.Name = "LoraButton";
            this.LoraButton.Size = new System.Drawing.Size(87, 22);
            this.LoraButton.TabIndex = 45;
            this.LoraButton.Text = "Select";
            this.LoraButton.UseVisualStyleBackColor = true;
            this.LoraButton.Click += new System.EventHandler(this.LoraButton_Click);
            // 
            // Loratxt
            // 
            this.Loratxt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Loratxt.Location = new System.Drawing.Point(318, 400);
            this.Loratxt.Margin = new System.Windows.Forms.Padding(2);
            this.Loratxt.Name = "Loratxt";
            this.Loratxt.ReadOnly = true;
            this.Loratxt.Size = new System.Drawing.Size(215, 20);
            this.Loratxt.TabIndex = 44;
            // 
            // Loralbl
            // 
            this.Loralbl.AutoSize = true;
            this.Loralbl.Location = new System.Drawing.Point(282, 403);
            this.Loralbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Loralbl.Name = "Loralbl";
            this.Loralbl.Size = new System.Drawing.Size(31, 13);
            this.Loralbl.TabIndex = 43;
            this.Loralbl.Text = "Lora:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(447, 357);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Mp4 Resolution:";
            // 
            // VideoRes
            // 
            this.VideoRes.FormattingEnabled = true;
            this.VideoRes.Items.AddRange(new object[] {
            "auto",
            "1920, 1080",
            "1366, 768",
            "1280, 720",
            "1024, 768",
            "768, 768",
            "720, 540",
            "854, 480",
            "640, 360 ",
            "512, 512"});
            this.VideoRes.Location = new System.Drawing.Point(445, 372);
            this.VideoRes.Margin = new System.Windows.Forms.Padding(2);
            this.VideoRes.MaxLength = 8;
            this.VideoRes.Name = "VideoRes";
            this.VideoRes.Size = new System.Drawing.Size(91, 21);
            this.VideoRes.TabIndex = 41;
            this.VideoRes.Text = "auto";
            this.VideoRes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VideoRes_KeyPress);
            // 
            // timer3
            // 
            this.timer3.Interval = 5000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // Updates
            // 
            this.Updates.AutoSize = true;
            this.Updates.Location = new System.Drawing.Point(382, 449);
            this.Updates.Name = "Updates";
            this.Updates.Size = new System.Drawing.Size(150, 17);
            this.Updates.TabIndex = 48;
            this.Updates.Text = "Check For Model Updates";
            this.Updates.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 577);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RunJobs);
            this.Controls.Add(this.RemoveJob);
            this.Controls.Add(this.JobList);
            this.Controls.Add(this.AddJob);
            this.Controls.Add(this.PlayVideo);
            this.Controls.Add(this.GenerateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Ruyi-GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label Img1lbl;
        private System.Windows.Forms.Label Img2lbl;
        private System.Windows.Forms.TextBox Img1;
        private System.Windows.Forms.TextBox Img2;
        private System.Windows.Forms.Button SelectImg1;
        private System.Windows.Forms.Button SelectImg2;
        private System.Windows.Forms.Button SelectVideoOut;
        private System.Windows.Forms.TextBox VideoOut;
        private System.Windows.Forms.Label Videolbl;
        private System.Windows.Forms.ComboBox FrameRate;
        private System.Windows.Forms.Label Frameslbl;
        private System.Windows.Forms.Label Resolutionlbl;
        private System.Windows.Forms.ComboBox Resolution;
        private System.Windows.Forms.Label Aspectlbl;
        private System.Windows.Forms.ComboBox AspectRatio;
        private System.Windows.Forms.Label Motionlbl;
        private System.Windows.Forms.ComboBox Motion;
        private System.Windows.Forms.Label Directionlbl;
        private System.Windows.Forms.ComboBox Direction;
        private System.Windows.Forms.CheckBox LowMemoryMode;
        private System.Windows.Forms.Label Offloadlbl;
        private System.Windows.Forms.TextBox GPUOffload;
        private System.Windows.Forms.Label Samplerlbl;
        private System.Windows.Forms.TextBox Steps;
        private System.Windows.Forms.Label Stepslbl;
        private System.Windows.Forms.TextBox Cfg;
        private System.Windows.Forms.Label Cfglbl;
        private System.Windows.Forms.TextBox Seed;
        private System.Windows.Forms.Label Seedlbl;
        private System.Windows.Forms.Label Schedulerlbl;
        private System.Windows.Forms.ComboBox Scheduler;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Button PlayVideo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button AddJob;
        private System.Windows.Forms.ListBox JobList;
        private System.Windows.Forms.Button RemoveJob;
        private System.Windows.Forms.Button RunJobs;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button Batch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox VideoRes;
        private System.Windows.Forms.TextBox Weighttxt;
        private System.Windows.Forms.Label Weightlbl;
        private System.Windows.Forms.Button LoraButton;
        private System.Windows.Forms.TextBox Loratxt;
        private System.Windows.Forms.Label Loralbl;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.CheckBox Updates;
    }
}

