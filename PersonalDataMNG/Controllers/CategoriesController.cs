using DataTablesParser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalDataMNG.Data;
using PersonalDataMNG.Models;
using PersonalDataMNG.Models.CommonViewModel;

namespace PersonalDataMNG.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _Category = await _context.Category.ToListAsync();
            if (_Category.Count < 1)
                await CreateTestData();
            return View();
        }

        public IActionResult Data()
        {
            var listCategory = _context.Category.Where(x => x.Cancelled == false).OrderByDescending(x => x.Id).AsQueryable();
            var parser = new Parser<Category>(Request.Form, listCategory);
            return Json(parser.Parse());
        }

        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            var vm = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            Category vm = new();
            if (id > 0) vm = await _context.Category.Where(x => x.Id == id).FirstOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Category vm)
        {
            JsonResultViewModel _JsonResultViewModel = new();
            try
            {
                if (ModelState.IsValid)
                {
                    Category _Categories = new();
                    if (vm.Id > 0)
                    {
                        _Categories = await _context.Category.FindAsync(vm.Id);

                        vm.CreatedDate = _Categories.CreatedDate;
                        vm.CreatedBy = _Categories.CreatedBy;
                        vm.ModifiedDate = DateTime.Now;
                        vm.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Entry(_Categories).CurrentValues.SetValues(vm);
                        await _context.SaveChangesAsync();

                        _JsonResultViewModel.AlertMessage = "Category Updated Successfully. ID: " + _Categories.Id;
                        _JsonResultViewModel.IsSuccess = true;
                        return new JsonResult(_JsonResultViewModel);
                    }
                    else
                    {
                        _Categories = vm;
                        _Categories.CreatedDate = DateTime.Now;
                        _Categories.ModifiedDate = DateTime.Now;
                        _Categories.CreatedBy = HttpContext.User.Identity.Name;
                        _Categories.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Add(_Categories);
                        await _context.SaveChangesAsync();

                        _JsonResultViewModel.AlertMessage = "Category Created Successfully. ID: " + _Categories.Id;
                        _JsonResultViewModel.IsSuccess = true;
                        return new JsonResult(_JsonResultViewModel);
                    }
                }
                _JsonResultViewModel.AlertMessage = "Operation failed.";
                _JsonResultViewModel.IsSuccess = false;
                return new JsonResult(_JsonResultViewModel);
            }
            catch (Exception ex)
            {
                _JsonResultViewModel.IsSuccess = false;
                _JsonResultViewModel.AlertMessage = ex.Message;
                return new JsonResult(_JsonResultViewModel);
                throw;
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _Categories = await _context.Category.FindAsync(id);
                _Categories.ModifiedDate = DateTime.Now;
                _Categories.ModifiedBy = HttpContext.User.Identity.Name;
                _Categories.Cancelled = true;

                _context.Update(_Categories);
                await _context.SaveChangesAsync();
                return new JsonResult(_Categories);
            }
            catch (Exception)
            {
                throw;
            }
        }




        private async Task CreateTestData()
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

        private IEnumerable<Category> GetCategoryList()
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
