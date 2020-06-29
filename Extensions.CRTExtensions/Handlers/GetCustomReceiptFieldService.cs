using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAX.Runtime.Extensions.CRTExtensions.Handlers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Data;
    using CRTExtensions.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Messages;

    public class GetCustomReceiptFieldService : IRequestHandler
    {

        public IEnumerable<Type> SupportedRequestTypes
        {
            get
            {
                return new[]
                {
                        typeof(GetSalesTransactionCustomReceiptFieldServiceRequest),
                    };
            }
        }

        public Response Execute(Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            Type requestedType = request.GetType();

            if (requestedType == typeof(GetSalesTransactionCustomReceiptFieldServiceRequest))
            {
                return this.GetCustomReceiptFieldForSalesTransactionReceipts((GetSalesTransactionCustomReceiptFieldServiceRequest)request);
            }

            throw new NotSupportedException(string.Format("Request '{0}' is not supported.", request.GetType()));
        }
        private GetCustomReceiptFieldServiceResponse GetCustomReceiptFieldForSalesTransactionReceipts(GetSalesTransactionCustomReceiptFieldServiceRequest request)
                                                                                            
        {
            string receiptFieldName = request.CustomReceiptField;
            string returnValue = string.Empty;
            string storeNumber = string.Empty;
            storeNumber = request.SalesOrder.StoreId;
            switch (receiptFieldName)
            {
                case "GRATITUDE":
                    {
                        returnValue = GetGratitude(request.RequestContext, storeNumber);
                        
                    }

                    break;
            }

            return new GetCustomReceiptFieldServiceResponse(returnValue);
        }

        private string GetGratitude(RequestContext context, string storeNumber)
        {
            QueryResultSettings queryResultSettings = QueryResultSettings.SingleRecord;
            var request = new GetGratitudeRequest(storeNumber) { QueryResultSettings = queryResultSettings };
            Gratitude gratitude = context.Execute<EntityDataServiceResponse<Gratitude>>(request).PagedEntityCollection.FirstOrDefault();

            return gratitude.ReceiptMessage;
        }
    }
}

