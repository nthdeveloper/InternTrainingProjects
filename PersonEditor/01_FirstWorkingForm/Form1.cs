using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.PersonDateOfBirth = DateTime.Today;

            if(f.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Add(f.PersonName, f.PersonSurname, f.PersonDateOfBirth);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                Form2 f = new Form2();
                f.PersonName = (string)row.Cells[0].Value;
                f.PersonSurname = (string)row.Cells[1].Value;
                f.PersonDateOfBirth = (DateTime)row.Cells[2].Value;

                if (f.ShowDialog() == DialogResult.OK)
                {
                    row.Cells[0].Value = f.PersonName;
                    row.Cells[1].Value = f.PersonSurname;
                    row.Cells[2].Value = f.PersonDateOfBirth;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var ds = MessageBox.Show("Do you want to delete the selected person?", "Confirm delete", MessageBoxButtons.YesNo);
                if(ds== DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                }
            }
        }
    }
}
