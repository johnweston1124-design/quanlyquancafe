using System.Data;
using quanlyquancafe.DAL;
using quanlyquancafe.DTO;

namespace quanlyquancafe.BLL
{
    public class ProductBLL
    {
        private ProductDAL productDAL = new ProductDAL();

        public DataTable GetAllProducts()
        {
            return productDAL.GetAll();
        }

        public DataTable SearchProducts(string keyword)
        {
            if (keyword == null)
                keyword = "";

            return productDAL.Search(keyword.Trim());
        }

        public DataTable GetCategories()
        {
            return productDAL.GetCategories();
        }

        public bool AddProduct(ProductDTO product)
        {
            return productDAL.Insert(product);
        }

        public bool UpdateProduct(ProductDTO product)
        {
            return productDAL.Update(product);
        }

        public bool DeleteProduct(int productId)
        {
            return productDAL.Delete(productId);
        }
        public DataTable SearchProducts(string keyword, int categoryId)
        {
            return productDAL.SearchProducts(keyword, categoryId);
        }
    }
}