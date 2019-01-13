using FilesOrganizer.Services;
using System;
using System.IO;
using System.Windows.Forms;

namespace FilesOrganizer
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        #region Form Events

        private void BtnOrganizer_Click(object sender, EventArgs e)
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
                foreach (var file in Directory.GetFiles(folderPath))
                {
                    var fileExtension = file.Substring(file.LastIndexOf(".") + 1);
                    if (Helper.IsValidExtension(fileExtension))
                    {
                        var modifiedDate = File.GetLastWriteTime(file).ToString("yyyy_MM_dd");
                        var destinationFolderPath = $"{folderPath}\\{modifiedDate}\\";
                        var destinationFilePath = destinationFolderPath + file.Substring(file.LastIndexOf("\\") + 1);

                        if (!Directory.Exists(destinationFolderPath))
                            Directory.CreateDirectory(destinationFolderPath);

                        if (!File.Exists(destinationFilePath))
                            File.Move(file, destinationFilePath);
                    }
                }

                ShowSuccess("Your files successfully moved to the folders :)");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return;
            }
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            HideLabels();
        }
        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtSourceFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void Label1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Private Methods
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
        
        #endregion

    }
}
