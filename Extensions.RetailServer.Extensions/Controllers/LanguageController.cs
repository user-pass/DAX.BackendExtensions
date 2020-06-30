namespace DAX.RetailServer.Extensions.Controllers
{
    using System.Runtime.InteropServices;
    using System.Web.Http;
    using System.Web.OData;
    using DAX.Runtime.Extensions.CRTExtensions.Messages;
    using DAX.Runtime.Extensions.CRTExtensions.DataModels;
    using SampleDataModel = Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Retail.RetailServerLibrary.ODataControllers;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Retail.RetailServerLibrary;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
    using System.Linq;
    using System.Collections.Generic;


    [ComVisible(false)]
    public class LanguageController : CommerceController<Language, long>
    {
        public override string ControllerName
        {
            get { return "LanguageController"; }
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public PagedResult<Language> GetAllLanguages()
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(114);

            var request = new GetAllLanguagesRequest() { QueryResultSettings = queryResultSettings };
            var languageResp = runtime.Execute<EntityDataServiceResponse<Language>>(request, null).PagedEntityCollection;
            return languageResp;
        }

    }
}
