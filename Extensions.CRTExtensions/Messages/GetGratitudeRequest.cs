namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;

    public sealed class GetGratitudeRequest: Request
    {
        public GetGratitudeRequest(string storeNumber)
        {
            this.StoreNumber = storeNumber;
        }

        public string StoreNumber { get; private set; }
    }
}
