using System;

namespace quanlyquancafe.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int TableID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPaid { get; set; }
    }
}   