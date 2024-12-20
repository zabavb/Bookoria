﻿using Microsoft.EntityFrameworkCore;
using System.Text;
using UserAPI.Data;
using UserAPI.Models;
using UserAPI.Models.Extensions;

namespace UserAPI.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly UserDbContext _context;

        public SubscriptionRepository(UserDbContext context) => _context = context;

        public async Task<PaginatedResult<Subscription>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm)
        {
            IEnumerable<Subscription> subscriptions;
            if (string.IsNullOrWhiteSpace(searchTerm))
                subscriptions = await SearchEntitiesAsync(searchTerm);
            else
                subscriptions = _context.Subscriptions.AsNoTracking();

            var totalSubscriptions = await Task.FromResult(subscriptions.Count());

            subscriptions = await Task.FromResult(subscriptions.Skip((pageNumber - 1) * pageSize).Take(pageSize));
            ICollection<Subscription> result = new List<Subscription>(subscriptions);
            return new PaginatedResult<Subscription>
            {
                Items = result,
                TotalCount = totalSubscriptions,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Subscription?> GetEntityByIdAsync(Guid id) =>
            await _context.Subscriptions.AsNoTracking().FirstOrDefaultAsync(s => s.SubscriptionId == id);

        public async Task<IEnumerable<Subscription>> SearchEntitiesAsync(string searchTerm)
        {
            var subscriptions = await _context.Subscriptions
                .AsNoTracking()
                .Where(s => s.Title.Contains(searchTerm) || s.Description!.Contains(searchTerm))
                .ToListAsync();

            return subscriptions;
        }

        public async Task AddEntityAsync(Subscription entity)
        {
            await _context.Subscriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntityAsync(Subscription entity)
        {
            if (!await _context.Subscriptions.AnyAsync(s => s.SubscriptionId == entity.SubscriptionId))
                throw new InvalidOperationException();

            _context.Subscriptions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);

            if (subscription == null)
                throw new KeyNotFoundException();

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }
    }
}
