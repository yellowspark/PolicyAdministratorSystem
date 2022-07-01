namespace policyMicroservice.Models
{
    public class Property
    {
        /*protected static int _lastId = 0;

        public int ID = ++Property._lastId;*/
        public int ID { get; set; } = 0;

        //public int BusinessID { get; set; } = 0;

        public decimal SquareFeet { get; set; } = 0;

        public string BuildingType { get; set; } = "";       //   (Owner/Rental)

        public int Storeys { get; set; } = 0;

        public int Age { get; set; } = 0;

        public int ConsumerID { get; set; }
        public decimal CostOfAssert { get; set; } = 0;
        public decimal SalvageValue { get; set; } = 0;
        public int UsefulLifeOfAssert { get; set; } = 0;

        public string PropertyType { get; set; } = "";     //(Building/Factory Equipment/Property In Transit)
    }
}
