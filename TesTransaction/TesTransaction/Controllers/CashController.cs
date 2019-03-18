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
        public ActionResult Index(string sortOrder, DateTime? searchString)
        {
            ViewBag.DateSortParam = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            var cashD = db.CASH_BOTTOM_DAYs.Include(c => c.TERMINAL);
            if (!String.IsNullOrEmpty((searchString).ToString()))
            {
                cashD = cashD.Where(s => s.dateDay == searchString);
            }
            switch (sortOrder)
            {
                case "date_desc":
                    cashD = cashD.OrderByDescending(d => d.dateDay);
                    break;
                default:
                    cashD = cashD.OrderBy(d => d.dateDay);
                    break;
            }

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
                try
                {
                    db.CASH_BOTTOM_DAYs.Add(cashDay);
                    db.SaveChanges();
                    Session["sessTerminalId"] = cashDay.terminalId;
                    return RedirectToAction("Index", "Transaction");
                }
                catch (Exception ex)
                {
                    //to do insert to log file
                    var e1 = ex.GetBaseException(); // --> log
                    var e4 = ex.Message; // --> log
                    var e5 = ex.Source; // --> log
                    var e8 = ex.GetType(); // --> log
                    var e9 = ex.GetType().Name; // --> log

                    ViewBag.Error = "Il existe déjà un fond de caisse sur ce terminal pour cette date";
                    ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", cashDay.terminalId);
                    return View(cashDay);
                }

            }
            ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", cashDay.terminalId);
            return View(cashDay);
        }

        // GET: Cash/Edit/5
        //id1 = dateday   id2 = terminalId
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

        // GET: Cash/End
        public ActionResult End()
        {
            var id1 = DateTime.Today;
            var id2 = Session["sessTerminalId"];
            if (id1 == null || id2 == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                TempData["Error"] = "Le terminal n'a pas été trouvé, existe-t-il un fond de caisse sur ce terminal pour cette date?";
                return RedirectToAction("Transaction", "Home");
            }
            CASH_BOTTOM_DAY cashD = db.CASH_BOTTOM_DAYs.Find(id1, id2);
            if (cashD == null)
            {
                return HttpNotFound();
            }
            ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", cashD.terminalId);
            return View(cashD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult End([Bind(Include = "dateDay,terminalId,beginningCash,endCash")] CASH_BOTTOM_DAY cashD)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(cashD).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //to do insert to log file
                    var e1 = ex.GetBaseException(); // --> log
                    var e4 = ex.Message; // --> log
                    var e5 = ex.Source; // --> log
                    var e8 = ex.GetType(); // --> log
                    var e9 = ex.GetType().Name; // --> log

                    ViewBag.Error = "??";
                }
            }
            ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", cashD.terminalId);
            return View(cashD);
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
