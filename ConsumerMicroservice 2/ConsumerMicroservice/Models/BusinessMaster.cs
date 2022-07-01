namespace ConsumerMicroservice.Models
{
    public class BusinessMaster
    {
        public int ID { get; set; }
        public Consumer Consumer { get; set; }
        public int BusinessValue { get; set; }
    }
}
