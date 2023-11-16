using prjDB_GamingForm_Show.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjDB_GamingForm_Show.Models.Shop
{
    public class ShoppingCar
    {
        [DisplayName("產品編號")]
        public int ProductID { get; set; }

        [DisplayName("產品名稱")]
        public string ProductName { get; set; }

        [DisplayName("價格")]
        public decimal Price { get; set; }
        [DisplayName("圖片ID")]
        public int ImageID { get; set; }

        [DisplayName("購買數量")]
        public int Count { get; set; }
        [DisplayName("合計金額")]
        public decimal total { get { return this.Count * this.Price; } }
        public Product product { get; set; }

        public int CarCount { get; set; }
    }
}