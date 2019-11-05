using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using lab1_Encryption_.Classes;

namespace lab1_Encryption_
{
    public partial class Form1 : Form
    {
        ICryptographer cryptographer;

        public string originalText;
        public string encryptedText;
        public string decryptedText;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cryptographer = new BitCryptographer(3);
            numericUpDownBias.Value = ((BitCryptographer)cryptographer).Bias;
        }

        private void NumericUpDownBias_ValueChanged(object sender, EventArgs e)
        {
            ((BitCryptographer)cryptographer).Bias = (int)numericUpDownBias.Value;
        }

        private void ButtonEncrypt_Click(object sender, EventArgs e)
        {
            textBoxEncrypted.Text = cryptographer.Encrypt(textBoxOriginal.Text);
        }

        private void ButtonDecrypt_Click(object sender, EventArgs e)
        {
            textBoxDecrypted.Text = cryptographer.Decrypt(textBoxEncrypted.Text);
        }
    }
}
