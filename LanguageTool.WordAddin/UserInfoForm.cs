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

        private void FetchBTN_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(userIDTB.Text))
            {
                errorLabel.Visible = true;
                return;
            }
            errorLabel.Visible = false;
            Settings.Default.userID = userIDTB.Text;
            this.Close();
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            Settings.Default.userID = String.Empty;
            this.Close();
        }
    }
}
