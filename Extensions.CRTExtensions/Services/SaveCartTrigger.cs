namespace DAX.Runtime.Extensions.CRTExtensions.Services
{
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Workflow;
    using System;
    using System.Collections.Generic;

    public class SaveCartTrigger : IRequestTrigger
    {
        public IEnumerable<Type> SupportedRequestTypes
        {
            get
            {
                return new[]
                {
                        typeof(SaveCartRequest)
                };
            }
        }

        public void OnExecuting(Request request)
        {

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            Type requestedType = request.GetType();

            if (requestedType == typeof(SaveCartRequest))
            {
                this.GetCommentForCart((SaveCartRequest)request);
            }
        }

        private SaveCartResponse GetCommentForCart(SaveCartRequest request)
        {
            request.Cart.Comment = "test";
            request.Cart.InvoiceComment = "test";

            var requestHandler = new SaveCartRequestHandler();
            var saveCartRequest = new SaveCartRequest(request.Cart);
            //return request.RequestContext.Runtime.Execute<SaveCartResponse>(request, request.RequestContext, requestHandler, skipRequestTriggers: true);
            return request.RequestContext.Runtime.Execute<SaveCartResponse>(saveCartRequest, saveCartRequest.RequestContext, requestHandler, skipRequestTriggers: true);
        }

        public void OnExecuted(Request request, Response response)
        {
        }
    }
}
