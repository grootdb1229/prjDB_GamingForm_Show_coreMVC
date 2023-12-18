using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Entities;
using System.Text;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Runtime.Caching;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.ViewModels;

namespace prjDB_GamingForm_Show.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;

        public PaymentController(IWebHostEnvironment host, DbGamingFormTestContext db)
        {
            _host = host;
            _db = db;

        }
        public COrderViewModel orderview()
        {
            
            COrderViewModel vm = new COrderViewModel();
            var order = _db.Orders.Where(x => x.MemberId == 41)//HttpContext.Session.GetInt32(CDictionary.SK_UserID))
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
            return vm;
        }
        //step1 : 網頁導入傳值到前端
        public IActionResult Index()
        {
            COrderViewModel vm = orderview();
            var orderId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            //需填入你的網址
            var website = $"https://b7c7-1-160-32-217.ngrok-free.app/";
            var order = new Dictionary<string, string>
            {
                { "MerchantTradeNo",  orderId},
                { "MerchantTradeDate",  DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},
                { "TotalAmount",  vm.Sumprice.ToString()},
                { "TradeDesc",  "無"},
                { "ItemName",  "Groot"},
                { "ExpireDate",  "3"},
                { "CustomField1",  ""},
                { "CustomField2",  ""},
                { "CustomField3",  ""},
                { "CustomField4",  ""},
                { "ReturnURL",  $"{website}Ecpay/AddPayInfo"},
                { "OrderResultURL", $"{website}Payment/PayInfo/{orderId}"},
                { "PaymentInfoURL",  $"{website}Ecpay/AddAccountInfo"},
                { "ClientRedirectURL",  $"{website}Payment/AccountInfo/{orderId}"},
                { "MerchantID",  "3002607"},
                { "IgnorePayment",  "BARCODE"},
                { "PaymentType",  "aio"},
                { "ChoosePayment",  "ALL"},
                { "EncryptType",  "1"},
            };
            //檢查碼
            order["CheckMacValue"] = GetCheckMacValue(order);
            return View(order);
            //return RedirectToAction("https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5");
        }
        /// step5 : 取得付款資訊，更新資料庫 OrderResultURL
        [HttpPost]
        public ActionResult PayInfo(FormCollection id)
        {
            var data = new Dictionary<string, string>();
            foreach (string key in id.Keys)
            {
                data.Add(key, id[key]);
            }
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            var Orders = db.EcpayOrders.ToList().Where(m => m.MerchantTradeNo == id["MerchantTradeNo"]).FirstOrDefault();
            Orders.RtnCode = int.Parse(id["RtnCode"]);
            if (id["RtnMsg"] == "Succeeded") Orders.RtnMsg = "已付款";
            Orders.PaymentDate = Convert.ToDateTime(id["PaymentDate"]);
            Orders.SimulatePaid = int.Parse(id["SimulatePaid"]);
            db.SaveChanges();
            return View("EcpayView", data);
            //return RedirectToAction("Shop","OrderDetail");
        }
        /// step5 : 取得虛擬帳號 資訊  ClientRedirectURL
        [HttpPost]
        public ActionResult AccountInfo(FormCollection id)
        {
            var data = new Dictionary<string, string>();
            foreach (string key in id.Keys)
            {
                data.Add(key, id[key]);
            }
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            var Orders = db.EcpayOrders.ToList().Where(m => m.MerchantTradeNo == id["MerchantTradeNo"]).FirstOrDefault();
            Orders.RtnCode = int.Parse(id["RtnCode"]);
            if (id["RtnMsg"] == "Succeeded") Orders.RtnMsg = "已付款";
            Orders.PaymentDate = Convert.ToDateTime(id["PaymentDate"]);
            Orders.SimulatePaid = int.Parse(id["SimulatePaid"]);
            db.SaveChanges();
            return View("EcpayView", data);
        }
        private string GetCheckMacValue(Dictionary<string, string> order)
        {
            var param = order.Keys.OrderBy(x => x).Select(key => key + "=" + order[key]).ToList();

            var checkValue = string.Join("&", param);

            //測試用的 HashKey
            var hashKey = "pwFHCqoQZGmho4w6";

            //測試用的 HashIV
            var HashIV = "EkRm7iFT261dpevs";

            checkValue = $"HashKey={hashKey}" + "&" + checkValue + $"&HashIV={HashIV}";

            checkValue = HttpUtility.UrlEncode(checkValue).ToLower();

            checkValue = GetSHA256(checkValue);

            return checkValue.ToUpper();
        }
        private string GetSHA256(string value)
        {
            var result = new StringBuilder();
            var sha256 = SHA256Managed.Create();
            var bts = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bts);

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
