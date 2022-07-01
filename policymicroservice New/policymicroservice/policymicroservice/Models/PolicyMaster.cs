namespace policymicroservice.Models
{
    public class PolicyMaster
    {
        public string Id { get; set; }

        public string PropertyType { get; set; }

        public string ConsumerType { get; set; }

        public double AssuredSum { get; set; }

        public int Tenure { get; set; }

        public int BussinessValue { get; set; }

        public int PropertyValue { get; set; }

        public string BaseLocation { get; set; }

        // public string PolicyType { get; set; }

    }

    /*public enum PropertyType
    {
        Building =1,
        FactoryEquipment =2,
        propertyInTransit =3

    }

    public enum ConsumerType
    {
        Owner =1,
        Rental =2,
        
    }

    public enum PolicyType
    {
        Replacement=1,
        payBack =2
    }*/


}
