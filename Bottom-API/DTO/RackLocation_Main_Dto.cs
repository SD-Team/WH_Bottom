using System;

namespace Bottom_API.DTO
{
    public class RackLocation_Main_Dto
    {
        public int ID { get; set; }
        public string Rack_Location { get; set; }
        public string Rack_Code { get; set; }
        public string Rack_Level { get; set; }
        public string Rack_Bin { get; set; }
        public string Factory_ID { get; set; }
        public string WH_ID { get; set; }
        public string Build_ID { get; set; }
        public string Floor_ID { get; set; }
        public string Area_ID { get; set; }
        public decimal? CBM { get; set; }
        public decimal? Max_per { get; set; }
        public string Memo { get; set; }
        public DateTime? Rack_Invalid_date { get; set; }
        public DateTime? Updated_Time { get; set; }
        public string Updated_By { get; set; }
        public string BuildingName { get; set; }
        public string FloorName { get; set; }
        public string AreaName { get; set; }

        public RackLocation_Main_Dto()
        {
            this.Updated_Time = DateTime.Now;
        }
    }
}