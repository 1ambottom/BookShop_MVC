using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Respository.IRespository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId,includeProperties:"Product")
            };
            foreach (var cal in shoppingCartVM.ShoppingCartList)
            {
                cal.Price = GetPriceBaseOnQuantity(cal);
                shoppingCartVM.OrderTotal += (cal.Price * cal.Count);
            }
            return View(shoppingCartVM);
        }

        public IActionResult Summary()
        {
            return View();
        }
        public IActionResult Plus(int cardId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cardId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cardId)
        {
            
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cardId);
            if (cartFromDb.Count <= 1)
            {
                //remove from cart
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cardId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cardId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        private double GetPriceBaseOnQuantity(ShoppingCart cart)
        {
            if (cart.Count<=50)
            {
                return cart.Product.Price;
            }
            else
            {
                if (cart.Count<=100)
                {
                    return cart.Product.Price50;
                }
                else
                {
                    return cart.Product.Price100;
                }
            }
        }
    }
}
