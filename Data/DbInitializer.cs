using MutluSepet.Models;
using System.Linq;

namespace MutluSepet.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Veritabanı varsa zaten oluşturulmuş, yoksa oluştur
            context.Database.EnsureCreated();

            // Kategori ekleme (eğer boşsa)
            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Kadın" },
                    new Category { Name = "Erkek" },
                    new Category { Name = "Anne & Çocuk" },
                    new Category { Name = "Ev & Yaşam" },
                    new Category { Name = "Süpermarket" },
                    new Category { Name = "Kozmetik" },
                    new Category { Name = "Ayakkabı & Çanta" },
                    new Category { Name = "Elektronik" },
                    new Category { Name = "Çok Satanlar" },
                    new Category { Name = "Yeni" },
                    new Category { Name = "Flaş Ürünler" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // İstersen başlangıç ürünlerini de buraya ekleyebilirsin
        }
    }
}
