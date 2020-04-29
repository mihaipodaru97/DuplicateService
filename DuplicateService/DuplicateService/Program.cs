using System.Threading;

namespace DuplicateService
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DuplicateService();
            while (true)
            {
                var cars = service.GetCars();
                var ids = service.GetDuplicates(cars);
                service.DeleteDuplicates(ids);
                Thread.Sleep(1000 * 60);
            }
        }
    }
}
