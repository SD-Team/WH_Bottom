using System;
using System.ComponentModel.DataAnnotations;

namespace Bottom_API.Models
{
    public class HP_Vendor_u01
    {
        [Key]
        public string Vendor_No {get;set;}
        public string Vendor_Desc {get;set;}
        public string Vendor_Name {get;set;}
        public string Boss {get;set;}
        public string Tel {get;set;}
        public string Fax_No {get;set;}
        public string Uniform_ID {get;set;}
        public string Type {get;set;}
        public decimal? Ticket_Days {get;set;}
        public decimal? Cash_Rate {get;set;}
        public string Address_No {get;set;}
        public string Address {get;set;}
        public string Invoice_Address {get;set;}
        public string Envelope_Print {get;set;}
        public string Contact {get;set;}
        public string Mail {get;set;}
        public string Status_YN {get;set;}
        public int? Import_Upper_Day {get;set;}
        public int? Import_Bottom_Day {get;set;}
        public int? Import_Transfer_Day {get;set;}
        public string In_Pay {get;set;}
        public string Forecast {get;set;}
        public string Amt_Code {get;set;}
        public string Check_Title {get;set;}
        public decimal? Discount_Day {get;set;}
        public string Within {get;set;}
        public string Country_ID {get;set;}
        public string Pay_Type {get;set;}
        public string Contract_Type {get;set;}
        public string Seller {get;set;}
        public string Shipper {get;set;}
        public string HP_User {get;set;}
        public DateTime? HP_Date {get;set;}
        public string Update_By {get;set;}
        public DateTime? Update_Time {get;set;}
    }
}