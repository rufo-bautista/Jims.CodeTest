using Jims.CodingTest.Console.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jims.CodingTest.Console.Services
{
    public class CalculatorService : ICalculatorService
    {
        private bool _disposed;
        private IConfiguration _config;
        private ILogger _logger;

        public CalculatorService(IConfiguration config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<ResultModel> Calculate(InputModel input)
        {
            _logger.Information("Calculate input {@input}", input);

            try
            {
                using HttpClient client = new HttpClient();
                string baseAddressUri = _config.GetSection("CalculatorApiUri").Value;
                client.BaseAddress = new Uri(baseAddressUri);
                using HttpResponseMessage response = await client.PostAsJsonAsync("calculate", input);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<dynamic>();
                    return JsonSerializer.Deserialize<ResultModel>(result);
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _config = null;
                _logger = null;
                _disposed = true;
            }
        }
    }
}
