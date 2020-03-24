using System;

namespace Bottom_API.DTO
{
    public class CodeID_Detail_Dto
    {
        public string Code_ID { get; set; }
        public int Code_Type { get; set; }
        public string Code_Cname { get; set; }
        public string Code_Ename { get; set; }
        public string Code_Lname { get; set; }
        public DateTime Updated_Time { get; set; }
        public string Updated_By { get; set; }

        public CodeID_Detail_Dto()
        {
            this.Updated_Time = DateTime.Now;
        }
    }
}