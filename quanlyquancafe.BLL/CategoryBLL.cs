using System.Data;
using quanlyquancafe.DAL;
using quanlyquancafe.DTO;

namespace quanlyquancafe.BLL
{
    public class CategoryBLL
    {
        private CategoryDAL categoryDAL = new CategoryDAL();

        public DataTable GetAllCategories()
        {
            return categoryDAL.GetAll();
        }

        public DataTable SearchCategories(string keyword)
        {
            return categoryDAL.Search(keyword);
        }

        public bool AddCategory(CategoryDTO category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                return false;

            if (categoryDAL.IsCategoryNameExists(category.CategoryName))
                return false;

            return categoryDAL.Insert(category);
        }

        public bool UpdateCategory(CategoryDTO category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                return false;

            if (categoryDAL.IsCategoryNameExists(category.CategoryName, category.CategoryId))
                return false;

            return categoryDAL.Update(category);
        }

        public bool DeleteCategory(int categoryId)
        {
            return categoryDAL.Delete(categoryId);
        }
    }
}