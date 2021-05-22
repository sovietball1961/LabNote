using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabNote
{
    public partial class Form1 : Form
    {
        private static readonly string settingsDirectory = $"{Directory.GetCurrentDirectory()}\\data";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(settingsDirectory))
            {
                Directory.CreateDirectory(settingsDirectory);
                CreateSettingsFile();
            }
            ListSettingsFiles();
        }

        private void TextBoxes_LimitNumberOnly(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                textBox.BackColor = Color.Tomato;
                e.Handled = true;
                return;
            }
            else
            {
                textBox.BackColor = SystemColors.Window;
            }
        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string[] textArray = richTextBox1.Text.Split('\n');
                Console.WriteLine(Regex.IsMatch(textArray[textArray.Length - 1], @"^ "));
                if (Regex.IsMatch(textArray[textArray.Length - 1], @"^ ") == true)
                {
                    int i = 0;
                    char[] strings = textArray[textArray.Length - 1].ToCharArray();
                    while (strings[i] == ' ') { i++; }
                    richTextBox1.AppendText("\n" + new string(' ', i));
                    e.Handled = true;
                }
            }
            else { return; }
        }

        private void ToggleButtons_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            Font baseFont = richTextBox1.SelectionFont;
            float offsetSize = (float)1.5;
            int offsetLocation = 2;
            if (button.Checked == true)
            {
                switch (button.Name)
                {
                    case "toolStripButton2":
                        Font fnt0 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style | FontStyle.Italic);
                        richTextBox1.SelectionFont = fnt0;

                        baseFont.Dispose();
                        fnt0.Dispose();
                        break;
                    case "toolStripButton3":
                        Font fnt1 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style | FontStyle.Underline);
                        richTextBox1.SelectionFont = fnt1;

                        baseFont.Dispose();
                        fnt1.Dispose();
                        break;
                    case "toolStripButton4":
                        Font fnt2 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style | FontStyle.Bold);
                        richTextBox1.SelectionFont = fnt2;

                        baseFont.Dispose();
                        fnt2.Dispose();
                        break;
                    case "toolStripButton5":
                        Font fnt3 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style | FontStyle.Strikeout);
                        richTextBox1.SelectionFont = fnt3;

                        baseFont.Dispose();
                        fnt3.Dispose();
                        break;
                    case "toolStripButton6":
                        if (toolStripButton7.Checked == true)
                        {
                            toolStripButton7.Checked = false;
                            Font fnt40 = new Font(baseFont.FontFamily,
                                                 baseFont.Size + offsetSize,
                                                 baseFont.Style);
                            richTextBox1.SelectionFont = fnt40;
                            richTextBox1.SelectionCharOffset = 0;

                            baseFont.Dispose();
                            fnt40.Dispose();
                        }
                        Font fnt4 = new Font(baseFont.FontFamily,
                                             baseFont.Size - offsetSize,
                                             baseFont.Style);
                        richTextBox1.SelectionFont = fnt4;
                        richTextBox1.SelectionCharOffset = offsetLocation;

                        baseFont.Dispose();
                        fnt4.Dispose();
                        break;
                    case "toolStripButton7":
                        if (toolStripButton6.Checked == true)
                        {
                            toolStripButton6.Checked = false;
                            Font fnt50 = new Font(baseFont.FontFamily,
                                                  baseFont.Size + offsetSize,
                                                  baseFont.Style);
                            richTextBox1.SelectionFont = fnt50;
                            richTextBox1.SelectionCharOffset = 0;

                            baseFont.Dispose();
                            fnt50.Dispose();
                        };
                        Font fnt5 = new Font(baseFont.FontFamily,
                                             baseFont.Size - offsetSize,
                                             baseFont.Style);
                        richTextBox1.SelectionFont = fnt5;
                        richTextBox1.SelectionCharOffset = -offsetLocation;

                        baseFont.Dispose();
                        fnt5.Dispose();
                        break;
                }
            }
            else if (button.Checked == false)
            {
                switch (button.Name)
                {
                    case "toolStripButton2":
                        Font fnt0 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style & ~FontStyle.Italic);
                        richTextBox1.SelectionFont = fnt0;

                        baseFont.Dispose();
                        fnt0.Dispose();
                        break;
                    case "toolStripButton3":
                        Font fnt1 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style & ~FontStyle.Underline);
                        richTextBox1.SelectionFont = fnt1;

                        baseFont.Dispose();
                        fnt1.Dispose();
                        break;
                    case "toolStripButton4":
                        Font fnt2 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style & ~FontStyle.Bold);
                        richTextBox1.SelectionFont = fnt2;

                        baseFont.Dispose();
                        fnt2.Dispose();
                        break;
                    case "toolStripButton5":
                        Font fnt3 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style & ~FontStyle.Strikeout);
                        richTextBox1.SelectionFont = fnt3;

                        baseFont.Dispose();
                        fnt3.Dispose();
                        break;
                    case "toolStripButton6":
                        Font fnt4 = new Font(baseFont.FontFamily,
                                              baseFont.Size + offsetSize,
                                              baseFont.Style);
                        richTextBox1.SelectionFont = fnt4;
                        richTextBox1.SelectionCharOffset = 0;

                        baseFont.Dispose();
                        fnt4.Dispose();
                        break;
                    case "toolStripButton7":
                        Font fnt5 = new Font(baseFont.FontFamily,
                                             baseFont.Size + offsetSize,
                                             baseFont.Style);
                        richTextBox1.SelectionFont = fnt5;
                        richTextBox1.SelectionCharOffset = 0;

                        baseFont.Dispose();
                        fnt5.Dispose();
                        break;
                }
            }
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void ListSettingsFiles()
        {
            listBox1.Items.Clear();
            string[] jsonFiles = Directory.GetFiles(settingsDirectory, "*.json");
            foreach (string filePath in jsonFiles)
            {
                listBox1.Items.Add(Path.GetFileName(filePath));
            }
        }

        private void CreateSettingsFile()
        {
            var jsonObjects = new JsonElements
            {
                Title = "",
                Recorder = "",
                Date = DateTime.Now,
                Weather = "",
                Temperature = 0,
                Humidity = 0,
                Pressure = 0,
                RtfPath = "",
            };
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonData = JsonSerializer.Serialize(jsonObjects, jsonOptions);
            var writer = new StreamWriter($"{settingsDirectory}\\{DateTime.Now.ToString("yy-MM-dd-HH")}.json");
            writer.Write(jsonData);
            writer.Close();
        }

        private void WriteSettingsFile()
        {
            var jsonObjects = new JsonElements
            {
                Title = textBox1.Text,
                Recorder = comboBox1.Text,
                Date = dateTimePicker1.Value,
                Weather = textBox2.Text,
                Temperature = int.Parse(textBox3.Text),
                Humidity = int.Parse(textBox4.Text),
                Pressure = int.Parse(textBox5.Text),
            };
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonData = JsonSerializer.Serialize(jsonObjects, jsonOptions);
            var writer = new StreamWriter($"{settingsDirectory}\\{ProgramProperties.SelectingFileName}.json");
            writer.Write(jsonData);
            writer.Close();
        }

        private void LoadSettingsFile()
        {

        }
    }

    public class ProgramProperties
    {
        public static string SelectingFileName { get; set; }
    }

    public class JsonElements
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("recorder")]
        public string Recorder { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("weather")]
        public string Weather { get; set; }

        [JsonPropertyName("temperature")]
        public int Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("rtfPath")]
        public string RtfPath { get; set; }
    }
}
