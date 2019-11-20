using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Forms
{
    class BitCryptographerControl : CryptographerControl
    {
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownBias;

        protected override void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownBias = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBias)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Смещение битов:";
            // 
            // numericUpDownBias
            // 
            this.numericUpDownBias.Location = new System.Drawing.Point(158, 65);
            this.numericUpDownBias.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownBias.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            -2147483648});
            this.numericUpDownBias.Name = "numericUpDownBias";
            this.numericUpDownBias.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownBias.TabIndex = 6;
            this.numericUpDownBias.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownBias.ValueChanged += ValuesChanged;
            // 
            // BitCryptographerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownBias);
            this.Name = "BitCryptographerControl";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
