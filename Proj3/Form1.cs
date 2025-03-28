using System.Data;

namespace Proj3
{
    public partial class Form1 : Form
    {
        GridViewData Grid;
        private int ID = 1;
        public Form1()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Imie", "Imie");
            dataGridView1.Columns.Add("Nazwisko", "Nazwisko");
            dataGridView1.Columns.Add("Wiek", "Wiek");
            dataGridView1.Columns.Add("Stanowisko", "Stanowisko");
        }
        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void AddRowToGrid(string imie, string nazwisko, int wiek, string stanowisko)
        {
            dataGridView1.Rows.Add(ID , imie, nazwisko, wiek, stanowisko);
            ID++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save S1 = new Save(this);
            S1.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Select a Row to delete", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExportToCSV(DataGridView dataGrid, string filename)
        {
            string csvContent = "ID,IMIE,NAZWISKO,WIEK,STANOWISKO" + Environment.NewLine;

            foreach(DataGridViewRow row in dataGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    csvContent += string.Join(",", Array.ConvertAll(row.Cells.Cast<DataGridViewCell>().ToArray(), c => c.Value)) + Environment.NewLine;
                }
            }
            File.WriteAllText(filename, csvContent); 
        }

        private void LoadCSVToDataGridView(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("PLIK CSV NIE ISTNIEJE!","B��d",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string[] lines = File.ReadAllLines(filePath);

            if(lines.Length < 2)
            {
                MessageBox.Show("NOT ENOUGHT DATA", "B��D", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            string[] headers = lines[0].Split(',',StringSplitOptions.TrimEntries);
            foreach (string header in headers)
            {
                dataGridView1.Columns.Add(header.Trim(),header.Trim());
            }
            for(int i = 1; i<lines.Length; i++)
            {
                string[] values = lines[i].Split(",");

                if (values.Length == dataGridView1.Columns.Count)
                {
                    dataGridView1.Rows.Add(values);
                }
                else
                {
                    MessageBox.Show("B��d w linii.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            saveFileDialog1.Title = "Wybierz lokalizacj� zapisu Pliku CSV";
            saveFileDialog1.ShowDialog();

            if(saveFileDialog1.FileName != "")
            {
                ExportToCSV(dataGridView1, saveFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            openFileDialog1.Title = "Wybierz Plik CSV do Wczytania";
            openFileDialog1.ShowDialog();

            if(openFileDialog1.FileName != "")
            {
                LoadCSVToDataGridView(openFileDialog1.FileName);
            }
        }
    }
}
