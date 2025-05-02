using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkFileRenamer_.NET_
{
    public partial class Form1: Form
    {
        private List<string> FilePathList = new List<string>();
        private List<string> NameList = new List<string>();
        private int StartCharIndex;
        private int EndCharIndex;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                AddNameToFilePathList();
                e.SuppressKeyPress = true;
            }
        }

        private void AddNameToFilePathList()
        {
            string name = textBox1.Text.Trim();

            if (!string.IsNullOrWhiteSpace(name) && !FilePathList.Contains(name))
            {
                FilePathList.Add(name);
                UpdateFilePathListBox();
                textBox1.Clear();
            }
        }

        private void UpdateFilePathListBox()
        {
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(FilePathList.ToArray());
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddStringToNameList();
                e.SuppressKeyPress = true;
            }
        }

        private void AddStringToNameList()
        {
            string name = textBox2.Text.Trim();

            if (!string.IsNullOrWhiteSpace(name) && !NameList.Contains(name))
            {
                NameList.Add(name);
                UpdateNameListBox();
                textBox2.Clear();
            }
        }

        private void UpdateNameListBox()
        {
            {
                listBox2.Items.Clear();
                listBox2.Items.AddRange(NameList.ToArray());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
