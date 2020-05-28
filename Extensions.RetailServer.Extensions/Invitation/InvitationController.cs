namespace DAX.RetailServer.Extensions.Invitation
{
    using System.Runtime.InteropServices;
    using System.Web.Http;
    using System.Web.OData;
    using DAX.Runtime.Extensions.CRTExtensions.Messages;
    using DAX.Runtime.Extensions.CRTExtensions.DataModel;
    using SampleDataModel = Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Retail.RetailServerLibrary.ODataControllers;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Retail.RetailServerLibrary;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
    using System.Linq;
    using System.Collections.Generic;


    [ComVisible(false)]
    public class InvitationController : CommerceController<Invitation, long>
    {
        public override string ControllerName
        {
            get { return "InvitationController"; }
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public string GetInvitation()
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.SingleRecord;
            queryResultSettings.Paging = new PagingInfo(10);

            var request = new GetInvitationRequest() { QueryResultSettings = queryResultSettings };
            var invitationResp = runtime.Execute<SingleEntityDataServiceResponse<string>>(request, null)?.Entity;
            return invitationResp;
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public PagedResult<Invitation> GetAllInvitations()
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(10);

            var request = new GetAllInvitationsRequest() { QueryResultSettings = queryResultSettings };
            var invitationResp = runtime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
            return invitationResp;
        }

        //[HttpPost]
        //[CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        //public PagedResult<Invitation> DeleteAllInvitations()
        //{
        //    var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

        //    QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
        //    queryResultSettings.Paging = new PagingInfo(10);

        //    var request = new DeleteAllInvitationsRequest() { QueryResultSettings = queryResultSettings };
        //    var invitationResp = runtime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
        //    return invitationResp;
        //}

        //[HttpPost]
        //[CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        //public PagedResult<Invitation> UpdateInvitation(ODataActionParameters parameters)
        //{
        //    var invitation = (Invitation)parameters["updateInvitationRecord"];
        //    var request = new UpdateInvitationRequest(invitation);
        //    var invitationResp = CommerceRuntime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
        //    return invitationResp;
        //}

        //[HttpPost]
        //[CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        //public PagedResult<Invitation> DeleteInvitation(ODataActionParameters parameters)
        //{
        //    var invitation = (Invitation)parameters["deleteInvitationRecord"];
        //    var request = new DeleteInvitationRequest(invitation);
        //    var invitationResp = CommerceRuntime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
        //    return invitationResp;
        //}

        //[HttpPost]
        //[CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        //public PagedResult<Invitation> InsertInvitation(ODataActionParameters parameters)
        //{
        //    var invitation = (Invitation)parameters["insertInvitationRecord"];
        //    var request = new InsertInvitationRequest(invitation);
        //    var invitationResp = CommerceRuntime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
        //    return invitationResp;
        //}

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public bool DeleteAllInvitations()
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(10);

            var request = new DeleteAllInvitationsRequest() { QueryResultSettings = queryResultSettings };
            var result = runtime.Execute<SingleEntityDataServiceResponse<bool>>(request, null).Entity;
            return result;
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public bool UpdateInvitation(ODataActionParameters parameters)
        {
            var invitation = (Invitation)parameters["updateInvitationRecord"];
            var request = new UpdateInvitationRequest(invitation);
            var result = CommerceRuntime.Execute<SingleEntityDataServiceResponse<bool>>(request, null).Entity;
            return result;
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public bool DeleteInvitation(ODataActionParameters parameters)
        {
            var invitation = (Invitation)parameters["deleteInvitationRecord"];
            var request = new DeleteInvitationRequest(invitation);
            var result = CommerceRuntime.Execute<SingleEntityDataServiceResponse<bool>>(request, null).Entity;
            return result;
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public bool InsertInvitation(ODataActionParameters parameters)
        {
            var invitation = (Invitation)parameters["insertInvitationRecord"];
            var request = new InsertInvitationRequest(invitation);
            var result = CommerceRuntime.Execute<SingleEntityDataServiceResponse<bool>>(request, null).Entity;
            return result;
        }
    }
}
