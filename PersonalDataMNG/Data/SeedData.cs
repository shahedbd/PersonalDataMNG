using PersonalDataMNG.Models;

namespace PersonalDataMNG.Data
{
    public static class SeedData
    {
        public static async Task CreateCreatedDateList(ApplicationDbContext _context)
        {
            foreach (var item in GetCategoryList())
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.CreatedBy = "Admin";
                item.ModifiedBy = "Admin";
                _context.Category.Add(item);
                await _context.SaveChangesAsync();
            }
        }
        private static IEnumerable<Category> GetCategoryList()
        {
            return new List<Category>
            {
                new Category { Name = "Item Category 01", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 02", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 03", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 04", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 05", Description = "Description of your category item: lorem ipsum" },

                new Category { Name = "Item Category 06", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 07", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 08", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 09", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 10", Description = "Description of your category item: lorem ipsum" },

                new Category { Name = "Item Category 11", Description = "Description of your category item: lorem ipsum" },
                new Category { Name = "Item Category 12", Description = "Description of your category item: lorem ipsum" },
            };
        }
    }
}
