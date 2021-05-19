using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabNote
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                    richTextBox1.AppendText("\n" + new string(' ', 4));
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
    }

    public class JsonElement
    {

    }
}
