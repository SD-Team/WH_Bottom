using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class BTW_PackingList_Detail
    {
        [Key][Column(Order = 0)]
        public string Receive_No {get;set;}
        [Key][Column(Order =  1)]
        public string PO_Size {get;set;}
        public decimal Plan_Qty {get;set;}
        public decimal Received_Qty {get;set;}
        public decimal Accum_Qty {get;set;}
        public decimal Balance_Qty {get;set;}
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
    }
}