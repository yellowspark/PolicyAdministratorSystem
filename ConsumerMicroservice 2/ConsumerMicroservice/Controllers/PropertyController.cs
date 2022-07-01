using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsumerMicroservice.Models;
using ConsumerMicroservice.Repository;
using System.Web.Http.Cors;
using System;

namespace ConsumerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PropertyController : ControllerBase
    {
        //PropertyRepo _repo = new PropertyRepo();
        PropertyMasterRepo _repo = new PropertyMasterRepo();

        [HttpGet("ViewConsumerProperty")]
        public ActionResult ViewConsumerProperty([FromQuery] int businessId, [FromQuery] int consumerId)
        {
            return Ok(_repo.GetAllPMaster().Find(x => (x.Property.ID == businessId) && (x.Property.ConsumerID == consumerId)));
        }

        [HttpPost("CreateBusinessProperty")]
        public ActionResult CreateBusinessProperty([FromBody] Property Prop)
        {
            if (Prop == null)
            {
                return NotFound("Invalid Details....");
            }
            else
            {
                return Ok(_repo.AddToPMaster(Prop));
            }
        }

        [HttpPut("UpdateBusinessProperty")]
        public ActionResult UpdateBusinessProperty([FromQuery] int businessId, [FromBody] Property Prop)
        {
            if (Prop == null)
            {
                return NotFound("Invalid Details....");
            }
            else
            {
                return Ok(_repo.UpdatePMaster(businessId, Prop));
            }
        }

        [HttpGet("GetAllBusinessProperty")]
        public ActionResult GetAllBusinessProperty()
        {
            return Ok(_repo.GetAllPMaster());
        }
    }
}
