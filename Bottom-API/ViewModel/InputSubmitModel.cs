using System.Collections.Generic;
using Bottom_API.DTO;

namespace Bottom_API.ViewModel
{
    public class InputSubmitModel
    {
        public List<Transaction_Detail_Dto> TransactionList {get;set;}
        public List<string> InputNoList{get;set;}
    }
}