using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MutluSepet.Data;
using MutluSepet.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Dummy email sender (test için, gerçek email göndermez)
builder.Services.AddSingleton<IEmailSender, DummyEmailSender>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// DbContext ekle (SQL Server bağlantısı)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity ekle (kullanıcı + rol)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // staj projesi için false
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");
app.MapRazorPages();

// ----------------- SEED İŞLEMİ -----------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        // 1️⃣ Identity (admin + roller)
        await IdentityDataInitializer.SeedAsync(services);
        logger.LogInformation("Identity seed tamamlandı.");

        var _context = services.GetRequiredService<ApplicationDbContext>();

        // ----------------- SIFIRLA -----------------
        _context.Products.RemoveRange(_context.Products);
        _context.Categories.RemoveRange(_context.Categories);
        _context.SaveChanges();
        logger.LogInformation("Önceki ürünler ve kategoriler temizlendi.");

        // 2a. Kategoriler
        var categoriesToSeed = new List<string>
        {
            "Kadın", "Erkek", "Anne & Çocuk", "Ev & Yaşam", "Süpermarket",
            "Kozmetik", "Ayakkabı & Çanta", "Elektronik", "Çok Satanlar",
            "Yeni", "Flaş Ürünler",
            // Yeni eklediğin kategoriler buraya eklenebilir
            
        };

        foreach (var categoryName in categoriesToSeed)
        {
            _context.Categories.Add(new Category { Name = categoryName });
        }

        _context.SaveChanges();
        logger.LogInformation("Kategori seed tamamlandı.");

        // Kategorileri çek (var olan tüm kategoriler)
        var categoriesDict = _context.Categories.ToDictionary(c => c.Name, c => c.Id);

        // 2b. Ürünler
        var productsToSeed = new List<Product>
        {
            new Product { Name = "Akıllı Telefon", Price = 4500, Description = "Yeni model telefonu", ImageUrl = "/images/telefon.jpg", Stock = 10, CategoryId = categoriesDict["Elektronik"] },
            new Product { Name = "Kadın Çanta", Price = 1200, Description = "Kırmızı Kadın Çanta", ImageUrl = "/images/Kırmızıcanta.jpg", Stock = 15, CategoryId = categoriesDict["Kadın"] },
            new Product { Name = "Anker Kablosuz Kulaklık", Price = 300, Description = "Kablosuz kulaklık", ImageUrl = "/images/KablosuzKulaklik.jpg", Stock = 20, CategoryId = categoriesDict["Elektronik"] },
            new Product { Name = "Huawei Kablosuz Kulaklık", Price = 1500, Description = "Kablosuz kulaklık", ImageUrl = "/images/KablosuzKulaklik2.jpg", Stock = 8, CategoryId = categoriesDict["Elektronik"] },
            new Product { Name = "Akıllı Saat", Price = 1500, Description = "Akıllı Saat", ImageUrl = "/images/akıllıSaat.jpg", Stock = 8, CategoryId = categoriesDict["Elektronik"] },
            new Product { Name = "Kırmızı Topuklu Ayakkabı", Price = 1000, Description = "Kadın Ayakkabı", ImageUrl = "/images/KırmızıTopukluAyakkabı.jpg", Stock = 8, CategoryId = categoriesDict["Kadın"] },
            new Product { Name = "Siyah Elbise", Price = 500, Description = "Kadın Elbise", ImageUrl = "/images/siyahElbise.jpg", Stock = 8, CategoryId = categoriesDict["Kadın"] },
            new Product { Name = "Spor Ayakkabı", Price = 1250, Description = "Spor Ayakkabı", ImageUrl = "/images/sporAyakkabıErkek.jpg", Stock = 8, CategoryId = categoriesDict["Erkek"] },
            new Product { Name = "Kırmızı Elbise", Price = 850, Description = "Kadın Elbise", ImageUrl = "/images/kırmızıElbise.jpg", Stock = 8, CategoryId = categoriesDict["Kadın"] },
            new Product { Name = "Pembe Elbise", Price = 850, Description = "Kadın Elbise", ImageUrl = "/images/PembeElbise.jpg", Stock = 8, CategoryId = categoriesDict["Kadın"] },
            new Product { Name = "Beyaz Pantolon", Price = 850, Description = "Kadın Pantolon", ImageUrl = "/images/beyazPantolon.jpg", Stock = 8, CategoryId = categoriesDict["Kadın"] },
            new Product { Name = "Beyaz Pantolon", Price = 850, Description = "Erkek Pantolon", ImageUrl = "/images/erkekPantolon.jpg", Stock = 8, CategoryId = categoriesDict["Erkek"] },
            new Product { Name = "Mavi Gömlek", Price = 850, Description = "Gömlek", ImageUrl = "/images/maviGömlekKadın.jpg", Stock = 8, CategoryId = categoriesDict["Kadın"] },
            new Product { Name = "Mavi Gömlek", Price = 850, Description = "Gömlek", ImageUrl = "/images/maviGömlek.jpg", Stock = 8, CategoryId = categoriesDict["Erkek"] }
        };

        foreach (var product in productsToSeed)
        {
            _context.Products.Add(product);
        }

        _context.SaveChanges();
        logger.LogInformation("Ürün seed tamamlandı.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Seed sırasında hata oluştu.");
    }
}

app.Run();
