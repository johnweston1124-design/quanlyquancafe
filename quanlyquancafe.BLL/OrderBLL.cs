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
        public int GetActiveOrderIdByTable(int tableId)
        {
            // You'll need a quick method in DAL that does: 
            // SELECT OrderId FROM Orders WHERE TableId = @id AND OrderStatus = 'Pending'
            return dal.GetActiveOrderId(tableId);
        }
        public DataTable GetOrderDetails(int orderId)
        {
            return dal.GetDetails(orderId);
        }

        public decimal GetTotal(int orderId)
        {
            return dal.GetTotal(orderId);
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