using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Forms
{
    class KeyCryptographerControl : CryptographerControl
    {
        public System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Label label1;

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key: ";
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(112, 62);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(131, 20);
            this.textBoxKey.TabIndex = 1;
            // 
            // KeyCryptographerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.textBoxKey);
            this.Controls.Add(this.label1);
            this.Name = "KeyCryptographerControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
