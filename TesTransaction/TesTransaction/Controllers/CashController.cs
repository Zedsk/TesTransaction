using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesTransaction.Data.Entity;

namespace TesTransaction.Controllers
{
    public class CashController : Controller
    {
        private TestTransactionEntities db = new TestTransactionEntities();

        // GET: Cash
        public ActionResult Index()
        {
            var cashD = db.CASH_BOTTOM_DAYs.Include(c => c.TERMINAL);
            return View(cashD.ToList());
        }

        // GET: Cash/Details/5
        public ActionResult Details(DateTime id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CASH_BOTTOM_DAY cashD = db.CASH_BOTTOM_DAYs.Find(id1, id2);
            if (cashD == null)
            {
                return HttpNotFound();
            }
            return View(cashD);
        }

        // GET: Cash/Create
        public ActionResult Create()
        {
            ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal");
            return View();
        }

        // POST: Cash/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dateDay,terminalId,beginningCash")] CASH_BOTTOM_DAY cashDay)
        {
            if (ModelState.IsValid)
            {
                db.CASH_BOTTOM_DAYs.Add(cashDay);
                db.SaveChanges();
                var tId = cashDay.terminalId;
                return RedirectToAction("Index", "Transaction", new { terminal = tId });
            }

            ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", cashDay.terminalId);
            return View(cashDay);
        }

        // GET: Cash/Edit/5
        public ActionResult Edit(DateTime id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CASH_BOTTOM_DAY cashD = db.CASH_BOTTOM_DAYs.Find(id1, id2);
            if (cashD == null)
            {
                return HttpNotFound();
            }
            ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", cashD.terminalId);
            return View(cashD);
        }

        // POST: Cash/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dateDay,terminalId,beginningCash,endCash")] CASH_BOTTOM_DAY cashD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", cashD.terminalId);
            return View(cashD);
        }

        // GET: Cash/Delete/5
        public ActionResult Delete(DateTime id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CASH_BOTTOM_DAY cashD = db.CASH_BOTTOM_DAYs.Find(id1, id2);
            if (cashD == null)
            {
                return HttpNotFound();
            }
            return View(cashD);
        }

        // POST: Cash/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id1, int? id2)
        {
            CASH_BOTTOM_DAY cashD = db.CASH_BOTTOM_DAYs.Find(id1, id2);
            db.CASH_BOTTOM_DAYs.Remove(cashD);
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
    }
}
