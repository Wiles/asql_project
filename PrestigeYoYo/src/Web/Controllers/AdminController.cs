///
///
///

namespace Prestige.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Prestige.DB.Models;
    using Prestige.Services;
    using Prestige.ViewModels;

    [PrestigeAuthorize(Roles = "Administrator")]
    public class AdminController : PrestigeController
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AdminController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public AdminController(
                IMappingEngine mapper,
                IUserService userService) : base(mapper)
        {
            if (userService == null)
            {
                throw new ArgumentException("userService");
            }

            this.UserService = userService;
        }

        /// <summary>
        /// Gets or sets the user service.
        /// </summary>
        /// <value>
        /// The user service.
        /// </value>
        private IUserService UserService { get; set; }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="guid">The user id.</param>
        /// <returns>Empty result.</returns>
        [HttpPost]
        public ActionResult Add(string username, string password, string role)
        {
            // do some validation
            if (!string.IsNullOrWhiteSpace(username)
                && !string.IsNullOrWhiteSpace(password)
                && !string.IsNullOrWhiteSpace(role))
            {
                // get their assigned role
                var newRole = this.UserService.GetRoles()
                                  .FirstOrDefault(r => r.Name == role);

                // just to be sure...
                if (newRole == null)
                {
                    newRole = this.UserService.GetRoles().FirstOrDefault(
                            r => r.Name == "User");
                }

                // create the new user
                var user = new User();
                user.UserName = username;
                user.Password = password;
                user.Roles = new Collection<Role>() { newRole };

                this.UserService.Add(user);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Edits a user.
        /// </summary>
        /// <param name="guid">The user id.</param>
        /// <returns>Empty result.</returns>
        [HttpPost]
        public ActionResult Edit(string guid, string username, string role)
        {
            // you can't edit root or yourself...
            if (username != "root"
                    && username != this.User.Identity.Name as string)
            {
                Guid id;
                if (Guid.TryParse(guid, out id))
                {
                    // find new role
                    var newRole = this.UserService.GetRoles()
                                      .FirstOrDefault(r => r.Name == role);

                    if (newRole != null)
                    {
                        // set the new role
                        this.UserService.SetRoles(id, new Role[] { newRole });
                    }
                }
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="guid">The user id.</param>
        /// <returns>Empty result.</returns>
        [HttpPost]
        public ActionResult Password(string guid, string password)
        {
            Guid id;
            if (Guid.TryParse(guid, out id))
            {
                // change the user's password
                this.UserService.ChangePassword(id, password);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Deletes a user by id.
        /// </summary>
        /// <param name="guid">The user id.</param>
        /// <returns>Empty result.</returns>
        [HttpPost]
        public ActionResult Delete(string guid)
        {
            Guid id;
            if (Guid.TryParse(guid, out id))
            {
                // find the user
                var user =
                    this.UserService.List().FirstOrDefault(u => u.Id == id);

                // you can't delete yourself or root
                if (user != null
                    && user.UserName != "root"
                    && user.UserName != this.User.Identity.Name as string)
                {
                    this.UserService.Delete(user);
                }
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Creates the roles grid.
        /// </summary>
        /// <returns>The roles grid partial view.</returns>
        public ActionResult RolesGrid()
        {
            // return the roles grid
            return PartialView();
        }

        /// <summary>
        /// Creates the passwords grid.
        /// </summary>
        /// <returns>The passwords grid partial view.</returns>
        public ActionResult PasswordGrid()
        {
            return PartialView();
        }

        /// <summary>
        /// Lists existing users for Grid consumption.
        /// </summary>
        /// <param name="sidx">The sidx.</param>
        /// <param name="sord">The sord.</param>
        /// <param name="page">The page.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="_search">if set to <c>true</c> [_search].</param>
        /// <param name="searchField">The search field.</param>
        /// <param name="searchOper">The search oper.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns>JSON encoded users list.</returns>
        [HttpPost]
        public ActionResult Data(
                string sidx,
                string sord,
                int page,
                int rows,
                bool _search,
                string searchField,
                string searchOper,
                string searchString)
        {
            // get all users...
            var users = this.UserService.List().ToArray().AsQueryable();
            var filtered = users;

            if (_search)
            {
                // search if necessary
                filtered = users.Search(searchField, searchString);
            }

            // sort the list
            var sorted = filtered.Sort(sidx, sord);

            // get the total, and page count
            var total = filtered.Count();
            var pages = (int)Math.Ceiling((double)total / (double)rows);

            if (page > pages)
            {
                page = pages;
            }

            // map to the view model
            var models = Mapper.Map<
                    IEnumerable<User>,
                    IEnumerable<UserListModel>>(
                        sorted.Skip((page - 1) * rows).Take(rows).ToArray());

            // get the grid objects
            var objs = from m in models
                       select new
                       {
                           Id = m.Guid,
                           cell = new object[] { m.Guid, m.UserName, m.Password, m.Roles }
                       };

            // build grid data object
            var data = new
            {
                total = pages,
                page = page,
                records = total,
                rows = objs
            };

            // return the grid data
            return Json(data);
        }
    }
}
