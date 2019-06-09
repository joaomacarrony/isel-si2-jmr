using SI2_CO.Entities;
using SI2_CO.Mapper;

namespace SI2_CO.Functions
{
    class StockUtils
    {
        public void Insert(Stock stock)
        {
            MapperStock mapper = new MapperStock();
            mapper.Create(stock);
        }
    }
}
