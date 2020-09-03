namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    public sealed class GetTenderTypesRequest : Request
    {
        public GetTenderTypesRequest() { }

        public string AllTenderTypes { get; private set; }

    }
}
