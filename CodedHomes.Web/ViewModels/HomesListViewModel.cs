using CodedHomes.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace CodedHomes.Web.ViewModels
{
    public class HomesListViewModel : ViewModelBase
    {
        public IList<Home> Homes { get; set; }

        public string HomesJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var homes = JsonConvert.SerializeObject(this.Homes, settings);
                return homes;
            }
        }

        public string ImageUrlPrefix
        {
            get { return CodedHomes.Web.Config.ImagesUrlPrefix; }
        }
    }
}