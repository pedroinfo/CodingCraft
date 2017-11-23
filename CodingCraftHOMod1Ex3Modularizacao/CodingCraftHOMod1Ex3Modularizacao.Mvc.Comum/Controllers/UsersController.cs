using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityMvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using CodingCraftHOMod1Ex3Modularizacao.Dominio.Identity;
using CodingCraftHOMod1Ex3Modularizacao.Dominio.Models;
using CodingCraftHOMod1Ex3Modularizacao.Mvc.Comum.ViewModels;

namespace CodingCraftHOMod1Ex3Modularizacao.Mvc.Comum.Controllers
{
    public class UsersController : Controller
    {
        RoleManager<IdentityRole> _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Users
        public ActionResult Index()
        {
            List<ApplicationUser> users = UserManager.Users.ToList();
            List<ViewUser> items = new List<ViewUser>();
            List<string> userRoles = new List<string>();
            List<string> roleNames = new List<string>();
            foreach (var item in users)
            {
                userRoles = item.Roles.Select(x => x.RoleId).ToList();
                roleNames = _roleManager.Roles.Where(x => userRoles.Contains(x.Id)).Select(x => x.Name).ToList();
                items.Add(new ViewUser { Id = item.Id, Email = item.Email, Roles = roleNames });
            }

            return View(items);
        }

        // GET: Create
        public ActionResult Create()
        {
            var items = new InsertUser();
            items.Roles = _roleManager.Roles.Select(x => new CheckBoxItem{ Id = x.Id, Text = x.Name , Checked = false }).ToList();
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,Password,ConfirmPassword")] InsertUser currentUser, string[] Roles)
        {
            var user = new ApplicationUser { Email = currentUser.Email, UserName = currentUser.Email };
            var result = UserManager.Create(user, currentUser.Password);
            if (result.Succeeded)
            {
                var currentRole = new IdentityRole();
                if (Roles != null)
                {
                    string[] roleNames = _roleManager.Roles.Where(x => Roles.Contains(x.Id)).Select(x => x.Name).ToArray();
                    UserManager.AddToRoles(user.Id, roleNames);
                }
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Errors.FirstOrDefault());
            currentUser.Roles = _roleManager.Roles.Select(x => new CheckBoxItem { Id = x.Id, Text = x.Name, Checked = false }).ToList();
            return View(currentUser);

        }

        public ActionResult Edit(string Id)
        {
            var item = GetEditUser(Id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email")] EditUser modelUser, string[] Roles)
        {
            if (ModelState.IsValid)
            {
                var dbUser = UserManager.Users.Where(x => x.Id == modelUser.Id).FirstOrDefault();
                if (dbUser != null)
                {
                    // delete roles that involve this user
                    string[] currentRoles = dbUser.Roles.Select(r => r.RoleId).ToArray();
                    string[] dbRoles = _roleManager.Roles.Where(x => currentRoles.Contains(x.Id)).Select(x => x.Name).ToArray();
                    UserManager.RemoveFromRoles(dbUser.Id, dbRoles);
                    // update email
                    dbUser.Email = modelUser.Email;
                    UserManager.Update(dbUser);
                    // get our roles
                    if (Roles != null)
                    {
                        var requiredRoles = _roleManager.Roles.Where(x => Roles.Contains(x.Id)).Select(x => x.Name).ToArray();
                        UserManager.AddToRoles(dbUser.Id, requiredRoles);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Model state is not valid!");
                var item = GetEditUser(modelUser.Id);
                modelUser.Roles = item.Roles;
                return View(modelUser);
            }
        }

        public ActionResult ChangePassword(string Id)
        {
            var currentUser = UserManager.Users.Where(x => x.Id == Id).FirstOrDefault();
            if (currentUser != null)
            {
                ChangePassword modelUser = new ChangePassword();
                modelUser.Id = Id;
                return View(modelUser);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "NewPassword,Id,ConfirmPassword")] ChangePassword model)
        {
            var currentUser = UserManager.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            if (currentUser != null)
            {
                var resultRemove = UserManager.RemovePassword(currentUser.Id);
                var resultAdd = UserManager.AddPassword(currentUser.Id, model.NewPassword);
                if (resultAdd.Succeeded && resultRemove.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", resultAdd.Errors.FirstOrDefault());
                    ModelState.AddModelError("", resultRemove.Errors.FirstOrDefault());
                }
            }
            else
            {
                ModelState.AddModelError("", "Cant find user!");
            }
            return View(model);
        }

        public ActionResult Delete(string Id) 
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            
            var currentUser = UserManager.Users.Where(x => x.Id == Id).Select(x => new ViewUser { Id = x.Id, Email = x.Email }).FirstOrDefault();
            if (currentUser != null)
            {
                return View(currentUser);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Id)
        {
            var currentUser = UserManager.Users.Where(x => x.Id == Id).FirstOrDefault();
            if (currentUser != null)
            {
                var result = UserManager.Delete(currentUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault());
                    return View(currentUser);
                }
            }
            else
                return HttpNotFound();
        }


        #region Helpers
        private EditUser GetEditUser(string Id) 
        {
            var dbUser = UserManager.Users.Where(x => x.Id == Id).FirstOrDefault();
            var currentRoles = dbUser.Roles.Select(x => x.RoleId).ToList();
            var allRoles = _roleManager.Roles.ToList();

            EditUser editUser = new EditUser();
            foreach (var x in allRoles)
            {
                if (currentRoles.Contains(x.Id))
                    editUser.Roles.Add(new CheckBoxItem { Id = x.Id, Text = x.Name, Checked = true });
                else
                    editUser.Roles.Add(new CheckBoxItem { Id = x.Id, Text = x.Name , Checked=false});
            }

            editUser.Email = dbUser.Email;
            editUser.Id = dbUser.Id;
            return editUser;
        }
        #endregion

    }
}