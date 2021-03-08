using CoreCooking.Models.Sites;

namespace CoreCooking.Website.ViewModels.Home
{
    public class HashtagLineViewModel
    {
        public HashtagLineViewModel()
        {

        }

        public HashtagLineViewModel(Hashtag item)
        {
            this.Name = item.Name;
            this.Count = item.Count;
        }

        public HashtagLineViewModel(string hashtag)
        {
            this.Name = hashtag;
        }


        public string Name { get; set; }
        public int Count { get; set; }
    }
}
