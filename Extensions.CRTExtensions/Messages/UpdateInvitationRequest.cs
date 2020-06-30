namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using DAX.Runtime.Extensions.CRTExtensions.DataModels;
    public sealed class UpdateInvitationRequest: Request
    {
        public UpdateInvitationRequest(Invitation updateInvitationRecord)
        {
            this.UpdateInvitationRecord = updateInvitationRecord;
        }

        public Invitation UpdateInvitationRecord { get; private set; }
    }
}
