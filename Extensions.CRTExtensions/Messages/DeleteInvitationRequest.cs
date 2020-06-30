namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using DAX.Runtime.Extensions.CRTExtensions.DataModels;
    public sealed class DeleteInvitationRequest: Request
    {
        public DeleteInvitationRequest(Invitation deleteInvitationRecord)
        {
            this.DeleteInvitationRecord = deleteInvitationRecord;
        }

        public Invitation DeleteInvitationRecord { get; private set; }
    }
}
