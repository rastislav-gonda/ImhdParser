using ImhdParser.Service;

namespace ImhdParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var _service = new ParseDataService();
            var busStops = _service.GetBusStops();
            var lines = _service.LinesUrl();
        }
    }
}
