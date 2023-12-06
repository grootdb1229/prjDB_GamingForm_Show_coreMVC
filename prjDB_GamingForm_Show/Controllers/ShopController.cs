﻿using DB_GamingForm_Show.Job.DeputeClass;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
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
            //public List<CShopPageViewModel> List { get; set; }
            //public IActionResult test(CKeyWord ck)
            //{
            //    String CK = ck.txtKeyword;
            //    _db.ProductTags.Load();
            //    _db.Products.Load();
            //    _db.SubTags.Load();
            //    List<CShopPageViewModel> List = new List<CShopPageViewModel>();
            //    CShopPageViewModel Pdb = null;
            //    if (string.IsNullOrEmpty(CK))
            //    {
            //        var data = (from n in _db.ProductTags
            //                    where n.Product.StatusId == 1
            //                    select new
            //                    {
            //                        n.Product.ProductId,
            //                        n.Product.ProductName,
            //                        n.Product.Price,
            //                        n.Product.FImagePath,
            //                        n.SubTag.Name
            //                    }).ToList();
            //        foreach (var item in data)
            //        {
            //            string s = "";
            //            var tagname = _db.ProductTags.Where(x => x.ProductId == item.ProductId).Select(x => x.SubTag.Name).ToList();
            //            foreach (var t in tagname)
            //            {
            //                s += t.ToString() + "/";
            //            }
            //            Pdb = new CShopPageViewModel
            //            {
            //                ProductId = item.ProductId,
            //                ProductName = item.ProductName,
            //                Price = item.Price,
            //                FImagePath = item.FImagePath,
            //                SubTagName = s
            //            };
            //            List.Add(Pdb);
            //        };
            //    }
            //    else
            //    {
            //        var data = from n in _db.ProductTags
            //                   where n.Product.ProductName.Contains(CK) && n.Product.StatusId == 1
            //                   select new
            //                   {
            //                       n.Product.ProductId,
            //                       n.Product.ProductName,
            //                       n.Product.Price,
            //                       n.Product.FImagePath,
            //                       SubTagName = n.SubTag.Name
            //                   };
            //        foreach (var item in data)
            //        {
            //            string s = "";
            //            var tagname = _db.ProductTags.Where(x => x.ProductId == item.ProductId).Select(x => x.SubTag.Name).ToList();
            //            foreach (var t in tagname)
            //            {
            //                s += t.ToString() + "/";
            //            }
            //            Pdb = new CShopPageViewModel
            //            {
            //                ProductId = item.ProductId,
            //                ProductName = item.ProductName,
            //                Price = item.Price,
            //                FImagePath = item.FImagePath,
            //                SubTagName = s
            //            };
            //            List.Add(Pdb);
            //        };
            //    }
            //    bool flag = false;
            //    List<CShopPageViewModel> List2 = new List<CShopPageViewModel>();
            //    List2.Add(List[0]);
            //    for (int i = 1; i < List.Count; i++)
            //    {
            //        for (int j = 0; j < List2.Count; j++)
            //        {
            //            if (List[i].ProductId == List2[j].ProductId)
            //            {
            //                flag=true; 
            //                break;
            //            }
            //        }
            //        if (flag == false)
            //        { 
            //            List2.Add(List[i]);
            //        }
            //        flag = false;
            //    }

            //    string json = "";
            //    ViewBag.Car = 0;
            //    if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCES_LIST))
            //    {
            //        json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
            //        List<ShoppingCar> car = JsonSerializer.Deserialize<List<ShoppingCar>>(json);
            //        ViewBag.Car = car.Count();
            //    }
            //    List = List.Distinct().ToList();
            //    return View(List2);
            //}
            public IActionResult Carousel()
            {   _db.Products.Load();
                _db.ProductTags.Load();
                return PartialView();
             
            }

            public IActionResult SelSubtag()
            {
              
                return PartialView();

            }
            public IActionResult Index(CKeyWord ck)
            {

                String CK = ck.txtKeyword;
                _db.ProductTags.Load();
                _db.Products.Load();
                _db.SubTags.Load();
                List<CShopPageViewModel> List = new List<CShopPageViewModel>();
                CShopPageViewModel Pdb = null;
                if (string.IsNullOrEmpty(CK))
                {
                    var data = (from n in _db.ProductTags
                                where n.Product.StatusId == 1
                                select new
                                {
                                    n.Product.ProductId,
                                    n.Product.ProductName,
                                    n.Product.Price,
                                    n.Product.FImagePath,
                                    n.SubTag.Name
                                }).ToList();
                    foreach (var item in data)
                    {
                        string s = "";
                        var tagname = _db.ProductTags.Where(x => x.ProductId == item.ProductId).Select(x => x.SubTag.Name).ToList();
                        foreach (var t in tagname)
                        {
                            s += t.ToString() + "/";
                        }
                        Pdb = new CShopPageViewModel
                        {
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Price = item.Price,
                            FImagePath = item.FImagePath,
                            SubTagName = s
                        };
                        List.Add(Pdb);
                    };
                }
                else
                {
                    var data = (from n in _db.ProductTags
                               where n.Product.ProductName.Contains(CK) && n.Product.StatusId == 1
                               select new
                               {
                                   n.Product.ProductId,
                                   n.Product.ProductName,
                                   n.Product.Price,
                                   n.Product.FImagePath,
                                   SubTagName = n.SubTag.Name
                               }).ToList();
                    foreach (var item in data)
                    {
                        string s = "";
                        var tagname = _db.ProductTags.Where(x => x.ProductId == item.ProductId).Select(x => x.SubTag.Name).ToList();
                        foreach (var t in tagname)
                        {
                            s += t.ToString() + "/";
                        }
                        Pdb = new CShopPageViewModel
                        {
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Price = item.Price,
                            FImagePath = item.FImagePath,
                            SubTagName = s
                        };
                        List.Add(Pdb);
                    };
                }
                if (List.Count==0)//////"查無資料"邏輯 
                {
                    //return RedirectToAction("Index");
                    ViewBag.Message = "查無資料，請確認輸入內容"; 
                    return View(); 
                }
                else
                {
                    bool flag = false;
                    List<CShopPageViewModel> List2 = new List<CShopPageViewModel>();
                    List2.Add(List[0]);
                    for (int i = 1; i < List.Count; i++)
                    {
                        for (int j = 0; j < List2.Count; j++)
                        {
                            if (List[i].ProductId == List2[j].ProductId)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag == false)
                        {
                            List2.Add(List[i]);
                        }
                        flag = false;
                    }

                    string json = "";
                    ViewBag.Car = 0;
                    if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCES_LIST))
                    {
                        json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                        List<ShoppingCar> car = JsonSerializer.Deserialize<List<ShoppingCar>>(json);
                        ViewBag.Car = car.Count();
                    }

                    var tags = _db.Tags.Where(x => x.TagId > 0 && x.TagId < 4).Select(x => x.Name);
                    //dynamic myModels = new ExpandoObject();      
                    //myModels.product = List2; 
                    //myModels.customerdetail = tags;
                    string jsonResult = JsonSerializer.Serialize(List2);
                    Trace.WriteLine("檢查裝什麼1" + jsonResult);
                    Trace.WriteLine("檢查裝什麼2" + List2);
                    return View(List2);
                }
            }
     
            public IActionResult IndexbyAjax(string CK)
            {
                _db.ProductTags.Load();
                _db.Products.Load();
                _db.SubTags.Load();

                var data = _db.ProductTags
                    .Where(n => (string.IsNullOrEmpty(CK) || n.Product.ProductName.Contains(CK)) && n.Product.StatusId == 1)
                    .Select(n => new
                    {
                        n.Product.AvailableDate,
                        n.Product.ProductId,
                        n.Product.ProductName,
                        n.Product.Price,
                        n.Product.FImagePath,
                        SubTagName = n.SubTag.Name
                    })           
                    .ToList();

                List<CShopPageViewModel> List = data.Select(item =>
                {   //將每個字以/串再一起。
                    string s = string.Join("/", _db.ProductTags.Where(x => x.ProductId == item.ProductId).Select(x => x.SubTag.Name));
                    return new CShopPageViewModel //返還這個物件 Lambda多行表達需要加return，不然將會返還錯誤的東西，部分程式碼將不被視為返還值之一
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        FImagePath = item.FImagePath,
                        SubTagName = s
                    };
                }).ToList();
                ///篩掉重複資料
                List<CShopPageViewModel> List2 = List.GroupBy(p => p.ProductId)
                                                     .Select(group => group.First())
                                                     .ToList();

                if (List2.Count == 0)//如果沒有任何搜尋結果
                {              
                    return Json(new { message = "查無資料，請確認輸入內容" });
                }

                string jsonResult = JsonSerializer.Serialize(List2);
                //Trace.WriteLine("AAA" + jsonResult);
                //Trace.WriteLine("BBB" + List2);
                return Content(jsonResult, "application/json");
            }

            public IActionResult IndexbyDate(string CK)
            {
                _db.ProductTags.Load();
                _db.Products.Load();
                _db.SubTags.Load();

                var data = _db.ProductTags
                    .Where(n => (string.IsNullOrEmpty(CK) || n.Product.ProductName.Contains(CK)) && n.Product.StatusId == 1)
                    .Select(n => new
                    {
                        n.Product.AvailableDate,
                        n.Product.ProductId,
                        n.Product.ProductName,
                        n.Product.Price,
                        n.Product.FImagePath,
                        SubTagName = n.SubTag.Name
                    })
                    .OrderByDescending(x => x.AvailableDate.Date)
                    .ToList();

                List<CShopPageViewModel> List = data.Select(item =>
                {   //將每個字以/串再一起。
                    string s = string.Join("/", _db.ProductTags.Where(x => x.ProductId == item.ProductId).Select(x => x.SubTag.Name));
                    return new CShopPageViewModel //返還這個物件 Lambda多行表達需要加return，不然將會返還錯誤的東西，部分程式碼將不被視為返還值之一
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        FImagePath = item.FImagePath,
                        SubTagName = s
                    };
                }).ToList();
                ///篩掉重複資料
                List<CShopPageViewModel> List2 = List.GroupBy(p => p.ProductId)
                                                     .Select(group => group.First())
                                                     .ToList();

                if (List2.Count == 0)//如果沒有任何搜尋結果
                {
                    return Json(new { message = "查無資料，請確認輸入內容" });
                }


                string jsonResult = JsonSerializer.Serialize(List2);
                //Trace.WriteLine("AAA" + jsonResult);
                //Trace.WriteLine("BBB" + List2);
                return Content(jsonResult, "application/json");
            }
            public IActionResult IndexbyPrice_H(String CK)
            {
                _db.ProductTags.Load();
                _db.Products.Load();
                _db.SubTags.Load();

                var data = _db.ProductTags
                    .Where(n => (string.IsNullOrEmpty(CK) || n.Product.ProductName.Contains(CK)) && n.Product.StatusId == 1)
                    .Select(n => new
                    {
                        n.Product.AvailableDate,
                        n.Product.ProductId,
                        n.Product.ProductName,
                        n.Product.Price,
                        n.Product.FImagePath,
                        SubTagName = n.SubTag.Name
                    })
                    .OrderByDescending(x => x.Price)
                    .ToList();

                List<CShopPageViewModel> List = data.Select(item =>
                {   //將每個字以/串再一起。
                    string s = string.Join("/", _db.ProductTags.Where(x => x.ProductId == item.ProductId).Select(x => x.SubTag.Name));
                    return new CShopPageViewModel //返還這個物件 Lambda多行表達需要加return，不然將會返還錯誤的東西，部分程式碼將不被視為返還值之一
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        FImagePath = item.FImagePath,
                        SubTagName = s
                    };
                }).ToList();
                ///篩掉重複資料 GroupBy把一樣的ID篩一起，.First取一個。
                List<CShopPageViewModel> List2 = List.GroupBy(p => p.ProductId)
                                                     .Select(group => group.First())
                                                     .ToList();

                if (List2.Count == 0)//如果沒有任何搜尋結果
                {
                    return Json(new { message = "查無資料，請確認輸入內容" });
                }

                string jsonResult = JsonSerializer.Serialize(List2);
                //Trace.WriteLine("AAA" + jsonResult);
                //Trace.WriteLine("BBB" + List2);
                return Content(jsonResult, "application/json");
            }
            public IActionResult IndexbyPrice_L(String CK)
            {

                _db.ProductTags.Load();
                _db.Products.Load();
                _db.SubTags.Load();

                var data = _db.ProductTags
                    .Where(n => (string.IsNullOrEmpty(CK) || n.Product.ProductName.Contains(CK)) && n.Product.StatusId == 1)
                    .Select(n => new
                    {
                        n.Product.AvailableDate,
                        n.Product.ProductId,
                        n.Product.ProductName,
                        n.Product.Price,
                        n.Product.FImagePath,
                        SubTagName = n.SubTag.Name
                    })
                    .OrderBy(x => x.Price)
                    .ToList();

                List<CShopPageViewModel> List = data.Select(item =>
                {   //將每個字以/串再一起。
                    string s = string.Join("/", _db.ProductTags.Where(x => x.ProductId == item.ProductId).Select(x => x.SubTag.Name));
                    return new CShopPageViewModel  //返還這個物件 Lambda多行表達需要加return，不然將會返還錯誤的東西，部分程式碼將不被視為返還值之一
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        FImagePath = item.FImagePath,
                        SubTagName = s
                    };
                }).ToList();
                    ///篩掉重複資料 GroupBy把一樣的ID篩一起，.First取一個。
                List<CShopPageViewModel> List2 = List.GroupBy(p => p.ProductId)
                                                     .Select(group => group.First())
                                                     .ToList();

                if (List2.Count == 0)//如果沒有任何搜尋結果
                {
                    return Json(new { message = "查無資料，請確認輸入內容" });
                }

                string jsonResult = JsonSerializer.Serialize(List2);
                //Trace.WriteLine("AAA" + jsonResult);
                //Trace.WriteLine("BBB" + List2);
                return Content(jsonResult, "application/json");
            }
            //    public IActionResult IndexPage(int? id) //拿來跳page用的 id用變數去計算，++--一個變數去控制讀取到的最後一個商品控制Page
            //{
            //    IEnumerable<Product> Pdb = null;
            //    Pdb = (from aa in _db.Products select aa).Skip((int)id).Take(25);//到最後一頁之後不能按 邏輯再補充
            //    return Json(Pdb);
            //}
            public ActionResult Create()
            {
                _db.Products.Load();
                return View();
            }

            public ActionResult language()
            {
                _db.SubTags.Load();
                var Lang = _db.SubTags.Where(p => p.TagId == 3).Select(s => new { s.SubTagId, s.Name }).ToList();
                return Json(Lang);
            }
            public ActionResult GameTag()
                {
				// 假設 SubTag 類別有一個名為 ProductTags 的導覽屬性
				var SelSub = _db.SubTags
					.Where(p => p.TagId == 1)
					.Select(s => new
					{
						s.SubTagId,
					    s.Name,
						ProductTagCount = s.ProductTags.Count() // 使用導覽屬性的 Count 方法
					})
					.ToList();

				return Json(SelSub);

			}
			public ActionResult WhenYouEditTags(int? id)
            {
                _db.ProductTags.Load();
                //Trace.WriteLine("BBBBB" + id);
                var SelSub = _db.ProductTags.Where(p => p.ProductId == id).Select(s => s.SubTagId).ToList();
                //Trace.WriteLine("AAAAA"+SelSub);
                return Json(SelSub);
            }

            [HttpPost]
            public ActionResult Create(超酷warp product) //先這樣Warp應該是用於資料驗證，有待看影片確認但目前不這樣寫驗證會一直錯誤
            {//但我其實也想把驗證改寫到前端，這些資料本質並不是重要到要寫後端驗證
                //Trace.WriteLine("AAAA" + product.GameTagOptions);
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors); //測試錯誤用程式碼
                    return View(product);
                }

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
                    x.StatusId = 7;
                    x.MemberId = product.MemberID;

                    _db.Products.Add(x);
                    _db.SaveChanges();
                }
                if (product.GameTagOptions != null)//確保有選標籤
                {
                    List<int> tagsList = product.GameTagOptions.Split(',').Select(int.Parse).ToList();
                    Trace.WriteLine("AAAA" + tagsList);
                    foreach (var tags in tagsList)
                    {
                        int tagsID = _db.SubTags.FirstOrDefault(x => x.SubTagId == tags).SubTagId;
                        ProductTag productTag = new ProductTag
                        {
                            SubTagId = tagsID,
                            ProductId = x.ProductId
                        };

                        _db.ProductTags.Add(productTag);
                    }

                    _db.SaveChanges();

                }//Thread.Sleep(3000);
                return RedirectToAction("Index");
            }



            public ActionResult Edit(int? id)
            {

                Product pdb = _db.Products.FirstOrDefault(p => p.ProductId == id);
                if (pdb == null)
                { return RedirectToAction("Index"); }
                超酷warp cProductWarp = new 超酷warp();

                cProductWarp.FImagePath = pdb.FImagePath;
                cProductWarp.ProductId = pdb.ProductId;
                cProductWarp.ProductName = pdb.ProductName;
                cProductWarp.Price = pdb.Price;
                cProductWarp.ProductContent = pdb.ProductContent;
                cProductWarp.UnitStock = pdb.UnitStock;
                cProductWarp.StatusID = (int)pdb.StatusId;
                cProductWarp.MemberID = pdb.MemberId;
                return View(cProductWarp);
            }

            [HttpPost]
            public ActionResult Edit(超酷warp product) //原Product物件   
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return View((超酷warp)product);
                }

                Product x = _db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (x != null)
                {
                    if (product.photo != null)
                    {
                        string photoName = Guid.NewGuid().ToString() + ".jpg";
                        x.FImagePath = photoName;
                        product.photo.CopyTo(new FileStream(_host.WebRootPath + "/images/shop/" + photoName, FileMode.Create));
                    }
                    x.ProductName = product.ProductName;
                    x.Price = (decimal)product.Price;
                    x.AvailableDate = product.AvailableDate;
                    x.ProductContent = product.ProductContent;
                    x.UnitStock = product.UnitStock;
                    x.StatusId = (int)product.StatusID;
                    x.MemberId = product.MemberID;

                    _db.SaveChanges();
                }
                if (product.GameTagOptions != null)//確保有選標籤
                {
                    _db.ProductTags.RemoveRange(_db.ProductTags.Where(x => x.ProductId == product.ProductId));


                    List<int> tagsList = product.GameTagOptions.Split(',').Select(int.Parse).ToList();
                    //Trace.WriteLine("AAAA" + tagsList);
                    foreach (var tags in tagsList)
                    {
                        int tagsID = _db.SubTags.FirstOrDefault(x => x.SubTagId == tags).SubTagId;
                        ProductTag productTag = new ProductTag
                        {
                            SubTagId = tagsID,
                            ProductId = x.ProductId
                        };

                        _db.ProductTags.Add(productTag);
                    }

                    _db.SaveChanges();

                }//Thread.Sleep(3000);
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
               
                var tagNames = _db.ProductTags
                 .Where(x => x.ProductId == id)
                 .Select(x => x.SubTag.Name)
                 .ToList();

                string s = string.Join("/", tagNames);
                //Trace.WriteLine("僅作查看"+s);
                List<CShopPageViewModel> aa = new List<CShopPageViewModel>();
                CShopPageViewModel ProductInfo = new CShopPageViewModel()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    FImagePath = x.FImagePath,
                    Price = x.Price,
                    ProductContent = x.ProductContent,
                    SubTagName=s
                    
                };
                return View(ProductInfo);
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

            public IActionResult payment()
            {
                _db.Payments.Load();
                var payment = _db.Payments.Select(x => x);
                return Json(payment);
            }
            [HttpPost]
            public IActionResult AddToCar(CShoppingCarViewModel vm)
            {

                Product product = _db.Products.FirstOrDefault(x => x.ProductId == vm.ProductID);
                if (product != null)
                {
                    string json = "";
                    List<CShoppingCarViewModel> car = null;
                    if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCES_LIST))
                    {
                        json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                        car = JsonSerializer.Deserialize<List<CShoppingCarViewModel>>(json);
                    }
                    else
                    {
                        car = new List<CShoppingCarViewModel>();
                    }
                    CShoppingCarViewModel x = new CShoppingCarViewModel();
                    x.Price = (decimal)product.Price;
                    x.ProductName = product.ProductName;
                    x.FImagePath = product.FImagePath;
                    x.Count = vm.txtCount;
                    x.ProductID = product.ProductId;


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
                List<CShoppingCarViewModel> car = JsonSerializer.Deserialize<List<CShoppingCarViewModel>>(json);
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
                List<CShoppingCarViewModel> car = JsonSerializer.Deserialize<List<CShoppingCarViewModel>>(json);
                CShoppingCarViewModel x = car.FirstOrDefault(p => p.ProductID == id);
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
                List<CShoppingCarViewModel> car = JsonSerializer.Deserialize<List<CShoppingCarViewModel>>(json);
                CShoppingCarViewModel x = car.FirstOrDefault(p => p.ProductID == vm.ProductID);
                if (x != null)
                {
                    car.Remove(x);
                }
                CShoppingCarViewModel n = new CShoppingCarViewModel()
                {
                    ProductID = vm.ProductID,
                    ProductName = product.ProductName,
                    FImagePath = product.FImagePath,
                    Price = (decimal)product.Price,
                    Count = vm.txtCount
                };
                car.Add(n);
                json = JsonSerializer.Serialize(car);
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCES_LIST, json);
                ViewBag.Car = car.Count();
                return RedirectToAction("CarView");
            }


            public IActionResult Purchase(int payment)
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
                List<CShoppingCarViewModel> car = JsonSerializer.Deserialize<List<CShoppingCarViewModel>>(json);
                Order order = new Order()
                {
                    MemberId = HttpContext.Session.GetInt32(CDictionary.SK_UserID),
                    ShipName = _db.Members.FirstOrDefault(x => x.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).Name,
                    OrderDate = DateTime.Now,
                    PaymentId = payment,
                    StatusId = 13,
                    ShipId = 1

                };
                _db.Orders.Add(order);
                _db.SaveChanges();
                foreach (var p in car)
                {
                    OrderProduct orderproduct = new OrderProduct()
                    {
                        OrderId = _db.Orders.Where(x => x.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).OrderByDescending(x => x.OrderId).First().OrderId,
                        ProductId = p.ProductID,
                        UnitPrice = p.Price,
                        Quantinty = p.Count,
                        Disconut = 0
                    };
                    _db.OrderProducts.Add(orderproduct);
                    _db.SaveChanges();
                }

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
