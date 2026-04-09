using System;
using System.Data;
using quanlyquancafe.DAL;
using quanlyquancafe.DTO;

namespace quanlyquancafe.BLL
{
    public class OrderBLL
    {
        OrderDAL dal = new OrderDAL();

        public int CreateOrder(int tableId)
        {
            return dal.CreateOrder(tableId);
        }

        public void AddItem(int orderId, int productId, int quantity, decimal price)
        {
            dal.AddItem(orderId, productId, quantity, price);
        }

        public DataTable GetOrderDetails(int orderId)
        {
            return dal.GetDetails(orderId);
        }

        public decimal GetTotal(int orderId)
        {
            DataTable dt = dal.GetDetails(orderId);
            decimal total = 0;

            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToInt32(row["Quantity"]) * Convert.ToDecimal(row["Price"]);
            }

            return total;
        }

        public void Pay(int orderId)
        {
            dal.Pay(orderId);
        }
        public int GetUnpaidOrder(int tableId)
        {
            return dal.GetUnpaidOrder(tableId);
        }
        public void DeleteItem(int orderDetailId)
        {
            dal.DeleteItem(orderDetailId);
        }
    }


}