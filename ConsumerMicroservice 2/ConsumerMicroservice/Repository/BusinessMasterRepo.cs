using ConsumerMicroservice.Models;

namespace ConsumerMicroservice.Repository
{
    public class BusinessMasterRepo
    {
        public int rand_int = new Random().Next(10);
        public List<BusinessMaster> Bmaster = new List<BusinessMaster>();
        public BusinessMaster AddBMaster(Consumer C)
        {
            BusinessMaster Bmas = new BusinessMaster() { Consumer = C, BusinessValue = rand_int };
            using (StreamWriter wr = new StreamWriter("Data.BusinessMaster.csv", append: true))
            {
                wr.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", Bmas.Consumer.ID, Bmas.Consumer.Name, Bmas.Consumer.DOB, Bmas.Consumer.Email, Bmas.Consumer.PAN, Bmas.Consumer.Business.ID, Bmas.Consumer.Business.BusinessType, Bmas.Consumer.Business.CapitalInvested, Bmas.Consumer.Business.AnnualTurnover, Bmas.Consumer.Business.TotalEmployees, Bmas.BusinessValue);
            }
            return Bmas;
        }

        public List<BusinessMaster> GetBusinessMaster()
        {
            using (StreamReader sr = new StreamReader("Data.BusinessMaster.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    Bmaster.Add(new BusinessMaster()
                    {
                        Consumer = new Consumer()
                        {
                            ID = Convert.ToInt32(values[0]),
                            Name = values[1],
                            DOB = values[2],
                            Email = values[3],
                            PAN = values[4],
                            Business = new Business() { ID = Convert.ToInt32(values[5]), BusinessType = values[6], CapitalInvested = Convert.ToDecimal(values[7]), AnnualTurnover = Convert.ToDecimal(values[8]), TotalEmployees = Convert.ToInt32(values[9]) }
                        },
                        BusinessValue = Convert.ToInt32(values[10])
                    });
                }
            }

            return Bmaster;
        }

        public BusinessMaster UpdateBMaster(int consumerId, Consumer C)
        {
            BusinessMaster Bmas = null;
            Bmaster = GetBusinessMaster();
            foreach (var B in Bmaster)
            {
                if (B.Consumer.ID == consumerId)
                {
                    B.Consumer.Name = C.Name;
                    B.Consumer.Email = C.Email;
                    B.Consumer.PAN = C.PAN;
                    B.Consumer.Business = C.Business;
                    //B.BusinessValue = Bmas.BusinessValue;
                    Bmas = B;
                }
            }

            using (StreamWriter wr = new StreamWriter("Data.BusinessMaster.csv"))
            {
                foreach (var B in Bmaster)
                {
                    wr.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", B.Consumer.ID, B.Consumer.Name, B.Consumer.DOB, B.Consumer.Email, B.Consumer.PAN, B.Consumer.Business.ID, B.Consumer.Business.BusinessType, B.Consumer.Business.CapitalInvested, B.Consumer.Business.AnnualTurnover, B.Consumer.Business.TotalEmployees, B.BusinessValue);
                }
            }


            return Bmas;

        }
    }
}
