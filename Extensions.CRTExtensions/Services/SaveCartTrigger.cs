//namespace DAX.Runtime.Extensions.CRTExtensions.Services
//{
//    using Microsoft.Dynamics.Commerce.Runtime;
//    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
//    using Microsoft.Dynamics.Commerce.Runtime.Messages;
//    using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
//    using Microsoft.Dynamics.Commerce.Runtime.Workflow;
//    using System;
//    using System.Collections.Generic;
//    using System.Diagnostics;
//    using System.Linq;

//    public class SaveCartTrigger : IRequestTrigger
//    {
//        public IEnumerable<Type> SupportedRequestTypes
//        {
//            get
//            {
//                return new[]
//                {
//                        typeof(SaveCartRequest)
//                };
//            }
//        }

//        public void OnExecuting(Request request)
//        {

//            if (request == null)
//            {
//                throw new ArgumentNullException("request");
//            }

//            Type requestedType = request.GetType();

//            if (requestedType == typeof(SaveCartRequest))
//            {
//                this.GetCommentForCart((SaveCartRequest)request);
//            }
//        }

//        private SaveCartResponse GetCommentForCart(SaveCartRequest request)
//        {


//            if (request.Cart.CartLines.Count > 0)
//            {
//                //decimal sum = 0.0M;

//                foreach (var line in request.Cart.CartLines.ToList())
//                {

//                    //sum += line.Price;

//                    CartLine cartLine = new CartLine();
//                    cartLine = line;
//                    cartLine.Comment = "discount";

//                    //DiscountLine discountLine = new DiscountLine();
//                    //discountLine.Percentage = 50;
//                    //cartLine.DiscountLines.Remove(cartLine.DiscountLines.FirstOrDefault());
//                    //cartLine.DiscountLines.Add(discountLine);

//                    request.Cart.CartLines.Remove(line);
//                    request.Cart.CartLines.Add(cartLine);
//                }



//                if (request.Cart.NetPrice >= 50.0M)
//                {
//                    request.Cart.TotalManualDiscountPercentage = 50;
//                }

//            }

//            //using (EventLog eventLog = new EventLog("Application"))
//            //{
//            //    eventLog.Source = "Application";
//            //    eventLog.WriteEntry("Final sum is " + request.Cart.NetPrice.ToString(), EventLogEntryType.Warning, 101, 1);
//            //}


//            var requestHandler = new SaveCartRequestHandler();
//            return request.RequestContext.Runtime.Execute<SaveCartResponse>(request, request.RequestContext, requestHandler, skipRequestTriggers: true);
//        }

//        public void OnExecuted(Request request, Response response)
//        {

//        }
//    }
//}
