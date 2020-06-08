namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;

    public sealed class GetAllLanguagesRequest: Request 
    {
        public GetAllLanguagesRequest() { }

        public string AllLanguages { get; private set; }

    }
}
