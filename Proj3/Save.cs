using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proj3
{
    public partial class Save : Form
    {
        private Form1 TestForm;
        public Save(Form1 form)
        {
            InitializeComponent();
            TestForm = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string imie = textBoxImie.Text;
            string nazwisko = textBoxNazwisko.Text;
            int wiek = int.Parse(textBoxWiek.Text);
            string stanowisko = comboBox1.Text;

            TestForm.AddRowToGrid(imie, nazwisko, wiek, stanowisko);

            this.Close();
        }

        private void Save_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
