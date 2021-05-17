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
            if (button.Checked == false)
            {
                switch (button.Name)
                {
                    case "toolStripButton2":
                        Font baseFont = richTextBox1.SelectionFont;
                        Font fnt = new Font(baseFont.FontFamily,
                                            baseFont.Size,
                                            baseFont.Style | FontStyle.Italic);
                        richTextBox1.SelectionFont = fnt;

                        baseFont.Dispose();
                        fnt.Dispose();
                        break;
                    case "toolStripButton3":
                        break;
                    case "toolStripButton4":
                        break;
                    case "toolStripButton5":
                        break;
                    case "toolStripButton6":
                        break;
                    case "toolStripButton7":
                        break;
                }
                button.Checked = true;
            }
        }
    }
}
