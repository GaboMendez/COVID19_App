using COVID19.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVID19.Services
{
    public interface IApiService
    {
        [Get("/all")]
        Task<Global> GetGlobalStatus();
    }
}
