using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CodedHomes.Web.Filters
{
    public class ValidationActionFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                var errors = new JObject();
                foreach (var Key in modelState.Keys)
                {
                    var state = modelState[Key];
                    if (state.Errors.Any())
                    {
                        errors[Key] = state.Errors.First().ErrorMessage;
                    }
                }
                actionContext.Response = actionContext.Request.CreateResponse<JObject>(HttpStatusCode.BadRequest, errors);
            }
           // base.OnActionExecuting(actionContext);
        }
    }
}