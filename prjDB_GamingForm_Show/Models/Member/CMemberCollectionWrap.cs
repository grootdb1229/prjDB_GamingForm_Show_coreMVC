namespace prjDB_GamingForm_Show.Models.Member
{
    public class CMemberCollectionWrap
    {
        private Entities.MemberCollection _memberCollection;

        public CMemberCollectionWrap()
        {
            _memberCollection = new Entities.MemberCollection();
        }

        public Entities.MemberCollection MemberCollection { get { return _memberCollection;} set{ this.MemberCollection = value; } }
        public int ID { get { return _memberCollection.Id; } set { this.MemberCollection.Id = value; } }
        public int memberID { get; set; }
        public int? CollectionID { get { return _memberCollection.CollectionId; } set { this.MemberCollection.CollectionId = value; } }
        public string Title { get { return _memberCollection.Title; } set { this.MemberCollection.Title = value; } }
        public string Intro { get { return _memberCollection.Intro; } set { this.MemberCollection.Intro = value; } }
        public string fImagePath   { get { return _memberCollection.FImagePath; }    set { this.MemberCollection.FImagePath = value; } }
        public string MyCollection { get { return _memberCollection.MyCollection; } set { this.MemberCollection.MyCollection = value; } }
        public string ModifeidDate { get { return _memberCollection.ModifiedDate; } set { this.MemberCollection.ModifiedDate = value; } }
        public IFormFile photo { get; set; }
    }
}
