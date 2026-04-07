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
            // --- GITHUB ÝÇÝN EKLENEN KONTROL ---
            // Eðer dosya bilgisayarda yoksa (ilk kez açýlýyorsa), 
            // programýn çökmemesi için boþ bir XML þablonu oluþturuluyor.
            if (!File.Exists(@"harcamatakip5.xml"))
            {
                XDocument baslangicDosyasi = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Müþteriler") // Tüm kayýtlarýn tutulacaðý ana düðüm
                );
                baslangicDosyasi.Save(@"harcamatakip5.xml");
            }
            // -----------------------------------

            txtFatura.Items.Add("Faturalý");
            txtFatura.Items.Add("Faturasýz");
            txtFatura.Items.Add("Dijital");

            txtKategori.Items.Add("Market");
            txtKategori.Items.Add("Ulaþým");
            txtKategori.Items.Add("Ödemeler");
            txtKategori.Items.Add("Yeme - Ýçme");
            txtKategori.Items.Add("Eðitim");
            txtKategori.Items.Add("Saðlýk");
            txtKategori.Items.Add("Eðlence");
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

                // Eðer dosya yeni oluþturulduysa ve içi boþsa hata vermemesi için kontrol
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
                var idler = xdosya.Root.Elements("Müþteri")
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
                MessageBox.Show("Lütfen Fatura Durumu ve Kategori seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string faturadurum = txtFatura.SelectedItem.ToString();
            string kategori = txtKategori.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Ad Soyad Boþ Olmamalý!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            XDocument xdosya = XDocument.Load(@"harcamatakip5.xml");
            XElement rootelement = xdosya.Root;
            XElement element = new XElement("Müþteri");

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

            MessageBox.Show("Kayýt Baþarýyla Eklendi!");
            KayitGetir();
            Temizle();
            YeniIDOlustur();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (txtFatura.SelectedItem == null || txtKategori.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Fatura Durumu ve Kategori seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string faturadurum = txtFatura.SelectedItem.ToString();
            string kategori = txtKategori.SelectedItem.ToString();

            XDocument xdosya = XDocument.Load(@"harcamatakip5.xml");
            XElement element = xdosya.Element("Müþteriler")
                .Elements("Müþteri")
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
                MessageBox.Show("Kayýt Baþarýyla Güncellendi!");
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
                MessageBox.Show("Kayýt Baþarýyla Silindi!");
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
                    Filter = "Excel Dosyasý|*.xlsx",
                    Title = "Excel Dosyasý Kaydet",
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
                        MessageBox.Show("Excel dosyasý baþarýyla oluþturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aktarýlacak veri bulunamadý!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            DialogResult cikis = MessageBox.Show("Çýkýþ yaparsanýz bazý bilgiler kaybolabilir. Çýkmak istediðinizden emin misiniz?", "Programdan Çýkýþ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (cikis == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}