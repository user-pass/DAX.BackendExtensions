using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAX.Runtime.Extensions.ReceiptsSample.Handlers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Data;
    using CRTExtensions.DataModel;

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
            string storeNumber = request.RequestContext.GetDeviceConfiguration().StoreNumber;

            string returnValue = string.Empty;
            switch (receiptFieldName)
            {
                case "Gratitude":
                    {
                        returnValue = "Hello";
                        //  GetGratitude(request, storeNumber);
                    }

                    break;
                //case "GRATITUDE":
                //    {
                //        returnValue = storeNumber;
                //    }

                //    break;
                    //case "1":
                    //    {
                    //        returnValue = storeNumber;
                    //    }

                    //    break;
                    //case "12345":
                    //    {
                    //        returnValue = storeNumber;
                    //    }

                    //    break;
                    //case "1_p":
                    //    {
                    //        returnValue = storeNumber;
                    //    }

                    //    break;
                    //case "Receipt":
                    //    {
                    //        returnValue = storeNumber;
                    //    }

                    //    break;
            }

            return new GetCustomReceiptFieldServiceResponse(returnValue);
        }

        private string GetGratitude(GetSalesTransactionCustomReceiptFieldServiceRequest request, string storeNumber)
        {
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ext",
                    Select = new ColumnSet("receiptMessage"),
                    From = "GRATITUDE",
                    Where = "storeNumber = @storeNumber"
                };
                query.Parameters["@storeNumber"] = storeNumber;

                var message = databaseContext.ReadEntity<Gratitude>(query).Results.FirstOrDefault().ReceiptMessage;

                return message;
            }
        }
    }
}

