namespace DAX.Runtime.Extensions.CRTExtensions.Services
{
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Workflow;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            

            if (request.Cart.CartLines.Count > 0)
            {
                int sum = 0;

                foreach (var line in request.Cart.CartLines.ToList())
                {
                    
                    sum += Convert.ToInt32(line.Price);

                    CartLine cartLine = new CartLine();
                    cartLine = line;
                    cartLine.Comment = "discount";
                    //DiscountLine discountLine = new DiscountLine();
                    //discountLine.Percentage = 50;
                    //cartLine.DiscountLines.Add(discountLine);
                    request.Cart.CartLines.Remove(line);
                    request.Cart.CartLines.Add(cartLine);
                }

                if (sum >= 50)
                {
                    request.Cart.TotalManualDiscountPercentage = 50;
                }
            } else
            {
                var emptyRequestHandler = new SaveCartRequestHandler();
                var saveEmptyCartRequest = new SaveCartRequest(request.Cart);
                return request.RequestContext.Runtime.Execute<SaveCartResponse>(saveEmptyCartRequest, saveEmptyCartRequest.RequestContext, emptyRequestHandler, skipRequestTriggers: true);
            }

            

            var requestHandler = new SaveCartRequestHandler();
            var saveCartRequest = new SaveCartRequest(request.Cart);
            return request.RequestContext.Runtime.Execute<SaveCartResponse>(saveCartRequest, saveCartRequest.RequestContext, requestHandler, skipRequestTriggers: true);
        }

        public void OnExecuted(Request request, Response response)
        {
            
        }
    }
}
