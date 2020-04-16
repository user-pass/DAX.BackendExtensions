namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    public sealed class GetAllInvitationsRequest: Request
    {
        public GetAllInvitationsRequest()
        {
        }

        public string AllInvitations { get; private set; }

    }
}
