# 🛒 MutluSepet – B2C E-Commerce Platform

**MutluSepet**, ASP.NET Core MVC mimarisi kullanılarak geliştirilmiş, rol bazlı yetkilendirmeye sahip, uçtan uca bir e-ticaret web uygulamasıdır. Proje; modern yazılım mimarisi, güvenli kimlik doğrulama ve veritabanı destekli dinamik özellikler sunar.

---

## 🚀 Proje Hakkında

Bu proje, bir e-ticaret sisteminde bulunması gereken temel işlevleri kapsayan **full-stack** bir çalışmadır.
Kullanıcılar ürünleri görüntüleyebilir, arama yapabilir, sepete ekleyebilir, favorilere alabilir, yorum yapabilir ve sipariş verebilir.
Admin kullanıcılar ise ürün, kategori ve siparişleri yönetebilir.

---

## 🛠️ Kullanılan Teknolojiler

* **Backend:** ASP.NET Core MVC 
* **Programlama Dili:** C#
* **Veritabanı:** Microsoft SQL Server
* **ORM:** Entity Framework Core (Code-First)
* **Kimlik Doğrulama:** ASP.NET Core Identity (Admin / User Rolleri)
* **Frontend:** HTML5, CSS3, Bootstrap 5
* **JavaScript:** jQuery, AJAX
* **Versiyon Kontrol:** Git & GitHub

---

## ⚙️ Temel Özellikler

### 🔐 Admin Paneli

* Rol bazlı yetkilendirme (`[Authorize(Roles = "Admin")]`)
* Ürün ekleme ve silme
* Kategori ekleme ve silme
* Siparişleri detaylı görüntüleme (kullanıcı + ürün bilgileri)

### 🛒 Kullanıcı İşlevleri

* Ürün listeleme ve kategoriye göre filtreleme
* Canlı arama (AJAX Search Suggestions)
* Sepete ürün ekleme / çıkarma
* Favori ürünler
* Ürünlere puan ve yorum ekleme
* Sipariş oluşturma (Checkout)
* Geçmiş siparişleri görüntüleme (`MyOrders`)

### 🌱 Veri Tohumlama (Seed)

* Uygulama ilk çalıştığında:

  * Roller ve admin kullanıcı oluşturulur
  * Kategoriler ve örnek ürünler otomatik eklenir

---

## 📂 Controller Yapısı

* **AdminController:** Ürün, kategori ve sipariş yönetimi
* **ProductController:** Ürün listeleme, detay, arama ve filtreleme
* **CartController:** Sepet işlemleri
* **CheckoutController:** Sipariş tamamlama süreci
* **OrdersController:** Kullanıcı sipariş geçmişi
* **CommentController:** Ürün yorumları
* **FavoriteController:** Favori ürünler
* **Identity (Razor Pages):** Giriş / Kayıt / Yetkilendirme

---

## 💾 Kurulum

1. Repoyu klonlayın:

```bash
git clone https://github.com/FirdevsKara/MutluSepet-ECommerce.git
```

2. `appsettings.json` içindeki bağlantı cümlesini SQL Server bilgilerinize göre düzenleyin.

3. Veritabanını oluşturun:

```bash
dotnet ef database update
```

4. Uygulamayı çalıştırın:

```bash
dotnet run
```

> İlk çalıştırmada örnek kategoriler ve ürünler otomatik olarak eklenir.

---


## 📷 Ekran Görüntüleri

| 🏠 Ana Sayfa | 🛒 Sepetim |
|--------------|------------|
| <img src="https://github.com/user-attachments/assets/aacd38d7-530c-4739-8be5-fd1df9fefc8b" width="100%"> | <img src="https://github.com/user-attachments/assets/6939436d-3c0e-4d76-9e5b-69ea9e7b6ce9" width="100%"> |

| ❤️ Favorilerim | 🔐 Admin Paneli |
|----------------|-----------------|
| <img src="https://github.com/user-attachments/assets/38df261b-c7c3-4317-92f6-53cecae7a604" width="100%"> | <img src="https://github.com/user-attachments/assets/3e0b0045-6c96-494f-91b6-b13f797ad76c" width="100%"> |



---

**Geliştirici: Firdevs Kara
🎓 Computer Engineering Student
💻 ASP.NET Core & Full-Stack Developer
