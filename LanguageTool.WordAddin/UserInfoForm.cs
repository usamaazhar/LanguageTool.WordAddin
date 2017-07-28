﻿using LanguageTool.WordAddin.Business;
using LanguageTool.WordAddin.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageTool.WordAddin
{
    public partial class UserInfoForm : Form
    {
        public UserInfoForm()
        {
            InitializeComponent();
        }
        int tries = 0;
        private async void FetchBTN_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(tokenTB.Text))
            {
                errorLabel.Visible = true;
                return;
            }

            errorLabel.Visible = false;
            //Settings.Default.token = userIDTB.Text;
            if (Settings.Default.retriesLeft > 0)
            {
                if (await ServerUpdater.IsTokenValid(tokenTB.Text))
                {
                    Settings.Default.isTokenValid = true;
                    LocalStorageManager.UpdateUserToken(tokenTB.Text);
                    successLabel.Visible = true;
                    this.Close();
                    return;
                }
                errorLabel.Visible = true;
                errorLabel.Text = $"Invalid Token Entered, you have {Settings.Default.retriesLeft} left";
                Settings.Default.retriesLeft--;
            }
            else
            {
                this.Close();
            }
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
           // Settings.Default.token = String.Empty;
            this.Close();
        }

        private void tokenModal_MouseEnter(object sender, EventArgs e)
        {
            tokenModal.Font = new Font(tokenModal.Font.Name, tokenModal.Font.SizeInPoints,
                FontStyle.Underline);

        }

        private void tokenModal_Click(object sender, EventArgs e)
        {
            ModalDialogForm form = new ModalDialogForm();
            form.ShowDialog();
        }

        private void tokenModal_MouseLeave(object sender, EventArgs e)
        {
            tokenModal.Font = new Font(tokenModal.Font.Name, tokenModal.Font.SizeInPoints,
                FontStyle.Regular);
        }
    }
}
