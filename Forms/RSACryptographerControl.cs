using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Forms
{
    public class RSACryptographerControl : CryptographerControl
    {
        public System.Windows.Forms.TextBox textBoxP;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxQ;
        public System.Windows.Forms.TextBox textBoxN;
        public System.Windows.Forms.TextBox textBoxE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Label label2;

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            this.textBoxP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxQ = new System.Windows.Forms.TextBox();
            this.textBoxN = new System.Windows.Forms.TextBox();
            this.textBoxE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxP
            // 
            this.textBoxP.Location = new System.Drawing.Point(41, 45);
            this.textBoxP.Name = "textBoxP";
            this.textBoxP.Size = new System.Drawing.Size(100, 22);
            this.textBoxP.TabIndex = 0;
            this.textBoxP.Text = "103";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "p:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "q:";
            // 
            // textBoxQ
            // 
            this.textBoxQ.Location = new System.Drawing.Point(41, 82);
            this.textBoxQ.Name = "textBoxQ";
            this.textBoxQ.Size = new System.Drawing.Size(100, 22);
            this.textBoxQ.TabIndex = 0;
            this.textBoxQ.Text = "101";
            // 
            // textBoxN
            // 
            this.textBoxN.Location = new System.Drawing.Point(211, 45);
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.Size = new System.Drawing.Size(100, 22);
            this.textBoxN.TabIndex = 0;
            // 
            // textBoxE
            // 
            this.textBoxE.Location = new System.Drawing.Point(211, 82);
            this.textBoxE.Name = "textBoxE";
            this.textBoxE.Size = new System.Drawing.Size(100, 22);
            this.textBoxE.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "n:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "e:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Public key:";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(53, 110);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 2;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // RSACryptographerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxN);
            this.Controls.Add(this.textBoxQ);
            this.Controls.Add(this.textBoxP);
            this.Name = "RSACryptographerControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            var maxIndex = 64;
            var primes = lab1_Encryption_.Classes.RSACryptographer.GeneratePrimesNaive(maxIndex).ToArray();
            var rand = new Random();
            var pIndex = rand.Next(maxIndex);
            var qIndex = rand.Next(maxIndex);

            textBoxP.Text = Convert.ToString(primes[pIndex]);
            textBoxQ.Text = Convert.ToString(primes[qIndex]);
        }
    }
}
