using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.Services.Abstracts;

namespace E_Commerce_Platform.Services.Concretes
{
    public class OrderService : IOrderService
    {
        private readonly ECommerceDBContext _dbContext;
        private readonly Random _random;
        private const string TRACKINGCODE_PREFIX = "OR";

        public OrderService(ECommerceDBContext dpContext)
        {
            _dbContext = dpContext;
            _random = new Random();
        }

        public string GenerateTrackingCode()
        {
            string trackingCode;

            do
            {
                int numberPart = _random.Next(100000, 1000000);
                trackingCode = TRACKINGCODE_PREFIX + numberPart;

            } while (_dbContext.Orders.Any(o => o.TrackingCode == trackingCode));


            return trackingCode;
        }
    }
}
