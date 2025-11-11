using EmployeeClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeClient.Services
{
    public class EmployeeApiService
    {
        private readonly HttpClient _client;

        public EmployeeApiService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7114/") // Web API のURL
            };
        }

        public async Task<List<Employee>> GetAllAsync()
            => await _client.GetFromJsonAsync<List<Employee>>("api/employee");

        public async Task AddAsync(Employee emp)
            => await _client.PostAsJsonAsync("api/employee", emp);

        public async Task UpdateAsync(Employee emp)
            => await _client.PutAsJsonAsync($"api/employee/{emp.Id}", emp);

        public async Task DeleteAsync(int id)
            => await _client.DeleteAsync($"api/employee/{id}");
    }
}

