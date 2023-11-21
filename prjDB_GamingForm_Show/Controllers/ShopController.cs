﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Transactions;

namespace prjDB_GamingForm_Show.Controllers
{
    namespace prjDB_GamingForm_Show.Controllers
    {
        public class ShopController : Controller
        {

            private readonly IWebHostEnvironment _host;
            private readonly DbGamingFormTestContext _db;
            public ShopController(IWebHostEnvironment host, DbGamingFormTestContext db)
            {
                _host = host;
                _db = db;
            }
            public IActionResult Index(CKeyWord ck)
            {
                //UI相關
                //Todo 商品切成XX個一頁，下面要有1~XX個頁，上面要有選項讓客人決定一頁呈現 20 OR 40個   //補充 蝦皮沒分 大約60件一頁
                //Todo 商品圖片大小不一需要修正
                //Todo 篩選 價格排序 上架日期排序 銷售件數排序
                //Todo 研究一下商品上架/修改的選擇圖片那區，檔案名移除+讓他好看點

                //軟體功能相關
                //Todo  你的交易邏輯有待考據。請洽GPT或者芳芳老師
                //Todo  tag選擇後要有DIV去接客人選擇的Subtag，可視化讓客人選擇自己商品的標籤，再用迴圈塞入資料庫
                //Todo  產品留言功能目前未實作

                String CK = ck.txtKeyword;
                IEnumerable<Product> Pdb = null;
                if (string.IsNullOrEmpty(CK))
                {
                    Pdb = (from aa in _db.Products
                           where aa.StatusId == 1
                          select aa)/*.Take(25)*/;
                }
                else
                {
                    Pdb = _db.Products.Where(p => p.ProductName.Contains(CK)&&p.StatusId==1);
                }
                string json = "";
                ViewBag.Car = 0;
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCES_LIST))
                {
                    json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                    List<ShoppingCar> car = JsonSerializer.Deserialize<List<ShoppingCar>>(json);
                    ViewBag.Car = car.Count();
                }
                return View(Pdb);

            }
            public IActionResult IndexPage(int? id) //拿來跳page用的 id用變數去計算，++--一個變數去控制讀取到的最後一個商品控制Page
            {
                IEnumerable<Product> Pdb = null;
                Pdb = (from aa in _db.Products select aa).Skip((int)id).Take(25);//到最後一頁之後不能按 邏輯再補充
                return Json(Pdb);
            }
            public ActionResult Create()
            {
                _db.Products.Load();
                return View();
            }
            public ActionResult CreateTag()
            {
                var Tag = _db.Tags.Where(p => p.TagId <= 3).Select(t => new { t.TagId, t.Name }).ToList();

                return Json(Tag);
            }
            public ActionResult SubTag(int? id)
            {
                _db.SubTags.Load();
                var Tag = _db.SubTags.Where(p => p.TagId == id).Select(s => new { s.SubTagId, s.Name }).ToList();
                return Json(Tag);
            }

            public ActionResult SelSubTag(int? id)
            {
                _db.SubBlogs.Load();
                var SelSub = _db.SubTags.Where(p => p.SubTagId == id).Select(s => new { s.SubTagId, s.Name }).ToList();
                return Json(SelSub);
            }

            [HttpPost]
            public ActionResult Create(CProductWarp product) //原Product物件
            {
                Product x = new Product();
                if (_db != null)
                {
                    if (product.photo != null)
                    {
                        string photoName = Guid.NewGuid().ToString() + ".jpg";
                        x.FImagePath = photoName;
                        product.photo.CopyTo(new FileStream(_host.WebRootPath + "/images/shop/" + photoName, FileMode.Create));
                    }
                    x.ProductName = product.ProductName;
                    x.Price = product.Price;
                    x.AvailableDate = product.AvailableDate;
                    x.ProductContent = product.ProductContent;
                    x.UnitStock = product.UnitStock;
                    x.StatusId = product.StatusID;
                    x.MemberId = product.MemberID;

                    _db.Products.Add(x);
                    _db.SaveChanges();
                }
                //Thread.Sleep(3000);
                return RedirectToAction("Index");
            }



            public ActionResult Edit(int? id)
            {

                Product pdb = _db.Products.FirstOrDefault(p => p.ProductId == id);
                if (pdb == null)
                    return RedirectToAction("Index");

                return View(pdb);
            }

            [HttpPost]
            public ActionResult Edit(CProductWarp product) //原Product物件   
            {
                Product x = _db.Products.FirstOrDefault(p => p.ProductId == product.ProductID);
                if (x != null)
                {
                    if (product.photo != null)
                    {
                        string photoName = Guid.NewGuid().ToString() + ".jpg";
                        x.FImagePath = photoName;
                        product.photo.CopyTo(new FileStream(_host.WebRootPath + "/images/shop/" + photoName, FileMode.Create));
                    }
                    x.ProductName = product.ProductName;
                    x.Price = product.Price;
                    x.AvailableDate = product.AvailableDate;
                    x.ProductContent = product.ProductContent;
                    x.UnitStock = product.UnitStock;
                    x.StatusId = product.StatusID;
                    x.MemberId = product.MemberID;
                    //目前沒有FirmID跟圖片功能
                    //MemberID跟StatusID是寫死在CProduct物件中的
                    _db.SaveChanges();
                }
				//Thread.Sleep(3000);
				return RedirectToAction("Index");
            }

            public ActionResult Delete(int id)    ////不確定是否要用刪除的方式來表達商品下架，刪除資料也會讓關聯表紀錄消失。
            {

                Product x = _db.Products.FirstOrDefault(p => p.ProductId == id);
                if (x != null)
                {
                    _db.Products.Remove(x);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            public ActionResult Details(int id)
            {

                _db.Products.Load();
                Product x = _db.Products.FirstOrDefault(p => p.ProductId == id);
                return View(x);
            }
            public IActionResult AddToCar(int? id)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.FID = id;
                return View();
            }

            [HttpPost]
            public IActionResult AddToCar(CShoppingCarViewModel vm)
            {
                Product product = _db.Products.FirstOrDefault(x => x.ProductId == vm.ProductID);
                if (product != null)
                {
                    string json = "";
                    List<ShoppingCar> car = null;
                    if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCES_LIST))
                    {
                        json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                        car = JsonSerializer.Deserialize<List<ShoppingCar>>(json);
                    }
                    else
                    {
                        car = new List<ShoppingCar>();
                    }
                    ShoppingCar x = new ShoppingCar();
                    x.Price = (decimal)product.Price;
                    x.ProductName = product.ProductName;
                    x.Count = vm.txtCount;
                    x.ProductID = product.ProductId;
                    x.product = product;
                    car.Add(x);
                    json = JsonSerializer.Serialize(car);
                    HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCES_LIST, json);
                    ViewBag.Car = car.Count();
                }
                
                return RedirectToAction("Index");
            }

            public IActionResult CarView()
            {
                if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCES_LIST))
                {
                    return RedirectToAction("Index");
                }
                string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                List<ShoppingCar> car = JsonSerializer.Deserialize<List<ShoppingCar>>(json);
                if (car == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Car = car.Count();
                return View(car);
            }

            public IActionResult DeleteFromCar(int id)////應該能正常移除購物車了/////
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                List<ShoppingCar> car = JsonSerializer.Deserialize<List<ShoppingCar>>(json);
                ShoppingCar x = car.FirstOrDefault(p => p.ProductID == id);
                if (x != null)
                {
                    car.Remove(x);
                }
                json = JsonSerializer.Serialize(car);
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCES_LIST, json);
                ViewBag.Car = car.Count();
                return RedirectToAction("CarView");
            }

            [HttpPost]
            public IActionResult EditCar(CShoppingCarViewModel vm)
            {
                Product product = _db.Products.FirstOrDefault(p => p.ProductId == vm.ProductID);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                List<ShoppingCar> car = JsonSerializer.Deserialize<List<ShoppingCar>>(json);
                ShoppingCar x = car.FirstOrDefault(p => p.ProductID == vm.ProductID);
                if (x != null)
                {
                    car.Remove(x);
                }
                ShoppingCar n = new ShoppingCar()
                {
                    ProductID = vm.ProductID,
                    ProductName = product.ProductName,
                    Price = (decimal)product.Price,
                    Count = vm.txtCount
                };
                car.Add(n);
                json = JsonSerializer.Serialize(car);
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCES_LIST, json);
                ViewBag.Car = car.Count();
                return RedirectToAction("CarView");
            }


            public IActionResult Purchase()
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                List<ShoppingCar> car = JsonSerializer.Deserialize<List<ShoppingCar>>(json);
                car.Clear();
                json = JsonSerializer.Serialize(car);
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCES_LIST, json);
                ViewBag.Car = 0;
                Thread.Sleep(1000);
                return RedirectToAction("Index");
            }

        }
    }
}
