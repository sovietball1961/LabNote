using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.ValueTuple;

namespace LabNote
{
    public partial class Form1 : Form
    {
        private static readonly string settingsDirectory = $"{Directory.GetCurrentDirectory()}\\data";

        public Form1()
        {
            InitializeComponent();
            var settings = ReadFormSettingsFile();
            ProgramProperties.IndentWidth = settings.indentWidth;
            ProgramProperties.RichTextBoxFont = settings.textBoxFont;
            ProgramProperties.IsPictureFlex = settings.isPictureFlex;
            ProgramProperties.PictureSizeLimit = settings.PictureSizeLimit;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(settingsDirectory))
            {
                Directory.CreateDirectory(settingsDirectory);
            }
            ListSettingsFiles();
            ReadUsersListFile();
            ReadSettingsFile();
            richTextBox1.SelectionFont = ProgramProperties.RichTextBoxFont;
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            richTextBox1.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            textBox1.Select();
        }

        private void TextBoxes_NumberOnly(object sender, KeyPressEventArgs e)
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

        private void RichTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                Font baseFont = richTextBox1.SelectionFont;
                if ((baseFont.Style & FontStyle.Italic) != 0)
                {
                    toolStripButton2.Checked = true;
                }
                else
                {
                    toolStripButton2.Checked = false;
                }

                if ((baseFont.Style & FontStyle.Underline) != 0)
                {
                    toolStripButton3.Checked = true;
                }
                else
                {
                    toolStripButton3.Checked = false;
                }

                if ((baseFont.Style & FontStyle.Bold) != 0)
                {
                    toolStripButton4.Checked = true;
                }
                else
                {
                    toolStripButton4.Checked = false;
                }

                if ((baseFont.Style & FontStyle.Strikeout) != 0)
                {
                    toolStripButton5.Checked = true;
                }
                else
                {
                    toolStripButton5.Checked = false;
                }

                if (richTextBox1.SelectionCharOffset > 0)
                {
                    toolStripButton6.Checked = true;
                    toolStripButton7.Checked = false;
                }
                else if (richTextBox1.SelectionCharOffset < 0)
                {
                    toolStripButton6.Checked = false;
                    toolStripButton7.Checked = true;
                }
                else
                {
                    toolStripButton6.Checked = false;
                    toolStripButton7.Checked = false;
                }

                string[] textArray0 = richTextBox1.Text.Split('\n');
                if (Regex.IsMatch(textArray0[richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart)], "・") == true)
                {
                    toolStripButton8.Checked = true;
                }
                else
                {
                    toolStripButton8.Checked = false;
                }
            }
        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            #region
            // if (e.KeyCode == Keys.Enter)
            // {
            //     int i = 0;
            //     Font baseFont = richTextBox1.SelectionFont;
            //     string[] textArray = richTextBox1.Text.Split('\n');
            //     Font fnt0 = new Font(baseFont.FontFamily,
            //                          baseFont.Size,
            //                          baseFont.Style & ~(FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout));
            //     richTextBox1.SelectionFont = fnt0;
            // 
            //     if (Regex.IsMatch(textArray[textArray.Length - 1], @"^ ") == true)
            //     {
            //         char[] strings = textArray[textArray.Length - 1].ToCharArray();
            //         while (strings[i] == ' ')
            //         {
            //             i++;
            //             if (i >= strings.Length)
            //             {
            //                 break;
            //             }
            //         }
            //         richTextBox1.AppendText("\n" + new string(' ', i));
            //         if (toolStripButton8.Checked == true)
            //         {
            //             richTextBox1.Select(i, 0);
            //             richTextBox1.AppendText("・");
            //             return;
            //         }
            //         e.Handled = true;
            //     }
            //     if (toolStripButton8.Checked == true)
            //     {
            //         char[] strings = textArray[textArray.Length - 1].ToCharArray();
            //         while (strings[i] == ' ')
            //         {
            //             i++;
            //             if (i >= strings.Length)
            //             {
            //                 break;
            //             }
            //         }
            //         richTextBox1.Select(i, 0);
            //         richTextBox1.AppendText("\n・");
            //         e.Handled = true;
            //     }
            //     richTextBox1.SelectionFont = baseFont;
            //     e.Handled = false;
            // }
            // if (e.KeyCode == Keys.Space)
            // {
            //     if (richTextBox1.Focused == true)
            //     {
            //         int i = 0;
            //         Font baseFont = richTextBox1.SelectionFont;
            //         string[] textArray = richTextBox1.Text.Split('\n');
            //         char[] strings = textArray[textArray.Length - 1].ToCharArray();
            // 
            //         int rowPos = SendMessage(richTextBox1.Handle, EM_LINEFROMCHAR, -1, 0);
            //         int lineIndex = SendMessage(richTextBox1.Handle, EM_LINEINDEX, -1, 0);
            //         int colPos = richTextBox1.SelectionStart - lineIndex;
            //         do
            //         {
            //             if (i >= strings.Length)
            //             {
            //                 break;
            //             }
            //         }
            //         while (strings[i++] == ' ');
            // 
            //         if (string.IsNullOrEmpty(richTextBox1.Text) == true)
            //         {
            //             Font fnt0 = new Font(baseFont.FontFamily,
            //                                  baseFont.Size,
            //                                  baseFont.Style & ~(FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout));
            //             richTextBox1.SelectionFont = fnt0;
            //             return;
            //         }
            // 
            //         string str = textArray[rowPos].Substring(0, colPos);
            //         if (rowPos == textArray.Length || colPos == 0 || Regex.IsMatch(str, @"\S") == false)
            //         {
            //             Font fnt0 = new Font(baseFont.FontFamily,
            //                                  baseFont.Size,
            //                                  baseFont.Style & ~(FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout));
            //             richTextBox1.SelectionFont = fnt0;
            //             return;
            //         }
            //         else
            //         {
            //             richTextBox1.SelectionFont = ProgramProperties.PreviousFont;
            //         }
            //     }
            // }
            // else
            // {
            //     richTextBox1.SelectionFont = ProgramProperties.PreviousFont;
            //     return;
            // }
            #endregion
        }

        private void ToolStripToggles_Click(object sender, EventArgs e)
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

                        toolStripButton2.BackColor = SystemColors.Control;
                        baseFont.Dispose();
                        fnt0.Dispose();
                        break;
                    case "toolStripButton3":
                        Font fnt1 = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style & ~FontStyle.Underline);
                        richTextBox1.SelectionFont = fnt1;

                        toolStripButton2.BackColor = SystemColors.Control;
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

        private void ToolStripButtons_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            switch (button.Name)
            {
                case "toolStripButton9":
                    richTextBox1.SelectionBullet = true;
                    break;
                case "toolStripButton10":
                    var currentSize = richTextBox1.SelectionFont.Size;
                    currentSize += 2.0F;
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily,
                                                          currentSize,
                                                          richTextBox1.SelectionFont.Style);

                    break;
                case "toolStripButton11":
                    var currentSize1 = richTextBox1.SelectionFont.Size;
                    currentSize1 -= 1;
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily,
                                                          currentSize1,
                                                          richTextBox1.SelectionFont.Style);
                    break;
            }
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 1)
            {
                bool isFormCorrect = CheckFormCorrect();
                if (isFormCorrect == true)
                {
                    if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
                    {
                        WriteSettingsFile($"{settingsDirectory}\\{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}");
                        ListSettingsFiles();
                        listBox1.SelectedIndex = listBox1.Items.Count - 2;
                    }
                    else
                    {
                        WriteSettingsFile($"{settingsDirectory}\\{listBox1.SelectedItem}");
                    }
                }
                else { return; }
            }
            else { return; }
        }

        private void ToolStripButton8_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    richTextBox1.SelectionIndent += ProgramProperties.IndentWidth;
                    break;
                case MouseButtons.Right:
                    richTextBox1.SelectionIndent = 0;
                    break;
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count != 0)
            {
                if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
                {
                    TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
                    foreach(var textBox in textBoxes)
                    {
                        textBox.Text = "";
                    }
                    comboBox1.Text = "";
                    richTextBox1.Text = "";
                    dateTimePicker1.Enabled = true;
                    dateTimePicker1.Value = DateTime.Now;
                }
                else
                {
                    dateTimePicker1.Enabled = false;
                    ReadSettingsFile();
                }
            }
            else { return; }
        }

        private void SearchingNotesTextBoxes_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Items.Clear();
            string[] jsonFiles = Directory.GetFiles(settingsDirectory, $"*{textBox6.Text}*-*{textBox7.Text}*-*{textBox8.Text}*_*{textBox9.Text}*-*{textBox10.Text}*-*{textBox11.Text}*.json");
            foreach (string filePath in jsonFiles)
            {
                listBox1.Items.Add(Path.GetFileNameWithoutExtension(filePath));
            }
        }

        private void ColorResetObjects_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender.GetType().Equals(typeof(TextBox)))
            {
                var textBox = sender as TextBox;
                textBox.BackColor = SystemColors.Window;
            }
            if (sender.GetType().Equals(typeof(ComboBox)))
            {
                var comboBox = sender as ComboBox;
                comboBox.BackColor = SystemColors.Window;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.BackColor = SystemColors.Window;
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
                        return true;
                    case Keys.I:
                        toolStripButton2.PerformClick();
                        return true;
                    case Keys.U:
                        toolStripButton3.PerformClick();
                        return true;
                    case Keys.B:
                        toolStripButton4.PerformClick();
                        return true;
                    case Keys.T:
                        toolStripButton5.PerformClick();
                        return true;
                    case Keys.L:
                        var window = new LICENSE();
                        window.Show();
                        return true;
                    case Keys.Q:
                        richTextBox1.SelectionIndent = 0;
                        return true;
                    case Keys.V:
                        var iData = Clipboard.GetDataObject();
                        if (ProgramProperties.IsPictureFlex == true && iData.GetDataPresent(DataFormats.Bitmap))
                        {
                            Image image = Clipboard.GetImage();
                            Image newImage = CreateThumbnail(image);
                            Clipboard.Clear();
                            Clipboard.SetImage(newImage);
                            richTextBox1.Paste();
                            return true;
                        }
                        else
                        {
                            break;
                        }
                    case Keys.OemPeriod:
                        toolStripButton9.PerformClick();
                        return true;
                    case Keys.Tab:
                        richTextBox1.SelectionIndent += ProgramProperties.IndentWidth;
                        return true;
                    case Keys.PageUp:
                        toolStripButton6.PerformClick();
                        return true;
                    case Keys.PageDown:
                        toolStripButton7.PerformClick();
                        return true;
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
                    }
                    else { break; }
                    return true;
            } 
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool CheckFormCorrect()
        {
            int incorrectFlag = 0;
            List<string> incorrects = new List<string>();
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.BackColor = Color.Tomato;
                incorrects.Add("タイトル");
                incorrectFlag |= 1;
            }
            else
            {
                textBox1.BackColor = SystemColors.Window;
            }

            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                comboBox1.BackColor = Color.Tomato;
                incorrects.Add("記録者");
                incorrectFlag |= 2;
            }
            else
            {
                comboBox1.BackColor = SystemColors.Window;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.BackColor = Color.Tomato;
                incorrects.Add("天候");
                incorrectFlag |= 4;
            }
            else
            {
                textBox2.BackColor = SystemColors.Window;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.BackColor = Color.Tomato;
                incorrects.Add("気温");
                incorrectFlag |= 8;
            }
            else
            {
                textBox3.BackColor = SystemColors.Window;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.BackColor = Color.Tomato;
                incorrects.Add("湿度");
                incorrectFlag |= 16;
            }
            else
            {
                textBox4.BackColor = SystemColors.Window;
            }

            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.BackColor = Color.Tomato;
                incorrects.Add("気圧");
                incorrectFlag |= 32;
            }
            else
            {
                textBox5.BackColor = SystemColors.Window;
            }

            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                incorrects.Add("ノート");
                incorrectFlag |= 64;
            }

            if (incorrectFlag == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show($"{string.Join(", ", incorrects)}が未記入です", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

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
            var jsonObjects = new NoteJsonElements
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

        private void ReadSettingsFile()
        {
            if (listBox1.SelectedIndices.Count == 1)
            {
                var targetPath = $"{settingsDirectory}\\{listBox1.SelectedItem}";
                var reader = new StreamReader(targetPath + ".json");
                var jsonData = reader.ReadToEnd();
                var jsonObjects = JsonSerializer.Deserialize<NoteJsonElements>(jsonData);
                reader.Close();
                textBox1.Text = jsonObjects.Title;
                textBox2.Text = jsonObjects.Weather;
                textBox3.Text = jsonObjects.Temperature.ToString();
                textBox4.Text = jsonObjects.Humidity.ToString();
                textBox5.Text = jsonObjects.Pressure.ToString();
                comboBox1.Text = jsonObjects.Recorder;
                dateTimePicker1.Value = jsonObjects.Date;
                richTextBox1.LoadFile($"{targetPath}_rtf.rtf", RichTextBoxStreamType.RichText);
            }
            else { return; }
        }

        private void ReadUsersListFile()
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
                ReadUsersListFile();
            }
        }

        private (Font textBoxFont, int indentWidth, bool isPictureFlex, int PictureSizeLimit) ReadFormSettingsFile()
        {
            var targetFile = $"{Directory.GetCurrentDirectory()}\\program_settings.json";

            var baseFont = richTextBox1.SelectionFont;
            Font defFont = new Font(baseFont.FontFamily, baseFont.Size);
            if (File.Exists(targetFile))
            {
                var reader = new StreamReader(targetFile);
                var jsonData = reader.ReadToEnd();
                var jsonObjects = JsonSerializer.Deserialize<FormJsonElements>(jsonData);
                reader.Close();

                var newFont = new Font(jsonObjects.DefaultFontFamily, jsonObjects.DefaultFontSize);
                return (newFont, jsonObjects.IndentWidth, jsonObjects.IsPictureFlex, jsonObjects.PictureSizeLimit);
            }
            else
            {
                var newJsonObjects = new FormJsonElements
                {
                    IndentWidth = 24,
                    DefaultFontSize = 14,
                    DefaultFontFamily = "Meiryo UI",
                    IsPictureFlex = true,
                    PictureSizeLimit = 50,
                };
                var jsonOptions = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true,
                };
                var newJsonData = JsonSerializer.Serialize(newJsonObjects, jsonOptions);
                var writer = new StreamWriter(targetFile);
                writer.Write(newJsonData);
                writer.Close();
                ReadFormSettingsFile();
                return (defFont, 24, true, 50);
            }
        }

        Image CreateThumbnail(Image image)
        {
            Console.WriteLine($"{image.Width}, {image.Height}");
            int w = ProgramProperties.PictureSizeLimit;
            double ratio = 1f;
            if (image.Width >= image.Height)
            {
                ratio = (double)image.Height / (double)image.Width;
            }
            else
            {
                ratio = (double)image.Width / (double)image.Height;
            }
            int h = (int)Math.Round(w * ratio);

            Bitmap canvas = new Bitmap(w, h);

            Graphics g = Graphics.FromImage(canvas);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, w, h);

            float fw = (float)w / (float)image.Width;
            float fh = (float)h / (float)image.Height;

            float scale = Math.Min(fw, fh);
            fw = image.Width * scale;
            fh = image.Height * scale;

            g.DrawImage(image, (w - fw) / 2, (h - fh) / 2, fw, fh);
            g.Dispose();

            return canvas;
        } 
    }

    public class NoteJsonElements
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

    public class FormJsonElements
    {
        [JsonPropertyName("indentWidth")]
        public int IndentWidth { get; set; }

        [JsonPropertyName("defaultFontSize")]
        public int DefaultFontSize { get; set; }

        [JsonPropertyName("defaultFontFamily")]
        public string DefaultFontFamily { get; set; }

        [JsonPropertyName("isPictureFlex")]
        public bool IsPictureFlex { get; set; }

        [JsonPropertyName("pictureSizeLimit")]
        public int PictureSizeLimit { get; set; }
    }

    public static class ProgramProperties
    {
        public static int IndentWidth { get; set; }

        public static Font RichTextBoxFont { get; set; }

        public static bool IsPictureFlex { get; set; }

        public static int PictureSizeLimit { get; set; }
    }
}
