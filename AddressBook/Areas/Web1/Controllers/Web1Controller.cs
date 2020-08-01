using System.Web.Mvc;
using AddressBook.Interfaces;
using AddressBook.Models;
using static AddressBook.GlobalVariables;

namespace AddressBook.Areas.Web1.Controllers
{
    [RouteArea("Web1")]
    public class Web1Controller : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhoneNumberRepository _phoneNumberRepository;
        public Web1Controller(IUserRepository userRepository, IPhoneNumberRepository phoneNumberRepository)
        {
            _userRepository = userRepository;
            _phoneNumberRepository = phoneNumberRepository;
            AreaName = "Web1";
            Address = 1;
        }

        public ActionResult Index()
        {
            var model = _userRepository.GetUsersForAddress(1);
            return View("Users", model);
        }

        public ActionResult AddUser()
        {
            var model = new User();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(User model)
        {
            if (ModelState.IsValid)
            {
                model.Address = Address;
                _userRepository.AddUser(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult EditUser(int id)
        {
            var model = _userRepository.GetUser(id);
            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditUser(User model)
        {
            if (ModelState.IsValid)
            {
                _userRepository.EditUser(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("");
        }


        public ActionResult UserPhoneNumbers(int id)
        {
            var model = _phoneNumberRepository.GetUserNumbers(id);
            CurrentUser = _userRepository.GetUser(id);
            return View(model);
        }

        public ActionResult AddNumber(int id)
        {
            var model = new PhoneNumber { UserId = id };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddNumber(PhoneNumber model)
        {
            if (ModelState.IsValid)
            {
                _phoneNumberRepository.AddNumber(model);
                return RedirectToAction("UserPhoneNumbers", new { id = model.UserId });
            }
            return View(model);
        }


        public ActionResult EditNumber(int id)
        {
            var model = _phoneNumberRepository.GetNumber(id);
            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditNumber(PhoneNumber model)
        {
            if (ModelState.IsValid)
            {
                _phoneNumberRepository.EditNumber(model);
                return RedirectToAction("UserPhoneNumbers", new { id = model.UserId });
            }
            return View(model);
        }

        public ActionResult DeleteNumber(int id)
        {
            var userId = _phoneNumberRepository.GetNumber(id).UserId;
            _phoneNumberRepository.DeleteNumber(id);
            return RedirectToAction("UserPhoneNumbers", new { id = userId });
        }

        public ActionResult DeleteAllNumbers(int id)
        {
            _phoneNumberRepository.DeleteAllNumbers(id);
            return RedirectToAction("UserPhoneNumbers", new { id = id });
        }
    }
}