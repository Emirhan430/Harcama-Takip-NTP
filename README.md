# Kişisel Harcama ve Bütçe Takip Uygulaması

Nesne Tabanlı Programlama (OOP) prensipleri kullanılarak geliştirilmiş, C# tabanlı bir masaüstü finans ve bütçe yönetimi uygulamasıdır. Kullanıcıların günlük harcamalarını takip etmelerini, verileri XML formatında saklamalarını ve raporlama için Excel'e aktarmalarını sağlar.

## Özellikler
* **Kayıt Yönetimi (CRUD):** Yeni harcama/gelir ekleme, mevcut kayıtları düzenleme ve silme işlemleri.
* **Lokal Veritabanı (XML):** Tüm veriler `harcamatakip5.xml` dosyası üzerinde lokal olarak saklanır. Uygulama ilk açıldığında veritabanı dosyası yoksa otomatik olarak oluşturulur, böylece her bilgisayarda sorunsuz çalışır.
* **Excel'e Dışa Aktarma:** `ClosedXML` kütüphanesi kullanılarak tüm harcama geçmişi tek tıkla `.xlsx` formatında dışa aktarılabilir.
* **Dinamik ID Yönetimi:** Yeni eklenen kayıtlara algoritmik olarak otomatik ID atanır.
* **Kategori ve Fatura Takibi:** Harcamalar belirli kategorilere (Market, Ulaşım, Sağlık vb.) ve fatura durumuna göre sınıflandırılır.

## Kullanılan Teknolojiler
* **Programlama Dili:** C#
* **Arayüz (GUI):** Windows Forms
* **Veri İşleme:** XML (LINQ to XML, XmlReader)
* **Ekstra Kütüphaneler:** ClosedXML (Excel işlemleri için)

## Nasıl Çalıştırılır?
1. Bu repository'yi bilgisayarınıza klonlayın veya `.zip` olarak indirin.
2. Klasör içindeki `NTP.sln` dosyasını Visual Studio ile açın.
3. Üst menüden `Start` (Başlat) butonuna basarak uygulamayı çalıştırın.
