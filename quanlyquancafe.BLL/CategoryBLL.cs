using System.Data;
using quanlyquancafe.DAL;

namespace quanlyquancafe.BLL
{
    public class CategoryBLL
    {
        private CategoryDAL categoryDAL = new CategoryDAL();

        public DataTable GetAllCategories()
        {
            return categoryDAL.GetAll();
        }
    }
}