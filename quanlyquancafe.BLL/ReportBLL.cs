using System;
using System.Data;
using quanlyquancafe.DAL;

namespace quanlyquancafe.BLL
{
    public class ReportBLL
    {
        private ReportDAL reportDAL = new ReportDAL();

        public DataTable GetRevenueByDate(DateTime fromDate, DateTime toDate)
        {
            return reportDAL.GetRevenueByDate(fromDate, toDate);
        }

        public DataTable GetRevenueByMonth(int year)
        {
            return reportDAL.GetRevenueByMonth(year);
        }

        public DataTable GetTopSellingProducts(DateTime fromDate, DateTime toDate)
        {
            return reportDAL.GetTopSellingProducts(fromDate, toDate);
        }

        public decimal GetTotalRevenue(DateTime fromDate, DateTime toDate)
        {
            return reportDAL.GetTotalRevenue(fromDate, toDate);
        }
    }
}