﻿using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;
using System.Text.Json;

namespace prjDB_GamingForm_Show.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public AdminHomeController(IWebHostEnvironment host, DbGamingFormTestContext db)
        {
            _host = host;
            _db = db;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CLoginViewModel vm)
        {
            Admin admin = _db.Admins.FirstOrDefault(
                a => a.AdminAccount.Equals(vm.txtAccount) && a.AdminPassword.Equals(vm.txtPassword));
            if (admin != null && admin.AdminPassword.Equals(vm.txtPassword))
            {
                string json使用者 = JsonSerializer.Serialize(admin);
                HttpContext.Session.SetString(CDictionary.SK_管理者登入資訊使用關鍵字, json使用者);
                return RedirectToAction("Index","Admin");
            }
            return View();
        }
    }
}
