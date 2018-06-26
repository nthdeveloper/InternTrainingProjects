using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplyNamingConventions
{
    public partial class FrmEditPerson : Form
    {
        public string PersonName;
        public string PersonSurname;
        public DateTime PersonDateOfBirth;

        public FrmEditPerson()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Validate
            if(String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please add a valid name");
                return;
            }

            if (String.IsNullOrWhiteSpace(txtSurname.Text))
            {
                MessageBox.Show("Please add a valid surname");
                return;
            }

            //Get new values
            this.PersonName = txtName.Text;
            this.PersonSurname = txtSurname.Text;
            this.PersonDateOfBirth = dateDateOfBirth.Value.Date;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmEditPerson_Load(object sender, EventArgs e)
        {
            //Put current values
            txtName.Text = PersonName;
            txtSurname.Text = PersonSurname;
            dateDateOfBirth.Value = PersonDateOfBirth;
        }
    }
}
