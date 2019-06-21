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
using FastReport;
using System.IO;
using System.IO.MemoryMappedFiles;


using System.Data.OleDb;
using System.Data.Common;

using System.Drawing.Imaging;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace OnlineAlisverisSistemi
{
    public partial class UrunIslemler : Form
    {
        public UrunIslemler()
        {
            InitializeComponent();
        }
        public SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=onlinemagaza;Integrated Security=True;Persist Security Info=True;User ID=hasan;Encrypt=True;TrustServerCertificate=True;Context Connection=False");
        public SqlDataAdapter da;
        public System.Data.DataTable dt = new System.Data.DataTable(); public int i, id,urunid,kullaniciid;
        public List<string> yetkiler = new List<string>();
		public DataSet dataset = new DataSet();
		public AnaSayfa ana;
        //-------------------------------GRİD DOLDUR
        public void doldur(string kosul="")
        {
            SqlCommand kmt;
            try
            {
                conn.Close();
                dt.Clear();
                conn.Open();
                if(kosul=="")
                {
                     kmt = new SqlCommand("Select u.urunID,u.urunAdi,u.urunFiyat,s.urunAdet from Urun u join Stok s on u.urunID=s.stokID order by s.urunAdet asc", conn);
                }
                else
                {
                     kmt = new SqlCommand("Select u.urunID,u.urunAdi,u.urunFiyat,s.urunAdet from Urun u join Stok s on u.urunID=s.stokID where u.urunAdi='"+kosul+"' order by s.urunAdet asc", conn);
                }
                SqlDataAdapter da = new SqlDataAdapter(kmt);
				da.Fill(dataset, "dataset");
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
                dataGridView1.Columns[0].Visible = false;   // İD KISMINI GORUNMEZ YAAP

                //YAZI RENGİ BEYAZ
                System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                dataGridViewCellStyle1.ForeColor = Color.Black;
                dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------GRİD DOLDUR
	    //-------------------------------ÜRÜN ADLARINI LİSTELE
        public void Adlistele()
        {
            try
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                conn.Close();
                comboBox1.Items.Clear();
                conn.Open();
                SqlCommand kmt = new SqlCommand("Select distinct(urunAdi) from Urun order by urunAdi asc ", conn);
                SqlDataReader dr = kmt.ExecuteReader();
                while (dr.Read())
                {
                    collection.Add(dr["urunAdi"].ToString());
                    comboBox1.Items.Add(dr["urunAdi"]);
                }
                conn.Close();
                comboBox1.AutoCompleteCustomSource = collection;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception)
            {

            }
        }
        //-------------------------------ÜRÜN ADLARINI LİSTELE
        private void button8_Click(object sender, EventArgs e)
        {
            doldur();
        }
		int urnid = 0;
        //-------------------------------ÜRÜNÜ YAZ GÜNCELLE SİL
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                label1.Text = row.Cells[1].Value.ToString();
                textBox1.Text= row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();

                urunid = Convert.ToInt16(row.Cells[0].Value.ToString());
				urnid = urunid;

				break;
            }
        }
		// TRİGGER EKLENDİ. <ONUR>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Ürünü Silmek İstediğinize Eminmisiniz?", "Servis Sil?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand kmt2 = new SqlCommand("delete from Urun where urunID=" + urunid + "", conn);
                    kmt2.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Ürün Başarıyla Silindi");
                    doldur();
                }
                else
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            doldur(comboBox1.Text);
        }
		private void groupBox3_Enter(object sender, EventArgs e)
		{
		}
		private void groupBox4_Enter(object sender, EventArgs e)
		{
		}
		OpenFileDialog file = new OpenFileDialog();
		private void button1_Click(object sender, EventArgs e)
		{
			
			file.ShowDialog();
			pictureBox1.Image = Image.FromFile(file.FileName);
		
		}
		// URUNEKLE PROCEDURE EKLENDİ <ONUR>
		private void button3_Click(object sender, EventArgs e)
		{
            try
            {
                string xsltFile = System.Windows.Forms.Application.StartupPath + @"\\" + textBox5.Text + ".jpg";
                pictureBox1.Image.Save(xsltFile, ImageFormat.Jpeg);
                conn.Open();
                string veri = "exec urunekle '" + textBox5.Text + "','" + textBox3.Text + "','" + textBox2.Text + "'";
                SqlCommand kmt2 = new SqlCommand(veri, conn);
                kmt2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Ürün başarıyla eklendi.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); 
            }	
		}
		private void button6_Click(object sender, EventArgs e) // FASTREPORT OTOMATİK VERİ KISMI <ONUR>
		{
			
		}
		private void button7_Click(object sender, EventArgs e)
		{
            
			OpenFileDialog dosya2 = new OpenFileDialog();
			dosya2.Filter = "Excell Dosyası | *.xlsx; | Tüm Dosyalar | *.* ";
			dosya2.Title = "Toplu Stok İşlemi       - Excell Dosyası Seçin!";
			dosya2.ShowDialog();
			string Dosyayolu = dosya2.FileName;
			excell excel = new excell(@"" + Dosyayolu + "", 1);
			for (int i = 1; i < 100; i++)
			{
				try
				{
					string urunadi = excel.ReadCell(i, 0);
					int adet = Convert.ToInt16(excel.ReadCell(i, 1));
					conn.Open();
					SqlCommand kmt2 = new SqlCommand("update Stok set urunAdet=urunAdet+" + adet + " where stokID=(Select urunID from Urun where urunAdi='" + urunadi + "')", conn);
					kmt2.ExecuteNonQuery();
					conn.Close();
				}
				catch (Exception)
				{
					button8.PerformClick();
					break;
				}
			}
			button8.PerformClick();
			excel.kapat();
		}
		public void export_deneme()
		{
			Microsoft.Office.Interop.Excel.Application exel1 = new Microsoft.Office.Interop.Excel.Application();

			exel1.Visible = true;
			object missing = Type.Missing;
			Workbook wb = exel1.Workbooks.Add(missing);
			Worksheet ws = (Worksheet)wb.Sheets[1];

			int baslamakolonu = 1;
			int baslamasatiri = 1;
			for (int j = 0; j < dataGridView1.Columns.Count; j++)
			{
				Range myRange = (Range)ws.Cells[baslamasatiri, baslamakolonu + j];
				myRange.Value2 = dataGridView1.Columns[j].HeaderText;
			}
			baslamasatiri++;
			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				for (int j = 0; j < dataGridView1.Columns.Count; j++)
				{
					Range myRange = (Range)ws.Cells[baslamasatiri + i, baslamakolonu + j];
					myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
					myRange.Select();
				}
			}
		}
		//export deneme SUCCESFUL...
		private void button9_Click(object sender, EventArgs e)
		{
			export_deneme();
			//exelexport("denemeonurexport.csv");
		}
		
		private void button4_Click(object sender, EventArgs e)
        {
            try
            {
				//urnid
                conn.Open();
				string veri = "exec urunislemleri '"+urnid+"','"+textBox1.Text+ "','" + textBox4.Text + "'";
                SqlCommand kmt2 = new SqlCommand(veri, conn);
                kmt2.ExecuteNonQuery();
                conn.Close();
				ana.magazaolustur();
                MessageBox.Show("Ürün Başarıyla Güncelleştirildi");
                button8.PerformClick();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //-------------------------------ÜRÜNÜ YAZ GÜNCELLE SİL

        private void UrunIslemler_Load(object sender, EventArgs e)
        {
            doldur(); Adlistele(); yetkileriayarla();
		}

        //-------------Yetkiler------------------
        public void yetkileriayarla()
        {
            if (yetkiler[1] == "False")
                button5.Enabled = false;
            if (yetkiler[2] == "False")
                button4.Enabled = false;
            if (yetkiler[3] == "False")
            {
                button3.Enabled = false;
                button7.Enabled = false;
            }
        }
    }
}
