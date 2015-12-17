using CodedHomes.Models;

namespace CodedHomes.Web.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public Home Home { get; set; }
        public bool IsNew { get; set; }

        public string ImageUrlPrefix
        {
            get { return CodedHomes.Web.Config.ImagesUrlPrefix; }
        }

        public HomeViewModel()
        {
            this.Home = new Home();
        }
    }
}