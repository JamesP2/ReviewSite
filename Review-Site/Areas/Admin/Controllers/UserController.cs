using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Review_Site.Core.Data;
using Review_Site.Models;
using Review_Site.Areas.Admin.Models;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> userRepository = new Repository<User>();
        private readonly IRepository<Role> roleRepository = new Repository<Role>();

        private List<SelectListItem> GetRoleList()
        {
            return roleRepository.GetAll()
                .Select(r => new SelectListItem
                {
                    Value = r.ID.ToString(),
                    Text = r.Name
                }).ToList();
        }

        //
        // GET: /Admin/User/
        [Restrict(Identifier = "Admin.User.Index")]
        public ViewResult Index()
        {
            return View(userRepository.GetAll().OrderBy(x => x.Username).ToList());
        }

        //
        // GET: /Admin/User/Details/5

        [Restrict(Identifier = "Admin.User.Index")]
        public ViewResult Details(Guid id)
        {
            var user = userRepository.Get(u => u.ID == id).Single();

            return View(user);
        }

        //
        // GET: /Admin/User/Create

        [Restrict(Identifier = "Admin.User.Create")]
        public ActionResult Create()
        {
            ViewBag.RoleList = GetRoleList();

            return View();
        }

        //
        // POST: /Admin/User/Create

        [HttpPost]
        [Restrict(Identifier = "Admin.User.Create")]
        public ActionResult Create(UserForm form)
        {
            if (ModelState.IsValid)
            {
                //We must check for passwords here, since they are not always required in the form.
                if (form.Password == null)
                {
                    ModelState.AddModelError("Password", "Please provide a Password.");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                if (form.SelectedRoleIds.Count == 0)
                {
                    ModelState.AddModelError("SelectedRoleIds", "Users must have at least one role assigned.");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                if (userRepository.Get(x => x.Username == form.Username.ToLower()).Any())
                {
                    ModelState.AddModelError("Username", "Username taken!");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                if (!form.Password.Equals(form.ConfirmedPassword))
                {
                    ModelState.AddModelError("ConfirmedPassword", "Passwords do not match!");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                userRepository.SaveOrUpdate(FormToUser(form));

                return RedirectToAction("Index");
            }

            ViewBag.RoleList = GetRoleList();

            return View(form);
        }

        //
        // GET: /Admin/User/Edit/5

        [Restrict(Identifier = "Admin.User.Edit")]
        public ActionResult Edit(Guid id)
        {
            var user = userRepository.Get(u => u.ID == id).Single();

            ViewBag.RoleList = GetRoleList();

            return View(UserToForm(user));
        }

        private User FormToUser(UserForm form)
        {
            var roleList = form.SelectedRoleIds.Select(g => roleRepository.Get(x => x.ID == g).Single()).ToList();

            return new User
            {
                ID = form.ID,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Username = form.Username,
                Password = PasswordHashing.GetHash(form.Password),
                Roles = roleList
            };
        }

        private static UserForm UserToForm(User user)
        {
            var roleGuids = user.Roles.Select(r => r.ID).ToList();

            return new UserForm
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username.ToLower(), //Just in case...
                SelectedRoleIds = roleGuids
            };
        }

        //
        // POST: /Admin/User/Edit/5

        [HttpPost]
        [Restrict(Identifier = "Admin.User.Edit")]
        public ActionResult Edit(UserForm form)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.Get(u => u.ID == form.ID).Single();

                //Check for new passwords
                if (form.Password != null && form.ConfirmedPassword != null)
                {
                    if (form.ConfirmedPassword.Equals(form.Password))
                    {
                        user.Password = PasswordHashing.GetHash(form.Password);
                    }
                    else
                    {
                        ModelState.AddModelError("ConfirmedPassword", "The new Passwords do not match.");
                        ViewBag.RoleList = GetRoleList();
                        return View(form);
                    }
                }

                if (form.SelectedRoleIds.Count == 0)
                {
                    ModelState.AddModelError("SelectedRoleIds", "Users must have at least one role assigned.");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                user.FirstName = form.FirstName;
                user.LastName = form.LastName;
                user.Username = form.Username;

                user.Roles.Clear();

                foreach (var g in form.SelectedRoleIds)
                {
                    var g1 = g;

                    user.Roles.Add(roleRepository.Get(x => x.ID == g1).Single());
                }

                userRepository.SaveOrUpdate(user);

                return RedirectToAction("Index");
            }

            ViewBag.RoleList = GetRoleList();

            return View(form);
        }
    }
}