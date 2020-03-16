using COVID19.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
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

        public async Task<ObservableCollection<Country>> GetGlobalCountries()
        {
            return await _apiService.GetGlobalCountries();
        }

    }
}
