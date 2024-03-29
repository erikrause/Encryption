﻿namespace lab1_Encryption_
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxEncrypted = new System.Windows.Forms.TextBox();
            this.textBoxOriginal = new System.Windows.Forms.TextBox();
            this.textBoxDecrypted = new System.Windows.Forms.TextBox();
            this.buttonEncrypt = new System.Windows.Forms.Button();
            this.buttonDecrypt = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCryptographerType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cryptographerControl1 = new lab1_Encryption_.Forms.CryptographerControl();
            this.SuspendLayout();
            // 
            // textBoxEncrypted
            // 
            this.textBoxEncrypted.Location = new System.Drawing.Point(270, 22);
            this.textBoxEncrypted.Multiline = true;
            this.textBoxEncrypted.Name = "textBoxEncrypted";
            this.textBoxEncrypted.Size = new System.Drawing.Size(171, 180);
            this.textBoxEncrypted.TabIndex = 0;
            // 
            // textBoxOriginal
            // 
            this.textBoxOriginal.Location = new System.Drawing.Point(12, 22);
            this.textBoxOriginal.Multiline = true;
            this.textBoxOriginal.Name = "textBoxOriginal";
            this.textBoxOriginal.Size = new System.Drawing.Size(171, 180);
            this.textBoxOriginal.TabIndex = 0;
            this.textBoxOriginal.Text = "Hello world!";
            // 
            // textBoxDecrypted
            // 
            this.textBoxDecrypted.Location = new System.Drawing.Point(528, 22);
            this.textBoxDecrypted.Multiline = true;
            this.textBoxDecrypted.Name = "textBoxDecrypted";
            this.textBoxDecrypted.Size = new System.Drawing.Size(171, 180);
            this.textBoxDecrypted.TabIndex = 0;
            // 
            // buttonEncrypt
            // 
            this.buttonEncrypt.Location = new System.Drawing.Point(189, 94);
            this.buttonEncrypt.Name = "buttonEncrypt";
            this.buttonEncrypt.Size = new System.Drawing.Size(75, 23);
            this.buttonEncrypt.TabIndex = 1;
            this.buttonEncrypt.Text = "Encrypt";
            this.buttonEncrypt.UseVisualStyleBackColor = true;
            this.buttonEncrypt.Click += new System.EventHandler(this.ButtonEncrypt_Click);
            // 
            // buttonDecrypt
            // 
            this.buttonDecrypt.Location = new System.Drawing.Point(447, 94);
            this.buttonDecrypt.Name = "buttonDecrypt";
            this.buttonDecrypt.Size = new System.Drawing.Size(75, 23);
            this.buttonDecrypt.TabIndex = 1;
            this.buttonDecrypt.Text = "Decrypt";
            this.buttonDecrypt.UseVisualStyleBackColor = true;
            this.buttonDecrypt.Click += new System.EventHandler(this.ButtonDecrypt_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 428);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(9, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(694, 2);
            this.label1.TabIndex = 3;
            // 
            // comboBoxCryptographerType
            // 
            this.comboBoxCryptographerType.FormattingEnabled = true;
            this.comboBoxCryptographerType.Items.AddRange(new object[] {
            "Шифрование смещением битов ( Лаб. № 1)",
            "Шифрование с ключом (Лаб № 2)",
            "ГОСТ (Лаб №3)",
            "RSA (Лаб №4)"});
            this.comboBoxCryptographerType.Location = new System.Drawing.Point(288, 223);
            this.comboBoxCryptographerType.Name = "comboBoxCryptographerType";
            this.comboBoxCryptographerType.Size = new System.Drawing.Size(211, 21);
            this.comboBoxCryptographerType.TabIndex = 7;
            this.comboBoxCryptographerType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCryptographerType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Тип шифрования: ";
            // 
            // cryptographerControl1
            // 
            this.cryptographerControl1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cryptographerControl1.Location = new System.Drawing.Point(187, 253);
            this.cryptographerControl1.Name = "cryptographerControl1";
            this.cryptographerControl1.Size = new System.Drawing.Size(335, 150);
            this.cryptographerControl1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 428);
            this.Controls.Add(this.cryptographerControl1);
            this.Controls.Add(this.comboBoxCryptographerType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.buttonDecrypt);
            this.Controls.Add(this.buttonEncrypt);
            this.Controls.Add(this.textBoxOriginal);
            this.Controls.Add(this.textBoxDecrypted);
            this.Controls.Add(this.textBoxEncrypted);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxEncrypted;
        private System.Windows.Forms.TextBox textBoxOriginal;
        private System.Windows.Forms.TextBox textBoxDecrypted;
        private System.Windows.Forms.Button buttonEncrypt;
        private System.Windows.Forms.Button buttonDecrypt;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCryptographerType;
        private System.Windows.Forms.Label label4;
        private Forms.CryptographerControl cryptographerControl1;
    }
}

