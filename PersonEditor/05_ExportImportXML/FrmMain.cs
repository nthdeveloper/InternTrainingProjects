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
using System.Xml.Serialization;

namespace ExportImportXML
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

            cmbFileType.SelectedIndex = 0;//Select the first file type by default
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Person _newPerson = new Person();
            FrmEditPerson _frmEdit = new FrmEditPerson(_newPerson);

            if (_frmEdit.ShowDialog() == DialogResult.OK)
            {
                _newPerson.GUID = System.Guid.NewGuid().ToString();
                m_PersonList.Add(_newPerson);
            }

            _frmEdit.Dispose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPersons.SelectedRows.Count > 0)
            {
                var _selectedPerson = dgvPersons.SelectedRows[0].DataBoundItem as Person;
                FrmEditPerson _frmEdit = new FrmEditPerson(_selectedPerson);
                _frmEdit.ShowDialog();
                _frmEdit.Dispose();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvPersons.SelectedRows.Count > 0)
            {
                var _selectedPerson = dgvPersons.SelectedRows[0].DataBoundItem as Person;
                var _confirmResult = MessageBox.Show($"Do you want to delete '{_selectedPerson.Name}'?", "Confirm delete", MessageBoxButtons.YesNo);
                if (_confirmResult == DialogResult.Yes)
                {
                    m_PersonList.Remove(_selectedPerson);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (cmbFileType.SelectedIndex == 0)//CSV
            {
                SaveFileDialog _saveDialog = new SaveFileDialog();
                _saveDialog.RestoreDirectory = true;
                _saveDialog.Filter = "CVS (*.csv)|*.csv";
                _saveDialog.AddExtension = true;

                if (_saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter _writer = new StreamWriter(File.Create(_saveDialog.FileName)))
                        {
                            //Write column names as the first line
                            _writer.Write("GUID,Name,Surname,Date of Birth");

                            for (int i = 0; i < m_PersonList.Count; i++)
                            {
                                Person _person = m_PersonList[i];

                                _writer.WriteLine();
                                _writer.Write($"{_person.GUID},{_person.Name},{_person.Surname},{_person.DateOfBirth.ToString()}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured. Message:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Person list exported.");
                }

                _saveDialog.Dispose();
            }
            else if(cmbFileType.SelectedIndex == 1)//XML
            {
                SaveFileDialog _saveDialog = new SaveFileDialog();
                _saveDialog.RestoreDirectory = true;
                _saveDialog.Filter = "XML (*.xml)|*.xml";
                _saveDialog.AddExtension = true;

                if (_saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XmlSerializer _xmlSerializer = new XmlSerializer(typeof(BindingList<Person>));

                        using (StreamWriter _writer = new StreamWriter(File.Create(_saveDialog.FileName)))
                        {
                            _xmlSerializer.Serialize(_writer, m_PersonList);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured. Message:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Person list exported.");
                }

                _saveDialog.Dispose();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (cmbFileType.SelectedIndex == 0)//CSV
            {
                OpenFileDialog _openDialog = new OpenFileDialog();
                _openDialog.RestoreDirectory = true;
                _openDialog.Filter = "CVS (*.csv)|*.csv";
                _openDialog.CheckFileExists = true;
                _openDialog.Multiselect = false;

                if (_openDialog.ShowDialog() == DialogResult.OK)
                {
                    //Clear existing items
                    m_PersonList.Clear();
                    try
                    {
                        using (StreamReader _reader = new StreamReader(File.OpenRead(_openDialog.FileName)))
                        {
                            bool _isHeaderLine = true;
                            string _csvLine = null;
                            while (!_reader.EndOfStream)
                            {
                                _csvLine = _reader.ReadLine();
                                if (_isHeaderLine)//Skip first line, since it contains the column headers
                                {
                                    _isHeaderLine = false;
                                    continue;
                                }

                                string[] _csvValues = _csvLine.Split(',');

                                Person _person = new Person();
                                _person.GUID = _csvValues[0];
                                _person.Name = _csvValues[1];
                                _person.Surname = _csvValues[2];
                                _person.DateOfBirth = DateTime.Parse(_csvValues[3]);

                                m_PersonList.Add(_person);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured. Message:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (cmbFileType.SelectedIndex == 1)//XML
            {
                OpenFileDialog _openDialog = new OpenFileDialog();
                _openDialog.RestoreDirectory = true;
                _openDialog.Filter = "XML (*.xml)|*.xml";
                _openDialog.CheckFileExists = true;
                _openDialog.Multiselect = false;

                if (_openDialog.ShowDialog() == DialogResult.OK)
                {
                    //Clear existing items
                    m_PersonList.Clear();
                    try
                    {
                        XmlSerializer _xmlSerializer = new XmlSerializer(typeof(BindingList<Person>));

                        using (StreamReader _reader = new StreamReader(File.OpenRead(_openDialog.FileName)))
                        {
                            BindingList<Person> _loadedList = (BindingList<Person>)_xmlSerializer.Deserialize(_reader);
                            if(_loadedList != null)
                            {
                                for(int i=0;i<_loadedList.Count;i++)
                                {
                                    m_PersonList.Add(_loadedList[i]);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured. Message:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }      
    }
}
