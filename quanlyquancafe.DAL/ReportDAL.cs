using System;
using System.Data;
using System.Data.SqlClient;

namespace quanlyquancafe.DAL
{
    public class ReportDAL
    {
        // Lấy doanh thu theo khoảng ngày
        public DataTable GetRevenueByDate(DateTime fromDate, DateTime toDate)
        {
            string query = @"
                SELECT 
                    CAST(o.CreatedAt AS DATE) AS [Date],
                    SUM(o.TotalAmount) AS Revenue
                FROM Orders o
                WHERE o.CreatedAt BETWEEN @FromDate AND @ToDate
                GROUP BY CAST(o.CreatedAt AS DATE)
                ORDER BY [Date] ASC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        // Lấy doanh thu theo tháng
        public DataTable GetRevenueByMonth(int year)
        {
            string query = @"
                SELECT 
                    MONTH(o.CreatedAt) AS [Month],
                    SUM(o.TotalAmount) AS Revenue
                FROM Orders o
                WHERE YEAR(o.CreatedAt) = @Year
                GROUP BY MONTH(o.CreatedAt)
                ORDER BY [Month] ASC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Year", year)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        // Lấy top sản phẩm bán chạy
        public DataTable GetTopSellingProducts(DateTime fromDate, DateTime toDate)
        {
            string query = @"
                SELECT 
                    p.ProductName,
                    SUM(od.Quantity) AS TotalSold,
                    SUM(od.Quantity * od.Price) AS Revenue
                FROM OrderDetails od
                INNER JOIN Products p ON od.ProductId = p.ProductId
                INNER JOIN Orders o ON od.OrderId = o.OrderId
                WHERE o.CreatedAt BETWEEN @FromDate AND @ToDate
                GROUP BY p.ProductName
                ORDER BY TotalSold DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        // Tổng doanh thu
        public decimal GetTotalRevenue(DateTime fromDate, DateTime toDate)
        {
            string query = @"
                SELECT SUM(TotalAmount)
                FROM Orders
                WHERE CreatedAt BETWEEN @FromDate AND @ToDate";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };

            object result = DataProvider.ExecuteScalar(query, parameters);

            if (result == DBNull.Value || result == null)
                return 0;

            return Convert.ToDecimal(result);
        }
    }
}