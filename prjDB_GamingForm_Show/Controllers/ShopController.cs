using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;
using System.Text.Json;

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
                String CK = ck.txtKeyword; /*Request.Form["txtKeyword"];*/
                IEnumerable<Product> Pdb = null;
                if (string.IsNullOrEmpty(CK))
                {
                    Pdb = from aa in _db.Products
                          select aa;
                }
                else
                {
                    Pdb = _db.Products.Where(p => p.ProductName.Contains(CK));
                }
                return View(Pdb);

            }

            public ActionResult Create()
            {
                _db.Products.Load();
                return View();
            }

            [HttpPost]
            public ActionResult Create(CProductWarp product) //原Product物件
            {
                Product x = new Product();

                x.ProductName = product.ProductName;
                x.Price = product.Price;
                x.AvailableDate = product.AvailableDate;
                x.ProductContent = product.ProductContent;
                x.UnitStock = product.UnitStock;
                x.StatusId = product.StatusID;
                x.MemberId = product.MemberID;

                //目前沒有FirmID跟圖片功能
                //MemberID跟StatusID是寫死在CProduct物件中的

                _db.Products.Add(x);
                _db.SaveChanges();

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
                    x.Count = vm.txtCount;
                    x.ProductID = product.ProductId;
                    x.product = product;
                    car.Add(x);
                    json = JsonSerializer.Serialize(car);
                    HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCES_LIST, json);
                }
                return RedirectToAction("Index");
            }
            //            public ActionResult AddToCar(CShoppingCarViewModel vm)
            //            {

            //                DbGamingFormTestContext db = new DbGamingFormTestContext();
            //                Product product = db.Products.FirstOrDefault(p => p.ProductId == vm.ProductID);
            //                if (product == null)
            //                {
            //                    return RedirectToAction("Index");
            //                }
            //                List<ShoppingCar> car = Session[CDictionary.SK_PURCHASED_PRODUCES_LIST] as List<ShoppingCar>;
            //                if (car == null)
            //                {
            //                    car = new List<ShoppingCar>();
            //                    Session[CDictionary.SK_PURCHASED_PRODUCES_LIST] = car;
            //                }
            //                ShoppingCar x = new ShoppingCar()
            //                {
            //                    ProductID = vm.ProductID,
            //                    ProductName = product.ProductName,
            //                    Price = (decimal)product.Price,
            //                    ImageID = (int)product.ImageId,
            //                    Count = vm.txtCount
            //                };
            //                car.Add(x);
            //                return RedirectToAction("Index");//ShoppingCar
            //            }
            //            public ActionResult ShoppingCar()
            //            {
            //                List<ShoppingCar> car = Session[CDictionary.SK_PURCHASED_PRODUCES_LIST] as List<ShoppingCar>;
            //                if (car == null)
            //                {
            //                    return RedirectToAction("Index");
            //                }
            //                return View(car);
            //            }
            //            public ActionResult DeleteFromCar(int id)////應該能正常移除購物車了/////
            //            {
            //                List<ShoppingCar> car = Session[CDictionary.SK_PURCHASED_PRODUCES_LIST] as List<ShoppingCar>;
            //                DbGamingFormTestContext db = new DbGamingFormTestContext();
            //                ShoppingCar x = car.FirstOrDefault(p => p.ProductID == id);
            //                if (x != null)
            //                {
            //                    car.Remove(x);
            //                }
            //                return RedirectToAction("ShoppingCar");
            //            }

            //            [HttpPost]
            //            public ActionResult EditCar(CShoppingCarViewModel vm)
            //            {
            //                DbGamingFormTestContext db = new DbGamingFormTestContext();
            //                Product product = db.Products.FirstOrDefault(p => p.ProductId == vm.ProductID);
            //                if (product == null)
            //                {
            //                    return RedirectToAction("List");
            //                }
            //                List<ShoppingCar> car = Session[CDictionary.SK_PURCHASED_PRODUCES_LIST] as List<ShoppingCar>;

            //                ShoppingCar x = car.FirstOrDefault(p => p.ProductID == vm.ProductID);
            //                if (x != null)
            //                {
            //                    car.Remove(x);
            //                }
            //                ShoppingCar n = new ShoppingCar()
            //                {
            //                    ProductID = vm.ProductID,
            //                    ProductName = product.ProductName,
            //                    Price = (decimal)product.Price,
            //                    ImageID = (int)product.ImageId,
            //                    Count = vm.txtCount
            //                };
            //                car.Add(n);
            //                return RedirectToAction("ShoppingCar");
            //            }
        }
    }
}
