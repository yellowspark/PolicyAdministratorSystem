namespace policyMicroservice.Models
{
    public class Business
    {
        /*protected static int _lastId = 0;

        public int ID = ++Business._lastId;*/
        public int ID { get; set; }
        public string BusinessType { get; set; } = "";        //  (Proprietorship/Partnership)
        public decimal CapitalInvested { get; set; } = 0;

        public decimal AnnualTurnover { get; set; } = 0;

        public int TotalEmployees { get; set; } = 0;
    }
}
