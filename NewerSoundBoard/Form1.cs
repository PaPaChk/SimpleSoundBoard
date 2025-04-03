using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using HtmlAgilityPack;
using System.Xml.Linq;
using System.Security.Policy;
using System.Text.Json;
using System.Diagnostics;



namespace Soundboard
{


    public partial class Form1 : Form
    {
        private Helper helper;
        
        public Form1()
        {
            InitializeComponent();
            helper = new Helper();
            helper.PlayStatusUpdate += (status) => SetPlayStatusLabel(status);
        }

        public void SetPlayStatusLabel(bool isPlaying)
        {
            if (isPlaying)
            {
                label6.Text = "🎵Playing🎵";
                label6.ForeColor = Color.Green;
            }
            else
            {
                label6.Text = "Not playing any sound yet";
                label6.ForeColor = Color.Black;
            }
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create enumerator
            var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active); // Only output devices


            foreach (var device in devices)
            {
                comboBox1.Items.Add(device); // Add the actual MMDevice object to the ComboBox
            }

            // Restore the last selected device
            var settings = SettingsManager.LoadSettings();
            string lastSelectedDevice = settings.LastSelectedDevice;
            if (!string.IsNullOrEmpty(lastSelectedDevice))
            {
                foreach (var item in comboBox1.Items)
                {
                    if (((MMDevice)item).FriendlyName == lastSelectedDevice)
                    {
                        comboBox1.SelectedItem = item; // Match and select the saved device
                        break;
                    }
                }
            }

            enumerator.Dispose(); // Clean up enumerator
            helper.PopulatePanel(flowLayoutPanel1, comboBox1, Environment.CurrentDirectory, label5);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var selectedDevice = (MMDevice)comboBox1.SelectedItem;
                var settings = SettingsManager.LoadSettings();
                settings.LastSelectedDevice = selectedDevice.FriendlyName;
                SettingsManager.SaveSettings(settings);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Stop the currently playing sound effect
            helper.StopSoundEffect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if the clipboard contains a URL starting with "https://www.myinstants.com/"
            string clipboardText = Clipboard.GetText();
            if (clipboardText.StartsWith("https://www.myinstants.com/"))
            {
                textBox1.Text = clipboardText; // Automatically paste it into textBox1
            }

            // Ensure there is a URL in textBox1 before proceeding
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please provide a valid URL in the text box or ensure your clipboard contains a valid URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Determine the name of the sound effect
            String SoundName = textBox2.Text;

            // Call ProcessSoundEffect with the URL from textBox1
            string url = textBox1.Text;
            string soundEffectName = helper.ProcessSoundEffect(url, SoundName); // Assuming ProcessSoundEffect is a synchronous method
            Console.WriteLine(soundEffectName);
            if (soundEffectName == "Error occurred!")
            {
                MessageBox.Show($"Invalid URL!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //// Clear the content of textBox1 after processing
            textBox1.Clear();
            textBox2.Clear();

            helper.PopulatePanel(flowLayoutPanel1, comboBox1, Environment.CurrentDirectory, label5);




            //// Use the determined name (e.g., log it, show a message, or save it)

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = "https://www.myinstants.com/en/index/hk/";

                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                helper.isChecked_playback = true;
            }
            else
            {
                helper.isChecked_playback = false;
            }
        }

    }

    public class Helper
    {
        private WasapiOut currentPlaybackSelected;
        private WasapiOut currentPlaybackDefault;
        //private AudioFileReader audioFile;
        private AudioFileReader audioFileDefault;
        private AudioFileReader audioFileSelected;
        private Dictionary<string, float> soundVolumes = new Dictionary<string, float>(); // Maps file paths to volume (0-1)
        private readonly string volumesFilePath;
        public bool isChecked_playback = false;
        public event Action<bool> PlayStatusUpdate;


        public Helper()
        {
            // Define the path to save volumes (e.g., AppData/Roaming/Soundboard/volumes.json)
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appDirectory = Path.Combine(appDataPath, "Soundboard");
            Directory.CreateDirectory(appDirectory); // Ensure directory exists
            volumesFilePath = Path.Combine(appDirectory, "volumes.json");

            LoadVolumes(); // Load saved volumes when Helper is initialized
        }
        private void LoadVolumes()
        {
            try
            {
                if (File.Exists(volumesFilePath))
                {
                    string json = File.ReadAllText(volumesFilePath);
                    soundVolumes = JsonSerializer.Deserialize<Dictionary<string, float>>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading volume settings: {ex.Message}");
            }
        }

        private void SaveVolumes()
        {
            try
            {
                string json = JsonSerializer.Serialize(soundVolumes);
                File.WriteAllText(volumesFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving volume settings: {ex.Message}");
            }
        }

        public void PopulatePanel(FlowLayoutPanel flowLayoutflowLayoutPanel1, ComboBox comboBox1, string folderPath, Label label5)
        {
            flowLayoutflowLayoutPanel1.Controls.Clear(); // Clear existing items
            string[] mp3Files = Directory.GetFiles(folderPath, "*.mp3"); // Find MP3 files in the folder

            foreach (string mp3FilePath in mp3Files)
            {
                string fileName = Path.GetFileNameWithoutExtension(mp3FilePath); // Get the name without extension

                // Create a container panel for the item
                Panel itemPanel = new Panel
                {
                    Width = flowLayoutflowLayoutPanel1.Width - 30, // Adjust for scrollbar
                    Height = 50, // Height of each item
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                // Create a label to display the name
                Label nameLabel = new Label
                {
                    Text = fileName,
                    Width = 250,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Left
                };

                int initialVolume = soundVolumes.ContainsKey(mp3FilePath) ? (int)(soundVolumes[mp3FilePath] * 100) : 50;
                // Create a trackbar for volume control
                TrackBar volumeTrackBar = new TrackBar
                {
                    Minimum = 0,
                    Maximum = 100,
                    Value = initialVolume, // Default volume
                    TickStyle = TickStyle.None,
                    Width = 150,
                    Anchor = AnchorStyles.Right // Prevent docking to ensure manual positioning

                };

                // Center the TrackBar vertically within the itemPanel
                int verticalCenter = (itemPanel.Height - volumeTrackBar.Height) / 2;
                volumeTrackBar.Location = new Point(nameLabel.Width + 10, verticalCenter);

                volumeTrackBar.Scroll += (s, e) =>
                {
                    int newVolume = volumeTrackBar.Value;
                    float volume = newVolume / 100f;

                    soundVolumes[mp3FilePath] = volume;
                    label5.Text = $"{newVolume}%";

                    // Update volume for both playback devices
                    if (audioFileSelected != null && audioFileSelected.FileName == mp3FilePath)
                    {
                        audioFileSelected.Volume = volume;
                    }
                    if (audioFileDefault != null && audioFileDefault.FileName == mp3FilePath)
                    {
                        audioFileDefault.Volume = volume;
                    }

                    SaveVolumes();
                    //Console.WriteLine($"Volume for {fileName}: {newVolume}");
                };

                // Create a play button with an icon
                Button playButton = new Button
                {
                    Width = 50,
                    Height = 50,
                    Text = "▶︎",
                    Dock = DockStyle.Right
                };
                playButton.Click += (s, e) =>
                {
                    Console.WriteLine($"Playing: {fileName}");
                    PlaySoundByPath(comboBox1, mp3FilePath);
                };

                // Create a delete button with an icon
                Button deleteButton = new Button
                {
                    Width = 50,
                    Height = 30,
                    Text = "🗑️",
                    Font = new Font("Segoe UI Emoji", 12, FontStyle.Regular),
                    Dock = DockStyle.Right
                };
                deleteButton.Click += (s, e) =>
                {
                    //if (MessageBox.Show($"Are you sure you want to delete {fileName}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    //{
                        File.Delete(mp3FilePath); // Delete the file
                        flowLayoutflowLayoutPanel1.Controls.Remove(itemPanel); // Remove the panel
                        Console.WriteLine($"Deleted: {fileName}");
                    //}
                };

                // Add controls to the item panel
                itemPanel.Controls.Add(deleteButton);
                itemPanel.Controls.Add(playButton);
                itemPanel.Controls.Add(volumeTrackBar);
                itemPanel.Controls.Add(nameLabel);

                // Add the item panel to flowLayoutPanel1
                flowLayoutflowLayoutPanel1.Controls.Add(itemPanel);
            }
        }
        public string ProcessSoundEffect(string inputUrl, string SoundName)
        {
            try
            {
                SoundName = SoundName.Trim();
                // Extract the name from the input URL
                Uri uri = new Uri(inputUrl);
                //string name = uri.Segments[3].Split('-')[0]; // Extract {name} from the input URL
                string relativePath = inputUrl.Substring(38);
                string name = relativePath.Split('/')[0];

                // Download the MP3
                string mp3Url;
                HtmlNode h1Node;


                // Access the input URL and read the h1 element
                using (HttpClient client = new HttpClient())
                {
                    string htmlContent = client.GetStringAsync(inputUrl).Result; // Use synchronous result
                    HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(htmlContent);

                    // Find the h1 element with the id "instant-page-title"
                    h1Node = htmlDocument.DocumentNode.SelectSingleNode("//h1[@id='instant-page-title']");

                    // Retrieve the download link with the attributes download and target="_blank"
                    var downloadNode = htmlDocument.DocumentNode.SelectSingleNode("//a[@download and @target='_blank']");
                    if (downloadNode != null)
                    {
                        // Construct the full MP3 URL
                        mp3Url = $"https://www.myinstants.com{downloadNode.GetAttributeValue("href", string.Empty)}";
                        //Console.WriteLine($"MP3 URL: {mp3Url}");
                    }
                    else
                    {
                        Console.WriteLine("No download link found.");
                        throw new InvalidOperationException("No download link found in the provided HTML.");
                    }
                }

                //Console.WriteLine(mp3Url);

                using (HttpClient client = new HttpClient())
                {
                    string mp3FilePath;
                    byte[] mp3Data = client.GetByteArrayAsync(mp3Url).Result; // Use synchronous result
                    if (SoundName == "")
                    {
                        mp3FilePath = Path.Combine(Environment.CurrentDirectory, $"{name}.mp3");

                    }
                    else
                    {
                        mp3FilePath = Path.Combine(Environment.CurrentDirectory, $"{SoundName}.mp3");
                    }
                    File.WriteAllBytes(mp3FilePath, mp3Data);
                    Console.WriteLine($"MP3 downloaded to: {mp3FilePath}");
                }

                if (SoundName == "")
                {
                    return h1Node?.InnerText.Trim();
                    //return SoundName;
                }
                else
                {
                    return SoundName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "Error occurred!";
            }
        }


        public void PlaySoundByPath(ComboBox comboBox1, string filePath)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an output device.");
                return;
            }

            var selectedDevice = (MMDevice)comboBox1.SelectedItem;

            // Get system's default output device
            var enumerator = new MMDeviceEnumerator();
            var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            try
            {
                StopSoundEffect(); // Stop any existing playback

                float volume = soundVolumes.TryGetValue(filePath, out float savedVolume)
                    ? savedVolume
                    : 0.5f;

                audioFileSelected = new AudioFileReader(filePath) { Volume = volume };
                currentPlaybackSelected = new WasapiOut(selectedDevice, AudioClientShareMode.Shared, false, 200);
                currentPlaybackSelected.Init(audioFileSelected);
                currentPlaybackSelected.Play();
                PlayStatusUpdate?.Invoke(true);


                if (isChecked_playback)
                {
                    audioFileDefault = new AudioFileReader(filePath) { Volume = volume };
                    currentPlaybackDefault = new WasapiOut(defaultDevice, AudioClientShareMode.Shared, false, 200);
                    currentPlaybackDefault.Init(audioFileDefault);
                    currentPlaybackDefault.Play();
                }

                // Handle playback stopped events
                currentPlaybackSelected.PlaybackStopped += (sender, e) => {
                    StopSoundEffect();
                    return;
                };
                if (isChecked_playback)
                {
                    currentPlaybackDefault.PlaybackStopped += (sender, e) => {
                        StopSoundEffect();
                        return;
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}");
                StopSoundEffect();
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public void StopSoundEffect()
        {
            // Stop and clean up both playback devices
            if (currentPlaybackSelected != null)
            {
                currentPlaybackSelected.Stop();
                currentPlaybackSelected.Dispose();
                currentPlaybackSelected = null;
            }
            if (currentPlaybackDefault != null)
            {
                currentPlaybackDefault.Stop();
                currentPlaybackDefault.Dispose();
                currentPlaybackDefault = null;
            }
            if (audioFileSelected != null)
            {
                audioFileSelected.Dispose();
                audioFileSelected = null;
            }
            if (audioFileDefault != null)
            {
                audioFileDefault.Dispose();
                audioFileDefault = null;
            }
            PlayStatusUpdate?.Invoke(false);
        }



    }

    public class AppSettings
    {
        public string LastSelectedDevice { get; set; }
    }

    public static class SettingsManager
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "YourAppName",
            "settings.json"
        );

        public static AppSettings LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    string json = File.ReadAllText(SettingsPath);
                    return JsonSerializer.Deserialize<AppSettings>(json);
                }
            }
            catch { /* Handle errors if needed */ }

            return new AppSettings(); // Return default settings if file doesn't exist
        }

        public static void SaveSettings(AppSettings settings)
        {
            try
            {
                string directory = Path.GetDirectoryName(SettingsPath);
                Directory.CreateDirectory(directory); // Ensure folder exists

                string json = JsonSerializer.Serialize(settings);
                File.WriteAllText(SettingsPath, json);
            }
            catch { /* Handle errors */ }
        }
    }
}
