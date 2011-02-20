using Dashboard.Models;
using Stormbreaker.Models;
using Stormbreaker.Web.UI;

namespace Dashboard.Web.Mvc.ViewModels {
    public interface IDashboardViewModel {
        IPageModel CurrentModel { get; }
        IStructureInfo StructureInfo { get; }
        NewPageModel NewPageModel { get; }
    }
}