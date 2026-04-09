using System.Data;
using quanlyquancafe.DAL;

namespace quanlyquancafe.BLL
{
    public class TableBLL
    {
        TableDAL dal = new TableDAL();

        public DataTable GetTables()
        {
            return dal.GetTables();
        }

        public void SetOccupied(int tableId)
        {
            dal.UpdateStatus(tableId, "Occupied");
        }

        public void SetEmpty(int tableId)
        {
            dal.UpdateStatus(tableId, "Empty");
        }
    }
}