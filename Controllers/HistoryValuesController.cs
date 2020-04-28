using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OneNETDataReceiver.Model;
using System.Data.SqlClient;
using PagedList;

namespace OneNETDataReceiver.Controllers
{
    public class HistoryValuesController : Controller
    {
        private IOTWeb db = new IOTWeb();
        
        private List<SelectListItem> GetMajorList()
        {
            var SNs = db.HistoryValue.OrderBy(m => m.SN).Select(m => m.SN).Distinct();
            var items = new List<SelectListItem>();
            foreach (string SN in SNs)
            {
                items.Add(new SelectListItem
                {
                    Text = SN,
                    Value = SN
                });
            }
            return items;
        }
        //分页控制
        private static readonly int PAGE_SIZE = 50;

        private int GetPageCount(int recordCount)
        {
            int pageCount = recordCount / PAGE_SIZE;
            if (recordCount % PAGE_SIZE != 0)
            {
                pageCount += 1;
            }
            return pageCount;
        }

        private List<HistoryValue> GetPagedDataSource(IQueryable<HistoryValue> Historys, int pageIndex, int recordCount)
        {
            var pageCount = GetPageCount(recordCount);
            if (pageIndex >= pageCount && pageCount >= 1)
            {
                pageIndex = pageCount - 1;
            }
            return Historys.OrderBy(m => m.SN)
         .Skip(pageIndex * PAGE_SIZE)
         .Take(PAGE_SIZE).ToList();
        }
        // GET: HistoryValues
        public ActionResult Index()
        {
            var Historys = db.HistoryValue  as IQueryable<HistoryValue>;
            var recordCount = Historys.Count();
            var pageCount = GetPageCount(recordCount);

            ViewBag.PageIndex = 0;
            ViewBag.PageCount = pageCount;

            ViewBag.MajorList = GetMajorList();
            return View(GetPagedDataSource(Historys, 0, recordCount));
           // ViewBag.MajorList = GetMajorList();
           // return View(db.HistoryValue.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SNs, string Value, int PageIndex)
        {   
            var HistoryValues = db.HistoryValue as IQueryable<HistoryValue>;
            if (!String.IsNullOrEmpty(Value))
            {
                HistoryValues = HistoryValues.Where(m => m.Value.Contains(Value));
            }

            if (!String.IsNullOrEmpty(SNs))
            {
                HistoryValues = HistoryValues.Where(m => m.SN == SNs);
            }
            var recordCount = HistoryValues.Count();
            var pageCount = GetPageCount(recordCount);
            if (PageIndex >= pageCount && pageCount >= 1)
            {
                PageIndex = pageCount - 1;
            }
            HistoryValues = HistoryValues.OrderBy(m => m.SN)
            .Skip(PageIndex * PAGE_SIZE).Take(PAGE_SIZE);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageCount = pageCount;

            ViewBag.MajorList = GetMajorList();
           // return View(HistoryValues.ToList());

           /* ViewBag.PageIndex = 0;
            ViewBag.PageCount = pageCount;
            ViewBag.MajorList = GetMajorList();
            //return View(); */
            return View(GetPagedDataSource(HistoryValues, 0, recordCount));
        }
        

        // GET: HistoryValues/Details/5
        public ActionResult Details(int? id,string SN)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryValue historyValue = db.HistoryValue.Find(id);
            historyValue = db.HistoryValue.Find(SN);
            if (historyValue == null)
            {
                return HttpNotFound();
            }
            return View(historyValue);
        }

        // GET: HistoryValues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistoryValues/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SN,Value,ReportTime,RSRP,SNR,Battery,ServerTime")] HistoryValue historyValue)
        {
            if (ModelState.IsValid)
            {
                db.HistoryValue.Add(historyValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(historyValue);
        }

        // GET: HistoryValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryValue historyValue = db.HistoryValue.Find(id);
            if (historyValue == null)
            {
                return HttpNotFound();
            }
            return View(historyValue);
        }

        // POST: HistoryValues/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SN,Value,ReportTime,RSRP,SNR,Battery,ServerTime")] HistoryValue historyValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historyValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(historyValue);
        }

        // GET: HistoryValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryValue historyValue = db.HistoryValue.Find(id);
            if (historyValue == null)
            {
                return HttpNotFound();
            }
            return View(historyValue);
        }

        // POST: HistoryValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoryValue historyValue = db.HistoryValue.Find(id);
            db.HistoryValue.Remove(historyValue);
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


        /*
                public class SearchManager
                {
                    public List<HistoryValue> GetValueBySearch(string search)
                    {
                        return new HistoryValuesController().GetValueBySearch(search);
                    }
                }   
                    public List<HistoryValue> GetValueBySearch(string search)
                    {
                        string sql = "select ID,SN,Value,ReportTime,RSRP,SNR,Battery from HistoryValue";
                        //sql += " inner join StudentClass on Students.ClassId=StudentClass.ClassId";
                        sql += " where ReportTime like '%{0}%'";
                        sql = string.Format(sql, search);
                        SqlDataReader objReader = SQLHelper.GetReader(sql);
                        List<HistoryValue> list = new List<HistoryValue>();
                        while (objReader.Read())
                        {
                            list.Add(new HistoryValue()
                            {
                                ID = Convert.ToInt32(objReader["ID"]),
                                SN = objReader["SN"].ToString(),
                                Value = objReader["Value"].ToString(),
                                Battery = Convert.ToInt32(objReader["Battery"]),
                                ReportTime = Convert.ToDateTime(objReader["ReportTime"])
                            });
                        }
                        objReader.Close();
                        return list;
                    }
                    public ActionResult GetsearchList()
                    {
                        //获取提交的数据
                        string search = Request.Params["search"];
                        //调用模型处理业务
                        List<HistoryValue> searchList = new SearchManager().GetValueBySearch(search);

                        //保存数据
                        ViewBag.search = search;//为后续继续使用
                        ViewBag.searchList = searchList;

                        //返回视图
                        return View("Index");
                    }*/
        
    }
}
