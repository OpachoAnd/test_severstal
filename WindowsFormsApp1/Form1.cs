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
        string path;

        public Form1()
        {
            InitializeComponent();
            this.path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "notes\\";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.AddRange(Directory.GetFiles(this.path));
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = "";
            this.richTextBox1.Text = "";
        }

        async private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Сохранение файла

            string path_note = toolStripComboBox1.Text;
            string text = richTextBox1.Text;
            
            if (string.IsNullOrEmpty(path_note))
            {
                // Создание нового
                if (toolStripComboBox1.Items.Count == 0)
                {
                    // Если заметок ещё нет
                    path_note = path + "1.txt";
                }
                else
                {
                    // Если заметки уже были
                    int note_name = toolStripComboBox1.Items.Count + 1; 
                    path_note = path + note_name.ToString() + ".txt";
                    
                }
                using (StreamWriter writer = new StreamWriter(path_note, false))
                {
                    await writer.WriteLineAsync(text);
                }

                toolStripComboBox1.Items.Add(path_note);
                toolStripComboBox1.Text = path_note;
            }
            else
            {
                // Редактирование существующего файла
                using (StreamWriter writer = new StreamWriter(path_note, false))
                {
                    await writer.WriteLineAsync(text);
                }
            }
            
        }

        async private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            // Вывод файла на экран
            string path_note = toolStripComboBox1.Text;
            if (!string.IsNullOrEmpty(path_note))
            {
                using (StreamReader reader = new StreamReader(path_note))
                {
                    string text = await reader.ReadToEndAsync();
                    richTextBox1.Text = text;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path_note = toolStripComboBox1.Text;

            richTextBox1.Text = "";
            toolStripComboBox1.Items.Remove(path_note);
            toolStripComboBox1.Text = "";

            if (!string.IsNullOrEmpty(path_note))
            {
                FileInfo fileInf = new FileInfo(path_note);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }
            }
        }
    }
}
