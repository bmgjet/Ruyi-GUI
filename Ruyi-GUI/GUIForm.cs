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
using System.Linq.Expressions;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ruyi_GUI
{
    public partial class GUIForm : Form
    {
        public static List<string> LogMessages = new List<string>();
        public string[] PythonCode;
        public static Process process;
        public bool DoingJobs = false;
        public bool Not100 = true;
        public DateTime StartTime;
        public string GPUInfo;
        public int MaxVram = 0;

        public GUIForm()
        {
            InitializeComponent();
            if (File.Exists("Lang.cfg")) { LoadLangFile(this.Controls, File.ReadAllLines("Lang.cfg")); }
            else { SaveLangFile(); }
            if (File.Exists("Config.cfg")) { LoadSettings(File.ReadAllText("Config.cfg")); }
            else { SaveSettings(); }
            if (!File.Exists("environment.bat") || !Directory.Exists("system"))
            {
                MessageBox.Show(MissingSystemlbl.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!Directory.Exists("Ruyi-Models-main") || !File.Exists(Path.Combine("Ruyi-Models-main", "predict_i2v.py")))
            {
                if (MessageBox.Show(MissingRuyilbl.Text, Errorlbl.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://github.com/IamCreateAI/Ruyi-Models/archive/refs/heads/main.zip", "Ruyi-Models.zip");
                        Thread.Sleep(1000);
                        if (File.Exists("Ruyi-Models.zip"))
                        {
                            ZipFile.ExtractToDirectory("Ruyi-Models.zip", Path.Combine(AssemblyDirectory()));
                            Thread.Sleep(1000);
                            try { File.Delete("Ruyi-Models.zip"); } catch { }
                        }
                    }
                }
            }
            if (File.Exists(Path.Combine("Ruyi-Models-main", "predict_i2v.py")))
            {
                PythonCode = File.ReadAllLines(Path.Combine("Ruyi-Models-main", "predict_i2v.py"));
                if (File.Exists("nvidia-smi.exe") && NvidiaSMI() != null) { GPUPerf.Enabled = true; }
            }
            else
            {
                MessageBox.Show(MissingPredictlbl.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                GenerateButton.Enabled = false;
                Batch.Enabled = false;
            }
        }

        private void RetrieveControlTexts(Control.ControlCollection controls, Dictionary<string, string> controlTexts)
        {
            foreach (Control control in controls)
            {
                if (control is Label || control is Button) { controlTexts.Add(control.Name, control.Text); }
                if (control.HasChildren) { RetrieveControlTexts(control.Controls, controlTexts); }
            }
        }

        private void SaveLangFile()
        {
            Dictionary<string, string> controlTexts = new Dictionary<string, string>();
            RetrieveControlTexts(this.Controls, controlTexts);
            using (StreamWriter writer = new StreamWriter("Lang.cfg"))
            {
                foreach (var kvp in controlTexts) { writer.WriteLine($"{kvp.Key},{kvp.Value}"); }
            }
        }

        private void LoadLangFile(Control.ControlCollection controls, string[] strings)
        {
            foreach (Control control in controls)
            {
                if (control is Label || control is Button)
                {
                    string controlname = control.Name + ",";
                    foreach (string s in strings)
                    {
                        if (s.StartsWith(controlname))
                        {
                            control.Text = s.Replace(controlname, "");
                            break;
                        }
                    }
                }
                if (control.HasChildren) { LoadLangFile(control.Controls, strings); }
            }
            this.Update();
            toolTip1.SetToolTip(teacacheenable, teacacheenabletxt.Text);
            toolTip1.SetToolTip(teacachecpu, teacachecputxt.Text);
            toolTip1.SetToolTip(teacachethreshold, teacachethresholdtxt.Text);
            toolTip1.SetToolTip(teacachestart, teacachestarttxt.Text);
            toolTip1.SetToolTip(teacacheend, teacacheendtxt.Text);
            toolTip1.SetToolTip(enhancevideoenable, enhancevideoenabletxt.Text);
            toolTip1.SetToolTip(enhancevideoweight, enhancevideoweighttxt.Text);
            toolTip1.SetToolTip(enhancevideostart, enhancevideostarttxt.Text);
            toolTip1.SetToolTip(enhancevideoend, enhancevideoendtxt.Text);
            toolTip1.SetToolTip(Steps, Stepstxt.Text);
        }

        private string SettingsString() { return Img1.Text + "|" + Img2.Text + "|" + VideoOut.Text + "|" + FrameRate.Text + "|" + AspectRatio.Text + "|" + Resolution.Text + "|" + Direction.Text + "|" + Motion.Text + "|" + GPUOffload.Text + "|" + LowMemoryMode.Checked + "|" + Steps.Text + "|" + Cfg.Text + "|" + Seed.Text + "|" + Scheduler.Text + "|" + VideoRes.Text + "|" + Loratxt.Text + "|" + Weighttxt.Text + "|" + Updates.Checked + "|" + discordhook.Text + "|" + fp8_quant_mode.Text + "|" + fp8_data_type.Text + "|" + teacacheenable.Checked + "|" + teacachecpu.Checked + "|" + teacachethreshold.Text + "|" + teacachestart.Text + "|" + teacacheend.Text + "|" + enhancevideoenable.Checked + "|" + enhancevideoweight.Text + "|" + enhancevideostart.Text + "|" + enhancevideoend.Text; }


        private void SaveSettings() { File.WriteAllText("Config.cfg", SettingsString()); }

        private void LoadSettings(string Settings, bool ShowError = true)
        {
            try
            {
                string[] SSettings = Settings.Split('|');
                try { Img1.Text = SSettings[0]; } catch { }
                try { Img2.Text = SSettings[1]; } catch { }
                try { VideoOut.Text = SSettings[2]; } catch { }
                try { FrameRate.Text = SSettings[3]; } catch { }
                try { AspectRatio.Text = SSettings[4]; } catch { }
                try { Resolution.Text = SSettings[5]; } catch { }
                try { Direction.Text = SSettings[6]; } catch { }
                try { Motion.Text = SSettings[7]; } catch { }
                try { GPUOffload.Text = SSettings[8]; } catch { }
                try { LowMemoryMode.Checked = bool.Parse(SSettings[9]); } catch { }
                try { Steps.Text = SSettings[10]; } catch { }
                try { Cfg.Text = SSettings[11]; } catch { }
                try { Seed.Text = SSettings[12]; } catch { }
                try { Scheduler.Text = SSettings[13]; } catch { }
                try { VideoRes.Text = SSettings[14]; } catch { }
                try { Loratxt.Text = SSettings[15]; } catch { }
                try { Weighttxt.Text = SSettings[16]; } catch { }
                try { Updates.Checked = bool.Parse(SSettings[17]); } catch { }
                try { discordhook.Text = SSettings[18]; } catch { }
                try { fp8_quant_mode.Text = SSettings[19]; } catch { }
                try { fp8_data_type.Text = SSettings[20]; } catch { }
                try { teacacheenable.Checked = bool.Parse(SSettings[21]); } catch { }
                try { teacachecpu.Checked = bool.Parse(SSettings[22]); } catch { }
                try { teacachethreshold.Text = SSettings[23]; } catch { }
                try { teacachestart.Text = SSettings[24]; } catch { }
                try { teacacheend.Text = SSettings[25]; } catch { }
                try { enhancevideoenable.Checked = bool.Parse(SSettings[26]); } catch { }
                try { enhancevideoweight.Text = SSettings[27]; } catch { }
                try { enhancevideostart.Text = SSettings[28]; } catch { }
                try { enhancevideoend.Text = SSettings[29]; } catch { }
            }
            catch
            {
                if (ShowError)
                {
                    MessageBox.Show(FaultLoadinglbl.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            try
            {
                if (File.Exists(Img1.Text)) { pictureBox1.BackgroundImage = new Bitmap(Img1.Text); }
                else { pictureBox1.BackgroundImage = null; }
            }
            catch { }
            try
            {
                if (File.Exists(Img2.Text)) { pictureBox2.BackgroundImage = new Bitmap(Img2.Text); }
                else { pictureBox2.BackgroundImage = null; }
            }
            catch { }
        }

        private string[] NvidiaSMI()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c nvidia-smi --query-gpu=temperature.gpu,memory.used,utilization.gpu --format=csv,noheader,nounits",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                string[] parts = output.TrimEnd('\r', '\n').Replace(" ", "").Split(',');
                if (parts.Length == 3) { return parts; }
            }
            return null;
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

        public byte[] GetHash(string inputString) { using (HashAlgorithm algorithm = SHA256.Create()) { return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString)); } }

        public string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString)) { sb.Append(b.ToString("X2")); }
            return sb.ToString();
        }

        private void LogFiltered(string message)
        {
            if (string.IsNullOrEmpty(message)) { return; }
            if (message.StartsWith("Fetching "))
            {
                if (message.Contains("files: 100%|")) { message = "Loading Ai..."; }
                LogMessages.Add(message);
                AppendTextBox(message + GPUInfo);
                return;
            }
            if (message.Contains("%|"))
            {
                LogMessages.Add(message + GPUInfo);
                AppendTextBox(message + GPUInfo);
            }
            else { LogMessages.Add(message); }
            if (message.Contains("100%") && Not100) { Not100 = false; return; }
            if (message.Contains("100%"))
            {
                Not100 = true;
                CrashChecker.Enabled = false;
                AppendTextBox("Saving Mp4...");
                var Elapsed = DateTime.Now - StartTime;
                if (GPUPerf.Enabled && MaxVram > 0) { LogMessages.Add("Max Vram Usage: [" + MaxVram + "MB]"); }
                LogMessages.Add("Finished Generation @ " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "[" + Math.Round(Elapsed.TotalSeconds, 2) + "s]");
                if (DoingJobs)
                {
                    while (!File.Exists(VideoOut.Text)) { Thread.Sleep(1000); }
                    string Job = Path.Combine("Jobs", JobList.Items[0].ToString());
                    if (File.Exists(Job))
                    {
                        Thread.Sleep(500);
                        try { File.Delete(Job); } catch { }
                    }
                    JobList.Items.RemoveAt(0);
                    DoingJobs = false;
                    if (!string.IsNullOrEmpty(discordhook.Text) && discordhook.Text.StartsWith(@"https://discord.")) { DiscordPostFile(discordhook.Text, VideoOut.Text); }
                    return;
                }
                Task.Run(() =>
                {
                    while (!File.Exists(VideoOut.Text)) { Thread.Sleep(500); }
                    ControlsUpdate(true);
                    AppendTextBox("");
                });
            }
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
                if (line.StartsWith("start_image_path"))
                {
                    code += @"start_image_path = """ + Img1.Text.Replace(@"\", @"\\") + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("end_image_path"))
                {
                    code += "end_image_path = " + Image2.Replace(@"\", @"\\") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("output_video_path"))
                {
                    code += @"output_video_path = """ + VideoOut.Text.Replace(@"\", @"\\") + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("video_length"))
                {
                    code += @"video_length = " + FrameRate.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("base_resolution"))
                {
                    code += @"base_resolution = " + Resolution.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("video_size"))
                {
                    code += @"video_size = " + VideoRes.Text.Replace("auto", "None") + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("aspect_ratio"))
                {
                    code += @"aspect_ratio = """ + AspectRatio.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("motion"))
                {
                    code += @"motion = """ + Motion.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("camera_direction"))
                {
                    code += @"camera_direction = """ + Direction.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("steps"))
                {
                    code += @"steps = " + Steps.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("cfg"))
                {
                    code += @"cfg = " + Cfg.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("scheduler_name"))
                {
                    code += @"scheduler_name = """ + Scheduler.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("low_gpu_memory_mode"))
                {
                    code += @"low_gpu_memory_mode = " + LowMemoryMode.Checked.ToString() + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("gpu_offload_steps"))
                {
                    code += @"gpu_offload_steps = " + GPUOffload.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("seed"))
                {
                    code += @"seed = " + Seed.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("lora_path"))
                {
                    code += @"lora_path = " + Lora + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("lora_weight"))
                {
                    code += @"lora_weight = " + Weighttxt.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("auto_update"))
                {
                    code += @"auto_update = " + Updates.Checked.ToString() + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("fp8_quant_mode"))
                {
                    code += @"fp8_quant_mode = """ + fp8_quant_mode.Text + @""""+  System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("fp8_data_type"))
                {
                    code += @"fp8_data_type = """ + fp8_data_type.Text + @"""" + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("tea_cache_enabled"))
                {
                    code += @"tea_cache_enabled = " + teacacheenable.Checked + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("tea_cache_offload_cpu"))
                {
                    code += @"tea_cache_offload_cpu = " + teacachecpu.Checked + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("tea_cache_threshold"))
                {
                    code += @"tea_cache_threshold = " + teacachethreshold.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("tea_cache_skip_start_steps"))
                {
                    code += @"tea_cache_skip_start_steps = " + teacachestart.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("tea_cache_skip_end_steps"))
                {
                    code += @"tea_cache_skip_end_steps = " + teacacheend.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("enhance_a_video_enabled"))
                {
                    code += @"enhance_a_video_enabled = " + enhancevideoenable.Checked + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("enhance_a_video_weight"))
                {
                    code += @"enhance_a_video_weight = " + enhancevideoweight.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("enhance_a_video_skip_start_steps"))
                {
                    code += @"enhance_a_video_skip_start_steps = " + enhancevideostart.Text + System.Environment.NewLine;
                    continue;
                }
                if (line.StartsWith("enhance_a_video_skip_end_steps"))
                {
                    code += @"enhance_a_video_skip_end_steps = " + enhancevideoend.Text + System.Environment.NewLine;
                    continue;
                }
                code += line + System.Environment.NewLine;
            }
            return code;
        }

        private void ExecuteCommand()
        {
            ControlsUpdate(false);
            string CallCode = ParsePythonCode();
            if (string.IsNullOrEmpty(CallCode)) { return; }
            Restart:
            try
            {
                File.WriteAllText(Path.Combine("Ruyi-Models-main", "i2v.py"), CallCode);
                if (File.Exists(VideoOut.Text)) { Thread.Sleep(500); try { File.Move(VideoOut.Text, VideoOut.Text.Replace(".mp4", "-" + GetTimestamp() + ".mp4")); } catch { } }
            }
            catch
            {
                Thread.Sleep(1000);
                goto Restart;
            }
            StartTime = DateTime.Now;
            LogMessages.Add("Starting Generation: [" + FrameRate.Text + "-" + Resolution.Text + "-" + (LowMemoryMode.Checked ? "Lowmem" : "Normal") + "-" + GPUOffload.Text + "] " + Path.GetFileName(Img1.Text) + " @ " + StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Task.Run(() =>
            {
                MaxVram = 0;
                AppendTextBox(LoadingAilbl.Text);
                var processInfo = new ProcessStartInfo("cmd.exe", "/c call " + Path.Combine(AssemblyDirectory(), "environment.bat") + " && cd " + Path.Combine(AssemblyDirectory(), "Ruyi-Models-main") + " && python i2v.py");
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;
                process = Process.Start(processInfo);
                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => LogFiltered(e.Data);
                process.BeginOutputReadLine();
                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => LogFiltered(e.Data);
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();
            });
            CrashChecker.Enabled = true;
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

        private string GetCommandLineArgs(int processId)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {processId}"))
                {
                    foreach (ManagementObject obj in searcher.Get()) { return obj["CommandLine"]?.ToString(); }
                }
            }
            catch { }
            return "";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileWatch.Enabled = false;
            JobsTimer.Enabled = false;
            CrashChecker.Enabled = false;
            GPUPerf.Enabled = false;
            SaveSettings();
            if (process != null) { try { process.Kill(); } catch { } }
            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                if (process.ProcessName.Equals("python", StringComparison.OrdinalIgnoreCase))
                {
                    if (GetCommandLineArgs(process.Id).Contains("i2v.py"))
                    {
                        if (MessageBox.Show(@"""python i2v.py"" " + IsRunninglbl.Text, Errorlbl.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            LogMessages.Add("Terminating Python Process");
                            process.Kill();
                        }
                    }
                }
            }
            if (LogMessages.Count > 0)
            {
                try
                {
                    File.AppendAllLines("log.txt", LogMessages);
                    LogMessages.Clear();
                }
                catch { }
            }
        }

        private void PlayVideo_Click(object sender, EventArgs e)
        {
            if (File.Exists(VideoOut.Text)) { System.Diagnostics.Process.Start(VideoOut.Text); }
        }

        private void FileWatch_Tick(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(VideoOut.Text)) { PlayVideo.Enabled = true; }
                else { PlayVideo.Enabled = false; }
            }
            catch { }
            if (LogMessages.Count > 0)
            {
                try
                {
                    File.AppendAllLines("log.txt", LogMessages);
                    LogMessages.Clear();
                }
                catch { }
            }
        }

        private bool ValidateInOut()
        {
            if (string.IsNullOrEmpty(VideoOut.Text)) { MessageBox.Show(Error1.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Img1.Text)) { MessageBox.Show(Error2.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(FrameRate.Text)) { MessageBox.Show(Error3.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(AspectRatio.Text)) { MessageBox.Show(Error4.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Resolution.Text)) { MessageBox.Show(Error5.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Direction.Text)) { MessageBox.Show(Error6.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Motion.Text)) { MessageBox.Show(Error7.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(VideoRes.Text)) { MessageBox.Show(Error8.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(GPUOffload.Text)) { MessageBox.Show(Error9.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Steps.Text)) { MessageBox.Show(Error10.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Cfg.Text)) { MessageBox.Show(Error11.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Seed.Text)) { MessageBox.Show(Error12.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Scheduler.Text)) { MessageBox.Show(Error13.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(Weighttxt.Text)) { MessageBox.Show(Error14.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (!AspectRatio.Text.Contains(":")) { MessageBox.Show(Error15.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (!VideoRes.Text.Contains(", ") && !VideoRes.Text.Contains("None") && !VideoRes.Text.Contains("auto")) { MessageBox.Show(Error16.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (int.TryParse(FrameRate.Text, out int FPS)) { if (FPS < 24 || FPS > 120) { MessageBox.Show(Error17.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; } }
            if (int.TryParse(Resolution.Text, out int RES)) { if (RES < 384 || RES > 1024) { MessageBox.Show(Error18.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; } }
            if (!GPUOffload.Items.Contains(GPUOffload.Text)) { MessageBox.Show(Error19.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (!Direction.Items.Contains(Direction.Text)) { MessageBox.Show(Error20.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (!Motion.Items.Contains(Motion.Text)) { MessageBox.Show(Error21.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (!fp8_quant_mode.Items.Contains(fp8_quant_mode.Text)) { MessageBox.Show(Error23.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (!fp8_data_type.Items.Contains(fp8_data_type.Text)) { MessageBox.Show(Error24.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(teacachethreshold.Text)) { MessageBox.Show(Error25.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(teacachestart.Text)) { MessageBox.Show(Error26.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(teacacheend.Text)) { MessageBox.Show(Error27.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(enhancevideoweight.Text)) { MessageBox.Show(Error28.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(enhancevideostart.Text)) { MessageBox.Show(Error29.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            if (string.IsNullOrEmpty(enhancevideoend.Text)) { MessageBox.Show(Error30.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
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
            if (File.Exists(Job)) { try { File.Delete(Job); } catch { } }
            JobList.Items.RemoveAt(JobList.SelectedIndex);
        }

        private void RunJobs_Click(object sender, EventArgs e)
        {
            JobsTimer.Enabled = true;
            JobList.SelectedIndex = -1;
        }

        private void JobsTimer_Tick(object sender, EventArgs e)
        {
            if (DoingJobs) { return; }
            if (JobList.Items.Count > 0)
            {
                DoingJobs = true;
                UpdateJobList(true);
                this.Update();
                Thread.Sleep(100);
                ExecuteCommand();
            }
            else
            {
                AppendTextBox("");
                ControlsUpdate(true);
                JobsTimer.Enabled = false;
                DoingJobs = false;
                return;
            }
        }

        private void ControlsUpdate(bool set)
        {
            RunJobs.BeginInvoke(new Action(() => { RunJobs.Enabled = set; }));
            JobList.BeginInvoke(new Action(() => { JobList.Enabled = set; }));
            discordhook.BeginInvoke(new Action(() => { discordhook.Enabled = set; }));
            discordlbl.BeginInvoke(new Action(() => { discordlbl.Enabled = set; }));
            AddJob.BeginInvoke(new Action(() => { AddJob.Enabled = set; }));
            RemoveJob.BeginInvoke(new Action(() => { RemoveJob.Enabled = set; }));
            GenerateButton.BeginInvoke(new Action(() => { GenerateButton.Enabled = set; }));
            panel1.BeginInvoke(new Action(() => { panel1.Enabled = set; }));
            Updates.BeginInvoke(new Action(() => { Updates.Enabled = set; }));
        }

        private void UpdateJobList(bool Force = false)
        {
            if (JobList.SelectedIndex == -1 && !Force) { return; }
            int index = (JobList.SelectedIndex == -1) ? 0 : JobList.SelectedIndex;
            if (File.Exists(Path.Combine("Jobs", JobList.Items[index].ToString())))
            {
                LoadSettings(File.ReadAllText(Path.Combine("Jobs", JobList.Items[index].ToString())), false);
            }
            else
            {
                JobList.Items.RemoveAt(index);
            }
        }

        private void JobList_SelectedIndexChanged(object sender, EventArgs e) { UpdateJobList(); }

        private void Batch_Click(object sender, EventArgs e)
        {
            if (this.Width == 815) { this.Width = 572; JobList.Items.Clear(); }
            else
            {
                if (Directory.Exists("Jobs"))
                {
                    DirectoryInfo d = new DirectoryInfo("Jobs");
                    FileInfo[] Files = d.GetFiles("*");
                    foreach (FileInfo file in Files)
                    {
                        if (file.Name.Length == 64)
                        {
                            if (!JobList.Items.Contains(file.Name)) { JobList.Items.Add(file.Name); }
                        }
                    }
                }
                this.Width = 815;
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

        private void CrashChecker_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            bool isPythonRunning = processes.Any(p => p.ProcessName.Equals("python", StringComparison.OrdinalIgnoreCase));
            if (!isPythonRunning)
            {
                try
                {
                    AppendTextBox(Errorlbl.Text);
                    CrashChecker.Enabled = false;
                    ControlsUpdate(true);
                    JobsTimer.Enabled = false;
                    DoingJobs = false;
                    MessageBox.Show(Error22.Text, Errorlbl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch { }
            }
        }

        public void DiscordPostFile(string url, string FilePath)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    var file_bytes = System.IO.File.ReadAllBytes(FilePath);
                    string FileName = Path.GetFileName(FilePath);
                    form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "png-" + FileName, FileName);
                    httpClient.PostAsync(url, form).Wait();
                    LogMessages.Add("Posted Video To Discord Webhook");
                }
            }
            catch (Exception ex) { LogMessages.Add("[Discord] " + ex.ToString()); }
        }

        private void GPUPerf_Tick(object sender, EventArgs e)
        {
            string[] Output = NvidiaSMI();
            if (Output != null)
            {
                try
                {
                    GPUInfo = " [GPU:" + Output[2] + "% " + Output[0] + "C " + Output[1] + "MB]";
                    if (int.TryParse(Output[1], out int Vram)) { if (Vram > MaxVram) { MaxVram = Vram; } }
                    this.Text = this.Text.Split(new string[] { " [GPU:" }, StringSplitOptions.RemoveEmptyEntries)[0] + GPUInfo;
                }
                catch { }
            }
        }
    }
}