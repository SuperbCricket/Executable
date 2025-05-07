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

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("The string cannot be empty or whitespace.");
                return;
            }

            if (name.Contains(" "))
            {
                var result = MessageBox.Show("The string contains spaces. Do you want to add it anyway?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (!NameList.Contains(name))
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

        private void buttonRun_Click(object sender, EventArgs e)
        {
            // Step 1: Validate inputs
            if (FilePathList.Count == 0)
            {
                MessageBox.Show("No file paths provided.");
                return;
            }

            if (NameList.Count == 0)
            {
                MessageBox.Show("No strings to remove provided.");
                return;
            }

            // Get numeric values
            int startCharsToRemove = (int)numericUpDown1.Value;
            int endCharsToRemove = (int)numericUpDown2.Value;

            // Step 2: Process each file path
            foreach (string directoryPath in FilePathList)
            {
                if (!System.IO.Directory.Exists(directoryPath))
                {
                    MessageBox.Show($"Directory does not exist: {directoryPath}");
                    continue;
                }

                // Get all files in the directory
                string[] files = System.IO.Directory.GetFiles(directoryPath);

                foreach (string filePath in files)
                {
                    string fileName = System.IO.Path.GetFileName(filePath);
                    string directory = System.IO.Path.GetDirectoryName(filePath);

                    string fileExtension = System.IO.Path.GetExtension(fileName);
                    string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);

                    // Step 3: Remove characters from the beginning and end
                    if (fileNameWithoutExtension.Length > startCharsToRemove + endCharsToRemove)
                    {
                        // Remove characters from the beginning
                        fileNameWithoutExtension = fileNameWithoutExtension.Substring(startCharsToRemove);

                        // Remove characters from the end
                        fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, fileNameWithoutExtension.Length - endCharsToRemove);
                    }
                    else
                    {
                        MessageBox.Show($"File name '{fileNameWithoutExtension}' is too short to remove {startCharsToRemove} characters from the beginning and {endCharsToRemove} characters from the end.");
                        continue;
                    }

                    // Recombine the file name with its extension
                    fileName = fileNameWithoutExtension + fileExtension;

                    // Step 4: Remove strings from NameList
                    foreach (string nameToRemove in NameList)
                    {
                        fileName = fileName.Replace(nameToRemove, string.Empty);
                    }

                    // Step 5: Rename the file
                    string newFilePath = System.IO.Path.Combine(directory, fileName);

                    try
                    {
                        System.IO.File.Move(filePath, newFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error renaming file {filePath}: {ex.Message}");
                    }
                }
            }

            MessageBox.Show("File renaming completed!");
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
