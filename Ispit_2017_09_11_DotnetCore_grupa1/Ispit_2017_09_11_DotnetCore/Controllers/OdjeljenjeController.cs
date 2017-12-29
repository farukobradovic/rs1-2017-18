﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Ispit_2017_09_11_DotnetCore.EntityModels;
using Ispit_2017_09_11_DotnetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ispit_2017_09_11_DotnetCore.Controllers
{
    public class OdjeljenjeController : Controller
    {
        private MojContext _context;
        public OdjeljenjeController(MojContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            OdjeljenjeIndexVM model=new OdjeljenjeIndexVM();
            model.Rows = _context.Odjeljenje.Select(x => new OdjeljenjeIndexVM.Row
            {
                OdjeljenjeID = x.Id,
                Razred = x.Razred,
                Oznaka = x.Oznaka,
                Razrednik = x.Nastavnik.ImePrezime,
                Prebacen = x.IsPrebacenuViseOdjeljenje,
                SkolskaGodina = x.SkolskaGodina,
                ProsjekOcjena = _context.DodjeljenPredmet.Where(y => y.OdjeljenjeStavka.OdjeljenjeId == x.Id).Average(y=> (double?)y.ZakljucnoKrajGodine)??0
            }).ToList();

            return View(model);
        }


        public IActionResult Detalji(int odjeljenjeID)
        {

            OdjeljenjeDetaljiVM model = _context.Odjeljenje
                .Where(x=> x.Id==odjeljenjeID)
                .Select(x => new OdjeljenjeDetaljiVM
            {
                OdjeljenjeID = x.Id,
                Razred = x.Razred,
                Oznaka = x.Oznaka,
                Razrednik = x.Nastavnik.ImePrezime,
                Prebacen = x.IsPrebacenuViseOdjeljenje,
                SkolskaGodina = x.SkolskaGodina,
                ProsjekOcjena =
                    _context.DodjeljenPredmet.Where(y => y.OdjeljenjeStavka.OdjeljenjeId == x.Id)
                        .Average(y => (double?) y.ZakljucnoKrajGodine) ?? 0
            }).SingleOrDefault();

            return View(model);
        }

        public IActionResult Obrisi(int odjeljenjeID)
        {
            Odjeljenje x = _context.Odjeljenje.Find(odjeljenjeID);
            _context.Odjeljenje.Remove(x);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}