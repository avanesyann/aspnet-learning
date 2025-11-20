using FinShark.Dtos;
using FinShark.Models;

namespace FinShark.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
            };
        }
    }
}
