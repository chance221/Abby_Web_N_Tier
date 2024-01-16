using Abby.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Abby.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;



        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //the include properties is important. It will automatically resolve
            //the id's to the respective table data meaning the category and food type
            //will automatically get populated when retriving the MenuItem that has a 
            //foreign key relationship to these models. 
            var menuItemList = _unitOfWork.MenuItem.GetAll(includeProperties:"Category,FoodType");
            return Json(new { data =  menuItemList });
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault( i => i.Id == id);
            _unitOfWork.MenuItem.Remove(objFromDb);
            _unitOfWork.Save();

            string webRootPath = _hostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            return Json(new { success = true, message= "Delete Successful"});

        }
    }
}
