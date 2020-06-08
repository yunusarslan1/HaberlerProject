using HaberlerProject.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HaberlerProject.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public UserStore<IdentityUser> UserStore => HttpContext.GetOwinContext().Get<UserStore<IdentityUser>>();
        public UserManager<IdentityUser> UserManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
        public SignInManager<IdentityUser, string> SignInManager => HttpContext.GetOwinContext().Get<SignInManager<IdentityUser, string>>();

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            var signInStatus = SignInManager.PasswordSignIn(model.UserName, model.Password, true, true);//giriş yapan logout yapana kadar girişi açık olacak.Bu kulanıcı ve sifreye ait kullanıcı var mı sifre doğru herşey dogruysa cookiye atar.giriş yapmış olur.
            switch (signInStatus)
            {
                case SignInStatus.Success:

                    return RedirectToAction("Index", "HomeAdmin");//giriş başarılı ise
                default:
                    ModelState.AddModelError("", "Giriş Başarısız");
                    return View(model);
            }

        }

        [Authorize(Roles = "Admin»FullYonetim")]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut();

            return RedirectToAction("Login");
        }
        [Authorize(Roles = "Admin»FullYonetim")]
        public ActionResult UserList()
        {

            // var user = UserManager.Users.Where(x => x.Roles.Any(y => y.RoleId == "06b26af1-a323-465b-9835-d42cef1eef88")).ToList();//admin rol olanları getirir.
            var user = UserManager.Users.ToList();
            var listUser = new List<UserVM>();
            listUser.AddRange(user.Select(x => new UserVM()
            {
                Id = x.Id,
                UserName = x.UserName,
                Password = "",
                Role = UserManager.GetRoles(x.Id).FirstOrDefault()

            }));
            return View(listUser);
        }

        [Authorize(Roles = "Admin»FullYonetim")]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin»FullYonetim")]
        public async Task<ActionResult> AddUser(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var iUser = await UserManager.FindByNameAsync(model.UserName);
                if (iUser != null)//kullanıcı varsa
                {
                    TempData["pnotify"] = "error,create," + model.UserName + " adlı hesap sistemimizde kayıtlıdır.Şifreniz ile giriş yapabilirsiniz.İlgili kayıt ";

                    return View(model);
                }

                //yeni kullanıcı bilgilerini async olarak ekler

                var addUser = await UserManager.CreateAsync(new IdentityUser(model.UserName), model.Password);

                if (addUser.Succeeded)//başarılı bir şekilde eklediyse
                {
                    iUser = await UserManager.FindByNameAsync(model.UserName);

                    UserManager.AddToRole(iUser.Id, model.Role);


                    TempData["pnotify"] = "success,create," + model.UserName;
                    return RedirectToAction("UserList", "Account");
                }
                TempData["pnotify"] = "error,create," + " İlgili kayıt (" + addUser.Errors.FirstOrDefault() + ") ";
            }
            else
            {
                TempData["pnotify"] = "error,create," + "Lütfen zorunlu alanları doldurunuz.İlgili kayıt ";
            }

            return View();
        }
        [Authorize(Roles = "Admin»FullYonetim")]
        public async Task<ActionResult> UpdateUser(string id)
        {
            var dbUser = await UserManager.FindByIdAsync(id);
            if (dbUser == null)
            {
                TempData["pnotify"] = "error,edit," + "Böyle bir kullanıcı yok";

                return View("UserList");
            }

            var userModel = new UserVM
            {
                Id = dbUser.Id,
                UserName = dbUser.UserName,
                Password = "",
                Role = UserManager.GetRoles(dbUser.Id).FirstOrDefault()

            };
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin»FullYonetim")]
        public async Task<ActionResult> UpdateUser(UserVM model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);
            try
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var newPasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                    await UserStore.SetPasswordHashAsync(user, newPasswordHash);
                    await UserStore.UpdateAsync(user);
                }

                var roleName = UserManager.GetRoles(user.Id).FirstOrDefault();
                UserManager.RemoveFromRole(user.Id, roleName);
                UserManager.AddToRole(user.Id, model.Role);

                TempData["pnotify"] = "success,edit," + user.UserName;
            }
            catch (Exception ex)
            {
                TempData["pnotify"] = "error,edit," + ex.Message;
            }

            return RedirectToAction("UserList");
        }
        [Authorize(Roles = "Admin»FullYonetim")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            try
            {
                UserManager.RemoveFromRole(user.Id, UserManager.GetRoles(user.Id).FirstOrDefault());
                await UserManager.DeleteAsync(user);
                TempData["pnotify"] = "success,delete," + user.UserName;
            }
            catch (Exception ex)
            {
                TempData["pnotify"] = "error,delete," + ex.Message;
            }

            return RedirectToAction("UserList");
        }



    }

}