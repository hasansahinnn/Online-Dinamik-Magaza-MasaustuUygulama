using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineAlisverisSistemi
{
    public partial class Sifremiunuttum : Form
    {
        public Sifremiunuttum()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox8.Text!="")
            MessageBox.Show(textBox8.Text + " Mail Adresine Bilgileriniz Gönderilmiştir!");
            else
            {
                MessageBox.Show("Lütfen Boş Bırakmayınız!");
            }
        }
    }
}
