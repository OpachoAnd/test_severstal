using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "notes\\";
            string[] files = Directory.GetFiles(path);
            toolStripComboBox1.Items.AddRange(files);

        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        async private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path_note = toolStripComboBox1.Text;
            string text = richTextBox1.Text;
            using (StreamWriter writer = new StreamWriter(path_note, false))
            {
                await writer.WriteLineAsync(text);
            }
        }

        async private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            string path_note = toolStripComboBox1.Text;
            using (StreamReader reader = new StreamReader(path_note))
            {
                string text = await reader.ReadToEndAsync();
                richTextBox1.Text = text;
            }
        }
    }
}
