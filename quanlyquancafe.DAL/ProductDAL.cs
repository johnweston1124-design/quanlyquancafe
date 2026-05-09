using System;
using System.Data;
using System.Data.SqlClient;
using quanlyquancafe.DTO;

namespace quanlyquancafe.DAL
{
    public class ProductDAL
    {
        public DataTable GetAll()
        {
            string query = @"
                SELECT p.ProductId, p.ProductName, p.CategoryId, c.CategoryName,
                       p.Price, p.Description, p.Unit, p.Image, p.Status,
                       p.CreatedAt, p.UpdatedAt, p.IsActive
                FROM Products p
                INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                ORDER BY p.ProductId DESC";

            return DataProvider.ExecuteQuery(query);
        }

        public DataTable Search(string keyword)
        {
            string query = @"
                SELECT p.ProductId, p.ProductName, p.CategoryId, c.CategoryName,
                       p.Price, p.Description, p.Unit, p.Image, p.Status,
                       p.CreatedAt, p.UpdatedAt, p.IsActive
                FROM Products p
                INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                WHERE p.ProductName LIKE @Keyword
                   OR c.CategoryName LIKE @Keyword
                ORDER BY p.ProductId DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        public DataTable GetCategories()
        {
            string query = @"SELECT CategoryId, CategoryName FROM Categories WHERE IsActive = 1 ORDER BY CategoryName";
            return DataProvider.ExecuteQuery(query);
        }

        public bool Insert(ProductDTO product)
        {
            string query = @"
                INSERT INTO Products
                (
                    ProductName, CategoryId, Price, Description, Unit, Image,
                    Status, CreatedAt, UpdatedAt, IsActive
                )
                VALUES
                (
                    @ProductName, @CategoryId, @Price, @Description, @Unit, @Image,
                    @Status, SYSDATETIME(), NULL, @IsActive
                )";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@CategoryId", product.CategoryId),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Description", string.IsNullOrWhiteSpace(product.Description) ? (object)DBNull.Value : product.Description),
                new SqlParameter("@Unit", product.Unit),
                new SqlParameter("@Image", string.IsNullOrWhiteSpace(product.Image) ? (object)DBNull.Value : product.Image),
                new SqlParameter("@Status", product.Status),
                new SqlParameter("@IsActive", product.IsActive)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Update(ProductDTO product)
        {
            string query = @"
                UPDATE Products
                SET ProductName = @ProductName,
                    CategoryId = @CategoryId,
                    Price = @Price,
                    Description = @Description,
                    Unit = @Unit,
                    Image = @Image,
                    Status = @Status,
                    UpdatedAt = SYSDATETIME(),
                    IsActive = @IsActive
                WHERE ProductId = @ProductId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductId", product.ProductId),
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@CategoryId", product.CategoryId),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Description", string.IsNullOrWhiteSpace(product.Description) ? (object)DBNull.Value : product.Description),
                new SqlParameter("@Unit", product.Unit),
                new SqlParameter("@Image", string.IsNullOrWhiteSpace(product.Image) ? (object)DBNull.Value : product.Image),
                new SqlParameter("@Status", product.Status),
                new SqlParameter("@IsActive", product.IsActive)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int productId)
        {
            string query = @"DELETE FROM Products WHERE ProductId = @ProductId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductId", productId)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool IsProductExists(string productName, int categoryId, int excludeProductId = 0)
        {
            string query = @"
                SELECT COUNT(*)
                FROM Products
                WHERE ProductName = @ProductName
                  AND CategoryId = @CategoryId
                  AND ProductId <> @ProductId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductName", productName),
                new SqlParameter("@CategoryId", categoryId),
                new SqlParameter("@ProductId", excludeProductId)
            };

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        public DataTable SearchProducts(string keyword, int categoryId)
        {
            throw new NotImplementedException();
        }
        public DataTable SearchProducts(string keyword, int categoryId)
        {
            // Query này vừa lọc theo tên (LIKE), vừa lọc theo Category (nếu != -1)
            string query = string.Format("SELECT p.*, c.CategoryName FROM Product p JOIN Category c ON p.CategoryId = c.CategoryId WHERE p.ProductName LIKE N'%{0}%'", keyword);

            if (categoryId != -1)
            {
                query += " AND p.CategoryId = " + categoryId;
            }

            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}