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


        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public PagedResult<Invitation> DeleteInvitation(Invitation deleteInvitationRecord)
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(10);

            var request = new DeleteInvitationRequest(deleteInvitationRecord) { QueryResultSettings = queryResultSettings };
            var invitationResp = runtime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
            return invitationResp;
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public PagedResult<Invitation> DeleteAllInvitations()
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(10);

            var request = new DeleteAllInvitationsRequest() { QueryResultSettings = queryResultSettings };
            var invitationResp = runtime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
            return invitationResp;
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public PagedResult<Invitation> InsertInvitation(Invitation insertInvitationRecord)
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(10);

            var request = new InsertInvitationRequest(insertInvitationRecord) { QueryResultSettings = queryResultSettings };
            var invitationResp = runtime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
            return invitationResp;
        }

        [HttpPost]
        [CommerceAuthorization(CommerceRoles.Anonymous, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public PagedResult<Invitation> UpdateInvitation(Invitation updateInvitationRecord)
        {
            var runtime = CommerceRuntimeManager.CreateRuntime(this.CommercePrincipal);

            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(10);

            var request = new UpdateInvitationRequest(updateInvitationRecord) { QueryResultSettings = queryResultSettings };
            var invitationResp = runtime.Execute<EntityDataServiceResponse<Invitation>>(request, null).PagedEntityCollection;
            return invitationResp;
        }
    }
}
