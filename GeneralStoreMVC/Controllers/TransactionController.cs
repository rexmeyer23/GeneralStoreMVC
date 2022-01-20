using GeneralStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _db.Transactions.ToList();
            List<Transaction> orderedList = transactionList.OrderBy(trans => trans.Customer).ToList();
            return View(transactionList);
        }

        //GET: Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(_db.Customers);
            ViewBag.CustomerID = new SelectList(_db.Products);
            return View();
        }

        // GET: Delete
        // Transaction/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Edit
        // Transaction/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Details
        // Transaction/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Create
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                Product product = _db.Products.Find(transaction.ProductID);
                if(transaction.NumberOfItems <= product.InventoryCount)
                {
                    _db.Transactions.Add(transaction);
                    product.InventoryCount -= transaction.NumberOfItems;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Number of Items", "More inventory is needed to complete transaction.");
                }
                
            }
            ViewBag.Customers = new SelectList(_db.Customers.OrderBy(c => c.FirstName).ToList());
            ViewBag.Products = new SelectList(_db.Products.OrderBy(p => p.Name).ToList());
            return View(transaction);
        }

        // POST: Delete
        // Transaction/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            Product product = _db.Products.Find(transaction.ProductID);
            _db.Transactions.Remove(transaction);
            product.InventoryCount += transaction.NumberOfItems;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        // POST: Edit
        // Transaction/Edit/{id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(transaction).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }
    }
}