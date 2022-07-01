using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsumerMicroservice.Models;
using ConsumerMicroservice.Repository;
using System.Web.Http.Cors;

namespace ConsumerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConsumerController : ControllerBase
    {
        BusinessMasterRepo _repo = new BusinessMasterRepo();

        [HttpGet("ViewConsumerBusiness")]

        public async Task<ActionResult> ViewConsumerBusiness([FromQuery] int consumerId)
        {
            return Ok(_repo.GetBusinessMaster().Find(x => x.Consumer.ID == consumerId));
        }

        [HttpPost("CreateConsumerBusiness")]
        public async Task<ActionResult> CreateConsumerBusiness([FromBody] Consumer consumer)
        {
            System.Console.WriteLine("ADDING : ", consumer.ToString());
            if (consumer == null)
            {
                return NotFound("Consumer Not Found");
            }
            else
            {
                var newBusiness = new Business()
                {
                    AnnualTurnover = consumer.Business.AnnualTurnover,
                    BusinessType = consumer.Business.BusinessType,
                    CapitalInvested = consumer.Business.CapitalInvested,
                    TotalEmployees = consumer.Business.TotalEmployees
                };

                Console.WriteLine("HERE");
                var newConsumer = new Consumer()
                {
                    Business = newBusiness,
                    DOB = consumer.DOB,
                    Email = consumer.Email,
                    Name = consumer.Name,
                    PAN = consumer.PAN,
                };

                _repo.AddBMaster(newConsumer);
                return Ok(newConsumer);
            }
        }

        [HttpPut("UpdateConsumerBusiness")]
        public async Task<ActionResult> UpdateConsumerBusiness([FromQuery] int consumerId, [FromBody] Consumer consumer)
        {
            if (consumer == null)
            {
                return NotFound("Consumer Not Found");
            }
            else
            {
                _repo.UpdateBMaster(consumerId, consumer);
                return Ok(consumer);
            }
        }

        [HttpGet("GetAllConsumerBusiness")]
        public async Task<ActionResult> GetAllConsumerBusiness()
        {
            var result = _repo.GetBusinessMaster();
            Console.WriteLine($"/GetAllConsumerBusiness Length : {result.Count}");
            return Ok(result);

        }


    }
}
