using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;

namespace prjDB_GamingForm_Show.Controllers
{
    namespace prjDB_GamingForm_Show.Controllers
    {
    public class ShopController : Controller
    {
        public IActionResult Index()

            public ActionResult Index(CKeyWord ck)
        {

                String CK = ck.txtKeyword; /*Request.Form["txtKeyword"];*/
                DbGamingFormTestContext db = new DbGamingFormTestContext();
                IEnumerable<Product> Pdb = null;
                if (string.IsNullOrEmpty(CK))
                {
                    Pdb = from aa in db.Products
                          select aa;
                }
                else
                {
                    Pdb = db.Products.Where(p => p.ProductName.Contains(CK));
                }
                return View(Pdb);

            }

            public ActionResult Create()
            {
            return View();
        }

            [HttpPost]
            public ActionResult Create(CProductWarp product) //原Product物件
            {
                DbGamingFormTestContext db = new DbGamingFormTestContext();
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

                db.Products.Add(x);
                db.SaveChanges();




                return RedirectToAction("Index");
            }


            public ActionResult Edit(int? id)
            {

                DbGamingFormTestContext db = new DbGamingFormTestContext();
                Product pdb = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (pdb == null)
                    return RedirectToAction("Index");

                return View(pdb);
            }




            [HttpPost]
            public ActionResult Edit(CProductWarp product) //原Product物件   
            {
                DbGamingFormTestContext db = new DbGamingFormTestContext();
                Product x = db.Products.FirstOrDefault(p => p.ProductId == product.ProductID);
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
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            public ActionResult Delete(int id)    ////不確定是否要用刪除的方式來表達商品下架，刪除資料也會讓關聯表紀錄消失。
            {

                DbGamingFormTestContext db = new DbGamingFormTestContext();
                Product x = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (x != null)
                {
                    db.Products.Remove(x);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            public ActionResult Details(int id)
            {

                DbGamingFormTestContext db = new DbGamingFormTestContext();
                Product x = db.Products.FirstOrDefault(p => p.ProductId == id);
                return View(x);
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
