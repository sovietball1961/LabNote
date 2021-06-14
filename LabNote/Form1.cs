using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Text.Encodings.Web;
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
                // CreateSettingsFile();
            }
            ListSettingsFiles();
            LoadUsersListFile();
            LoadSettingsFile();
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
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
                int i = 0;
                string[] textArray = richTextBox1.Text.Split('\n');
                Console.WriteLine(Regex.IsMatch(textArray[textArray.Length - 1], @"^ "));
                if (Regex.IsMatch(textArray[textArray.Length - 1], @"^ ") == true)
                {
                    char[] strings = textArray[textArray.Length - 1].ToCharArray();
                    while (strings[i] == ' ')
                    {
                        i++;
                        if (i >= strings.Length)
                        {
                            break;
                        }
                    }
                    richTextBox1.AppendText("\n" + new string(' ', i));
                    e.Handled = true;
                    if (toolStripButton8.Checked == true)
                    {
                        richTextBox1.Select(i, 0);
                        richTextBox1.AppendText("・");
                        return;
                    }
                }
                if (toolStripButton8.Checked == true)
                {
                    char[] strings = textArray[textArray.Length - 1].ToCharArray();
                    while (strings[i] == ' ') { i++; }
                    richTextBox1.Select(i, 0);
                    richTextBox1.AppendText("\n・");
                    e.Handled = true;
                }
            }
            else { return; }
        }

        private void ToggleButtons_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            Font baseFont = richTextBox1.SelectionFont;
            int offsetSize = 2;
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
                                                  baseFont.Size,
                                                  baseFont.Style);
                            richTextBox1.SelectionFont = fnt40;
                            richTextBox1.SelectionCharOffset = offsetLocation;

                            baseFont.Dispose();
                            fnt40.Dispose();
                            break;
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
                                                  baseFont.Size,
                                                  baseFont.Style);
                            richTextBox1.SelectionFont = fnt50;
                            richTextBox1.SelectionCharOffset = -offsetLocation;

                            baseFont.Dispose();
                            fnt50.Dispose();
                            break;
                        }
                        Font fnt5 = new Font(baseFont.FontFamily,
                                             baseFont.Size - offsetSize,
                                             baseFont.Style);
                        richTextBox1.SelectionFont = fnt5;
                        richTextBox1.SelectionCharOffset = -offsetLocation;

                        baseFont.Dispose();
                        fnt5.Dispose();
                        break;
                    case "toolStripButton8":
                        int i = 0;
                        string[] textArray = richTextBox1.Text.Split('\n');
                        char[] strings = textArray[textArray.Length - 1].ToCharArray();
                        while (strings[i] == ' ')
                        {
                            i++;
                            if (i >= strings.Length)
                            {
                                break;
                            }
                        }
                        richTextBox1.Select(i - 1, 0);
                        richTextBox1.AppendText("・");
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
            if(listBox1.SelectedIndex == listBox1.Items.Count - 1)
            {
                WriteSettingsFile($"{settingsDirectory}\\{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}");
                ListSettingsFiles();
                listBox1.SelectedIndex = listBox1.Items.Count -  2;
            }
            if(listBox1.SelectedIndices.Count == 1)
            {
                WriteSettingsFile($"{settingsDirectory}\\{listBox1.SelectedItem}");
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != 0)
            {
                if (listBox1.SelectedIndex + 1 == listBox1.Items.Count)
                {
                    TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
                    foreach(var textBox in textBoxes)
                    {
                        textBox.Text = "";
                    }
                    comboBox1.Text = "";
                    richTextBox1.Text = "";
                }
                else
                {
                    LoadSettingsFile();
                }
            }
        }

        private void SearchingNote_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Items.Clear();
            string[] jsonFiles = Directory.GetFiles(settingsDirectory, $"*{textBox6.Text}*-*{textBox7.Text}*-*{textBox8.Text}*_*{textBox9.Text}*-*{textBox10.Text}*-*{textBox11.Text}*.json");
            foreach (string filePath in jsonFiles)
            {
                listBox1.Items.Add(Path.GetFileNameWithoutExtension(filePath));
            }
            //if ((textBox6.Text == "") && (textBox7.Text == "") && (textBox8.Text == "") && (textBox9.Text == "") && (textBox10.Text == "") && (textBox11.Text == ""))
            //{
            //    ListSettingsFiles();
            //}
        }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys code = keyData & Keys.KeyCode;
            Keys modi = keyData & Keys.Modifiers;

            if (modi == Keys.Control)
            {
                switch (code)
                {
                    case Keys.S:
                        toolStripButton1.PerformClick();
                        break;
                    case Keys.I:
                        toolStripButton2.Checked = toolStripButton2.Checked;
                        toolStripButton2.PerformClick();
                        break;
                    case Keys.U:
                        toolStripButton3.Checked = toolStripButton3.Checked;
                        toolStripButton3.PerformClick();
                        break;
                    case Keys.B:
                        toolStripButton4.Checked = toolStripButton4.Checked;
                        toolStripButton4.PerformClick();
                        break;
                    case Keys.T:
                        toolStripButton5.Checked = toolStripButton5.Checked;
                        toolStripButton5.PerformClick();
                        break;
                    case Keys.PageUp:
                        toolStripButton6.PerformClick();
                        break;
                    case Keys.PageDown:
                        toolStripButton7.PerformClick();
                        break;
                    case Keys.OemPeriod:
                        toolStripButton8.Checked = toolStripButton8.Checked;
                        toolStripButton8.PerformClick();
                        break;
                    default:
                        break;
                }
            }
            switch (code)
            {
                case Keys.Tab:
                    if (richTextBox1.Focused == true)
                    {
                        richTextBox1.AppendText(new string(' ', 4));
                        textBox5.Focus();
                    }
                    break;
            } 
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ListSettingsFiles()
        {
            listBox1.Items.Clear();
            string[] jsonFiles = Directory.GetFiles(settingsDirectory, "*.json");
            foreach (string filePath in jsonFiles)
            {
                listBox1.Items.Add(Path.GetFileNameWithoutExtension(filePath));
            }
            listBox1.Items.Add("    新規作成");
        }

        private void WriteSettingsFile(string targetPath)
        {
            var jsonObjects = new MainJsonElements
            {
                Title = textBox1.Text,
                Recorder = comboBox1.Text,
                Date = dateTimePicker1.Value,
                Weather = textBox2.Text,
                Temperature = int.Parse(textBox3.Text),
                Humidity = int.Parse(textBox4.Text),
                Pressure = int.Parse(textBox5.Text),
                RtfPath = $"{settingsDirectory}\\{listBox1.SelectedItem}_rtf.rtf",
            };
            var jsonOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            var jsonData = JsonSerializer.Serialize(jsonObjects, jsonOptions);
            var writer = new StreamWriter(targetPath + ".json");
            richTextBox1.SaveFile($"{targetPath}_rtf.rtf", RichTextBoxStreamType.RichText);
            writer.Write(jsonData);
            writer.Close();
        }

        private void LoadSettingsFile()
        {
            if (listBox1.SelectedIndices.Count == 1)
            {
                var targetPath = $"{settingsDirectory}\\{listBox1.SelectedItem}";
                var reader = new StreamReader(targetPath + ".json");
                var jsonData = reader.ReadToEnd();
                var jsonObjects = JsonSerializer.Deserialize<MainJsonElements>(jsonData);
                reader.Close();
                textBox1.Text = jsonObjects.Title;
                textBox2.Text = jsonObjects.Weather;
                textBox3.Text = jsonObjects.Temperature.ToString();
                textBox4.Text = jsonObjects.Humidity.ToString();
                textBox5.Text = jsonObjects.Pressure.ToString();
                comboBox1.Text = jsonObjects.Recorder;
                richTextBox1.LoadFile($"{targetPath}_rtf.rtf", RichTextBoxStreamType.RichText);
            }
            else { return; }
        }

        private void LoadUsersListFile()
        {
            var targetFile = $"{Directory.GetCurrentDirectory()}\\users.json";
            if (File.Exists(targetFile))
            {
                var reader = new StreamReader(targetFile);
                var jsonData = reader.ReadToEnd();
                var jsonObject = JsonSerializer.Deserialize<UsersJsonElement>(jsonData);
                reader.Close();
                comboBox1.Items.AddRange(jsonObject.Users.ToArray());
            }
            else
            {
                var jsonObject = new UsersJsonElement
                {
                    Users = new List<string>() { "プロイセン", "ぽるすか", "オーストリア帝国", "ロシア帝国" }
                };
                var jsonOptions = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true,
                };
                var jsonData = JsonSerializer.Serialize(jsonObject, jsonOptions);
                var writer = new StreamWriter(targetFile);
                writer.Write(jsonData);
                writer.Close();
                LoadUsersListFile();
            }
        }
    }

    public class MainJsonElements
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

    public class UsersJsonElement
    {
        [JsonPropertyName("users")]
        public List<string> Users { get; set; }
    }
}
