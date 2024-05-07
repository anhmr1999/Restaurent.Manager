namespace Restaurent.Manager.Models
{
    public class PagingModel<TData>
    {
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<TData> Datas { get; set; }
    }
}
