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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmEditPerson _frmEdit = new FrmEditPerson();
            _frmEdit.PersonDateOfBirth = DateTime.Today;

            if(_frmEdit.ShowDialog() == DialogResult.OK)
            {
                dgvPersons.Rows.Add(_frmEdit.PersonName, _frmEdit.PersonSurname, _frmEdit.PersonDateOfBirth);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPersons.SelectedRows.Count > 0)
            {
                var _selectedRow = dgvPersons.SelectedRows[0];
                FrmEditPerson _frmEdit = new FrmEditPerson();
                _frmEdit.PersonName = (string)_selectedRow.Cells[0].Value;
                _frmEdit.PersonSurname = (string)_selectedRow.Cells[1].Value;
                _frmEdit.PersonDateOfBirth = (DateTime)_selectedRow.Cells[2].Value;

                if (_frmEdit.ShowDialog() == DialogResult.OK)
                {
                    _selectedRow.Cells[0].Value = _frmEdit.PersonName;
                    _selectedRow.Cells[1].Value = _frmEdit.PersonSurname;
                    _selectedRow.Cells[2].Value = _frmEdit.PersonDateOfBirth;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvPersons.SelectedRows.Count > 0)
            {
                var _confirmResult = MessageBox.Show("Do you want to delete the selected person?", "Confirm delete", MessageBoxButtons.YesNo);
                if(_confirmResult == DialogResult.Yes)
                {
                    dgvPersons.Rows.Remove(dgvPersons.SelectedRows[0]);
                }
            }
        }
    }
}
