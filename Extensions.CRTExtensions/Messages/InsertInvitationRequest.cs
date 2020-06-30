namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using DAX.Runtime.Extensions.CRTExtensions.DataModels;
    public sealed class InsertInvitationRequest: Request
    {
        public InsertInvitationRequest(Invitation insertInvitationRecord)
        {
            this.InsertInvitationRecord = insertInvitationRecord;
        }

        public Invitation InsertInvitationRecord { get; private set; }
    }
}
