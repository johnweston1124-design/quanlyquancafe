using System;
using System.Data;
using System.Data.SqlClient;
using quanlyquancafe.DTO;

namespace quanlyquancafe.DAL
{
    public class CategoryDAL
    {
        public DataTable GetAll()
        {
            string query = @"SELECT CategoryId, CategoryName, Description, IsActive, CreatedAt, UpdatedAt
                             FROM Categories
                             ORDER BY CategoryId DESC";

            return DataProvider.ExecuteQuery(query);
        }

        public DataTable Search(string keyword)
        {
            string query = @"SELECT CategoryId, CategoryName, Description, IsActive, CreatedAt, UpdatedAt
                             FROM Categories
                             WHERE CategoryName LIKE @Keyword
                             ORDER BY CategoryId DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        public bool Insert(CategoryDTO category)
        {
            string query = @"INSERT INTO Categories(CategoryName, Description, IsActive, CreatedAt, UpdatedAt)
                             VALUES(@CategoryName, @Description, @IsActive, SYSDATETIME(), NULL)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CategoryName", category.CategoryName),
                new SqlParameter("@Description", string.IsNullOrWhiteSpace(category.Description) ? (object)DBNull.Value : category.Description),
                new SqlParameter("@IsActive", category.IsActive)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Update(CategoryDTO category)
        {
            string query = @"UPDATE Categories
                             SET CategoryName = @CategoryName,
                                 Description = @Description,
                                 IsActive = @IsActive,
                                 UpdatedAt = SYSDATETIME()
                             WHERE CategoryId = @CategoryId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CategoryId", category.CategoryId),
                new SqlParameter("@CategoryName", category.CategoryName),
                new SqlParameter("@Description", string.IsNullOrWhiteSpace(category.Description) ? (object)DBNull.Value : category.Description),
                new SqlParameter("@IsActive", category.IsActive)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int categoryId)
        {
            string query = @"DELETE FROM Categories WHERE CategoryId = @CategoryId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CategoryId", categoryId)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool IsCategoryNameExists(string categoryName, int excludeCategoryId = 0)
        {
            string query = @"SELECT COUNT(*)
                             FROM Categories
                             WHERE CategoryName = @CategoryName
                               AND CategoryId <> @CategoryId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CategoryName", categoryName),
                new SqlParameter("@CategoryId", excludeCategoryId)
            };

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }
    }
}