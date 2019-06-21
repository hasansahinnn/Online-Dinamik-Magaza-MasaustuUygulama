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
using System.Threading;
namespace OnlineAlisverisSistemi
{
    public partial class AnaSayfa : Form
    {
       public SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=onlinemagaza;Integrated Security=True;Persist Security Info=True;User ID=hasan;Encrypt=True;TrustServerCertificate=True;Context Connection=False");

        List<int> urunid = new List<int>();
        List<int> adet = new List<int>();
        List<string> urunAdi = new List<string>();
        public List<string> yetkiler = new List<string>();
        public SqlDataAdapter da;
        public DataTable dt = new DataTable();public int satisid;public double iadetutar;public string iadeidler;
		public DateTime dt2; public string iadeaciklama="NULL"; public string kargodurum="NULL";
        public Form1 frm1;
        public AnaSayfa()
        {
            InitializeComponent();
        }
        public int id,kullaniciid;
        public void panelgoster(Panel p)
        {
            magazaPanel.Visible = false;
            Gecmis.Visible = false;
            Bakiye.Visible = false;
            Hesabım.Visible = false;
            Personel.Visible = false;
            p.Visible = true;
        }
     

        private void button8_Click(object sender, EventArgs e)
        {
            panelgoster(Hesabım);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            frm1.Show();

            this.Hide();
        }


        //-------------------------- MAGAZA URUN ARTTIR------------------------

        void arttirclick(object sender,EventArgs e)
        {
            Button btn = sender as Button;
            var ucrets = (Controls.Find(btn.Name+"Ucret", true));
            var ucret = (Label)ucrets[0];
            var stoks = (Controls.Find(btn.Name + "Stok", true));
            var stok = (Label)stoks[0];
            var Toplams = (Controls.Find(btn.Name + "Toplam", true));
            var Toplam = (Label)Toplams[0];
            var Adets = (Controls.Find(btn.Name + "Adet", true));
            var Adet = (TextBox)Adets[0];
            var ids = (Controls.Find(btn.Name + "id", true));
            var id = (Label)ids[0];
            if (Convert.ToInt16(Adet.Text)<Convert.ToInt16(stok.Text))
            {
                Adet.Text = (Convert.ToInt16(Adet.Text) + 1).ToString();
                Toplam.Text=(Convert.ToInt16(Adet.Text)*Convert.ToDouble(ucret.Text)).ToString();
                Toplamtutar.Text = (Convert.ToDouble(Toplamtutar.Text) + Convert.ToDouble(ucret.Text)).ToString();
                int aranaIndex = urunid.BinarySearch(Convert.ToInt16(id.Text));
                if (aranaIndex == -7)
                    aranaIndex = 6;
                else if (aranaIndex == -14)
                    aranaIndex = 14;
                if (aranaIndex > -1)
                    adet[aranaIndex]++;
                else
                {

                }
                
            }
        }
        //-------------------------- MAGAZA URUN ARTTIR------------------------



        //-------------------------- MAGAZA URUN AZALT------------------------

        void azaltclick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var ucrets = (Controls.Find(btn.Name + "Ucret", true));
            var ucret = (Label)ucrets[0];
            var stoks = (Controls.Find(btn.Name + "Stok", true));
            var stok = (Label)stoks[0];
            var Toplams = (Controls.Find(btn.Name + "Toplam", true));
            var Toplam = (Label)Toplams[0];
            var Adets = (Controls.Find(btn.Name + "Adet", true));
            var Adet = (TextBox)Adets[0];
            var ids = (Controls.Find(btn.Name + "id", true));
            var id = (Label)ids[0];
            if (Convert.ToInt16(Adet.Text) > 0)
            {
                Adet.Text = (Convert.ToInt16(Adet.Text) - 1).ToString();
                Toplam.Text = (Convert.ToInt16(Adet.Text) * Convert.ToDouble(ucret.Text)).ToString();
                Toplamtutar.Text = (Convert.ToDouble(Toplamtutar.Text) - Convert.ToDouble(ucret.Text)).ToString();
                int aranaIndex = urunid.BinarySearch(Convert.ToInt16(id.Text));
                if(aranaIndex>-1)
                adet[aranaIndex]--;
            }
        }

        //-------------------------- MAGAZA URUN AZALT------------------------



        //-------------------------- MAGAZA------------------------
        public void magazaolustur()
        {
            try
            {
                magazaPanel.Controls.Clear();
                conn.Close();
                conn.Open();
                SqlCommand kmt2 = new SqlCommand("Select u.urunID,u.urunAdi,u.urunFiyat,s.urunAdet from Urun u join Stok s on u.urunID=s.stokID", conn);
                SqlDataReader dr2 = kmt2.ExecuteReader();
                int satir = 8; int genislik = 17; int sayac = 0, i = 0;
                while (dr2.Read())
                {

                    urunid.Add(Convert.ToInt16(dr2["urunID"].ToString()));
                    urunAdi.Add(dr2["urunAdi"].ToString());
                    adet.Add(0);
                    GroupBox urun = new GroupBox();
                    urun.Name = i + "urun";
                    urun.Size = new System.Drawing.Size(169, 287);
                    urun.Location = new Point(satir, genislik);
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Location = new Point(20, 20);
                    pic.Size = new Size(126, 116);
                    pic.BorderStyle = BorderStyle.Fixed3D;
                    pic.ImageLocation = dr2["urunAdi"].ToString() + ".jpg";
                    pic.Name = i + "pic";
                    urun.Controls.Add(pic);



                    Label l15 = new Label();
                    l15.ForeColor = Color.Black;
                    l15.Text = dr2["urunID"].ToString();
                    l15.Location = new Point(60, 139);
                    l15.Name = i + "id";
                    l15.Size = new Size(100, 13); l15.Visible = false;
                    urun.Controls.Add(l15);

                    Label l1 = new Label();
                    l1.ForeColor = Color.Black;
                    l1.Text = dr2["urunAdi"].ToString();
                    l1.Location = new Point(60, 139);
                    l1.Name = i + "Ad";
                    l1.Size = new Size(100, 13);
                    urun.Controls.Add(l1);



                    Label l2 = new Label();
                    l2.ForeColor = Color.Black;
                    l2.Text = "Fiyat:";
                    l2.Location = new Point(20, 163);
                    l2.Size = new Size(32, 13);
                    l2.Name = i + "l1";
                    urun.Controls.Add(l2);
                    Label l3 = new Label();
                    l3.ForeColor = Color.Black;
                    l3.Text = dr2["urunFiyat"].ToString();
                    l3.Location = new Point(55, 163);
                    l3.Size = new Size(25, 13);
                    l3.Name = i + "Ucret";
                    urun.Controls.Add(l3);
                    Label l4 = new Label();
                    l4.ForeColor = Color.Black;
                    l4.Text = "TL";
                    l4.Size = new Size(24, 13);
                    l4.Location = new Point(80, 163);
                    l4.Name = i + "l5";
                    urun.Controls.Add(l4);


                    Label l5 = new Label();
                    l5.ForeColor = Color.Black;
                    l5.Text = "Stok Adet: ";
                    l5.Size = new Size(60, 13);
                    l5.Location = new Point(20, 187);
                    l5.Name = i + "l2";
                    urun.Controls.Add(l5);
                    Label l6 = new Label();
                    l6.ForeColor = Color.Black;
                    l6.Text = dr2["urunAdet"].ToString();
                    l6.Size = new Size(25, 13);
                    l6.Location = new Point(77, 187);
                    l6.Name = i + "Stok";
                    urun.Controls.Add(l6);
                    Label l16 = new Label();
                    l16.ForeColor = Color.Black;
                    l16.Text = "Adet";
                    l16.Size = new Size(40, 13);
                    l16.Location = new Point(100, 187);
                    l16.Name = i + "l16";
                    urun.Controls.Add(l16);



                    Label l7 = new Label();
                    l7.Text = "Toplam Tutar:";
                    l7.Location = new Point(20, 209);
                    l7.Size = new Size(75, 13);
                    l7.ForeColor = Color.Green;
                    l7.Name = i + "l3";
                    urun.Controls.Add(l7);
                    Label l8 = new Label();
                    l8.Text = "0";
                    l8.Location = new Point(95, 209);
                    l8.Size = new Size(40, 13);
                    l8.ForeColor = Color.Green;
                    l8.Name = i + "Toplam";
                    urun.Controls.Add(l8);
                    Label l9 = new Label();
                    l9.Text = "TL";
                    l9.Location = new Point(125, 209);
                    l8.Size = new Size(30, 13);
                    l9.ForeColor = Color.Green;
                    l9.Name = i + "l31";
                    urun.Controls.Add(l9);




                    Button btn = new Button();
                    btn.Image = Image.FromFile("arttir.png");
                    btn.Size = new System.Drawing.Size(41, 38);
                    btn.Location = new Point(20, 235);
                    btn.Click += new EventHandler(this.arttirclick);
                    btn.Name = i.ToString();
                    urun.Controls.Add(btn);


                    TextBox txt = new TextBox();
                    txt.Text = "0";
                    txt.Multiline = true;
                    txt.Size = new Size(32, 30);
                    txt.BackColor = System.Drawing.Color.FromArgb(19, 35, 47);
                    txt.ForeColor = Color.White;
                    txt.TextAlign = HorizontalAlignment.Center;
                    txt.Font = new Font(txt.Font.FontFamily, 16);
                    txt.Location = new Point(67, 239); txt.ReadOnly = true;
                    txt.Name = i + "Adet";
                    urun.Controls.Add(txt);





                    Button btn2 = new Button();
                    btn2.Image = Image.FromFile("azalt.png");
                    btn2.Size = new System.Drawing.Size(41, 38);
                    btn2.Location = new Point(105, 235);
                    btn2.Click += new EventHandler(this.azaltclick);
                    btn2.Name = i.ToString();
                    urun.Controls.Add(btn2);

                    magazaPanel.Controls.Add(urun); satir += 180;

                    sayac++; i++;
                    if (sayac == 6)
                    {
                        satir = 8;
                        genislik += 290;
                        sayac = 0;
                    }
                }
                conn.Close();
            }
            catch (Exception)
            {

            }
		
           
        }
        //-------------------------- MAGAZA------------------------





        public void doldur()
        {
            try
            {
                //---------------------BAKİYE VE HESABIMI DOLDUR-------------------

                conn.Close();
                conn.Open();
                SqlCommand kmt2 = new SqlCommand("Select * from Kullanici where kullaniciID="+kullaniciid+"", conn);
                SqlDataReader dr2 = kmt2.ExecuteReader();
                if (dr2.Read())
                {
                    label86.Text = dr2["Bakiye"].ToString();
                    textBox1.Text = dr2["kullaniciAd"].ToString();
                    textBox2.Text = dr2["kullaniciSoyad"].ToString();
                    textBox3.Text = dr2["kullaniciSifre"].ToString();
                    textBox6.Text = dr2["kullaniciMail"].ToString();
                    textBox4.Text = dr2["kullaniciAdres"].ToString();
                }
                conn.Close();
                //---------------------BAKİYE VE HESABIMI DOLDUR-------------------


                //---------------------YETKİ PANEL BUTONUNU GÖSTER-------------------
                conn.Open();
                SqlCommand kmt3 = new SqlCommand("Select * from Yetki where yetkiID=" + kullaniciid + "", conn);
                SqlDataReader dr3 = kmt3.ExecuteReader();
                if (dr3.Read())
                {
                    yetkiler.Add(dr3["okuma"].ToString());
                    yetkiler.Add(dr3["silme"].ToString());
                    yetkiler.Add(dr3["guncelleme"].ToString());
                    yetkiler.Add(dr3["yazma"].ToString());
                    yetkiler.Add(dr3["adminYetki"].ToString());

                    if (Convert.ToBoolean(dr3["okuma"]) == true || Convert.ToBoolean(dr3["silme"]) == true || Convert.ToBoolean(dr3["guncelleme"]) == true || Convert.ToBoolean(dr3["yazma"]) == true || Convert.ToBoolean(dr3["adminYetki"]) == true)
                    {
                        button5.Visible = true;
                        if (Convert.ToBoolean(dr3["silme"]) != true && Convert.ToBoolean(dr3["adminYetki"]) != true)
                            button72.Enabled = false;
                        if (Convert.ToBoolean(dr3["adminYetki"]) != true)
                            button67.Enabled = false;
                       else
                        {
                            yedekBtn.Visible = true;button13.Visible = true;
                        }
                    }
                }
                conn.Close();
                //---------------------YETKİ PANEL BUTONUNU GÖSTER-------------------






            }
            catch (Exception)
            {
            }
        }







        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            magazaolustur();doldur(); 
            Gecmis.Size= new System.Drawing.Size(1070 , 486); 
            Bakiye.Size = new System.Drawing.Size(466 , 381); 
            Hesabım.Size = new System.Drawing.Size(422 , 462);
            magazaPanel.Size = new System.Drawing.Size(1106 , 494); 
            Personel.Size = new System.Drawing.Size(1099 , 480);
            Hesabım.Location=new Point(142, 74);
            Bakiye.Location=new Point(142, 74);
            magazaPanel.Location=new Point(142, 69);
            Gecmis.Location=new Point(142, 74);
            Personel.Location=new Point(142, 74);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        //-------------------------- PANELLERİ GÖSTER BUTONU------------------------

        private void button11_Click(object sender, EventArgs e)
        {
            panelgoster(Gecmis);
			griddoldur();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            panelgoster(Bakiye);

        }

        private void button67_Click(object sender, EventArgs e)
        {
            UyeIslemleri uye = new UyeIslemleri();uye.kullaniciid = kullaniciid;
            uye.ShowDialog();
        }

        private void button71_Click(object sender, EventArgs e)
        {
            UrunIslemler urun = new UrunIslemler();
			urun.kullaniciid = kullaniciid;
			urun.yetkiler = yetkiler;
			urun.ana = this;
            urun.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            panelgoster(Personel);
			panelsatislar();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panelgoster(magazaPanel);

        }

        //-------------------------- PANELLERİ GÖSTER BUTONU------------------------






        //-------------------------- HESABIMI GÜNCELLE BUTONU------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Close();
                conn.Open();
                SqlCommand kmt2 = new SqlCommand("update Kullanici set kullaniciAd='" + textBox1.Text + "',kullaniciSoyad='" + textBox2.Text + "',kullaniciSifre='" + textBox3.Text + "',kullaniciMail='" + textBox6.Text + "',kullaniciAdres='" + textBox4.Text + "' where kullaniciID="+ kullaniciid + "", conn);
                kmt2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Başarıyla Güncelleştirildi");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //-------------------------- HESABIMI GÜNCELLE BUTONU------------------------





        //-------------------------- BAKİYE EKLE BUTONU------------------------

        private void button61_Click(object sender, EventArgs e)
        {
            if(textBox12.Text!="" && textBox13.Text != "" && textBox14.Text != "" && textBox15.Text != "" && textBox16.Text != "" )
            {
                conn.Open();
                SqlCommand kmt2 = new SqlCommand("update Kullanici set Bakiye=Bakiye+"+Convert.ToInt16(textBox12.Text)+" where kullaniciID=" + kullaniciid + "", conn);
                kmt2.ExecuteNonQuery();
                conn.Close();
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox15.Text = "";
                textBox16.Text = "";
                doldur();
                MessageBox.Show("Bakiye Eklendi");
            }
            else
            {
                MessageBox.Show("Boş Yer Bırakmayınız");
            }
        }
        //-------------------------- BAKİYE EKLE BUTONU------------------------

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
             (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
           (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
           (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
           (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button65_Click(object sender, EventArgs e)
        {
            griddoldur(textBox17.Text,1);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            griddoldur(dateTimePicker4.Value.ToString("MM/dd/yyyy"));
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            griddoldur();
        }

        // ------------------------- SATIŞ İŞLEMLERİ---------------------------


        //--------------------- Geçmiş Siparişler
        public void griddoldur(string kosul = "", int s = 0)
        {
            SqlCommand kmt;
            try
            {
                conn.Close();
                dt.Clear();
                conn.Open();
                if (kosul == "")
                {
                    kmt = new SqlCommand("Select s.satisID,s.Satisaciklama,CONVERT(VARCHAR(8), s.satisTarih, 4),s.toplamTutar,k.kargoDurum,s.SatisIade from Satis s join Kargo k on s.satisID=k.kargoID where s.kullaniciID=" + kullaniciid + "", conn);
                }
                else if (kosul != "" && s == 1)
                {
                    kmt = new SqlCommand("Select s.satisID,s.Satisaciklama,CONVERT(VARCHAR(8), s.satisTarih, 4),s.toplamTutar,k.kargoDurum,s.SatisIade from Satis s join Kargo k on s.satisID=k.kargoID where s.satisID=" + kosul + " and s.kullaniciID=" + kullaniciid + "", conn);
                }
                else
                {
                    kmt = new SqlCommand("Select s.satisID,s.Satisaciklama,CONVERT(VARCHAR(8), s.satisTarih, 4),s.toplamTutar,k.kargoDurum,s.SatisIade from Satis s join Kargo k on s.satisID=k.kargoID where s.satisTarih='" + kosul + "' and s.kullaniciID=" + kullaniciid + "", conn);
                }
                SqlDataAdapter da = new SqlDataAdapter(kmt);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[5].Visible = false;
                conn.Close();

                //YAZI RENGİ BEYAZ
                System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                dataGridViewCellStyle1.ForeColor = Color.Black;
                dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;

            }
            catch (Exception)
            {
            }
        }

        //--------------------- Geçmiş Siparişler








        private void button2_Click(object sender, EventArgs e) //Kargoyu aldım
        {
            if(satisid != 0 )
            {
                if(kargodurum != "Kargo iptal Edilmiştir.")
                {
                    DialogResult dialogResult = MessageBox.Show("Kargonuzun Geldiğini Onaylamak İstediğinize Eminmisiniz?", "Kargom Geldi!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        conn.Open();
                        SqlCommand kmt2 = new SqlCommand("update Kargo set kargoDurum='Kargoyu Aldım' where kargoID=" + satisid + "", conn);
                        kmt2.ExecuteNonQuery();
                        griddoldur();
                        conn.Close();

                    }
                    else
                    { }
                }
                else
                {
                    MessageBox.Show("Satış İptal Edilmiş.Lütfen Yeni Satın Alım Yapın.");
                }
               
            }
            else
            {
                MessageBox.Show("Lütfen önce Satiş Seçin!");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                iadetutar= Convert.ToDouble(row.Cells[3].Value.ToString());
				iadeaciklama = row.Cells[1].Value.ToString();
                satisid = Convert.ToInt16(row.Cells[0].Value.ToString());// KARGO ID yi al
				dt2 = Convert.ToDateTime(row.Cells[2].Value.ToString());
				kargodurum = row.Cells[4].Value.ToString();
                iadeidler = row.Cells[5].Value.ToString();
                break;
            }
        }
       
        private void button63_Click(object sender, EventArgs e) // Satış İptal
		{
			if (satisid != 0)
			{
				DialogResult dialogResult = MessageBox.Show("Satın Almayı İptal Etmek İstediğinize Eminmisiniz", "Satın Alma İptal", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
                    if(kargodurum!= "Kargo iptal Edilmiştir.")
                    {
                        conn.Close();
                        conn.Open();
                        SqlCommand kmt2 = new SqlCommand("exec  uruniade '" + iadeidler + "','" + satisid + "' ", conn);
                        kmt2.ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        SqlCommand kmt32 = new SqlCommand("update Kullanici set Bakiye=Bakiye+" + iadetutar + " where kullaniciID=" + kullaniciid + "", conn);
                        kmt32.ExecuteNonQuery();
                        conn.Close();
                        button6.PerformClick();
                        doldur();
                        magazaolustur();
                        MessageBox.Show("İşlem Tamam");
                    }
                    else
                    {
                        MessageBox.Show("Kargonuz Zaten İptal Edildi!");
                    }
					
					
				}
				else
				{ }
			}
			else
			{
				MessageBox.Show("Lütfen önce Satiş Seçin!");
			}
		}

		// ------------------------- SATIŞ İŞLEMLERİ---------------------------




		//-------------------------- SATIN AL BUTONU------------------------

		private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                int satisid = 1;
                string aciklama = "";
                string idler = "";
                if (Convert.ToDouble(label86.Text) >= Convert.ToDouble(Toplamtutar.Text))
                {
                    for (int i = 0; i < urunid.Count; i++)
                    {
                        if (adet[i] > 0)
                        {
                            aciklama += adet[i].ToString() + " Adet " + urunAdi[i] + ",";
                            idler += urunid[i] + "," + adet[i] + ",";
                            conn.Open();
                            SqlCommand kmt6 = new SqlCommand("update Stok set urunAdet=urunAdet-" + adet[i] + " where stokID=(select urunID from Urun where urunAdi='" + urunAdi[i] + "')", conn);
                            kmt6.ExecuteNonQuery();
                            conn.Close();
                            //-------------------------- acıklama ve id kısmı------------------------

                        }
                    }
                    conn.Close();
                    conn.Open();
                    SqlCommand kmt2 = new SqlCommand("select top 1 satisID from Satis order by satisID desc", conn);
                    SqlDataReader dr2 = kmt2.ExecuteReader();
                    if (dr2.Read())
                    {
                        satisid = Convert.ToInt16(dr2["satisID"]) + 1;
                    }
                    conn.Close();
                  

                    conn.Open();
                    SqlCommand kmt4 = new SqlCommand("insert into Satis(kullaniciID,Satisaciklama,toplamTutar,SatisIade) values(" + kullaniciid + ",'" + aciklama + "'," + Convert.ToInt16(Toplamtutar.Text) + ",'" + idler + "')", conn);
                    kmt4.ExecuteNonQuery();
                    conn.Close();
                    //}

                    conn.Close();
                    conn.Open();
                    SqlCommand kmt3 = new SqlCommand("insert into Kargo values(" + satisid + ",'Kargonuz Yola Çıkmıştır.')", conn);
                    kmt3.ExecuteNonQuery();
                    conn.Close();
                    //-------------------------- sipariş oluşturuldu------------------------
                    conn.Open();
                    SqlCommand kmt5 = new SqlCommand("update Kullanici set Bakiye=Bakiye-" + Convert.ToDouble(Toplamtutar.Text) + " where kullaniciID=" + kullaniciid + "", conn);
                    kmt5.ExecuteNonQuery();
                    conn.Close();
                    //-------------------------- bakiye güncellendi------------------------
                    label86.Text = (Convert.ToDouble(label86.Text) - Convert.ToDouble(Toplamtutar.Text)).ToString();
                    Toplamtutar.Text = "0";

                    griddoldur();
                    magazaolustur();
                    MessageBox.Show("Satın Alım Başarılı.Ürününüz Yolda!");
                }
                else
                {
                    MessageBox.Show("Bakiye Yetersiz");
                }
            }
            catch (Exception)
            {
            }
          
            
			
        }

        //-------------------------- SATIN AL BUTONU------------------------




        //-------------------------- PANELDEKİ SATIN ALIMLAR------------------------

        public void panelsatislar(string kosul = "", int s = 0)
        {
            SqlCommand kmt;
            try
            {
                conn.Close();
                dt.Clear();
                conn.Open();
                if (kosul == "")
                {
                    kmt = new SqlCommand("Select s.satisID,s.Satisaciklama,s.satisTarih,s.toplamTutar,k.kargoDurum,s.SatisIade from Satis s join Kargo k on s.satisID=k.kargoID ", conn);
                }
                else if (kosul != "" && s == 1)
                {
                    kmt = new SqlCommand("Select s.satisID,s.Satisaciklama,s.satisTarih,s.toplamTutar,k.kargoDurum,s.SatisIade from Satis s join Kargo k on s.satisID=k.kargoID  where s.kullaniciID=(select kullaniciID from Kullanici where kullaniciNick='" + kosul+"')", conn);
                }
                else if (kosul != "" && s == 2)
                {
                    kmt = new SqlCommand("Select s.satisID,s.Satisaciklama,s.satisTarih,s.toplamTutar,k.kargoDurum,s.SatisIade from Satis s join Kargo k on s.satisID=k.kargoID where s.kullaniciID=(select kullaniciID from Kullanici where kullaniciTC='" + kosul + "')", conn);
                }
                else 
                {
                    kmt = new SqlCommand("Select s.satisID,s.Satisaciklama,s.satisTarih,s.toplamTutar,k.kargoDurum,s.SatisIade from Satis s join Kargo k on s.satisID=k.kargoID where s.satisTarih='" + kosul + "'", conn);
                }
                SqlDataAdapter da = new SqlDataAdapter(kmt);
                da.Fill(dt);
                dataGridView3.DataSource = dt;
                conn.Close();
                dataGridView3.Columns[5].Visible = false;

                //YAZI RENGİ BEYAZ
                System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                dataGridViewCellStyle1.ForeColor = Color.Black;
                dataGridView3.DefaultCellStyle = dataGridViewCellStyle1;

            }
            catch (Exception)
            {
            }
        }

        private void button70_Click(object sender, EventArgs e)
        {
            panelsatislar(textBox7.Text, 2);
        }

        private void button69_Click(object sender, EventArgs e)
        {
            panelsatislar(textBox5.Text, 1);

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            panelsatislar();
        }

        private void button72_Click(object sender, EventArgs e) // Kayit iptal asagıdaki degiskenler kullanilabilir
        {
            if (satisid != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Siparişi Almayı İptal Etmek İstediğinize Eminmisiniz", "Satın Alma İptal", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (kargodurum != "Kargo iptal Edilmiştir.")
                    {
                        conn.Close();
                        conn.Open();
                        SqlCommand kmt2 = new SqlCommand("exec  uruniade '" + iadeidler + "','" + satisid + "' ", conn);
                        kmt2.ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        SqlCommand kmt32 = new SqlCommand("update Kullanici set Bakiye=Bakiye+" + iadetutar + " where kullaniciID=(select kullaniciID from Kullanici where kullaniciID=(Select kullaniciID from Satis where satisID="+satisid+"))", conn);
                        kmt32.ExecuteNonQuery();
                        conn.Close();
                        button7.PerformClick();
                        doldur();
                        magazaolustur();
                        MessageBox.Show("İşlem Tamam");
                    }
                    else
                    {
                        MessageBox.Show("Kargonuz Zaten İptal Edildi!");
                    }


                }
                else
                { }
            }
            else
            {
                MessageBox.Show("Lütfen önce Satiş Seçin!");
            }
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                iadetutar = Convert.ToDouble(row.Cells[3].Value.ToString());
                iadeaciklama = row.Cells[1].Value.ToString();
                satisid = Convert.ToInt16(row.Cells[0].Value.ToString());// KARGO ID yi al
                dt2 = Convert.ToDateTime(row.Cells[2].Value.ToString());
                kargodurum = row.Cells[4].Value.ToString();
                iadeidler = row.Cells[5].Value.ToString();
                break;
            }
        }
		
		private void button12_Click(object sender, EventArgs e)
		{

			//foreach (DataGridViewRow row in dataGridView3.SelectedRows)
			//{
			//	//GEREKLİ VERİLER ALINDI   <ONUR>
			//	iadeaciklama =row.Cells[1].Value.ToString();
			//	dt2 = Convert.ToDateTime(row.Cells[2].Value.ToString());

			//	kargodurum = row.Cells[4].Value.ToString();
			//	break;
			//}

			//using (Report yeni1 = new Report())
			//{
			//	//VERİLER REPORT PARAMETRELERİNE GÖNDERİLDİ <ONUR>
			//	yeni1.Load(Application.StartupPath + "\\Report3.frx");
			//	yeni1.SetParameterValue("Parameter", satisid.ToString());         //PARAMETRE BU ŞEKİLDE GÖNDERİLİYOR.
			//	yeni1.SetParameterValue("Parameter1", iadeaciklama.ToString());
			//	yeni1.SetParameterValue("Parameter2", dt2.ToString());
			//	yeni1.SetParameterValue("Parameter3", iadetutar.ToString());
			//	yeni1.SetParameterValue("Parameter4", kargodurum.ToString());

			//	yeni1.Show();

			//}
			using (Report yeni1 = new Report())
			{
				yeni1.Load(Application.StartupPath + "\\Report4.frx");
				int m = 0;
				for (int i=0;i< dataGridView1.Rows.Count-1;i++)
			     { 
				for (int j = 0; j < dataGridView1.Rows[0].Cells.Count-1;j++)
				{
						
						string s = dataGridView1.Rows[i].Cells[j].Value.ToString();
						if (m==0) yeni1.SetParameterValue("Parameter", s);
						else yeni1.SetParameterValue("Parameter" + m + "", s);
						// MessageBox.Show( dataGridView1.Rows[i].Cells[j].Value.ToString());
						m++;

					}
				}
				yeni1.Show();
			}
		}

		private void magazaPanel_Paint(object sender, PaintEventArgs e)
		{

		}

		private void yedekBtn_Click(object sender, EventArgs e)
		{
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Sql Backup Dosyası|*.bak";
                save.OverwritePrompt = true;
                save.ShowDialog();
                conn.Open();
                SqlCommand backupKod = new SqlCommand("BACKUP DATABASE [onlinemagaza] TO  DISK = '" + save.FileName + "' WITH INIT ", conn);
                backupKod.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {

            }
           
		}

		private void button13_Click(object sender, EventArgs e)
		{
            try
            {
                OpenFileDialog dosya2 = new OpenFileDialog();

                dosya2.Title = "Toplu Yedek İşlemi       - Backup Dosyası Seçin!";
                dosya2.ShowDialog();
                string Dosyayolu = dosya2.FileName;
                conn.Open();
                SqlCommand backupDon = new SqlCommand("USE [master] ALTER DATABASE[onlinemagaza]  SET offline with rollback immediate RESTORE DATABASE [onlinemagaza] FROM  DISK = N'" + Dosyayolu + "' WITH  REPLACE  alter database [onlinemagaza] set online with rollback immediate", conn);
                backupDon.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
            }
          
		}

		private void button68_Click(object sender, EventArgs e)
        {
           
            panelsatislar(dateTimePicker1.Value.ToString("MM/dd/yyyy"));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            magazaolustur();
        }

		private void textBox8_TextChanged(object sender, EventArgs e)
		{

		}

		private void label8_Click(object sender, EventArgs e)
		{

		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void button16_Click(object sender, EventArgs e)
		{
			using (Report yeni1 = new Report())
			{
				yeni1.Load(Application.StartupPath + "\\rprt3.frx");
				int m = 0;
				for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
				{
					for (int j = 0; j < dataGridView3.Rows[0].Cells.Count - 1; j++)
					{

						string s = dataGridView3.Rows[i].Cells[j].Value.ToString();
						if (m == 0) yeni1.SetParameterValue("Parameter", s);
						else yeni1.SetParameterValue("Parameter" + m + "", s);
						// MessageBox.Show( dataGridView1.Rows[i].Cells[j].Value.ToString());
						m++;

					}
				}
				yeni1.Show();
			}
		}

		private void AnaSayfa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        //-------------------------- PANELDEKİ SATIN ALIMLAR------------------------
    }
}
