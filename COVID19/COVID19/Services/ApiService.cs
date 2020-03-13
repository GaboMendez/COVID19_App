using COVID19.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVID19.Services
{
    public class ApiService : IApiService
    {
        private readonly IApiService _apiService;

        public ApiService()
        {
            _apiService = RestService.For<IApiService>(Config.api_url);
        }

        public async Task<Global> GetGlobalStatus()
        {
            return await _apiService.GetGlobalStatus();
        }
    }
}
