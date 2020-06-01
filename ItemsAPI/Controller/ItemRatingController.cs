using ItemsAPI.Data;
using ItemsAPI.Models;
using ItemsAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Controller.ItemsAPI
{
    [Route("api/items/{itemId}/itemrating")]
    [ApiController]
    public class ItemRatingController : ControllerBase
    {
        private readonly ILogger<ItemRatingController> _logger;
        private readonly IMailService _mailService;

        public ItemRatingController(ILogger<ItemRatingController> logger, IMailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public IActionResult GetRatings(int itemId)
        {
            try
            {
                //throw new Exception("e example");
                var item = ItemsDataStore.Current.Items.FirstOrDefault(x => x.Id == itemId);

                if (item == null)
                {
                    _logger.LogInformation($"Item with id {itemId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                return Ok(item.Rating);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Exception while getting points of interest for item with id {itemId}", e);
                return StatusCode(500, "A problem happened while handling your request");
            }

            
        }

        [HttpGet]
        [Route("{id}", Name = "GetItemRating")]
        public IActionResult GetRating(int itemId, int id)
        {
            var item = ItemsDataStore.Current.Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
            {
                return NotFound();
            }

            var rating = item.Rating.FirstOrDefault(x => x.Id == id);

            if(rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        [HttpPost]
        public IActionResult CreateRating(int itemId, [FromBody] RatingForCreationDto rating)
        {
            if (rating.Description == rating.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = ItemsDataStore.Current.Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
            {
                return NotFound();
            }

            var newId = ItemsDataStore.Current.Items.SelectMany(x => x.Rating).Max(r => r.Id);

            var finalRate = new RatingDto()
            {
                Id = newId,
                Name = rating.Name,
                Description = rating.Description
            };

            item.Rating.Add(finalRate);

            return CreatedAtRoute(
                "GetItemRating",
                new { itemId, id = finalRate.Id }, finalRate);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRating(int itemId, int id, [FromBody] RatingForUpdateDto rating)
        {
            if (rating.Description == rating.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = ItemsDataStore.Current.Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
            {
                return NotFound();
            }

            var ratingFromStore = item.Rating.FirstOrDefault(x => x.Id == id);

            if(ratingFromStore == null)
            {
                return NotFound();
            }

            ratingFromStore.Name = rating.Name;
            ratingFromStore.Description = rating.Description;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchRating(int itemId, int id, [FromBody] JsonPatchDocument<RatingForUpdateDto> patchDoc)
        {
            var item = ItemsDataStore.Current.Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
            {
                return NotFound();
            }

            var ratingFromStore = item.Rating.FirstOrDefault(x => x.Id == id);

            if (ratingFromStore == null)
            {
                return NotFound();
            }

            var ratingToPatch = new RatingForUpdateDto()
            {
                Name = ratingFromStore.Name,
                Description = ratingFromStore.Description
            };

            patchDoc.ApplyTo(ratingToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if the patch Dto is Valid (ModelState.IsValid above only check if the PatchDocument is valid )
            if (ratingToPatch.Description == ratingToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if(!TryValidateModel(ratingToPatch))
            {
                return BadRequest(ModelState);
            }

            ratingFromStore.Name = ratingToPatch.Name;
            ratingFromStore.Description = ratingToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int itemId, int id)
        {
            var item = ItemsDataStore.Current.Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
            {
                return NotFound();
            }

            var ratingFromStore = item.Rating.FirstOrDefault(x => x.Id == id);

            if (ratingFromStore == null)
            {
                return NotFound();
            }

            item.Rating.Remove(ratingFromStore);

            _mailService.Send("Rating deleted.", $"Rating {ratingFromStore.Name} with id {ratingFromStore.Id} was deleted.");

            return NoContent();
        }
    }
}