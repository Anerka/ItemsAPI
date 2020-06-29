using ItemsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI.Services
{
    public interface IItemInfoRepository
    {
        IEnumerable<Item> GetItems();

        Item GetItem(int itemId, bool includeRating);

        IEnumerable<Rating> GetRatingsForItem(int itemId);

        Rating GetRatingForItem(int itemId, int ratingId);

        bool ItemExists(int itemId);
    }
}
