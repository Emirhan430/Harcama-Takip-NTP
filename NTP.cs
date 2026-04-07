using System.IO;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using ClosedXML.Excel;
using System;
using System.Linq;
using System.Windows.Forms;

namespace NTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // --- GITHUB ���N EKLENEN KONTROL ---
            // E�er dosya bilgisayarda yoksa (ilk kez a��l�yorsa), 
            // program�n ��kmemesi i�in bo� bir XML �ablonu olu�turuluyor.
            if (!File.Exists(@"harcamatakip5.xml"))
            {
                XDocument baslangicDosyasi = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("M��teriler") // T�m kay�tlar�n tutulaca�� ana d���m
                );
                baslangicDosyasi.Save(@"harcamatakip5.xml");
            }
            // -----------------------------------

            txtFatura.Items.Add("Fatural�");
            txtFatura.Items.Add("Faturas�z");
            txtFatura.Items.Add("Dijital");

            txtKategori.Items.Add("Market");
            txtKategori.Items.Add("Ula��m");
            txtKategori.Items.Add("�demeler");
            txtKategori.Items.Add("Yeme - ��me");
            txtKategori.Items.Add("E�itim");
            txtKategori.Items.Add("Sa�l�k");
            txtKategori.Items.Add("E�lence");
            txtKategori.Items.Add("Teknoloji");

            KayitGetir();
            YeniIDOlustur();
        }

        void KayitGetir()
        {
            if (File.Exists(@"harcamatakip5.xml"))
            {
                DataSet dset = new DataSet();
                XmlReader reader = XmlReader.Create(@"harcamatakip5.xml", new XmlReaderSettings());
                dset.ReadXml(reader);
                reader.Close();

                // E�er dosya yeni olu�turulduysa ve i�i bo�sa hata vermemesi i�in kontrol
                if (dset.Tables.Count > 0)
                {
                    dataGridView1.DataSource = dset.Tables[0];
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
        }

        void Temizle()
        {
            txtAdSoyad.Clear();
            txtAdres.Clear();
            txtMiktar.Clear();
            txtTelefon.Clear();
            txtSebep.Clear();
            dateTimePicker1.Value = DateTime.Today;
            txtFatura.Text = string.Empty;
            txtKategori.Text = string.Empty;
        }

        private void YeniIDOlustur()
        {
            int yeniID = 1;
            if (File.Exists("harcamatakip5.xml"))
            {
                XDocument xdosya = XDocument.Load("harcamatakip5.xml");
                var idler = xdosya.Root.Elements("M��teri")
                .Select(p => (int?)Convert.ToInt32((string)p.Element("ID")))
                .ToList();

                if (idler.Any())
                {
                    yeniID = idler.Max().Value + 1;
                }
            }
            txtID.Text = yeniID.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtFatura.SelectedItem == null || txtKategori.SelectedItem == null)
            {
                MessageBox.Show("L�tfen Fatura Durumu ve Kategori se�iniz!", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string faturadurum = txtFatura.SelectedItem.ToString();
            string kategori = txtKategori.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Ad Soyad Bo� Olmamal�!", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            XDocument xdosya = XDocument.Load(@"harcamatakip5.xml");
            XElement rootelement = xdosya.Root;
            XElement element = new XElement("M��teri");

            XElement ID = new XElement("ID", txtID.Text);
            XElement AdSoyad = new XElement("AdSoyad", txtAdSoyad.Text);
            XElement Telefon = new XElement("Telefon", txtTelefon.Text);
            XElement Tarih = new XElement("Tarih", dateTimePicker1.Text);
            XElement FaturaDurum = new XElement("FaturaDurumu", faturadurum);
            XElement Kategori = new XElement("Kategori", kategori);
            XElement Miktar = new XElement("Miktar", (txtMiktar.Text));
            XElement Adres = new XElement("Adres", txtAdres.Text);
            XElement Sebep = new XElement("Sebep", txtSebep.Text);

            element.Add(ID, AdSoyad, FaturaDurum, Miktar, Adres, Sebep, Telefon, Tarih, Kategori);
            rootelement.Add(element);
            xdosya.Save(@"harcamatakip5.xml");

            MessageBox.Show("Kay�t Ba�ar�yla Eklendi!");
            KayitGetir();
            Temizle();
            YeniIDOlustur();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (txtFatura.SelectedItem == null || txtKategori.SelectedItem == null)
            {
                MessageBox.Show("L�tfen Fatura Durumu ve Kategori se�iniz!", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string faturadurum = txtFatura.SelectedItem.ToString();
            string kategori = txtKategori.SelectedItem.ToString();

            XDocument xdosya = XDocument.Load(@"harcamatakip5.xml");
            XElement element = xdosya.Element("M��teriler")
                .Elements("M��teri")
                .FirstOrDefault(x => (string)x.Element("ID") == txtID.Text);

            if (element != null)
            {
                element.SetElementValue("ID", txtID.Text);
                element.SetElementValue("AdSoyad", txtAdSoyad.Text);
                element.SetElementValue("Telefon", txtTelefon.Text);
                element.SetElementValue("FaturaDurumu", txtFatura.Text);
                element.SetElementValue("Kategori", txtKategori.Text);
                element.SetElementValue("Tarih", dateTimePicker1.Text);
                element.SetElementValue("Miktar", (txtMiktar.Text));
                element.SetElementValue("Adres", txtAdres.Text);
                element.SetElementValue("Sebep", txtSebep.Text);

                xdosya.Save(@"harcamatakip5.xml");
                MessageBox.Show("Kay�t Ba�ar�yla G�ncellendi!");
                KayitGetir();
                Temizle();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["ID"].Value?.ToString();
                txtAdSoyad.Text = row.Cells["AdSoyad"].Value?.ToString();
                txtTelefon.Text = row.Cells["Telefon"].Value?.ToString();
                txtMiktar.Text = row.Cells["Miktar"].Value?.ToString();
                txtAdres.Text = row.Cells["Adres"].Value?.ToString();
                txtSebep.Text = row.Cells["Sebep"].Value?.ToString();
                dateTimePicker1.Text = row.Cells["Tarih"].Value?.ToString();
                string fatura = row.Cells["FaturaDurumu"].Value?.ToString();
                txtFatura.SelectedItem = txtFatura.Items.Contains(fatura) ? fatura : null;
                string kategori = row.Cells["Kategori"].Value?.ToString();
                txtKategori.SelectedItem = txtKategori.Items.Contains(kategori) ? kategori : null;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                XDocument xdosya = XDocument.Load(@"harcamatakip5.xml");
                xdosya.Root.Elements().Where(x => x.Element("ID").Value == dataGridView1.CurrentRow.Cells[0].Value.ToString()).Remove();
                xdosya.Save(@"harcamatakip5.xml");
                MessageBox.Show("Kay�t Ba�ar�yla Silindi!");
                KayitGetir();
                Temizle();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnExcelAktar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.DataSource != null)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Dosyas�|*.xlsx",
                    Title = "Excel Dosyas� Kaydet",
                    FileName = "Kisiler.xlsx"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        DataTable dt = new DataTable();
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            dt.Columns.Add(col.HeaderText);
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                DataRow dRow = dt.NewRow();
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    dRow[i] = row.Cells[i].Value?.ToString() ?? "";
                                }
                                dt.Rows.Add(dRow);
                            }
                        }
                        wb.Worksheets.Add(dt, "Kisiler");
                        wb.SaveAs(sfd.FileName);
                        MessageBox.Show("Excel dosyas� ba�ar�yla olu�turuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aktar�lacak veri bulunamad�!", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            DialogResult cikis = MessageBox.Show("��k�� yaparsan�z baz� bilgiler kaybolabilir. ��kmak istedi�inizden emin misiniz?", "Programdan ��k��", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (cikis == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
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

<?xml version="1.0" encoding="utf-8"?>
<root>
  <!--
    Microsoft ResX Schema

    Version 2.0

    The primary goals of this format is to allow a simple XML format
    that is mostly human readable. The generation and parsing of the
    various data types are done through the TypeConverter classes
    associated with the data types.

    Example:

    ... ado.net/XML headers & schema ...
    <resheader name="resmimetype">text/microsoft-resx</resheader>
    <resheader name="version">2.0</resheader>
    <resheader name="reader">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>
    <resheader name="writer">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>
    <data name="Name1"><value>this is my long string</value><comment>this is a comment</comment></data>
    <data name="Color1" type="System.Drawing.Color, System.Drawing">Blue</data>
    <data name="Bitmap1" mimetype="application/x-microsoft.net.object.binary.base64">
        <value>[base64 mime encoded serialized .NET Framework object]</value>
    </data>
    <data name="Icon1" type="System.Drawing.Icon, System.Drawing" mimetype="application/x-microsoft.net.object.bytearray.base64">
        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>
        <comment>This is a comment</comment>
    </data>

    There are any number of "resheader" rows that contain simple
    name/value pairs.

    Each data row contains a name, and value. The row also contains a
    type or mimetype. Type corresponds to a .NET class that support
    text/value conversion through the TypeConverter architecture.
    Classes that don't support this are serialized and stored with the
    mimetype set.

    The mimetype is used for serialized objects, and tells the
    ResXResourceReader how to depersist the object. This is currently not
    extensible. For a given mimetype the value must be set accordingly:

    Note - application/x-microsoft.net.object.binary.base64 is the format
    that the ResXResourceWriter will generate, however the reader can
    read any of the formats listed below.

    mimetype: application/x-microsoft.net.object.binary.base64
    value   : The object must be serialized with
            : System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            : and then encoded with base64 encoding.

    mimetype: application/x-microsoft.net.object.soap.base64
    value   : The object must be serialized with
            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter
            : and then encoded with base64 encoding.

    mimetype: application/x-microsoft.net.object.bytearray.base64
    value   : The object must be serialized into a byte array
            : using a System.ComponentModel.TypeConverter
            : and then encoded with base64 encoding.
    -->
  <xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
    <xsd:import namespace="http://www.w3.org/XML/1998/namespace" />
    <xsd:element name="root" msdata:IsDataSet="true">
      <xsd:complexType>
        <xsd:choice maxOccurs="unbounded">
          <xsd:element name="metadata">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" />
              </xsd:sequence>
              <xsd:attribute name="name" use="required" type="xsd:string" />
              <xsd:attribute name="type" type="xsd:string" />
              <xsd:attribute name="mimetype" type="xsd:string" />
              <xsd:attribute ref="xml:space" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="assembly">
            <xsd:complexType>
              <xsd:attribute name="alias" type="xsd:string" />
              <xsd:attribute name="name" type="xsd:string" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="data">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                <xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" msdata:Ordinal="1" />
              <xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3" />
              <xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4" />
              <xsd:attribute ref="xml:space" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="resheader">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" />
            </xsd:complexType>
          </xsd:element>
        </xsd:choice>
      </xsd:complexType>
    </xsd:element>
  </xsd:schema>
  <resheader name="resmimetype">
    <value>text/microsoft-resx</value>
  </resheader>
  <resheader name="version">
    <value>2.0</value>
  </resheader>
  <resheader name="reader">
    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <resheader name="writer">
    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
</root>
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="ClosedXML" Version="0.105.0" />
  </ItemGroup>

</Project>

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.14.36109.1 d17.14
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "NTP", "NTP.csproj", "{975B5C0C-F929-B8C7-8B42-4BD1B1387056}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{975B5C0C-F929-B8C7-8B42-4BD1B1387056}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{975B5C0C-F929-B8C7-8B42-4BD1B1387056}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{975B5C0C-F929-B8C7-8B42-4BD1B1387056}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{975B5C0C-F929-B8C7-8B42-4BD1B1387056}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {F5190FD9-50FD-42B9-91F9-C0846467E4BF}
	EndGlobalSection
EndGlobal

namespace NTP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
