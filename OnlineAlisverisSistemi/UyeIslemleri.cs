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

namespace OnlineAlisverisSistemi
{
    public partial class UyeIslemleri : Form
    {
        public UyeIslemleri()
        {
            InitializeComponent();
        }
        public SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=onlinemagaza;Integrated Security=True;Persist Security Info=True;User ID=hasan;Encrypt=True;TrustServerCertificate=True;Context Connection=False");
        public SqlDataAdapter da;
        public DataTable dt = new DataTable(); public int i, id, urunid, kullaniciid;public string nick;


        public void doldur(string kosul="",int sec=0)
        {
            try
            {
                SqlCommand kmt=null;
                conn.Close();
                conn.Open();
                if (sec == 0)
                {
                    kmt = new SqlCommand("Select k.kullaniciID,kullaniciNick,k.kullaniciAd,k.kullaniciSoyad,k.kullaniciSifre,k.kullaniciTel,k.kullaniciMail,y.okuma,y.silme,y.guncelleme,y.yazma,y.adminYetki from Kullanici k join Yetki y on k.kullaniciID=y.yetkiID where k.kullaniciNick='" + kosul+"'", conn);
                }
                //Prosedür sorgusu , koşul stringine göre taricak
           
                else if (sec == 2)
                {
                    kmt = new SqlCommand("Select k.kullaniciID,kullaniciNick,k.kullaniciAd,k.kullaniciSoyad,k.kullaniciSifre,k.kullaniciTel,k.kullaniciMail,y.okuma,y.silme,y.guncelleme,y.yazma,y.adminYetki from Kullanici k join Yetki y on k.kullaniciID=y.yetkiID  where k.kullaniciTel='" + kosul + "'", conn);
                }
                SqlDataReader dr = kmt.ExecuteReader();
                if(dr.Read())
                {
                    id = Convert.ToInt16(dr["kullaniciID"]);
                    label7.Text = dr["kullaniciAd"].ToString();
                    label11.Text = dr["kullaniciSoyad"].ToString();
                    label12.Text = dr["kullaniciSifre"].ToString();
                    label13.Text = dr["kullaniciMail"].ToString();
                    label10.Text = dr["kullaniciTel"].ToString();
                    nick= dr["kullaniciNick"].ToString();
                    checkBox1.Checked=Convert.ToBoolean(dr["okuma"]);
                    checkBox2.Checked = Convert.ToBoolean(dr["guncelleme"]);
                    checkBox3.Checked = Convert.ToBoolean(dr["silme"]);
                    checkBox4.Checked = Convert.ToBoolean(dr["yazma"]);
                    checkBox5.Checked = Convert.ToBoolean(dr["adminYetki"]);
                }
                conn.Close();

            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text!="")
            {
                doldur(comboBox1.Text, 0);
                comboBox1.Text = "";
            }
          
            else if (comboBox3.Text != "")
            {
                doldur(comboBox3.Text, 2);
                comboBox3.Text = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)   // Yetkileri güncelle
        {
            try
            {
                if (label7.Text != "")
                {
                    DialogResult dialogResult = MessageBox.Show(nick + " Kullanıcı Yetkilerini Güncellemek İstediğinize Eminmisiniz?", "Yetkilendirme!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        conn.Close();
                        conn.Open();
                        SqlCommand kmt = new SqlCommand("update  Yetki set okuma='"+checkBox1.Checked+"',silme='"+checkBox3.Checked+"',yazma='"+checkBox4.Checked+"',guncelleme='"+checkBox2.Checked+"',adminyetki='"+checkBox5.Checked+"' where yetkiID="+id+"", conn);
                        kmt.ExecuteNonQuery();
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e) // Hesabı Sil
        {
            try
            {
                if (label7.Text != "")
                {
                    DialogResult dialogResult = MessageBox.Show(nick + " Kullanıcı Adlı Bu Hesabı Silmek İstediğinize Eminmisiniz?", "Üyelik Sil!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        conn.Close();
                        conn.Open();
                        SqlCommand kmt = new SqlCommand("delete from Kullanici where id=" + id + "", conn);
                        kmt.ExecuteNonQuery();
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
          
        }
          

        public void comboboxdoldur()
        {
            try
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection3 = new AutoCompleteStringCollection();
                conn.Close();
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
                conn.Open();
                SqlCommand kmt = new SqlCommand("Select * from Kullanici ", conn);
                SqlDataReader dr = kmt.ExecuteReader();
                while (dr.Read())
                {
                    collection.Add(dr["kullaniciNick"].ToString());
                    comboBox1.Items.Add(dr["kullaniciNick"]);

                    collection3.Add(dr["kullaniciTel"].ToString());
                    comboBox3.Items.Add(dr["kullaniciTel"]);
                }
                conn.Close();
                comboBox1.AutoCompleteCustomSource = collection;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox3.AutoCompleteCustomSource = collection3;
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception)
            {
            }
           
        }
        private void UyeIslemleri_Load(object sender, EventArgs e)
        {
            comboboxdoldur();
        }
    }
}
