using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Respository.IRespository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> companytList = _unitOfWork.Company.GetAll().ToList();
            return View(companytList);
        }
        public IActionResult Upsert(int? id)
        {
            CompanyVM companyVM = new()
            {
                Company = new Company()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(companyVM);
            }
            else
            {
                //Update
                companyVM.Company = _unitOfWork.Company.Get(x => x.Id == id);
                return View(companyVM);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Company company)
        {
            //if (category.Name == category.displayOrder.ToString())
            //{
            //    ModelState.AddModelError("name","The Display Order can't match the Name.");
            //}

            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                TempData["Success"] = "Save successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(company);
            }
        }

        //public IActionResult Delete(int? Id)
        //{
        //    Product? product = _unitOfWork.Product.Get(x => x.Id == Id);
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? Id)
        //{
        //    Product? product = _unitOfWork.Product.Get(x => x.Id == Id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(product);
        //    _unitOfWork.Save();
        //    TempData["Success"] = "Deleted successfully.";
        //    return RedirectToAction(nameof(Index));
        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyObj = _unitOfWork.Company.Get(x => x.Id == id);
            if (companyObj == null)
            {
                return Json(new { success = false,message = "Error while deleting" });
            }
            _unitOfWork.Company.Remove(companyObj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
