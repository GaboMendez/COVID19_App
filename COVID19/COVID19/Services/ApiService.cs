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
        private readonly IApiService _apiCountryService;


        public ApiService()
        {   
            _apiService = RestService.For<IApiService>
                (new HttpClient
                    {
                        BaseAddress = new Uri(Config.api_url),
                        Timeout = TimeSpan.FromSeconds(3)
                    });

            _apiCountryService = RestService.For<IApiService>
                (new HttpClient
                    {
                        BaseAddress = new Uri(Config.api_url_country),
                        Timeout = TimeSpan.FromSeconds(3)
                    });
        }

        public async Task<Global> GetGlobalStatus()
        {
            return await _apiService.GetGlobalStatus();
        }

        public async Task<ObservableCollection<Country>> GetGlobalCountries()
        {
            return await _apiCountryService.GetGlobalCountries();
        }

    }
}
