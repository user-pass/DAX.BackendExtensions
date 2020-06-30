namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using DAX.Runtime.Extensions.CRTExtensions.DataModels;

    public sealed class GetInvitationRequest : Request
    {
        public GetInvitationRequest(Invitation invitation)
        {
            this.Invitation = invitation;
        }

        public Invitation Invitation { get; private set; }

    }
}
