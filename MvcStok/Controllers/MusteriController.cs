using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(/*int sayfa=1,*/ string p)
        {
            var degerler = from d in db.TblMusteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.TblMusteriler.ToList().ToPagedList(sayfa, 4);
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler p1) 
        {
            if (!ModelState.IsValid) 
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(p1);   
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id) 
        {
            var musteri = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id) 
        {
            var mus = db.TblMusteriler.Find(id);
            return View("MusteriGetir",mus);
        }
        public ActionResult Guncelle(TblMusteriler m1)
        {
            var musteri = db.TblMusteriler.Find(m1.MusteriId);
            musteri.MusteriAd = m1.MusteriAd;
            musteri.MusteriSoyad = m1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
}