using System.Data;
using System.Data.SqlClient;
using quanlyquancafe.DAL;

namespace CoffeeShop.DAL.Repositories
{
    public class ProductRepository
    {
        public DataTable GetAllAvailable()
        {
            const string sql = @"
SELECT
    p.ProductId,
    p.ProductName,
    p.CategoryId,
    c.CategoryName,
    p.Price,
    p.Description,
    p.Unit,
    p.Image,
    p.Status,
    p.CreatedAt,
    p.UpdatedAt,
    p.IsActive
FROM Products AS p
INNER JOIN Categories AS c
    ON c.CategoryId = p.CategoryId
WHERE p.IsActive = 1
  AND c.IsActive = 1
  AND p.Status = @Status
ORDER BY c.CategoryName, p.ProductName;";

            return DataProvider.Instance.ExecuteQuery(
                sql,
                CommandType.Text,
                null,
                new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = "Available" });
        }

        public DataTable GetByCategory(int categoryId)
        {
            const string sql = @"
SELECT
    p.ProductId,
    p.ProductName,
    p.CategoryId,
    c.CategoryName,
    p.Price,
    p.Description,
    p.Unit,
    p.Image,
    p.Status,
    p.CreatedAt,
    p.UpdatedAt,
    p.IsActive
FROM Products AS p
INNER JOIN Categories AS c
    ON c.CategoryId = p.CategoryId
WHERE p.CategoryId = @CategoryId
  AND p.IsActive = 1
  AND c.IsActive = 1
  AND p.Status = @Status
ORDER BY p.ProductName;";

            return DataProvider.Instance.ExecuteQuery(
                sql,
                CommandType.Text,
                null,
                new SqlParameter("@CategoryId", SqlDbType.Int) { Value = categoryId },
                new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = "Available" });
        }
    }
}
