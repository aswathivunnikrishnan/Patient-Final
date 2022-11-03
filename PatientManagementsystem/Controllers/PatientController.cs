﻿using PatientManagementsystem.DAL;
using PatientManagementsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;
using System.Web.Services.Description;

namespace PatientManagementsystem.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        // GET: /home/create
        [HttpGet]
        public ActionResult CreatePatient()
        {
            return View();
        }
        //GET: /home/create
        [HttpPost]
        public ActionResult CreatePatient(Patient p)
        {
            bool result = false;
            PatientDBHelper helper = new PatientDBHelper();
            try
            {
                if (ModelState.IsValid)
                {
                    result = helper.CreatePatientDetails(p);
                    ModelState.Clear();
                  
                    return View("Index");
                }
                else
                    return View();
            
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return View();
            }
        }

        //Get :
        
        public ActionResult GetAll()
        {

            try
            {
                PatientDBHelper helper = new PatientDBHelper();
                List<Patient> patients = helper.GetAll();
                return Json(new { data = patients }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return View();
            }
            
        }

        public ActionResult Edit(int id)
        {
            PatientDBHelper objDBHandle = new PatientDBHelper();
           
            
            var pat = objDBHandle.GetPatientById(id);

            
            return View("Edit", pat);

        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Patient objPatient)
        {
            try
            {
               
                if (ModelState.IsValid)
                {

                    PatientDBHelper objDBHandle = new PatientDBHelper();
                    objDBHandle.UpdatePatient(objPatient);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            finally
            {
                ViewData["Final"] = "Final excecuted!";
            }
        }


        public ActionResult Delete(int id)
        {
           
                PatientDBHelper helper = new PatientDBHelper();
                Patient objPatient = helper.GetPatientById(id);
                return View("Delete", objPatient);

        }


        [HttpPost]
        public ActionResult Delete(int id, Patient patient)
        {
            bool result = false;
            try
            {
                PatientDBHelper objDBHandle = new PatientDBHelper();
                result = objDBHandle.DeleteData(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

    }

}