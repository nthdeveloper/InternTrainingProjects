using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseEntityClassAndBinding
{
    public partial class FrmEditPerson : Form
    {
        private Person m_Person;

        public FrmEditPerson(Person person)
        {
            InitializeComponent();

            m_Person = person;

            txtName.Text = m_Person.Name;
            txtSurname.Text = m_Person.Surname;
            dateDateOfBirth.Value = m_Person.DateOfBirth;
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
            m_Person.Name = txtName.Text;
            m_Person.Surname = txtSurname.Text;
            m_Person.DateOfBirth = dateDateOfBirth.Value.Date;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }        
    }
}
