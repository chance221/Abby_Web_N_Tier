using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [Range(1, 100, ErrorMessage ="Please Select a Number Between 1 and 100.")]
        public int Count { get; set; }

        [BindProperty]
        public MenuItem MenuItem { get; set; }

        public DetailsModel (IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        
        public void OnGet(string id)
        {
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(i => i.Id.ToString() == id, includeProperties: "FoodType,Category");
        }
    }
}
