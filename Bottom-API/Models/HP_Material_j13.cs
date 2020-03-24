using System;
using System.ComponentModel.DataAnnotations;

namespace Bottom_API.Models
{
    public class HP_Material_j13
    {
        [Key]
        public string Material_ID {get;set;}
        public string Material_C_Name {get;set;}
        public string Material_E_Name {get;set;}
        public string C_Unit {get;set;}
        public string E_Unit {get;set;}
        public string Purchase_Factory {get;set;}
        public string Material_Type {get;set;}
        public string Purchase_Type {get;set;}
        public string Estimate_Type {get;set;}
        public string Purchase_Print {get;set;}
        public string Size_Print {get;set;}
        public string Check_Material_YN {get;set;}
        public string Check_Material_Level {get;set;}
        public decimal KG_Conversion_Rate {get;set;}
        public string IO_No {get;set;}
        public decimal Out_Box_Length {get;set;}
        public decimal Out_Box_Width {get;set;}
        public decimal Out_Box_Height {get;set;}
        public string Out_Box_Unit {get;set;}
        public decimal Out_Box_Meas {get;set;}
        public string Out_Box_YN {get;set;}
        public decimal Net_Weight {get;set;}
        public decimal Gross_Weight {get;set;}
        public string Internal_Account_Main {get;set;}
        public string Internal_Account_Sub {get;set;}
        public string External_Account_Main {get;set;}
        public string External_Account_Sub {get;set;}
        public string Last_Style {get;set;}
        public string Last_Scolor {get;set;}
        public string Material_Customer_No {get;set;}
        public string Tool_Class {get;set;}
        public string Check_YN {get;set;}
        public DateTime? Check_Date {get;set;}
        public string Purchase_Dept_ID {get;set;}
        public decimal Conversion_Rate {get;set;}
        public string HP_User {get;set;}
        public DateTime? HP_Date {get;set;}
        public string Service_Parts_Factory {get;set;}
        public string Auditor {get;set;}
        public string Update_By {get;set;}
        public DateTime? Update_Time {get;set;}
    }
}