namespace DAX.RetailServer.Extensions.Controllers
{
    using System.Runtime.InteropServices;
    using System.Web.Http;
    using Runtime.Extensions.CRTExtensions.Messages;
    using Runtime.Extensions.CRTExtensions.DataModels;
    using Microsoft.Dynamics.Retail.RetailServerLibrary.ODataControllers;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Retail.RetailServerLibrary;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;



    [ComVisible(false)]
    public class TenderTypeController : CommerceController<TenderTypeModel, long>
    {
        public override string ControllerName
        {
            get { return "TenderTypeController"; }
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public PagedResult<TenderTypeModel> GetTenderTypes()
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(11);

            var request = new GetTenderTypesRequest() { QueryResultSettings = queryResultSettings };
            var tenderTypesResponse = runtime.Execute<EntityDataServiceResponse<TenderTypeModel>>(request, null).PagedEntityCollection;
            return tenderTypesResponse;
        }
    }
}
