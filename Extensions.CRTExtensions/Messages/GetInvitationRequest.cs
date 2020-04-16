namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;

    public sealed class GetInvitationRequest : Request
    {
        public GetInvitationRequest()
        {
        }

        public string Invitation { get; private set; }

    }
}
