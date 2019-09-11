using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            kisilistele();

        }
        SqlConnection con;
        SqlCommand komut;
        SqlDataAdapter da;
        string klasoryolu;
        void kisilistele()
        {
            con = new SqlConnection("Data Source=XIGMATEK\\SQLEXPRESS;Initial Catalog=resim;Persist Security Info=True;User ID=sa;Password=123456");
            con.Open();
            da = new SqlDataAdapter("Select * From gorsel",con);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.png| Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            textBox3.Text = dosyayolu;
            pictureBox1.ImageLocation = dosyayolu;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO gorsel(ad,soyad,resim) VALUES (@ad,@soyad,@resim)";
            komut = new SqlCommand(sorgu, con);
            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            komut.Parameters.AddWithValue("@soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@resim", textBox3.Text);
            con.Open();
            komut.ExecuteNonQuery();
            con.Close();
            kisilistele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "Update gorsel Set ad=@Ad,soyad=@Soyad,resim=@Resim Where id=@Id";
            komut = new SqlCommand(sorgu, con);
            komut.Parameters.AddWithValue("@Ad", textBox1.Text);
            komut.Parameters.AddWithValue("@Soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@Resim", textBox3.Text);
            komut.Parameters.AddWithValue("@Id", (dataGridView1.CurrentRow.Cells[0].Value));
            con.Open();
            komut.ExecuteNonQuery();
            con.Close();
            kisilistele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete From gorsel Where id=@Id";
            komut = new SqlCommand(sorgu, con);
            komut.Parameters.AddWithValue("@Id", (dataGridView1.CurrentRow.Cells[0].Value));
            con.Open();
            komut.ExecuteNonQuery();
            con.Close();
            kisilistele();
        }
    }
}

