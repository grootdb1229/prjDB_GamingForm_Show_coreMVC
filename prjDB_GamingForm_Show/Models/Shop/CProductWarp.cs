using prjDB_GamingForm_Show.Models.Entities;
using System.ComponentModel;
using static prjDB_GamingForm_Show.Models.Shop.CProductWarp;

namespace prjDB_GamingForm_Show.Models.Shop
{
    public class CProductWarp
    {
        public CProductWarp()
        {
            _product = new Product();
        }

        private Product _product;
        public Product product
        {
            get { return _product; }
            set { _product = value; }
        }

        [DisplayName("產品編號")]
        public int ProductID
        {
            get { return _product.ProductId; }
            set { _product.ProductId = value; }
        }

        [DisplayName("產品名稱")]
        public string ProductName
        {
            get { return _product.ProductName; }
            set { _product.ProductName = value; }
        }

        [DisplayName("價格")]
        public decimal Price
        {
            get { return _product.Price; }
            set { _product.Price = value; }
        }

        [DisplayName("上市日期")]//是否轉換成string呢    如上課所教   //同時先暫時移除DateTime?
        public DateTime AvailableDate { get { return DateTime.Now; } }  //.ToString("yyyyMMddHH");



        [DisplayName("商品描述")]
        public string ProductContent
        {
            get { return _product.ProductContent; }
            set { _product.ProductContent = value; }
        }

        [DisplayName("庫存")]
        public int UnitStock
        {
            get { return _product.UnitStock; }
            set { _product.UnitStock = value; }
        }


        [DisplayName("狀態ID")]  //預設商品為"上架中"狀態
        public int StatusID { get { return 1; } }

        [DisplayName("上架會員ID")] //在系統完成前先使用 id34號作業
        public int MemberID { get { return 34; } }

        [DisplayName("圖片ID")]//
        public int ImageID { get; set; }


        public int TagID { get; set; }
        public int SubTagID { get; set; }

    }
}
