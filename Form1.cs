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

        protected byte[] encryptedData;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CryptographerControl = cryptographerControl1;
            buttonEncrypt.Enabled = false;
            buttonDecrypt.Enabled = false;
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
                },
                {
                    typeof(GOSTCryptographer),
                    () =>
                    {
                        var control = (GOSTCryptographerControl)CryptographerControl;
                        var cryptographer = (GOSTCryptographer)Cryptographer;
                        cryptographer.Key = control.textBoxKey.Text;
                    }
                },
                {
                    typeof(RSACryptographer),
                    () =>
                    {
                        var control = (RSACryptographerControl)CryptographerControl;
                        var cryptographer = (RSACryptographer)Cryptographer;

                        var p = Convert.ToUInt64(control.textBoxP.Text);
                        var q = Convert.ToUInt64(control.textBoxQ.Text);
                        cryptographer.SetKeys(p, q);

                        var nValue = cryptographer.n;
                        var eValue = cryptographer.e;
                        control.textBoxN.Text = Convert.ToString(nValue);
                        control.textBoxE.Text = Convert.ToString(eValue);
                    }
                }
            };
            cryptoTypes[Cryptographer.GetType()]();
        }

        private void ButtonEncrypt_Click(object sender, EventArgs e)
        {
            var data = Encoding.ASCII.GetBytes(textBoxOriginal.Text);
            encryptedData = Cryptographer.Encrypt(data);    // Храним зашифрованный текст в переменной, т.к. textbox стирает служебные символы.
            textBoxEncrypted.Text = Encoding.ASCII.GetString(encryptedData);
        }

        private void ButtonDecrypt_Click(object sender, EventArgs e)
        {
            var bytes = Cryptographer.Decrypt(encryptedData);
            textBoxDecrypted.Text = Encoding.ASCII.GetString(bytes);
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
                        newControl.textBoxKey.TextChanged += CryptographerControl_ValueChanged;
                        return new KeyCryptographer(newControl.textBoxKey.Text);
                    }
                },
                {
                    2,
                    () =>
                    {
                        var newControl = new GOSTCryptographerControl();
                        ReplaceControl(newControl);
                        newControl.textBoxKey.TextChanged += CryptographerControl_ValueChanged;
                        return new GOSTCryptographer(newControl.textBoxKey.Text);
                    }
                },
                {
                    3,
                    () =>
                    {
                        var newControl = new RSACryptographerControl();
                        ReplaceControl(newControl);
                        newControl.textBoxP.TextChanged += CryptographerControl_ValueChanged;
                        newControl.textBoxQ.TextChanged += CryptographerControl_ValueChanged;
                        var p = Convert.ToUInt64(newControl.textBoxP.Text);
                        var q = Convert.ToUInt64(newControl.textBoxQ.Text);
                        
                        var cryptographer = new RSACryptographer(p, q);
                        var nValue = cryptographer.n;
                        var eValue = cryptographer.e;
                        newControl.textBoxN.Text = Convert.ToString(nValue);
                        newControl.textBoxE.Text = Convert.ToString(eValue);
                        return cryptographer;
                    }
                }
            };

            buttonEncrypt.Enabled = true;
            buttonDecrypt.Enabled = true;
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
