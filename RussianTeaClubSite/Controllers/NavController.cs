using System.Linq;
using System.Web.Mvc;
using RussianTeaClubSite.ViewModels;

namespace RussianTeaClubSite.Controllers
{
    public class NavController : Controller
    {
        public PartialViewResult Menu(string item = null)
        {
            var menuitems = GetMenuItems().Select(i =>
            {
                if (i.ItemName.Equals(item))
                {
                    i.IsSelected = true;
                }
                return i;
            });

            return PartialView(menuitems);
        }

        [NonAction]
        private MenuViewModel[] GetMenuItems()
        {
            return new[]
            {
                new MenuViewModel { ItemName = "Домой", ControllerName = "Article" }, 
                new MenuViewModel { ItemName = "Вакансии", ControllerName = "Vacancy" },
            };
        }
    }
}