//namespace DAX.Runtime.Extensions.CRTExtensions.Services
//{
//    using Microsoft.Dynamics.Commerce.Runtime;
//    using Microsoft.Dynamics.Commerce.Runtime.Messages;
//    using Microsoft.Dynamics.Commerce.Runtime.Workflow;

//    public sealed class SaveCartHandler : SingleRequestHandler<SaveCartRequest, SaveCartResponse>
//    {
//        protected override SaveCartResponse Process(SaveCartRequest request)
//        {
//            ThrowIf.Null(request, "request");

//            request.Cart.Comment = "test";
//            request.Cart.InvoiceComment = "test";

//            var requestHandler = new SaveCartRequestHandler();
//            return request.RequestContext.Runtime.Execute<SaveCartResponse>(request, request.RequestContext, requestHandler, skipRequestTriggers: false);
//        }
//    }
//}
