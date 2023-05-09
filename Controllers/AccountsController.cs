using Assessment.Helpers;
using Assessment.V_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Controllers
{
    public class AccountsController : Controller
    {
        private IConfiguration _config;
        commonHelpers _helper;
         
        public AccountsController(IConfiguration config, commonHelpers helper)
        {
            _config = config;
            _helper = new commonHelpers(_config);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
          public IActionResult Register(RegisterViewModel vm) 
          {
            string UserExistQuery = $"Select * from [UserTable] where UserName='{vm.UserID}'" +
                $"OR Email='{vm.Email}'";
            bool userExists = _helper.UserAlreadyExists(UserExistQuery, _helper.Get_Config());
            if(userExists == true)
            {
                ViewBag.Error = "User and Email Already Exists";
                return View("Register", "Accounts");
            }
            string Query = "Insert into [UserTable](FirstName,LastName,UserID,Password," + $"Email,PhoneNumber)values('{vm.FirstName}' , ' {vm.LastName}'" +
             $",'{vm.UserID}' , '{vm.Password}' , ' {vm.Email}' , ' {vm.PhoneNumber}' , ' {2}')";

            int result = _helper.DMLTransaction(Query);
            if (result > 0)
            {

                EntryIntoSession(vm.UserID);
                ViewBag.Success = "Thanks for Registering";
                return View();
            }
            return View();
          }

         private void EntryIntoSession(string UserID)
        {
            HttpContext.Session.SetString("UserID", UserID);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
