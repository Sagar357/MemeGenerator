﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemeGenerator.Services;

namespace MemeGenerator.Controllers
{
    public class UploadController : Controller
    {
        private static FileService service = new FileService();
        // GET: Upload
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase uploadfile)
        {
            string uploadpath=Server.MapPath("~/image");
            string Message = FileService.Upload(uploadfile, uploadpath);
       
            return RedirectToAction("Index", "Home" ,new { message=Message });
        }


        // GET: Upload/Details/5
        public ActionResult Details(int id)
        {
            return View();

        }

        // GET: Upload/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Upload/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Upload/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Upload/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Upload/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Upload/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
