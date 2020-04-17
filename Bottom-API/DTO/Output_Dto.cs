using System.Collections.Generic;

namespace Bottom_API.DTO
{
    public class Output_Dto
    {
        public List<OutputMain_Dto> Outputs { get; set; }
        public List<Material_Sheet_Size_Dto> MaterialSheetSizes { get; set; }
    }
}