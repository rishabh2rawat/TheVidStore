﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheVidStore.Models;
using TheVidStore.ViewModels;
using System.Data.Entity;

namespace TheVidStore.Controllers

{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }



        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: customer/Det


        public ActionResult Det(int id)
        {
            var customers = _context.Customres.SingleOrDefault(c => c.Id == id);
            {
                if (customers == null)
                    return HttpNotFound();
                else
                    return View(customers);
            };
        }


        


        public ViewResult Index()
        {
            var customers = _context.Customres.Include(c => c.MembershipType).ToList();
            return View(customers);
        }
        public ActionResult Edit(int Id)
        {
            var Customer = _context.Customres.SingleOrDefault(c => c.Id == Id);
            if (Customer == null)
            {
                return HttpNotFound();
            }

            var ViewModel = new NewCustomerViewModel
            {
                Customer = Customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", ViewModel);
        }



        public ActionResult New()
        {
            var MembershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = MembershipTypes
            };
            return View("CustomerFrom",viewModel);
        }


        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            _context.Customres.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}