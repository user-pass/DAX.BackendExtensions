namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;

    public sealed class GetCustomerByIdRequest : Request
    {
        public GetCustomerByIdRequest(string customerIdNumber)
        {
            this.CustomerIdNumber = customerIdNumber;
        }

        public string CustomerIdNumber { get; private set; }

    }
}
