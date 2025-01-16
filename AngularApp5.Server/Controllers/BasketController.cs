using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp5.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepo;

        public BasketController(IBasketRepository basketRepo)
        {
            this.basketRepo = basketRepo;
        }

        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById([FromQuery] string id)
        {
            var basket = await basketRepo.GetBasketAsync(id);

            if (basket == null)
            {
                // Tworzymy nowy koszyk, jeśli nie istnieje
                var newBasket = new CustomerBasket(id);
                await basketRepo.PostBasket(newBasket);

                // Zwracamy nowo utworzony koszyk
                return Ok(newBasket);
            }

            return Ok(basket);
        }
        //metoda służaca tylko do tworzenia nowego koszyka po wywołaniu GetBasketAsync
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> PostBasket(CustomerBasket newBasket)
        {
            await basketRepo.PostBasket(newBasket);

            // Zwracamy nowo utworzony koszyk
            return Ok(newBasket);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            if (basket == null || string.IsNullOrWhiteSpace(basket.Id))
            {
                return BadRequest("Basket cannot be null and must have a valid Id.");
            }
            var updatedBasket = await basketRepo.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
            var success = await basketRepo.DeleteBasketAsync(id);

            if (!success)
            {
                return NotFound($"Basket with id {id} not found.");
            }

            return NoContent(); //204 
        }
    }
}
