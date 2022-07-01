using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace QuotesMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuotesController : ControllerBase
    {
        int quote = 0;
        [HttpGet("GetQuotes")]
        public IActionResult GetQuotes([FromQuery] int businessValue, [FromQuery] int propertyValue, [FromQuery] string propertyType)
        {
            //Console.WriteLine("/GetQuotes");

            if (businessValue >= 0 && businessValue <= 2 && propertyValue >= 0 && propertyValue <= 2 && string.Equals(propertyType, "Factory Equipment", StringComparison.OrdinalIgnoreCase))
                quote = 80000;
            else if (businessValue >= 3 && businessValue < 5 && propertyValue >= 3 && propertyValue < 5 && string.Equals(propertyType, "Factory Equipment", StringComparison.OrdinalIgnoreCase))
                quote = 50000;
            else if (businessValue >= 0 && businessValue <= 2 && propertyValue >= 0 && propertyValue <= 2 && string.Equals(propertyType, "Building", StringComparison.OrdinalIgnoreCase))
                quote = 80000;
            else if (businessValue >= 5 && businessValue <= 10 && propertyValue >= 3 && propertyValue <= 10 && string.Equals(propertyType, "Factory Equipment", StringComparison.OrdinalIgnoreCase))
                quote = 50000;
            else if (businessValue >= 3 && businessValue < 5 && propertyValue >= 3 && propertyValue < 5 && string.Equals(propertyType, "Building", StringComparison.OrdinalIgnoreCase))
                quote = 50000;
            else if (businessValue >= 5 && businessValue <= 10 && propertyValue >= 3 && propertyValue <= 10 && string.Equals(propertyType, "Building", StringComparison.OrdinalIgnoreCase))
                quote = 50000;
            else if (businessValue >= 0 && businessValue <= 2 && propertyValue >= 0 && propertyValue <= 2 && string.Equals(propertyType, "Property in Transit", StringComparison.OrdinalIgnoreCase))
                quote = 80000;
            else if (businessValue >= 3 && businessValue < 5 && propertyValue >= 3 && propertyValue < 5 && string.Equals(propertyType, "Property in Transit", StringComparison.OrdinalIgnoreCase))
                quote = 50000;
            else if (businessValue >= 5 && businessValue <= 10 && propertyValue >= 3 && propertyValue <= 10 && string.Equals(propertyType, "Property in Transit", StringComparison.OrdinalIgnoreCase))
                quote = 50000;
            else
                quote = 83100;

            return Ok(quote);
        }
    }
}
