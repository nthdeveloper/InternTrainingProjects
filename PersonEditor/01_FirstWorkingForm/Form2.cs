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
    public partial class Form2 : Form
    {
        public string PersonName;
        public string PersonSurname;
        public DateTime PersonDateOfBirth;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Validate
            if(String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please add a valid name");
                return;
            }

            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please add a valid surname");
                return;
            }

            //Get new values
            this.PersonName = textBox1.Text;
            this.PersonSurname = textBox2.Text;
            this.PersonDateOfBirth = dateTimePicker1.Value.Date;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Put current values
            textBox1.Text = PersonName;
            textBox2.Text = PersonSurname;
            dateTimePicker1.Value = PersonDateOfBirth;
        }
    }
}
