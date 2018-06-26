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
    public partial class FrmMain : Form
    {
        BindingList<Person> m_PersonList;

        public FrmMain()
        {
            InitializeComponent();

            m_PersonList = new BindingList<Person>();
            dgvPersons.AutoGenerateColumns = false;
            dgvPersons.DataSource = m_PersonList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Person _newPerson = new Person();
            FrmEditPerson _frmEdit = new FrmEditPerson(_newPerson);

            if(_frmEdit.ShowDialog() == DialogResult.OK)
            { 
                _newPerson.GUID = System.Guid.NewGuid().ToString();
                m_PersonList.Add(_newPerson);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPersons.SelectedRows.Count > 0)
            {
                var _selectedPerson = dgvPersons.SelectedRows[0].DataBoundItem as Person;
                FrmEditPerson _frmEdit = new FrmEditPerson(_selectedPerson);
                _frmEdit.ShowDialog();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvPersons.SelectedRows.Count > 0)
            {
                var _selectedPerson = dgvPersons.SelectedRows[0].DataBoundItem as Person;
                var _confirmResult = MessageBox.Show($"Do you want to delete '{_selectedPerson.Name}'?", "Confirm delete", MessageBoxButtons.YesNo);
                if(_confirmResult == DialogResult.Yes)
                {
                    m_PersonList.Remove(_selectedPerson);
                }
            }
        }
    }
}
