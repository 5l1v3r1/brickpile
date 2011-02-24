using System.ComponentModel.DataAnnotations;
using Stormbreaker.Models;

namespace Stormbreaker.Example.Models {
    [PageModel("Home page")]
    public class Home : ModelBase {
        [Display(Name = "H�mta nyheter fr�n")]
        public PageReference PageLink { get; set; }        
    } 
}