﻿namespace DAX.Runtime.Extensions.CRTExtensions.Services
{
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Workflow;
    using System.Linq;

    public sealed class SaveCartHandler : SingleRequestHandler<SaveCartRequest, SaveCartResponse>
    {
        protected override SaveCartResponse Process(SaveCartRequest request)
        {
            ThrowIf.Null(request, "request");

            foreach (var line in request.Cart.CartLines.ToList())
            {
                line.Comment = "test discount";
            }

            if (request.Cart.NetPrice >= 50.0M)
            {

                request.Cart.TotalManualDiscountPercentage = 10;

                

                //request.Cart.NetPrice = request.Cart.NetPrice - request.Cart.NetPrice / 10;

                //foreach (var line in request.Cart.CartLines.ToList())
                //{
                //    line.LineDiscount = line.Price / 10;
                //    line.DiscountAmount = line.Price / 10;
                //    line.DiscountAmountWithoutTax = line.Price / 10; ;
                //    DiscountLine discountLine = new DiscountLine();
                //    discountLine.Percentage = 10;
                //    discountLine.Amount = line.Price / 10;
                //    line.DiscountLines.Add(discountLine);
                //    line.Price = line.Price - line.Price / 10;
                //}
            }


            var requestHandler = new SaveCartRequestHandler();
            return request.RequestContext.Runtime.Execute<SaveCartResponse>(request, request.RequestContext, requestHandler, skipRequestTriggers: true);
        }
    }
}
