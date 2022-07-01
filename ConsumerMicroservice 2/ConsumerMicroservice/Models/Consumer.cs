using System.ComponentModel.DataAnnotations;
namespace ConsumerMicroservice.Models
{
    public class Consumer
    {
        protected static int _lastId = 0;

        public int ID { get; set; } = ++Consumer._lastId;

        public string Name { get; set; } = "";

        // [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string DOB { get; set; }

        public string Email { get; set; } = "";

        public string PAN { get; set; } = "";

        public Business Business { get; set; }

        public override string ToString()
        {
            return $"{ID} {Name} {DOB.ToString()} {Email} {PAN}";
        }

        public Consumer()
        {
            this.ID = ++Consumer._lastId;
            Console.WriteLine("ID " + this.ID + $"{DateTime.Now.ToShortTimeString()}");
        }
    }
}
