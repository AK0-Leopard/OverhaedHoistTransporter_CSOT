using System;
using System.Windows.Forms;

namespace Mirle.BigDataCollection
{
    public partial class InputPassword : Form
    {
        private DataCollectionService _dataCollectionService;
        private bool _isShow;

        public InputPassword(DataCollectionService dataCollectionService, bool isShow)
        {
            InitializeComponent();
            _dataCollectionService = dataCollectionService;
            _isShow = isShow;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "p@ssw0rd" && _isShow)
            {
                _dataCollectionService.Visible = true;
            }
            else if (txtPassword.Text == "22099478" && !_isShow)
            {
                _dataCollectionService.AppClose();
            }
            else
            {
                MessageBox.Show("Password is Error.");
            }
            Close();
        }
    }
}