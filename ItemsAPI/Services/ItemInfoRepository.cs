using ItemsAPI.Context;
using ItemsAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI.Services
{
    public class ItemInfoRepository : IItemInfoRepository
    {
        private readonly ItemInfoContext _context;

        public ItemInfoRepository(ItemInfoContext context)
        {
            _context = context;
        }

        public Item GetItem(int itemId, bool includeRating)
        {
            if(includeRating)
            {
                _context.Items.Include(i => i.Rating).Where(i => i.Id == itemId).FirstOrDefault();
            }

            return _context.Items.Where(i => i.Id == itemId).FirstOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _context.Items.OrderBy(i => i.Name).ToList();
        }

        public Rating GetRatingForItem(int itemId, int ratingId)
        {
            return _context.Rating.Where(r => r.ItemId == itemId && r.Id == ratingId).FirstOrDefault();
        }

        public IEnumerable<Rating> getRatingsForItem(int itemId)
        {
            return _context.Rating.Where(r => r.Id == itemId).ToList();
        }
    }
}
