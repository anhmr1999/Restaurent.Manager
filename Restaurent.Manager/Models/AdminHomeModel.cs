using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Models
{
    public class AdminHomeModel
    {
        public double Revene { get; set; }
        public int BillCount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int GroupType { get; set; }
        public IDictionary<string, double> ChartData { get; set; }
    }
}
