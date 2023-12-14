using DB_GamingForm_Show.Job.DeputeClass;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Transactions;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace prjDB_GamingForm_Show.Controllers
{
	namespace prjDB_GamingForm_Show.Controllers
	{
		public class ShopController : Controller
		{

			private readonly IWebHostEnvironment _host;
			private readonly DbGamingFormTestContext _db;
			public int couponid { get; set; }
            public ShopController(IWebHostEnvironment host, DbGamingFormTestContext db)
			{
				_host = host;
				_db = db;
            
            }

            public List<CShopPageViewModel> Listx { get; set; }
            public List<CShopPageViewModel> Temp { get; set; }
			public void listLoad()   // 這是讓DB只要執行一次查詢結果的方法。 
			{
			 Listx = new List<CShopPageViewModel>();
                Temp =new List<CShopPageViewModel>();		
                _db.ProductTags.Load();
                _db.Products.Load();
                _db.SubTags.Load();

                var data = _db.ProductTags
                    .Where(n =>n.Product.StatusId == 1)
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

                List<CShopPageViewModel> List2 = data.Select(item =>
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
                 Listx = List2.GroupBy(p => p.ProductId).Select(group => group.First()).ToList();
                Temp = Listx;

            }
			
			public IActionResult MutipleSearch_Shop(string txtMutiKeywords)
			{
                
                string result = "";
                listLoad();
				Temp = Listx;
				IEnumerable<CShopPageViewModel> datas = null;
				if (txtMutiKeywords != null)//你的一堆標籤字串
				{
                    List<string> keywords = txtMutiKeywords.Split(',').ToList();
                    foreach (var item in keywords)
					{
						if (string.IsNullOrEmpty(item))//你沒選東西
						{
							string noneResult = JsonSerializer.Serialize(Temp);
							return Content(noneResult, "application/json");
						}					
						datas = Temp.Where(n => (n.SubTagName.Trim().ToLower().Contains(item.Trim().ToLower()))).OrderByDescending(x => x.ProductId).ToList();
                        Temp = datas.ToList();
                        if (Temp.Count == 0)//沒有結果時
						{
							return Json(new { message = "沒有符合的條件" });
						}
					}
					 result = JsonSerializer.Serialize(Temp);
					return Content(result, "application/json");
				}
                 result = JsonSerializer.Serialize(Temp);
                return Content(result, "application/json");
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
			public IActionResult Carousel()//大廣告牆
			{
				return PartialView();
			}

			public IActionResult SelSubtag()
			{
				return PartialView();
			}
			public IActionResult HotProduct()
			{
				return PartialView();
			}
			public IActionResult SeenProduct()
			{
				return PartialView();
			}
			public IActionResult HotTopFive() //取熱門商品
			{
				var TopFive = _db.Products.Select(x => new { x.FImagePath, x.ViewCount, x.ProductName, x.ProductId,x.Price }).OrderByDescending(x => x.ViewCount).Take(5).ToList();
				return Json(TopFive);
			}
			public IActionResult YourFavorite()
			{
				return PartialView();
			}
			public IActionResult FavoriteTop5()
			{

				int userId = 0;//檢測有沒有登入
				if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null) //利用Session確認登入狀況
				{ userId = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID); }//有登入複寫Userid，反之維持0。
				else { return Json(new { message = "請先登入" }); }
				var top5 = _db.WishLists.Where(x => x.MemberId == userId).Select(x => x.Product).Take(5).ToList();
				//Trace.WriteLine("檢查TOP5"+top5);
				return Json(top5);
			}
			public void Cookie(int? id)  //Cookies設定
			{
				int userId = 0;//檢測有沒有登入
				if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null) //利用Session確認登入狀況
					userId = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID); //有登入複寫Userid，反之維持0。

				string record = "";
				if (HttpContext.Request.Cookies[userId.ToString()] != null) //此用戶沒有Cookies讓他的紀錄=""，有的畫幫他調閱。
					record = HttpContext.Request.Cookies[userId.ToString()];

				CookieOptions options = new CookieOptions();
				options.Expires = DateTime.Now.AddDays(30);//餅乾效期設定
				record += $"{id},";//紀錄id 用，逗號分隔，待會以逗號切割Split。
				HttpContext.Response.Cookies.Append(userId.ToString(), record, options);//設定Cookies 同時裡面只能接受字串所以用ToString
			}

			public IActionResult GetCookie() ///Cookie存取客人看過的商品
			{

				List<Product> CookieList = new List<Product>();
				int userId = 0;
				if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
					userId = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
				string record = "";
				if (HttpContext.Request.Cookies[userId.ToString()] != null)
					record = HttpContext.Request.Cookies[userId.ToString()];
				if (userId == 0)
				{
					return Json(new { message = "請先登入" });
				}
				string[] strResult = record.Split(',');
				strResult = strResult.Reverse().Distinct().ToArray();
				IEnumerable<Product> datas = null;
				foreach (var item in strResult)
				{
					if (!string.IsNullOrEmpty(item))
					{
						datas = from n in _db.Products
								where n.ProductId == Convert.ToInt32(item)
								select n;
						foreach (var data in datas)
						{
							CookieList.Add(data);
						}
					}

				}
				return Json(CookieList.Take(5));
			}
            public IActionResult LoveList()
            {
			var aa=	_db.WishLists.Where(x => x.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).Select(a => a.Product).ToList();
				List < CLoveListViewModel> LL= new List<CLoveListViewModel>();
                
				foreach (var x in aa)
				{
					CLoveListViewModel CLVM =new CLoveListViewModel();
					CLVM.ProductId=x.ProductId;
					CLVM.FImagePath = x.FImagePath;
					CLVM.ProductName = x.ProductName;
					CLVM.Price= x.Price;
					LL.Add(CLVM);
				}

				//ViewBag.LList = LL.Count();
				//TP.LListCount=LL.Count();
				return View(LL);
            }
            public IActionResult RemoveProduct(int? id) //Ajax刷新最愛
            {
                int memberID = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
                WishList removeProduct = _db.WishLists.FirstOrDefault(x => x.ProductId == id && x.MemberId == memberID);
                _db.WishLists.Remove(removeProduct);
                _db.SaveChanges();

                var aa = _db.WishLists.Where(x => x.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).Select(a => a.Product).ToList();
                List<CLoveListViewModel> LL = new List<CLoveListViewModel>();

                foreach (var x in aa)
                {  
                    CLoveListViewModel CLVM = new CLoveListViewModel();
					CLVM.ProductId=x.ProductId;
                    CLVM.FImagePath = x.FImagePath;
                    CLVM.ProductName = x.ProductName;
                    CLVM.Price = x.Price;
                    LL.Add(CLVM);
                }
                return Json(LL);
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
				if (List.Count == 0)//////"查無資料"邏輯 
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
					//Trace.WriteLine("檢查裝什麼1" + jsonResult);
					//Trace.WriteLine("檢查裝什麼2" + List2);
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
				var SelSub = _db.SubTags
					.Where(p => p.TagId == 1)
					.Select(s => new
					{
						s.SubTagId,
						s.Name,
                        ProductCount = s.ProductTags.Count()
					})
					.ToList();
				return Json(SelSub);
			}

            public IActionResult Coupon()
            {
                var Coupon = _db.Coupons
                    .Where(p => p.StatusId == 23)
                    .Select(s => new
                    {
						s.CouponId,
                        s.Title,
                        s.Discount,
						s.Reduce
                    }).ToList();
                return Json(Coupon);
            }
			
			public IActionResult Couponselect(int id)
			{
				var Coupon = _db.Coupons
					.Where(p => p.CouponId == id)
					.Select(s => new
					{
						s.CouponId,
						s.Discount,
						s.Reduce
					}).ToList();
                
                string json = "";
				json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
				List<CShoppingCarViewModel> car = JsonSerializer.Deserialize<List<CShoppingCarViewModel>>(json);

                string jsoncoupon = "";
                jsoncoupon = JsonSerializer.Serialize(Coupon);
                HttpContext.Session.SetString(CDictionary.SK_COUPON, jsoncoupon);
                double sumprice = 0;

				foreach (var item in Coupon)
				{
					if (item.Discount != 0&&item.Discount!=null)
					{
						double dis = (double)item.Discount;
						sumprice = (double)car.Sum(c => c.Price) * dis;
					}
					else
					{
						int reduce = (int)item.Reduce;
						sumprice = (int)car.Sum(c => c.Price) - reduce;
					}
				}
				

                return Content(sumprice.ToString("#0"));
			}
            [HttpPost]
			public ActionResult Create(超酷warp product)
			//先這樣Warp應該是用於資料驗證，有待看影片確認但目前不這樣寫驗證會一直錯誤
			{//但我其實也想把驗證改寫到前端，這些資料本質並不是重要到要寫後端驗證
			 //Trace.WriteLine("AAAA" + product.GameTagOptions);

				using (var transaction = _db.Database.BeginTransaction())
				{
					try
					{
						if (!ModelState.IsValid)
						{
							var errors = ModelState.Values.SelectMany(v => v.Errors); //測試錯誤用程式碼
							return View(product);
						}
						Product x = new Product();
						string MulPic = "";
						if (_db != null)
						{
							if (product.Photos != null && product.Photos.Count > 0)
							{
								foreach (var photo in product.Photos)
								{
									if (photo != null)
									{
										string photoName = Guid.NewGuid().ToString() + ".jpg";

										var photoPath = Path.Combine(_host.WebRootPath, "images", "shop", photoName);

										using (var stream = new FileStream(photoPath, FileMode.Create))
										{
											photo.CopyTo(stream);
										}
										MulPic += photoName + "/";
									}
								}
								string[] pics = MulPic.Split('/');  ///一長串圖片被我拆分儲存
								if (pics.Length > 0)
								{
									string Mainpic = pics[0];//第一張是主圖存入商品資料行
									x.FImagePath = Mainpic;
								}
								x.ProductName = product.ProductName;
								x.Price = product.Price;
								x.AvailableDate = product.AvailableDate;
								x.ProductContent = product.ProductContent;
								x.UnitStock = product.UnitStock;
								x.StatusId = 7;//記得改回7
								x.MemberId = product.MemberID;

								_db.Products.Add(x);
								_db.SaveChanges();
							}
							if (product.Photos.Count >= 1) ///如果有超過一張圖就執行多餘圖片存取
							{
								//List<int> imageIds = new List<int>(); //裝圖片ID們
								//剩下的圖存入參考表
								string[] OtherPic = MulPic.Split("/").Skip(1).ToArray();//沒圖會給空字串

								foreach (string picpath in OtherPic)
								{
									if (!string.IsNullOrEmpty(picpath))
									{
										Image ElsePic = new Image();//裡還外?
										ElsePic.FImagePath = picpath;
										_db.Images.Add(ElsePic);
										_db.SaveChanges();
										ProductImage productImage = new ProductImage
										{
											ProductId = x.ProductId,
											ImageId = ElsePic.ImageId
										};
										_db.ProductImages.Add(productImage);
										_db.SaveChanges();
									}
								}

							}
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

						}


						transaction.Commit();
						return RedirectToAction("Index");
					}


					catch (Exception ex)
					{
						transaction.Rollback();
						Trace.WriteLine("圖片上傳的錯誤" + ex.ToString());
						throw;
					}
				}
			}

			public ActionResult WhenYouEditTags(int? id)//修改商品時讀取標籤
			{
				_db.ProductTags.Load();

				var SelSub = _db.ProductTags.Where(p => p.ProductId == id).Select(s => s.SubTagId).ToList();

				return Json(SelSub);
			}
			//public ActionResult WhenYouEditPics(int? id)//修改商品時讀取圖片
			//{
			//	_db.Images.Load();
			//	_db.ProductImages.Load();

			//	var MulPic = _db.ProductImages.Where(x => x.ProductId == id).Select(x => new { FImagePath = x.Image.FImagePath }).ToList();
			//	return Json(MulPic);
			//}

			public ActionResult Edit(int? id)
			{
				string PicString="";
				Product pdb = _db.Products.FirstOrDefault(p => p.ProductId == id);
				var mulpic=_db.ProductImages.Where(x => x.ProductId == id).Select(s => s).ToList();
				foreach (var imgid in mulpic)
				{
					var MulpicTostring = _db.Images.FirstOrDefault(x => x.ImageId == imgid.ImageId);
					if (MulpicTostring != null) 
					{
						PicString += MulpicTostring.FImagePath.ToString() + "/";
					}
				}
				if (pdb == null)
				{ return RedirectToAction("Index"); }
				超酷warp cProductWarp = new 超酷warp();
				cProductWarp.MulPic = PicString;
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
				{
					//if (!ModelState.IsValid)
					//{
					//    var errors = ModelState.Values.SelectMany(v => v.Errors);
					//    return View((超酷warp)product);
					//}

					//Product x = _db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
					//if (x != null)
					//{
					//    if (product.photo != null)
					//    {
					//        string photoName = Guid.NewGuid().ToString() + ".jpg";
					//        x.FImagePath = photoName;
					//        product.photo.CopyTo(new FileStream(_host.WebRootPath + "/images/shop/" + photoName, FileMode.Create));
					//    }
					//    x.ProductName = product.ProductName;
					//    x.Price = (decimal)product.Price;
					//    x.AvailableDate = product.AvailableDate;
					//    x.ProductContent = product.ProductContent;
					//    x.UnitStock = product.UnitStock;
					//    x.StatusId = (int)product.StatusID;
					//    x.MemberId = product.MemberID;

					//    _db.SaveChanges();
					//}
					//if (product.GameTagOptions != null)//確保有選標籤
					//{
					//    _db.ProductTags.RemoveRange(_db.ProductTags.Where(x => x.ProductId == product.ProductId));


					//    List<int> tagsList = product.GameTagOptions.Split(',').Select(int.Parse).ToList();
					//    //Trace.WriteLine("AAAA" + tagsList);
					//    foreach (var tags in tagsList)
					//    {
					//        int tagsID = _db.SubTags.FirstOrDefault(x => x.SubTagId == tags).SubTagId;
					//        ProductTag productTag = new ProductTag
					//        {
					//            SubTagId = tagsID,
					//            ProductId = x.ProductId
					//        };

					//        _db.ProductTags.Add(productTag);
					//    }

					//    _db.SaveChanges();

					//}//Thread.Sleep(3000);
					//return RedirectToAction("Index");
				}//以防萬一 這是單圖存檔用
				using (var transaction = _db.Database.BeginTransaction())
				{
					try
					{
						if (!ModelState.IsValid)
						{
							var errors = ModelState.Values.SelectMany(v => v.Errors); //測試錯誤用程式碼
							return View((超酷warp)product);
						}


						Product x = _db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
						string MulPic = "";
						if (_db != null)
						{
							if (product.Photos != null && product.Photos.Count > 0)
							{
								foreach (var photo in product.Photos)
								{
									if (photo != null)
									{
										string photoName = Guid.NewGuid().ToString() + ".jpg";

										var photoPath = Path.Combine(_host.WebRootPath, "images", "shop", photoName);

										using (var stream = new FileStream(photoPath, FileMode.Create))
										{
											photo.CopyTo(stream);
										}
										MulPic += photoName + "/";
									}
								}
								string[] pics = MulPic.Split('/');  ///一長串圖片被我拆分儲存
								if (pics.Length > 0)
								{
									string Mainpic = pics[0];//第一張是主圖存入商品資料行
									x.FImagePath = Mainpic;
								}
								x.ProductName = product.ProductName;
								x.Price = product.Price;
								x.AvailableDate = product.AvailableDate;
								x.ProductContent = product.ProductContent;
								x.UnitStock = product.UnitStock;
								x.StatusId = 1;//記得改回7
								x.MemberId = product.MemberID;

								_db.SaveChanges();
							}
							if (product.Photos.Count >= 1) ///如果有超過一張圖就執行多餘圖片存取
							{
								_db.ProductImages.RemoveRange(_db.ProductImages.Where(p => p.ProductId == x.ProductId));
								var removedImageIds = _db.ProductImages
									.Where(p => p.ProductId == x.ProductId)
									.Select(p => p.ImageId)
									.ToList(); 						
								foreach (var imageId in removedImageIds)
								{
									var imageToRemove = _db.Images.FirstOrDefault(i => i.ImageId == imageId);
									if (imageToRemove != null)
									{
										_db.Images.Remove(imageToRemove);
									}
								}

								// 提交變更
								_db.SaveChanges();
								string[] OtherPic = MulPic.Split("/").Skip(1).ToArray();

								foreach (string picpath in OtherPic)
								{
									if (!string.IsNullOrEmpty(picpath))
									{
										Image ElsePic = new Image();//裡還外?
										ElsePic.FImagePath = picpath;
										_db.Images.Add(ElsePic);
										_db.SaveChanges();
										ProductImage productImage = new ProductImage
										{
											ProductId = x.ProductId,
											ImageId = ElsePic.ImageId
										};
										_db.ProductImages.Add(productImage);
										_db.SaveChanges();
									}
								}

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
								transaction.Commit();
							}
						}
						return RedirectToAction("Index");
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						Trace.WriteLine("圖片上傳的錯誤" + ex.ToString());
						throw;
					}
				}
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
				string Likestatus = null;
				int memberID = 0;//測登入沒
				if ((HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null))
				{
					memberID = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
					var HaveProduct = _db.WishLists.Any(x => x.ProductId == id && x.MemberId == memberID);//檢查你有沒有置入願望清單
					if (!HaveProduct) { Likestatus = "你還沒收藏過"; }
					else { Likestatus = "你收藏過了"; }
				}

				//存近期看過
				Cookie(id);


				_db.Products.Load();
				Product x = _db.Products.FirstOrDefault(p => p.ProductId == id);

				var tagNames = _db.ProductTags
				 .Where(x => x.ProductId == id)
				 .Select(x => x.SubTag.Name)
				 .ToList();
				string s = string.Join("/", tagNames);
				
                string MulPicString = "";
                var MulPicsID = _db.ProductImages.Where(x => x.ProductId == id).Select(a=>a.Image.FImagePath).ToList();
                if (MulPicsID != null)
                {    
                    MulPicString = string.Join("/", MulPicsID); 				
                }
               
                Trace.WriteLine("僅作查看"+MulPicString);
				List<CShopPageViewModel> aa = new List<CShopPageViewModel>();
				CShopPageViewModel ProductInfo = new CShopPageViewModel()
				{
					ProductId = x.ProductId,
					ProductName = x.ProductName,
					FImagePath = x.FImagePath,
					Price = x.Price,
					ProductContent = x.ProductContent,
					SubTagName = s,
					favourite = Likestatus,
					MulPic= MulPicString
				};
				x.ViewCount++;
				_db.SaveChanges();
				//Trace.WriteLine(ProductInfo.favourite);
				return View(ProductInfo);
			}



			public ActionResult favourite(int? id, bool isLiked)
			{
				int productID = (int)id;
				int memberID = 0;
				if ((HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null))  //看看登入沒
				{
					memberID = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID); //取會員ID
				}

				var HaveProduct = _db.WishLists.Any(x => x.ProductId == productID && x.MemberId == memberID); //檢查有沒有過了

				if (!HaveProduct)//沒加過幫他加進去
				{
					WishList wishList = new WishList();
					{
						wishList.MemberId = memberID;
						wishList.ProductId = productID;
					};
					_db.WishLists.Add(wishList);
					_db.SaveChanges();
					return Json(new { message = "加入最愛" });
				}
				else
				{
					WishList removeProduct = _db.WishLists.FirstOrDefault(x => x.ProductId == id && x.MemberId == memberID);
					_db.WishLists.Remove(removeProduct);
					_db.SaveChanges();
					return Json(new { message = "你加過了" });
				}


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

            public IActionResult AddToCar2(int? id)
            {

                Product product = _db.Products.FirstOrDefault(x => x.ProductId == id);
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
                    x.Count = 1;
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


			public void Purchase(int payment)
			{
				string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCES_LIST);
				List<CShoppingCarViewModel> car = JsonSerializer.Deserialize<List<CShoppingCarViewModel>>(json);
				Order order = null;
				List<Coupon> coupon = null;

                if (HttpContext.Session.GetString(CDictionary.SK_COUPON) != null)
				{ 
					 string jsoncoupon = HttpContext.Session.GetString(CDictionary.SK_COUPON);
                     coupon = JsonSerializer.Deserialize<List<Coupon>>(jsoncoupon);
				}
               
                if (coupon != null)
				{
					foreach (var i in coupon)
					{
						order = new Order()
						{
							MemberId = HttpContext.Session.GetInt32(CDictionary.SK_UserID),
							OrderDate = DateTime.Now,
							PaymentId = payment,
							StatusId = 13,
							CouponId = i.CouponId
						};
					}
				}
				else
				{
                    order = new Order()
                    {
                        MemberId = HttpContext.Session.GetInt32(CDictionary.SK_UserID),
                        OrderDate = DateTime.Now,
                        PaymentId = payment,
                        StatusId = 13,
                    };
                }
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
                
			}

			public IActionResult OrderDetail()
			{ 
				List<COrderViewModel> vm = new List<COrderViewModel>();
				var order =  _db.Orders.Where(x=>x.MemberId== HttpContext.Session.GetInt32(CDictionary.SK_UserID))
							.OrderByDescending(x => x.OrderId)
							.Select(x => new { x.OrderId,x.Payment.Name, x.Coupon.Title,x.OrderDate});
				COrderViewModel n = null;

				foreach (var i in order)
				{
					n = new COrderViewModel()
					{
						OrderId = i.OrderId,
						CouponTitle = i.Title,
						OrderDate = i.OrderDate,
                        PaymentName = i.Name,
                        products = new List<CProductNamePrice>()
					};

					var orderproduct = _db.OrderProducts.Where(x => x.OrderId == i.OrderId).Select(x => x.ProductId);
                    foreach (var pp in orderproduct)
					{
                        var op = _db.Products.Where(x => x.ProductId == pp).Select(x =>new { x.ProductName,x.Price });
						CProductNamePrice cpnp = null;
						foreach (var ppp in op)
						{
							cpnp = new CProductNamePrice()
							{
								ProductName = ppp.ProductName,
								Price = ppp.Price
							};
							n.products.Add(cpnp);
						}
					}
					if (n.Coupon != null)
					{
                        if (n.Coupon.Discount != 0)
                        {
                            double dis = (double)n.Coupon.Discount;
                            n.Sumprice = (double)n.products.Sum(c => c.Price) * dis;
                        }
                        else
                        {
                            int reduce = (int)n.Coupon.Reduce;
                            n.Sumprice = (int)n.products.Sum(c => c.Price) - reduce;
                        }
					}
					else
					{
						n.Sumprice = (double)n.products.Sum(x => x.Price);

                    }
                    vm.Add(n);
				}
				//SendOrderEmail(vm.First());
				return View(vm);
			}

			public void orderview()
			{
                COrderViewModel vm = new COrderViewModel();
                var order = _db.Orders.Where(x => x.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID))
                            .OrderByDescending(x => x.OrderId).Take(1)
                            .Select(x => new { x.OrderId, x.Payment.Name, x.Coupon.Title, x.OrderDate });
                COrderViewModel n = null;

                foreach (var i in order)
                {
                    n = new COrderViewModel()
                    {
                        OrderId = i.OrderId,
                        CouponTitle = i.Title,
                        OrderDate = i.OrderDate,
                        PaymentName = i.Name,
                        products = new List<CProductNamePrice>()
                    };

                    var orderproduct = _db.OrderProducts.Where(x => x.OrderId == i.OrderId).Select(x => x.ProductId);
                    foreach (var pp in orderproduct)
                    {
                        var op = _db.Products.Where(x => x.ProductId == pp).Select(x => new { x.ProductName, x.Price });
                        CProductNamePrice cpnp = null;
                        foreach (var ppp in op)
                        {
                            cpnp = new CProductNamePrice()
                            {
                                ProductName = ppp.ProductName,
                                Price = ppp.Price
                            };
                            n.products.Add(cpnp);
                        }
                    }
                    if (n.Coupon != null)
                    {
                        if (n.Coupon.Discount != 0)
                        {
                            double dis = (double)n.Coupon.Discount;
                            n.Sumprice = (double)n.products.Sum(c => c.Price) * dis;
                        }
                        else
                        {
                            int reduce = (int)n.Coupon.Reduce;
                            n.Sumprice = (int)n.products.Sum(c => c.Price) - reduce;
                        }
                    }
                    else
                    {
                        n.Sumprice = (double)n.products.Sum(x => x.Price);

                    }
                    vm = n;
                }
                SendOrderEmail(vm);
            }

            public IActionResult SendOrderEmail(COrderViewModel vm)
			{
				string s = "<div class='row'>";
				foreach (var item in vm.products)
				{
					s += "<div class='col' style='color:black'>商品名稱:" + item.ProductName + "</div>"
						+"<div class='col' style='color:black'>商品價格:" + item.Price.ToString("#0") + "元</div>";
				}
				s += "</div>";
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
				message.To.Add(new MailboxAddress(_db.Members.FirstOrDefault(x => x.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).Name, "kakuc0e0ig@gmail.com"));
				message.Subject = "Your Order from GrootShopping";
				message.Body = new TextPart("html")
				{
					Text = "<!DOCTYPE html>"
					+
							"<html>" +
							"<h2>  您的訂單資訊 </h2>" +
							"<p>   您的訂單編號為:" + vm.OrderId + "</p>" +
							"<p>   價格為:" + vm.Sumprice.ToString("#0") + "元</p>" +
							"<p>   下單日期為:" + vm.OrderDate + "</p>" +
							s +
							"</html>"
				};
				//用迴圈抓商品名稱 可能要抓折扣後的價格(折扣) 可能要抓(訂單編號) 下單時間

				using (var client = new SmtpClient())
				{
					client.Connect("smtp.gmail.com", 587, false);
					client.Authenticate("grootdb1229@gmail.com", "fmgx uucs lgkv vqxm");
					client.Send(message);
					client.Disconnect(true);
				}
				return RedirectToAction("OrderDetail", "Shop");
			}
		}
    }
}
