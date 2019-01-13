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

namespace FilesOrganizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrganizer_Click(object sender, EventArgs e)
        {
            var folderPath = txtSourceFolder.Text;
            HideLabels();

            if (!Directory.Exists(folderPath))
            {
                ShowError("Image folder path is not valid!");
                return;
            }
            
            try
            {
                foreach (var file in Directory.GetFiles(folderPath, "*.JPG"))
                {
                    var modifiedDate = File.GetLastWriteTime(file).ToString("yyyy_MM_dd");
                    var destinationFolderPath = $"{folderPath}\\{modifiedDate}\\";
                    var destinationFilePath = destinationFolderPath + file.Substring(file.LastIndexOf("\\") + 1);

                    if (!Directory.Exists(destinationFolderPath))
                        Directory.CreateDirectory(destinationFolderPath);

                    if (!File.Exists(destinationFilePath))
                        File.Move(file, destinationFilePath);
                }

                ShowSuccess("Your files successfully moved to the folders :)");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return;
            }
        }

        private void ClearForm()
        {
            txtSourceFolder.Text = String.Empty;
        }

        private void HideLabels()
        {
            lblSuccess.Visible = false;
            lblError.Visible = false;
        }

        private void ShowError(string errorMsg)
        {
            lblError.Text = errorMsg;
            lblError.Visible = true;
            ClearForm();
        }

        private void ShowSuccess(string successMsg)
        {
            lblSuccess.Text = successMsg;
            lblSuccess.Visible = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            HideLabels();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtSourceFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
