using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using policymicroservice.Models;
using System.Web.Http.Cors;
using policyMicroservice.Models;

namespace policymicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PolicyController : ControllerBase
    {
        public List<ConsumerPolicy> policyList = new List<ConsumerPolicy>();
        public ConsumerPolicy? returnPolicy = null;

        [HttpPost("createPolicy")]
        // public async Task<ActionResult> createPolicy([FromQuery] int ID)
        public async Task<ActionResult> CreatePolicy([FromBody] PolicyDetails policyDetails)
        {
            int ID = policyDetails.ConsumerId;
            int quotes = policyDetails.Quotes;
            // int quotes = 80000;

            /*"https://localhost:7219/api/"*/
            List<BusinessMaster> Bmaster = new List<BusinessMaster>();
            using (var client = new HttpClient())
            {
                // URL for policy microservice
                client.BaseAddress = new Uri("https://localhost:7219/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // using (var response = await client.GetAsync("api/Consumer/ViewConsumerBusiness"))
                using (var response = await client.GetAsync("api/Consumer/GetAllConsumerBusiness"))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest(response);
                    }

                    var apires = await response.Content.ReadAsStringAsync();
                    Bmaster = JsonConvert.DeserializeObject<List<BusinessMaster>>(apires);


                    BusinessMaster? Bmas = null;
                    try
                    {
                        Bmas = Bmaster?.FirstOrDefault(m => m.Consumer.ID == ID);         //Bmas contain the business value
                    }
                    catch (Exception e)
                    {
                        Console.Error.Write(e.Message);
                    }

                    if (null == Bmas)
                    {
                        return NotFound($"No conumer details found for ID : {policyDetails.ConsumerId}");
                    }

                    Console.WriteLine("BMAS: " + Bmas?.Consumer.ID);

                    ConsumerPolicy cp = new ConsumerPolicy()
                    {
                        Id = Bmas.Consumer.ID,
                        BusinessMaster = Bmas,
                        AcceptedQuotes = new AcceptedQuotes()
                        {
                            Status = "Initiated",
                            Quotes = quotes
                        }
                    };
                    policyList.Add(cp);
                    //uploading all the data in policyList.csv file
                    using (StreamWriter wr = new StreamWriter("Data.policyList.csv", append: true))
                    {
                        wr.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                            cp.Id,
                            cp.BusinessMaster.Consumer.ID,
                            cp.BusinessMaster.Consumer.Name,
                            cp.BusinessMaster.Consumer.DOB,
                            cp.BusinessMaster.Consumer.Email,
                            cp.BusinessMaster.Consumer.PAN,
                            cp.BusinessMaster.Consumer.Business.ID,
                            cp.BusinessMaster.Consumer.Business.BusinessType,
                            cp.BusinessMaster.Consumer.Business.CapitalInvested,
                            cp.BusinessMaster.Consumer.Business.AnnualTurnover,
                            cp.BusinessMaster.Consumer.Business.TotalEmployees,
                            cp.BusinessMaster.BusinessValue,
                            cp.AcceptedQuotes.Status,
                            cp.AcceptedQuotes.Quotes);
                    }


                    List<PropertyMaster> Pmaster = new List<PropertyMaster>();
                    //retriving the Property value
                    using (var clien = new HttpClient())
                    {
                        // Base URL for policy microservice
                        clien.BaseAddress = new Uri("https://localhost:7219/");
                        clien.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        using (var respons = await clien.GetAsync("api/Property/GetAllBusinessProperty"))
                        {
                            if (!respons.IsSuccessStatusCode)
                            {
                                return BadRequest(respons);
                            }

                            var apire = await respons.Content.ReadAsStringAsync();
                            Pmaster = JsonConvert.DeserializeObject<List<PropertyMaster>>(apire);
                            PropertyMaster Pmas = Pmaster.First(m => m.Property.ConsumerID == ID);  //Pmas contains the property value

                            Console.WriteLine("PMAS ", Pmas.Property.ID);
                        }
                    }


                    return Ok(cp);
                }


            }
        }


        [HttpPost("issuePolicy")]
        public ActionResult IssuePolicy([FromQuery] int policyId)
        {
            this.returnPolicy = null;

            using (StreamReader sr = new StreamReader("Data.policyList.csv"))
            {
                // Read all the policies
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    policyList.Add(new ConsumerPolicy()
                    {
                        Id = Convert.ToInt32(values[0]),
                        BusinessMaster = new BusinessMaster()
                        {
                            Consumer = new Consumer()
                            {
                                ID = Convert.ToInt32(values[1]),
                                Name = values[2],
                                // DOB = Convert.ToDateTime(values[3]),
                                DOB = values[3],
                                Email = values[4],
                                PAN = values[5],
                                Business = new Business()
                                {
                                    ID = Convert.ToInt32(values[6]),
                                    BusinessType = values[7],
                                    CapitalInvested = Convert.ToDecimal(values[8]),
                                    AnnualTurnover = Convert.ToDecimal(values[9]),
                                    TotalEmployees = Convert.ToInt32(values[10])
                                }
                            },
                            BusinessValue = Convert.ToInt32(values[11])
                        },
                        AcceptedQuotes = new AcceptedQuotes()
                        {
                            Status = values[12],
                            Quotes = Convert.ToInt32(values[13])

                        }
                    }

                    );

                }
            }


            // Search the policy with given id
            foreach (var policy in policyList)
            {
                if (policy.Id == policyId)
                {
                    // p = policyList.FirstOrDefault(p => p.Id == policy.Id);
                    // var foundPolicy = policyList.First(p => p.Id == policyId);
                    // var foundPolicy = policy;

                    // if (p != null)
                    policy.AcceptedQuotes.Status = "Issued";

                    this.returnPolicy = policy;
                }
            }

            // Write all the policies back to the CSV
            using (StreamWriter wr = new StreamWriter("Data.policyList.csv"))
            {
                foreach (var policy in this.policyList)
                {
                    wr.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                        policy.Id,
                        policy.BusinessMaster.Consumer.ID,
                        policy.BusinessMaster.Consumer.Name,
                        policy.BusinessMaster.Consumer.DOB,
                        policy.BusinessMaster.Consumer.Email,
                        policy.BusinessMaster.Consumer.PAN,
                        policy.BusinessMaster.Consumer.Business.ID,
                        policy.BusinessMaster.Consumer.Business.BusinessType,
                        policy.BusinessMaster.Consumer.Business.CapitalInvested,
                        policy.BusinessMaster.Consumer.Business.AnnualTurnover,
                        policy.BusinessMaster.Consumer.Business.TotalEmployees,
                        policy.BusinessMaster.BusinessValue,
                        policy.AcceptedQuotes.Status,
                        policy.AcceptedQuotes.Quotes);
                }
            }

            if (null != this.returnPolicy)
                return Ok(this.returnPolicy);

            return NotFound($"No policy with given id : {policyId}");

        }


        [HttpGet("viewPolicy")]
        public ActionResult ViewPolicy([FromQuery] int policyId) //[FromQuery] int ConsumerId
        {
            using (StreamReader sr = new StreamReader("Data.policyList.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    policyList.Add(new ConsumerPolicy()
                    {
                        Id = Convert.ToInt32(values[0]),
                        BusinessMaster = new BusinessMaster()
                        {
                            Consumer = new Consumer()
                            {
                                ID = Convert.ToInt32(values[1]),
                                Name = values[2],
                                // DOB = Convert.ToDateTime(values[3]),
                                DOB = values[3],
                                Email = values[4],
                                PAN = values[5],
                                Business = new Business()
                                {
                                    ID = Convert.ToInt32(values[6]),
                                    BusinessType = values[7],
                                    CapitalInvested = Convert.ToDecimal(values[8]),
                                    AnnualTurnover = Convert.ToDecimal(values[9]),
                                    TotalEmployees = Convert.ToInt32(values[10])
                                }
                            },
                            BusinessValue = Convert.ToInt32(values[11])

                        },
                        AcceptedQuotes = new AcceptedQuotes()
                        {
                            Status = values[12],
                            Quotes = Convert.ToInt32(values[13])

                        }
                    }

                    );

                }
            }

            var foundPolicy = policyList.FirstOrDefault(policyList => policyList.Id == policyId);

            if (null != foundPolicy)
                return Ok(foundPolicy);
            else
                return NotFound($"No policy with given id : {policyId}");
        }

        [HttpGet("getQuotes")]
        public ActionResult GetQuotes([FromQuery] int BusinessValue, [FromQuery] int PropertyValue)
        {
            return Ok();
        }

    }

}



