using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DinhHoKimXuyen_2122110457.Helpers;
using DinhHoKimXuyen_2122110457.Models;

namespace DinhHoKimXuyen_2122110457.Controllers
{
    public class UserController : Controller
    {
        private ASPEntities db = new ASPEntities();

        // GET: User
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "id,firstname,lastname,username,password,gender,phone,email,address")] user user)
        {
            if (ModelState.IsValid)
            {
                // Hash the password using BCrypt
                user.password = PasswordHelper.GetMd5Hash(user.password);

                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("login");
            }

            return View(user);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,Password")] user model)
        {
            if (ModelState.IsValid)
            {
                var user = db.users.SingleOrDefault(u => u.username == model.username);
                if (user != null && PasswordHelper.VerifyMd5Hash(model.password, user.password))
                {
                    // Successfully authenticated, set up session or authentication cookie
                    // Example:
                    Session["UserName"] = user.username;
                    Session["UserId"] = user.id;
                    Session["Roles"] = user.roles;
                    Session["Address"] = user.address;
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: Home/Index

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,username,password,gender,phone,email,address")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,username,password,gender,phone,email,address")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public  ActionResult UserOrder(int id)
        {
            var user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var orders = db.orders.Where(o => o.userid == id).ToList();
            var ordersWithDetails = orders.Select(o => new OrderWithDetailsModel
            {
                Order = o,
                OrderDetails = db.orderdetails.Where(od => od.order_id == o.id).ToList()
            }).ToList();
            var viewModel = new UserProfileViewModel
            {
                User = user,
                Orders = ordersWithDetails
            };
            return View(viewModel);
        }
        public new ActionResult Profile(int id)
        {
            var user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var orders = db.orders.Where(o => o.userid == id).ToList();
            var ordersWithDetails = orders.Select(o => new OrderWithDetailsModel
            {
                Order = o,
                OrderDetails = db.orderdetails.Where(od => od.order_id == o.id).ToList()
            }).ToList();
            var viewModel = new UserProfileViewModel
            {
                User = user,
                Orders = ordersWithDetails
            };
            return View(viewModel);
        }

    }
}
