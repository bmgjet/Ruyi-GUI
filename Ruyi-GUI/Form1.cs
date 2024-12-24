/*▄▄▄    ███▄ ▄███▓  ▄████  ▄▄▄██▀▀▀▓█████▄▄▄█████▓
▓█████▄ ▓██▒▀█▀ ██▒ ██▒ ▀█▒   ▒██   ▓█   ▀▓  ██▒ ▓▒
▒██▒ ▄██▓██    ▓██░▒██░▄▄▄░   ░██   ▒███  ▒ ▓██░ ▒░
▒██░█▀  ▒██    ▒██ ░▓█  ██▓▓██▄██▓  ▒▓█  ▄░ ▓██▓ ░ 
░▓█  ▀█▓▒██▒   ░██▒░▒▓███▀▒ ▓███▒   ░▒████▒ ▒██▒ ░ 
░▒▓███▀▒░ ▒░   ░  ░ ░▒   ▒  ▒▓▒▒░   ░░ ▒░ ░ ▒ ░░   
▒░▒   ░ ░  ░      ░  ░   ░  ▒ ░▒░    ░ ░  ░   ░    
 ░    ░ ░      ░   ░ ░   ░  ░ ░ ░      ░    ░      
 ░             ░         ░  ░   ░      ░  ░*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ruyi_GUI
{
    public partial class Form1 : Form
    {
        public static List<string> LogMessages = new List<string>();
        public string[] PythonCode;
        public bool DoingJobs = false;
        public bool Not100 = true;

        public Form1()
        {
            InitializeComponent();
            if (File.Exists("Config.cfg")) { LoadSettings(File.ReadAllText("Config.cfg")); }
            else { SaveSettings(); }
            if(!File.Exists("environment.bat"))
            {
                MessageBox.Show("environment.bat file missing, Is this exe being ran from Forge Install folder?","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (!Directory.Exists("Ruyi-Models"))
            {
                MessageBox.Show("Missing Ruyi-Models Folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!File.Exists(Path.Combine("Ruyi-Models", "predict_i2v.py")))
            {
                MessageBox.Show("Missing predict_i2v.py", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PythonCode = File.ReadAllLines(Path.Combine("Ruyi-Models", "predict_i2v.py"));
        }

        private string SettingsString() { return Img1.Text + "|" + Img2.Text + "|" + VideoOut.Text + "|" + FrameRate.Text + "|" + AspectRatio.Text + "|" + Resolution.Text + "|" + Direction.Text + "|" + Motion.Text + "|" + GPUOffload.Text + "|" + LowMemoryMode.Checked + "|" + Steps.Text + "|" + Cfg.Text + "|" + Seed.Text + "|" + Scheduler.Text + "|" + VideoRes.Text + "|" + Loratxt.Text + "|" + Weighttxt.Text + "|" + Updates.Checked; }

        private void SaveSettings() { File.WriteAllText("Config.cfg", SettingsString()); }

        private void LoadSettings(string Settings)
        {
            try
            {
                string[] SSettings = Settings.Split('|');
                Img1.Text = SSettings[0];
                Img2.Text = SSettings[1];
                VideoOut.Text = SSettings[2];
                FrameRate.Text = SSettings[3];
                AspectRatio.Text = SSettings[4];
                Resolution.Text = SSettings[5];
                Direction.Text = SSettings[6];
                Motion.Text = SSettings[7];
                GPUOffload.Text = SSettings[8];
                LowMemoryMode.Checked = bool.Parse(SSettings[9]);
                Steps.Text = SSettings[10];
                Cfg.Text = SSettings[11];
                Seed.Text = SSettings[12];
                Scheduler.Text = SSettings[13];
                VideoRes.Text = SSettings[14];
                Loratxt.Text = SSettings[15]; 
                Weighttxt.Text = SSettings[16];
                Updates.Checked = bool.Parse(SSettings[17]);
            }
            catch
            {
                MessageBox.Show("Fault loading Config.cfg, Maybe Try Delete it so it can regenerate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (File.Exists(Img1.Text)) { pictureBox1.BackgroundImage = new Bitmap(Img1.Text); }
            if (File.Exists(Img2.Text)) { pictureBox2.BackgroundImage = new Bitmap(Img2.Text); }
            else { pictureBox2.BackgroundImage = null; }
        }

        private string GetTimestamp()
        {
            DateTime currentTime = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            return unixTime.ToString();
        }

        private void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            this.Text = "Ruyi-GUI " + value;
        }

        private string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        public byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create()) { return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString)); }
        }

        public string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString)) { sb.Append(b.ToString("X2")); }
            return sb.ToString();
        }

        private void LogFiltered(string message)
        {
            if (string.IsNullOrEmpty(message) || message.Contains("  attn_output = torch.nn.functional.scaled_dot_product_attention(")) { return; }
            LogMessages.Add(message);
            if (message.Contains("%") || message.StartsWith("Fetching ")) { AppendTextBox(message); }
            if (message.StartsWith("Fetching ")) { return; }
            if (message.Contains("100%") && Not100) { Not100 = false; return; }
            if (message.Contains("100%"))
            {
                Not100 = true;
                timer3.Enabled = false;
                AppendTextBox("Saving Mp4...");
                GenerateButton.BeginInvoke(new Action(() => { GenerateButton.Enabled = true; }));
                panel1.BeginInvoke(new Action(() => { panel1.Enabled = true; }));
                LogMessages.Add("Finished Generation @ " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (DoingJobs)
                {
                    while (!File.Exists(VideoOut.Text)) { Thread.Sleep(1000); }
                    DoingJobs = false;
                    string Job = Path.Combine("Jobs", JobList.Items[0].ToString());
                    if (File.Exists(Job)) { File.Delete(Job); }
                    JobList.Items.RemoveAt(0);
                    return;
                }
                Task.Run(() =>
                {
                    while (!File.Exists(VideoOut.Text)) { Thread.Sleep(1000); }
                    AppendTextBox("");
                });
            }
        }

        public static string Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return Convert.ToBase64String(output.ToArray());
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (ValidateInOut())
            {
                if (this.Width == 810) { this.Width = 575; JobList.Items.Clear(); }
                ExecuteCommand();
            }
        }

        private string ParsePythonCode()
        {
            string code = "";
            string Image2 = string.IsNullOrEmpty(Img2.Text) ? "None" : @"""" + Img2.Text.Replace(@"\", @"\\") + @"""";
            string Lora = string.IsNullOrEmpty(Loratxt.Text) ? "None" : @"""" + Loratxt.Text.Replace(@"\", @"\\") + @"""";
            foreach (string line in PythonCode)
            {
                if(line.StartsWith("start_image_path    = "))
                {
                    code += @"start_image_path    = """ + Img1.Text.Replace(@"\", @"\\") + @""""+ System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("end_image_path      = "))
                {
                    code += "end_image_path      = " + Image2.Replace(@"\", @"\\") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("output_video_path   = "))
                {
                    code += @"output_video_path   = """ + VideoOut.Text.Replace(@"\", @"\\") + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("video_length        = "))
                {
                    code += @"video_length        = " + FrameRate.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("base_resolution     = "))
                {
                    code += @"base_resolution     = " + Resolution.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("video_size          = "))
                {
                    code += @"video_size          = " + VideoRes.Text.Replace("auto","None") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("aspect_ratio        = "))
                {
                    code += @"aspect_ratio        = """ + AspectRatio.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("motion              = "))
                {
                    code += @"motion              = """ + Motion.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("camera_direction    = "))
                {
                    code += @"camera_direction    = """ + Direction.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("steps               = "))
                {
                    code += @"steps               = " + Steps.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("cfg                 = "))
                {
                    code += @"cfg                 = " + Cfg.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("scheduler_name      = "))
                {
                    code += @"scheduler_name      = """ + Scheduler.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("low_gpu_memory_mode = "))
                {
                    code += @"low_gpu_memory_mode = " + LowMemoryMode.Checked.ToString() + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("gpu_offload_steps   = "))
                {
                    code += @"gpu_offload_steps   = " + GPUOffload.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("seed                = "))
                {
                    code += @"seed                = " + Seed.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("lora_path           = "))
                {
                    code += @"lora_path           = " + Lora + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("lora_weight         = "))
                {
                    code += @"lora_weight         = " + Weighttxt.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("auto_update         = "))
                {
                    code += @"auto_update         = " + Updates.Checked.ToString() + System.Environment.NewLine;
                    continue;
                }
                code += line + System.Environment.NewLine;
            }
            return code;
        }

        private void ExecuteCommand()
        {
            string CallCode = ParsePythonCode(); 
            if (string.IsNullOrEmpty(CallCode)) { MessageBox.Show("Bad Python Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            File.WriteAllText(Path.Combine("Ruyi-Models", "i2v.py"), CallCode);
            if (File.Exists(VideoOut.Text)) { File.Move(VideoOut.Text, VideoOut.Text.Replace(".mp4", "-" + GetTimestamp() + ".mp4")); }
            LogMessages.Add("Starting Generation: ["+FrameRate.Text + "-"+ Resolution.Text +"] " + Path.GetFileName(Img1.Text) + " @ " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Task.Run(() =>
            {
                GenerateButton.BeginInvoke(new Action(() => { GenerateButton.Enabled = false; }));
                panel1.BeginInvoke(new Action(() => { panel1.Enabled = false; }));
                AppendTextBox("Loading AI Please Wait...");
                var processInfo = new ProcessStartInfo("cmd.exe", "/c call " + Path.Combine(AssemblyDirectory(), "environment.bat") + " && cd " + Path.Combine(AssemblyDirectory(), "Ruyi-Models") + " && python i2v.py %*");
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;
                var process = Process.Start(processInfo);
                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => LogFiltered(e.Data);
                process.BeginOutputReadLine();
                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => LogFiltered(e.Data);
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();
            });
            timer3.Enabled = true;
        }

        private string SelectImageButton()
        {
            string path = "";
            var dialog = new OpenFileDialog();
            dialog.Title = "Open Image";
            dialog.Filter = "Image Files (*.jpg;*.jpeg,*.png)|*.JPG;*.JPEG;*.PNG";
            if (dialog.ShowDialog() == DialogResult.OK) { path = dialog.FileName; }
            dialog.Dispose();
            return path;
        }

        private void SelectImg1_Click(object sender, EventArgs e)
        {
            Img1.Text = SelectImageButton();
            if (string.IsNullOrEmpty(Img1.Text))
            {
                pictureBox1.BackgroundImage = null;
                return;
            }
            pictureBox1.BackgroundImage = new Bitmap(Img1.Text);
        }

        private void SelectImg2_Click(object sender, EventArgs e)
        {
            Img2.Text = SelectImageButton();
            if (string.IsNullOrEmpty(Img2.Text))
            {
                pictureBox2.BackgroundImage = null;
                return;
            }
            pictureBox2.BackgroundImage = new Bitmap(Img2.Text);
        }

        private void SelectVideoOut_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Video Output|*.mp4";
            saveFileDialog1.Title = "Save an Video File";
            saveFileDialog1.ShowDialog();
            VideoOut.Text = saveFileDialog1.FileName;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) { SaveSettings(); }

        private void PlayVideo_Click(object sender, EventArgs e)
        {
            if (File.Exists(VideoOut.Text))
            {
                System.Diagnostics.Process.Start(VideoOut.Text);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (File.Exists(VideoOut.Text)) { PlayVideo.Enabled = true; }
            else { PlayVideo.Enabled = false; }
            if (LogMessages.Count > 0)
            {
                File.AppendAllLines("log.txt", LogMessages);
                LogMessages.Clear();
            }
        }

        private bool ValidateInOut()
        {
            if (string.IsNullOrEmpty(VideoOut.Text)) { MessageBox.Show("You Must Select A Video Output", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Img1.Text)) { MessageBox.Show("You Must Select A Input Image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(FrameRate.Text)) { MessageBox.Show("Frame Rate Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(AspectRatio.Text)) { MessageBox.Show("Aspect Ratio Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Resolution.Text)) { MessageBox.Show("Resolution Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Direction.Text)) { MessageBox.Show("Direction Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Motion.Text)) { MessageBox.Show("Motion Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(VideoRes.Text)) { MessageBox.Show("Mp4 Resolution Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(GPUOffload.Text)) { MessageBox.Show("GPU Offload Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Steps.Text)) { MessageBox.Show("Steps Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Cfg.Text)) { MessageBox.Show("Cfg Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Seed.Text)) { MessageBox.Show("Seed Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Scheduler.Text)) { MessageBox.Show("Scheduler Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Weighttxt.Text)) { MessageBox.Show("Lora Weight Can't Be Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if(!AspectRatio.Text.Contains(":")){ MessageBox.Show("Aspect Ratio Not Valid Format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (!VideoRes.Text.Contains(", ") && !VideoRes.Text.Contains("None") && !VideoRes.Text.Contains("auto")) { MessageBox.Show("Mp4 Resolution Not Valid Format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            return true;
        }

        private void AddJob_Click(object sender, EventArgs e)
        {
            if (ValidateInOut())
            {
                string Settings = SettingsString();
                string Job = GetHashString(Settings);
                if (JobList.Items.Contains(Job)) { return; }
                if (!Directory.Exists("Jobs")) { Directory.CreateDirectory("Jobs"); }
                File.WriteAllText(Path.Combine("Jobs", Job), Settings);
                JobList.Items.Add(Job);
            }
        }

        private void RemoveJob_Click(object sender, EventArgs e)
        {
            if (JobList.SelectedIndex == -1) return;
            string Job = Path.Combine("Jobs", JobList.Items[JobList.SelectedIndex].ToString());
            if (File.Exists(Job)) { File.Delete(Job); }
            JobList.Items.RemoveAt(JobList.SelectedIndex);
        }

        private void RunJobs_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            RunJobs.Enabled = false;
            JobList.Enabled = false;
            AddJob.Enabled = false;
            RemoveJob.Enabled = false;
            JobList.SelectedIndex = -1;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (DoingJobs) { return; }
            if (JobList.Items.Count > 0)
            {
                DoingJobs = true;
                UpdateJobList(true);
                this.Update();
                ExecuteCommand();
            }
            else
            {
                AppendTextBox("");
                RunJobs.Enabled = true;
                JobList.Enabled = true;
                AddJob.Enabled = true;
                RemoveJob.Enabled = true;
                timer2.Enabled = false;
                DoingJobs = false;
                return;
            }
        }

        private void UpdateJobList(bool Force = false)
        {
            if (JobList.SelectedIndex == -1 && !Force) { return; }
            int index = (JobList.SelectedIndex == -1) ? 0 : JobList.SelectedIndex;
            if (File.Exists(Path.Combine("Jobs", JobList.Items[index].ToString())))
            {
                LoadSettings(File.ReadAllText(Path.Combine("Jobs", JobList.Items[index].ToString())));
            }
        }

        private void JobList_SelectedIndexChanged(object sender, EventArgs e) { UpdateJobList(); }

        private void Batch_Click(object sender, EventArgs e)
        {
            if (this.Width == 810) { this.Width = 575; JobList.Items.Clear(); GenerateButton.Enabled = true; }
            else
            {
                GenerateButton.Enabled = false;
                if (Directory.Exists("Jobs"))
                {
                    DirectoryInfo d = new DirectoryInfo("Jobs");
                    FileInfo[] Files = d.GetFiles("*"); //Getting Text files
                    foreach (FileInfo file in Files)
                    {
                        if (file.Name.Length == 64)
                        {
                            if (!JobList.Items.Contains(file.Name))
                            {
                                JobList.Items.Add(file.Name);
                            }
                        }
                    }
                }
                this.Width = 810;
            }
        }

        private void AspectRatio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ':';
        }

        private void NumberOnly(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Cfg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void NumberAndAuto(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 'a' && e.KeyChar != 'u' && e.KeyChar != 't' && e.KeyChar != 'o';
        }

        private void VideoRes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != ' ' && e.KeyChar != 'N' && e.KeyChar != 'o' && e.KeyChar != 'n' && e.KeyChar != 'e' && e.KeyChar != 'a' && e.KeyChar != 'u' && e.KeyChar != 't'; 
        }

        private void LoraButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select Lora File";
            dialog.Filter = "safetensors (*.safetensors)|*.safetensors";
            if (dialog.ShowDialog() == DialogResult.OK) { Loratxt.Text = dialog.FileName; }
            dialog.Dispose();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            bool isPythonRunning = processes.Any(p => p.ProcessName.Equals("python", StringComparison.OrdinalIgnoreCase));
            if (!isPythonRunning)
            {
                timer3.Enabled = false;
                MessageBox.Show("python.exe is not running. Maybe it crashed, Please check the log.txt","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}