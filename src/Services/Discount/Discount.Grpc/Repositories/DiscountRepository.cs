using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_connString);

            var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName,Description,Amount) Values (@ProductName,@Description,@Amount)",new {ProductName = coupon.ProductName, Description = coupon.Description,Amount = coupon.Amount});


            if(affected == 0)
                return false;
            return true; 
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_connString);

            var affected = await connection.ExecuteAsync("Delete From Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (affected == 0)
                return false;
            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_connString);

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("Select * FROM Coupon where ProductName = @ProductName",new {ProductName = productName});

            if(coupon==null)
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_connString);

            var affected = await connection.ExecuteAsync("Update Coupon SET ProductName = @ProductName, Description= @Description,Amount = @Amount WHERE Id = @Id ", new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount,Id=coupon.Id });

            if (affected == 0)
                return false;
            return true;
        }
    }
}
