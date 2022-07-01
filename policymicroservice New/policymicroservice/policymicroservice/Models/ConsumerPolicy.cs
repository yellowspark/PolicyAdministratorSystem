using policyMicroservice.Models;

namespace policymicroservice.Models
{
    public class ConsumerPolicy
    {
        public int Id { get; set; }

        //public Consumer Consumer { get; set; }

        //public Business Bussiness { get; set; }

        public BusinessMaster BusinessMaster { get; set; }

        public AcceptedQuotes AcceptedQuotes { get; set; }

    }
}
