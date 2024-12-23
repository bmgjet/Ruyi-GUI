using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
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
        public string PythonCode;
        public bool DoingJobs = false;
        public bool Not100 = true;

        public Form1()
        {
            InitializeComponent();
            PythonCode = Encoding.UTF8.GetString(Convert.FromBase64String(Decompress(Convert.FromBase64String("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"))));
            if (File.Exists("Config.cfg")) { LoadSettings(File.ReadAllText("Config.cfg")); }
            else
            {
                FrameRate.SelectedIndex = 0;
                AspectRatio.SelectedIndex = 0;
                Resolution.SelectedIndex = 0;
                Direction.SelectedIndex = 0;
                Motion.SelectedIndex = 0;
                Scheduler.SelectedIndex = 0;
                SaveSettings();
            }
        }

        private string SettingsString() { return Img1.Text + "|" + Img2.Text + "|" + VideoOut.Text + "|" + FrameRate.SelectedIndex + "|" + AspectRatio.SelectedIndex + "|" + Resolution.SelectedIndex + "|" + Direction.SelectedIndex + "|" + Motion.SelectedIndex + "|" + GPUOffload.Text + "|" + LowMemoryMode.Checked + "|" + Steps.Text + "|" + Cfg.Text + "|" + Seed.Text + "|" + Scheduler.SelectedIndex; }

        private void SaveSettings() { File.WriteAllText("Config.cfg", SettingsString()); }

        private void LoadSettings(string Settings)
        {
            string[] SSettings = Settings.Split('|');
            Img1.Text = SSettings[0];
            Img2.Text = SSettings[1];
            VideoOut.Text = SSettings[2];
            FrameRate.SelectedIndex = int.Parse(SSettings[3]);
            AspectRatio.SelectedIndex = int.Parse(SSettings[4]);
            Resolution.SelectedIndex = int.Parse(SSettings[5]);
            Direction.SelectedIndex = int.Parse(SSettings[6]);
            Motion.SelectedIndex = int.Parse(SSettings[7]);
            GPUOffload.Text = SSettings[8];
            LowMemoryMode.Checked = bool.Parse(SSettings[9]);
            Steps.Text = SSettings[10];
            Cfg.Text = SSettings[11];
            Seed.Text = SSettings[12];
            Scheduler.SelectedIndex = int.Parse(SSettings[13]);
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
            if (string.IsNullOrEmpty(message)) { return; }
            LogMessages.Add(message);
            if (message.Contains("%")) { AppendTextBox(message); }
            if (message.Contains("100%") && Not100) { Not100 = false; return; }
            if (message.Contains("100%"))
            {
                Not100 = true;
                AppendTextBox("Saving Mp4...");
                GenerateButton.BeginInvoke(new Action(() => { GenerateButton.Enabled = true; }));
                panel1.BeginInvoke(new Action(() => { panel1.Enabled = true; }));
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
            if (string.IsNullOrEmpty(VideoOut.Text)) { MessageBox.Show("You Must Select A Video Output", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (string.IsNullOrEmpty(Img1.Text)) { MessageBox.Show("You Must Select A Input Image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            string Image2 = string.IsNullOrEmpty(Img2.Text) ? "None" : @"""" + Img2.Text.Replace(@"\", @"\\") + @"""";
            string CallCode = PythonCode.Replace("{$IMAGE1}", Img1.Text.Replace(@"\", @"\\")).Replace("{$IMAGE2}", Image2).Replace("{$VIDEO}", VideoOut.Text.Replace(@"\", @"\\")).Replace("{$FRAMES}", FrameRate.Text).Replace("{$RES}", Resolution.Text).Replace("{$ASPECT}", AspectRatio.Text).Replace("{$MOTION}", Motion.Text).Replace("{$DIRECTION}", Direction.Text).Replace("{$STEPS}", Steps.Text).Replace("{$CFG}", Cfg.Text).Replace("{$SCHEDULER}", Scheduler.Text).Replace("{$LOWMEM}", LowMemoryMode.Checked.ToString()).Replace("{$OFFLOAD}", GPUOffload.Text).Replace("{$SEED}", Seed.Text);
            File.WriteAllText(Path.Combine("Ruyi-Models", "i2v.py"), CallCode);
            if (File.Exists(VideoOut.Text)) { File.Move(VideoOut.Text, VideoOut.Text.Replace(".mp4", "-" + GetTimestamp() + ".mp4")); }
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

        private void AddJob_Click(object sender, EventArgs e)
        {
            string Settings = SettingsString();
            string Job = GetHashString(Settings);
            if (JobList.Items.Contains(Job)) { return; }
            if (!Directory.Exists("Jobs")) { Directory.CreateDirectory("Jobs"); }
            File.WriteAllText(Path.Combine("Jobs", Job), Settings);
            JobList.Items.Add(Job);
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
            if (this.Width == 810) { this.Width = 575; JobList.Items.Clear(); }
            else
            {
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
    }
}