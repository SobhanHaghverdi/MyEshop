

namespace MyEshop.Data.Services
{
    public class GroupRepository : IGroupRepository
    {
        private MyEshopContext _context;
        public GroupRepository(MyEshopContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
            return _context.Categories
                 .Select(c => new ShowGroupViewModel()
                 {
                     GroupId = c.Id,
                     GroupName = c.Name,
                     ProductCount = c.CategoryToProducts.Count(g => g.CategoryId == c.Id)
                 }).ToList();
        }
    }
}
