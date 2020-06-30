namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    public sealed class DeleteAllInvitationsRequest: Request
    {
        public DeleteAllInvitationsRequest()
        {
        }

        public string DeleteAllInvitations { get; private set; }
    }
}
