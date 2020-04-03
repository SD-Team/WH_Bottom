using System.ComponentModel.DataAnnotations;

namespace Bottom_API.Models
{
    public class WMSB_Transaction_Detail
    {
        [Key]
        public int ID { get; set; }
        public string MyProperty { get; set; }
    }
}