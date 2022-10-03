using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Accesso
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string filename = "File.csv";

        private void button1_Click(object sender, EventArgs e)
        {
            string cerca = textBox1_ingresso.Text.ToUpper();
            label_nome.Text = ("nome: " + Ricerca(filename, cerca));
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        static string Ricerca(string filename, string nomecercato)
        {
            string line = "";
            var f = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(f);
            BinaryWriter writer = new BinaryWriter(f);
            int tot = Convert.ToInt32(f.Length);
            int length = 528;
            tot /= 528;

            string result = "";

            int lung = Convert.ToInt32(f.Length);
            int i = 0, j = tot - 1, m, pos = -1;

            do
            {
                m = (i + j) / 2;
                f.Seek(m * length, SeekOrigin.Begin);
                line = Encoding.ASCII.GetString(reader.ReadBytes(length));
                result = FromString(line, 0);

                if (myCompare(result, nomecercato) == 0)
                {
                    pos = m;
                }
                else if (myCompare(result, nomecercato) == -1)
                {
                    i = m + 1;
                }
                else
                    j = m - 1;


            } while (i <= j && pos == -1);

            if (pos != -1)
                MessageBox.Show("Campo trovato in posizione: " + pos + 1);
            else
                throw new Exception("Errore! Campo inesistente.");
            string fine = FromString(line, 7);
            f.Close();
            return fine;

        }

        static int myCompare(string stringa1, string stringa2)
        {
            if (stringa1 == stringa2)
                return 0;

            char[] char1 = stringa1.ToCharArray();
            char[] char2 = stringa2.ToCharArray();
            int l = char1.Length;
            if (char2.Length < l)
                l = char2.Length;

            for (int i = 0; i < l; i++)
            {
                if (char1[i] < char2[i])
                    return -1;
                if (char1[i] > char2[i])
                    return 1;
            }

            return 0;
        }

        public static string FromString(string Stringa, int pos, string sep = ";")
        {
            string[] ris = Stringa.Split(';');
            return ris[pos];
        }
    }
}
