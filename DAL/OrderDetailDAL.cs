using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace quanlyquancafe.DAL
{
    public class OrderDetailDAL
    {
        public DataTable GetDetailsByOrderId(int orderId)
        {
            string query = @"
                SELECT 
                    p.ProductName, 
                    od.Quantity, 
                    od.UnitPrice, 
                    od.LineTotal, 
                    od.Notes
                FROM OrderDetails od
                JOIN Products p ON od.ProductId = p.ProductId
                WHERE od.OrderId = @orderId";

            return DataProvider.ExecuteQuery(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@orderId", orderId)
                }
            );
        }
    }
}
