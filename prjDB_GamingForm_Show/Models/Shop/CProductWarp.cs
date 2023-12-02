using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using prjDB_GamingForm_Show.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static prjDB_GamingForm_Show.Models.Shop.CProductWarp;

namespace prjDB_GamingForm_Show.Models.Shop
{
    public class CProductWarp
    {
        public CProductWarp()
        {
            _product = new Product();
        }
        public virtual IEnumerable<Product> Product { get; set; } = new List<Product>();
        private Product _product;
        public Product product
        {
            get { return _product; }
            set { _product = value; }
        }



        [DisplayName("產品編號")]
        public int ProductId
        {
            get;
            set;
        }

        [Required( ErrorMessage= "您尚未輸入資訊")]
        [DisplayName("產品名稱")]
		public string ProductName
        {
            get;
            set;
        }


     
        [Range(1, (double)decimal.MaxValue, ErrorMessage = "商品金額必須是合理的數字")]
        [DisplayName("價格")]
       public decimal Price
        {
            get;
            set;
        }
 
        [DisplayName("上市日期")]//是否轉換成string呢    如上課所教   //同時先暫時移除DateTime?
        public DateTime AvailableDate { get { return DateTime.Now; } }  //.ToString("yyyyMMddHH");



        [Required(ErrorMessage = "您尚未輸入資訊")]
        [StringLength(7999,ErrorMessage ="請輸入至少10個字元，讓客人更好了解您的商品",MinimumLength =10)]
        [DisplayName("商品描述")]
        public string ProductContent
        {
            get;
            set;
        }



        [Range(1, int.MaxValue, ErrorMessage = "商品庫存必須是合理的數字")]
        [DisplayName("庫存")]
        public int UnitStock
        {
            get;
            set;
        }

        
        [DisplayName("狀態ID")]  //預設商品為"上架中"狀態
        public int? StatusID {
            get;
            set;
        }
  
        [DisplayName("上架會員ID")] //在系統完成前先使用 id34號作業
        public int? MemberID {
            get;
            set;
        }
    
     
        [DisplayName("圖片ID")]//
        public int ImageID { get; set; }

   
        public string? FImagePath {
            get;
            set;
        }
        //public string? LanguageOptions { get; set; 

        public string? GameTagOptions { get; set; }
        [BindNever]
        public virtual Status? Status
        
            //get { return _product.Status; }
            //set { _product.Status = null!; }
          { get; set; } = null;

        public virtual IEnumerable<SubTag> SubTag { get; set; } = new List<SubTag>();

        public virtual IEnumerable<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
        public IFormFile? photo { get; set; }


    }


}
