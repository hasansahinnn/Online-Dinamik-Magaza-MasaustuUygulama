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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn2 = new SqlConnection("Data Source=(local);Initial Catalog=onlinemagaza;Integrated Security=True;Persist Security Info=True;User ID=hasan;Encrypt=True;TrustServerCertificate=True;Context Connection=False");
        SqlConnection conn3 = new SqlConnection("Data Source=(local);Initial Catalog=onlinemagaza;Integrated Security=True;Persist Security Info=True;User ID=hasan;Encrypt=True;TrustServerCertificate=True;Context Connection=False");

        private void button2_Click(object sender, EventArgs e)
        {
            btnGirisSayfasi.BackColor = System.Drawing.Color.FromArgb(26, 177,136);
            btnKaydolSayfasi.BackColor = System.Drawing.Color.DimGray;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            btnKaydol.Visible = false;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            btnGirisYap.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;
            this.Height -= 200;
            this.MaximumSize = new System.Drawing.Size(455,435);
            this.MinimumSize = new System.Drawing.Size(455, 435);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.MaximumSize = new System.Drawing.Size(455, 435);
            this.MinimumSize = new System.Drawing.Size(455, 435);
            btnGirisSayfasi.BackColor = System.Drawing.Color.FromArgb(26, 177, 136);
            btnKaydolSayfasi.BackColor = System.Drawing.Color.DimGray;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            btnKaydol.Visible = false;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.MinimumSize = new System.Drawing.Size(455, 600);
            this.MaximumSize = new System.Drawing.Size(455, 600);
            btnKaydolSayfasi.BackColor = System.Drawing.Color.FromArgb(26, 177, 136);
            btnGirisSayfasi.BackColor = System.Drawing.Color.DimGray;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
            btnKaydol.Visible = true;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            btnGirisYap.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            this.Height +=200;
        }

        private void label12_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("d");

        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            label12.ForeColor= System.Drawing.Color.White;
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            label12.ForeColor = System.Drawing.Color.FromArgb(26,177,136);

        }

        private void label12_Click(object sender, EventArgs e)
        {
            Sifremiunuttum s = new Sifremiunuttum();
            s.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                    conn2.Open();
                    SqlCommand kmt33 = new SqlCommand("select kullaniciID from Kullanici where kullaniciNick='" + textBox8.Text.Trim() + "' and kullaniciSifre='" + textBox9.Text.Trim() + "'", conn2);
                    SqlDataReader dr = kmt33.ExecuteReader();
                    if (dr.Read())
                    {

                        int id = Convert.ToInt16(dr["kullaniciID"].ToString()); conn2.Close();
                         AnaSayfa ana = new AnaSayfa();
                    ana.kullaniciid = id;
                    ana.frm1 = this;
                            ana.Show();
                    this.Hide();
                    }
                    else
                    {
                    conn2.Close(); MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı!", "UYARI!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
            }
            catch (Exception)
            {
                throw;
            }
          

        }
        //------------------------------- UYE Oluşturma ----------------------------

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                conn2.Open();
                SqlCommand kmt33 = new SqlCommand("select kullaniciNick from Kullanici where kullaniciNick='" + textBox6.Text.Trim() + "'", conn2);
                SqlDataReader dr = kmt33.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Kullanıcı Mevcut!", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if ((textBox1.Text.Trim() == "") || (textBox2.Text.Trim() == "") || (textBox3.Text.Trim() == "") || (textBox4.Text.Trim() == "") || (textBox5.Text.Trim() == ""))

                    {
                        if (textBox1.Text == "")
                            errorProvider1.SetError(textBox1, "Bu alan boş geçilemez");
                        else
                            errorProvider1.SetError(textBox1, "");
                        if (textBox2.Text == "")
                            errorProvider1.SetError(textBox2, "Bu alan boş geçilemez");
                        else
                            errorProvider1.SetError(textBox2, "");
                        if (textBox3.Text == "")
                            errorProvider1.SetError(textBox3, "Bu alan boş geçilemez");
                        else
                            errorProvider1.SetError(textBox3, "");
                        if (textBox4.Text == "")
                            errorProvider1.SetError(textBox4, "Bu alan boş geçilemez");
                        else
                            errorProvider1.SetError(textBox4, "");
                        if (textBox5.Text == "")
                            errorProvider1.SetError(textBox5, "Bu alan boş geçilemez");
                        else
                            errorProvider1.SetError(textBox5, "");
                        if (textBox6.Text == "")
                            errorProvider1.SetError(textBox6, "Bu alan boş geçilemez");
                        else
                            errorProvider1.SetError(textBox6, "");
                        if (textBox7.Text == "")
                            errorProvider1.SetError(textBox7, "Bu alan boş geçilemez");
                        else
                            errorProvider1.SetError(textBox7, "");

                    }
                    else
                    {
                        errorProvider1.SetError(textBox1, "");
                        errorProvider1.SetError(textBox2, "");
                        errorProvider1.SetError(textBox3, "");
                        errorProvider1.SetError(textBox4, "");
                        errorProvider1.SetError(textBox5, "");
                        errorProvider1.SetError(textBox6, "");
                        errorProvider1.SetError(textBox7, "");

					

						conn3.Open();
						string ins = "exec InsertYetkiKul '"+textBox3.Text+"','"+textBox2.Text+"','"+textBox4.Text+"','"+textBox1.Text+"','"+textBox6.Text+"','"+textBox7.Text+ "','" + textBox5.Text + "'";
						SqlCommand kmt55 = new SqlCommand(ins, conn3);
						kmt55.ExecuteNonQuery();
                        conn3.Close();



						MessageBox.Show("Tebrikler! Hesabınız Başarıyla Oluşturuldu! ", "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        btnGirisSayfasi.PerformClick();

                    }
                }
                conn2.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kayıt Başarısız! ", "Başarısız!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn2.Close(); textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                btnGirisSayfasi.PerformClick();
                conn3.Close();
            }
              
           
        }
        //------------------------------- UYE Oluşturma ----------------------------
    }
}
