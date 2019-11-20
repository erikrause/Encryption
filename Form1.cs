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
using lab1_Encryption_.Forms;

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
            numericUpDownBias.Visible = false;
            textBoxKey.Visible = false;
            cryptographerControl = cryptographerControl1;
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

        private void TextBoxKey_TextChanged(object sender, EventArgs e)
        {
            ((KeyCryptographer)cryptographer).Key = textBoxKey.Text;
        }

        CryptographerControl cryptographerControl;

        private void ComboBoxCryptographerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxCryptographerType.SelectedIndex;

            var cryptoTypes = new Dictionary<int, Func<ICryptographer>>
            {
                {
                    0,
                    () =>
                    {
                        numericUpDownBias.Visible = true;

                        var newControl = new BitCryptographerControl();
                        ReplaceControl(cryptographerControl, newControl);

                        return new BitCryptographer((int)newControl.numericUpDownBias.Value);
                    }
                },
                {
                    1,
                    () =>
                    {
                        textBoxKey.Visible = true;
                        return new KeyCryptographer(textBoxKey.Text);
                    }
                }
            };
            cryptographer = cryptoTypes[index]();
        }

        protected void ReplaceControl(Control oldControl, Control newControl)
        {
            Controls.Remove(oldControl);
            newControl.Location = oldControl.Location;
            Controls.Add(newControl);
        }
    }
}
