using System.ComponentModel.DataAnnotations;
using Stormbreaker.Models;

namespace Stormbreaker.Example.Models {
    [PageModel("Home page")]
    public class Home : ModelBase {

        [Display(Name = "H�mta nyheter fr�n")]
        public PageReference PageLink { get; set; }

        [Display(Name = "H�mta nyheter fr�n")]
        public PageReference PageLink2 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Header { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image2 { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image3 { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image4 { get; set; }
    } 
}