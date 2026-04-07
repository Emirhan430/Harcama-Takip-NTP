namespace NTP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            dataGridView1 = new DataGridView();
            txtID = new TextBox();
            label2 = new Label();
            txtAdSoyad = new TextBox();
            label3 = new Label();
            label4 = new Label();
            txtMiktar = new TextBox();
            label5 = new Label();
            txtAdres = new RichTextBox();
            label6 = new Label();
            txtFatura = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            dateTimePicker1 = new DateTimePicker();
            btnEkle = new Button();
            btnDuzenle = new Button();
            btnSil = new Button();
            btnTemizle = new Button();
            btnCikis = new Button();
            btnExcelAktar = new Button();
            txtKategori = new ComboBox();
            txtSebep = new RichTextBox();
            label9 = new Label();
            txtTelefon = new MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label1.Location = new Point(25, 274);
            label1.Name = "label1";
            label1.Size = new Size(28, 18);
            label1.TabIndex = 0;
            label1.Text = "ID";
            label1.Click += label1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Top;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1040, 241);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // txtID
            // 
            txtID.Enabled = false;
            txtID.Location = new Point(77, 271);
            txtID.Margin = new Padding(3, 4, 3, 4);
            txtID.Name = "txtID";
            txtID.Size = new Size(89, 27);
            txtID.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label2.Location = new Point(228, 274);
            label2.Name = "label2";
            label2.Size = new Size(86, 18);
            label2.TabIndex = 3;
            label2.Text = "Ad Soyad";
            // 
            // txtAdSoyad
            // 
            txtAdSoyad.Location = new Point(327, 274);
            txtAdSoyad.Margin = new Padding(3, 4, 3, 4);
            txtAdSoyad.Name = "txtAdSoyad";
            txtAdSoyad.Size = new Size(150, 27);
            txtAdSoyad.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label3.Location = new Point(520, 276);
            label3.Name = "label3";
            label3.Size = new Size(68, 18);
            label3.TabIndex = 5;
            label3.Text = "Telefon";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label4.Location = new Point(792, 278);
            label4.Name = "label4";
            label4.Size = new Size(60, 18);
            label4.TabIndex = 7;
            label4.Text = "Miktar";
            // 
            // txtMiktar
            // 
            txtMiktar.Location = new Point(864, 275);
            txtMiktar.Margin = new Padding(3, 4, 3, 4);
            txtMiktar.Name = "txtMiktar";
            txtMiktar.Size = new Size(150, 27);
            txtMiktar.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label5.Location = new Point(25, 356);
            label5.Name = "label5";
            label5.Size = new Size(56, 18);
            label5.TabIndex = 9;
            label5.Text = "Adres";
            // 
            // txtAdres
            // 
            txtAdres.Location = new Point(25, 392);
            txtAdres.Margin = new Padding(3, 4, 3, 4);
            txtAdres.Name = "txtAdres";
            txtAdres.Size = new Size(502, 102);
            txtAdres.TabIndex = 10;
            txtAdres.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label6.Location = new Point(572, 392);
            label6.Name = "label6";
            label6.Size = new Size(132, 18);
            label6.TabIndex = 11;
            label6.Text = "Fatura Durumu";
            label6.Click += label6_Click;
            // 
            // txtFatura
            // 
            txtFatura.FormattingEnabled = true;
            txtFatura.Location = new Point(572, 431);
            txtFatura.Margin = new Padding(3, 4, 3, 4);
            txtFatura.Name = "txtFatura";
            txtFatura.Size = new Size(199, 28);
            txtFatura.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label7.Location = new Point(25, 534);
            label7.Name = "label7";
            label7.Size = new Size(78, 18);
            label7.TabIndex = 13;
            label7.Text = "Kategori";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label8.Location = new Point(911, 392);
            label8.Name = "label8";
            label8.Size = new Size(49, 18);
            label8.TabIndex = 15;
            label8.Text = "Tarih";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(854, 428);
            dateTimePicker1.Margin = new Padding(3, 4, 3, 4);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(160, 27);
            dateTimePicker1.TabIndex = 16;
            // 
            // btnEkle
            // 
            btnEkle.BackColor = Color.SandyBrown;
            btnEkle.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnEkle.Location = new Point(381, 586);
            btnEkle.Margin = new Padding(3, 4, 3, 4);
            btnEkle.Name = "btnEkle";
            btnEkle.Size = new Size(168, 36);
            btnEkle.TabIndex = 17;
            btnEkle.Text = "Ekle";
            btnEkle.UseVisualStyleBackColor = false;
            btnEkle.Click += btnEkle_Click;
            // 
            // btnDuzenle
            // 
            btnDuzenle.BackColor = Color.SandyBrown;
            btnDuzenle.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnDuzenle.Location = new Point(381, 668);
            btnDuzenle.Margin = new Padding(3, 4, 3, 4);
            btnDuzenle.Name = "btnDuzenle";
            btnDuzenle.Size = new Size(168, 36);
            btnDuzenle.TabIndex = 18;
            btnDuzenle.Text = "Düzenle";
            btnDuzenle.UseVisualStyleBackColor = false;
            btnDuzenle.Click += btnDuzenle_Click;
            // 
            // btnSil
            // 
            btnSil.BackColor = Color.SandyBrown;
            btnSil.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnSil.Location = new Point(597, 586);
            btnSil.Margin = new Padding(3, 4, 3, 4);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(168, 36);
            btnSil.TabIndex = 19;
            btnSil.Text = "Sil";
            btnSil.UseVisualStyleBackColor = false;
            btnSil.Click += btnSil_Click;
            // 
            // btnTemizle
            // 
            btnTemizle.BackColor = Color.SandyBrown;
            btnTemizle.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnTemizle.Location = new Point(597, 668);
            btnTemizle.Margin = new Padding(3, 4, 3, 4);
            btnTemizle.Name = "btnTemizle";
            btnTemizle.Size = new Size(168, 36);
            btnTemizle.TabIndex = 20;
            btnTemizle.Text = "Temizle";
            btnTemizle.UseVisualStyleBackColor = false;
            btnTemizle.Click += btnTemizle_Click;
            // 
            // btnCikis
            // 
            btnCikis.BackColor = Color.SandyBrown;
            btnCikis.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnCikis.Location = new Point(819, 668);
            btnCikis.Margin = new Padding(3, 4, 3, 4);
            btnCikis.Name = "btnCikis";
            btnCikis.Size = new Size(168, 36);
            btnCikis.TabIndex = 21;
            btnCikis.Text = "Çıkış";
            btnCikis.UseVisualStyleBackColor = false;
            btnCikis.Click += btnCikis_Click;
            // 
            // btnExcelAktar
            // 
            btnExcelAktar.BackColor = Color.SandyBrown;
            btnExcelAktar.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnExcelAktar.Location = new Point(819, 586);
            btnExcelAktar.Margin = new Padding(3, 4, 3, 4);
            btnExcelAktar.Name = "btnExcelAktar";
            btnExcelAktar.Size = new Size(168, 36);
            btnExcelAktar.TabIndex = 22;
            btnExcelAktar.Text = "Excele Aktar";
            btnExcelAktar.UseVisualStyleBackColor = false;
            btnExcelAktar.Click += btnExcelAktar_Click;
            // 
            // txtKategori
            // 
            txtKategori.FormattingEnabled = true;
            txtKategori.Location = new Point(25, 567);
            txtKategori.Margin = new Padding(3, 4, 3, 4);
            txtKategori.Name = "txtKategori";
            txtKategori.Size = new Size(276, 28);
            txtKategori.TabIndex = 23;
            // 
            // txtSebep
            // 
            txtSebep.Location = new Point(25, 668);
            txtSebep.Margin = new Padding(3, 4, 3, 4);
            txtSebep.Name = "txtSebep";
            txtSebep.Size = new Size(276, 72);
            txtSebep.TabIndex = 24;
            txtSebep.Text = "";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label9.Location = new Point(25, 625);
            label9.Name = "label9";
            label9.Size = new Size(59, 18);
            label9.TabIndex = 25;
            label9.Text = "Sebep";
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(601, 273);
            txtTelefon.Margin = new Padding(3, 4, 3, 4);
            txtTelefon.Mask = "(999) 000-0000";
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Size = new Size(140, 27);
            txtTelefon.TabIndex = 26;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1040, 776);
            Controls.Add(txtTelefon);
            Controls.Add(label9);
            Controls.Add(txtSebep);
            Controls.Add(txtKategori);
            Controls.Add(btnExcelAktar);
            Controls.Add(btnCikis);
            Controls.Add(btnTemizle);
            Controls.Add(btnSil);
            Controls.Add(btnDuzenle);
            Controls.Add(btnEkle);
            Controls.Add(dateTimePicker1);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(txtFatura);
            Controls.Add(label6);
            Controls.Add(txtAdres);
            Controls.Add(label5);
            Controls.Add(txtMiktar);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtAdSoyad);
            Controls.Add(label2);
            Controls.Add(txtID);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Günlük Harcama Takip Sistemi";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private TextBox txtID;
        private Label label2;
        private TextBox txtAdSoyad;
        private Label label3;
        private Label label4;
        private TextBox txtMiktar;
        private Label label5;
        private RichTextBox txtAdres;
        private Label label6;
        private ComboBox txtFatura;
        private Label label7;
        private Label label8;
        private DateTimePicker dateTimePicker1;
        private Button btnEkle;
        private Button btnDuzenle;
        private Button btnSil;
        private Button btnTemizle;
        private Button btnCikis;
        private Button btnExcelAktar;
        private ComboBox txtKategori;
        private RichTextBox txtSebep;
        private Label label9;
        private MaskedTextBox txtTelefon;
    }
}
