using Microsoft.Extensions.Configuration;
using POSTGRES_NET_Core_Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace POSTGRES_NET_Core_Example.Database
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(string firstName, string lastName);

        Task<Customer> GetAsync(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            ConnectionString = _configuration.GetConnectionString("CustomerDb");
        }

        public async Task<Customer> CreateAsync(string firstName, string lastName)
        {
            var sql = "INSERT INTO customer.Customers (firstname, lastname) values (@firstName, @lastName) returning id;";

            using(var conn = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                var id = await conn.ExecuteScalarAsync<int>(sql, new { firstName, lastName });

                return await GetAsync(id);
            }
        }

        public async Task<Customer> GetAsync(int id)
        {
            var sql = "select * from customer.Customers where id=@id";

            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                return await conn.QuerySingleOrDefaultAsync<Customer>(sql, new { id });
            }
        }
    }
}
