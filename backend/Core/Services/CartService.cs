using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Interfaces;
using Models;
using StackExchange.Redis;

namespace Core.Services
{
    public class CartService : ICartService
    {
        private readonly IDatabase _database;

        public CartService(IConnectionMultiplexer connectionMultiplexer) =>
            _database = connectionMultiplexer.GetDatabase();

        public async Task<bool> EmptyCartAsync(string id) => await _database.KeyDeleteAsync(id);

        public async Task<Cart> GetCartAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            return redisValue.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(redisValue);
        }

        public async Task<Cart> UpdateOrCreateCartAsync(Cart cart)
        {
            var cartAlreadyExists = await _database.StringSetAsync(
                cart.Id,
                JsonSerializer.Serialize(cart),
                TimeSpan.FromDays(30)
            );

            if (!cartAlreadyExists) return null;

            return await GetCartAsync(cart.Id);
        }
    }
}