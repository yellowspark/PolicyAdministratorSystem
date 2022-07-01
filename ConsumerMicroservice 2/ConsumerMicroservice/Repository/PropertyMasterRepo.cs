using ConsumerMicroservice.Models;

namespace ConsumerMicroservice.Repository
{
    public class PropertyMasterRepo
    {
        public int rand_int = new Random().Next(11);
        public List<PropertyMaster> Pmaster = new List<PropertyMaster>();
        public PropertyMaster AddToPMaster(Property P)
        {
            PropertyMaster Pmas = new PropertyMaster() { Property = P, PropertyValue = rand_int };
            using (StreamWriter wr = new StreamWriter("Data.PropertyMaster.csv", append: true))
            {
                wr.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", Pmas.Property.ID, Pmas.Property.SquareFeet, Pmas.Property.BuildingType, Pmas.Property.Storeys, Pmas.Property.Age, Pmas.Property.ConsumerID, Pmas.Property.CostOfAsset, Pmas.Property.SalvageValue, Pmas.Property.UsefulLifeOfAsset, Pmas.Property.PropertyType, Pmas.PropertyValue));
            }
            return Pmas;
        }

        public List<PropertyMaster> GetAllPMaster()
        {
            using (StreamReader sr = new StreamReader("Data.PropertyMaster.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    Pmaster.Add(new PropertyMaster() { Property = new Property() { ID = Convert.ToInt32(values[0]), SquareFeet = Convert.ToDecimal(values[1]), BuildingType = values[2], Storeys = Convert.ToInt32(values[3]), Age = Convert.ToInt32(values[4]), ConsumerID = Convert.ToInt32(values[5]), CostOfAsset = Convert.ToDecimal(values[6]), SalvageValue = Convert.ToDecimal(values[7]), UsefulLifeOfAsset = Convert.ToInt32(values[8]), PropertyType = values[9] }, PropertyValue = Convert.ToInt32(values[10]) });
                }
            }

            return Pmaster;
        }

        public PropertyMaster UpdatePMaster(int buisinessId, Property p)
        {
            PropertyMaster Pmas = null;
            Pmaster = GetAllPMaster();
            foreach (var P in Pmaster)
            {
                if (P.Property.ID == buisinessId)
                {
                    P.Property.ConsumerID = p.ConsumerID;
                    P.Property.BuildingType = p.BuildingType;
                    P.Property.Age = p.Age;
                    P.Property.Storeys = p.Storeys;
                    P.Property.SquareFeet = p.SquareFeet;
                    P.Property.UsefulLifeOfAsset = p.UsefulLifeOfAsset;
                    P.Property.CostOfAsset = p.CostOfAsset;
                    P.Property.SalvageValue = p.SalvageValue;
                    P.Property.PropertyType = p.PropertyType;
                    Pmas = P;
                }
            }

            using (StreamWriter wr = new StreamWriter("Data.PropertyMaster.csv"))
            {
                foreach (var P in Pmaster)
                {
                    wr.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", P.Property.ID, P.Property.SquareFeet, P.Property.BuildingType, P.Property.Storeys, P.Property.Age, P.Property.ConsumerID, P.Property.CostOfAsset, P.Property.SalvageValue, P.Property.UsefulLifeOfAsset, P.Property.PropertyType, P.PropertyValue));
                }
            }

            return Pmas;
        }

    }
}
