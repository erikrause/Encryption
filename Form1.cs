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
        ICryptographer Cryptographer;

        public string originalText;
        public string encryptedText;
        public string decryptedText;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CryptographerControl = cryptographerControl1;
        }

        private void CryptographerControl_ValueChanged(object sender, EventArgs e)
        {
            var cryptoTypes = new Dictionary<Type, Action>
            {
                {
                    typeof(BitCryptographer),
                    () =>
                    {
                        var control = (BitCryptographerControl)CryptographerControl;
                        var cryptographer = (BitCryptographer)Cryptographer;
                        cryptographer.Bias = (int)control.numericUpDownBias.Value;
                    }
                },
                {
                    typeof(KeyCryptographer),
                    () =>
                    {
                        var control = (KeyCryptographerControl)CryptographerControl;
                        var cryptographer = (KeyCryptographer)Cryptographer;
                        cryptographer.Key = control.textBoxKey.Text;
                    }
                }
            };
            cryptoTypes[Cryptographer.GetType()]();
        }

        private void ButtonEncrypt_Click(object sender, EventArgs e)
        {
            textBoxEncrypted.Text = Cryptographer.Encrypt(textBoxOriginal.Text);
        }

        private void ButtonDecrypt_Click(object sender, EventArgs e)
        {
            textBoxDecrypted.Text = Cryptographer.Decrypt(textBoxEncrypted.Text);
        }

        CryptographerControl CryptographerControl;

        private void ComboBoxCryptographerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxCryptographerType.SelectedIndex;

            var cryptoTypes = new Dictionary<int, Func<ICryptographer>>
            {
                {
                    0,
                    () =>
                    {

                        var newControl = new BitCryptographerControl();
                        ReplaceControl(newControl);
                        newControl.numericUpDownBias.ValueChanged += CryptographerControl_ValueChanged;
                        return new BitCryptographer((int)newControl.numericUpDownBias.Value);
                    }
                },
                {
                    1,
                    () =>
                    {
                        var newControl = new KeyCryptographerControl();
                        ReplaceControl(newControl);
                        CryptographerControl.ValuesChanged += CryptographerControl_ValueChanged;
                        return new KeyCryptographer(newControl.textBoxKey.Text);
                    }
                }
            };
            Cryptographer = cryptoTypes[index]();
        }

        protected void ReplaceControl(CryptographerControl newControl)
        {
            Controls.Remove(CryptographerControl);
            newControl.Location = CryptographerControl.Location;
            Controls.Add(newControl);
            CryptographerControl = newControl;
        }
    }
}
