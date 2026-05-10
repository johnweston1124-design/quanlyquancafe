using System.Data;
using quanlyquancafe.DAL;

namespace quanlyquancafe.BLL
{
    public class TableBLL
    {
        private TableDAL dal = new TableDAL();

        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        public void Insert(string name, int capacity, string status, string zone)
        {
            dal.Insert(name, capacity, status, zone);
        }

        public void Update(int id, string name, int capacity, string status, bool isActive, string zone)
        {
            dal.Update(id, name, capacity, status, isActive, zone);
        }

        public void Delete(int id)
        {
            dal.Delete(id);
        }
    }
}