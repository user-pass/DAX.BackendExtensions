using DAX.Runtime.Extensions.CRTExtensions.DataModels;
using DAX.Runtime.Extensions.CRTExtensions.Messages;
using Microsoft.Dynamics.Commerce.Runtime;
using Microsoft.Dynamics.Commerce.Runtime.DataModel;
using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
using Microsoft.Dynamics.Commerce.Runtime.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAX.Runtime.Extensions.CRTExtensions.Services
{
    public sealed class SearchJournalTransactionsServiceRequestHandler : SingleRequestHandler<SearchJournalTransactionsServiceRequest, SearchJournalTransactionsServiceResponse>
    {
        private const string SsnFilter = "SSNNumberCustomerSearchFilter";

        protected override SearchJournalTransactionsServiceResponse Process(SearchJournalTransactionsServiceRequest request)
        {
            if (request.Criteria.CustomFilters.Any(x => x.Key.Contains(SsnFilter)))
            {
                var account = GetCustomerId(request);
                request.Criteria.CustomerAccountNumber = string.IsNullOrEmpty(account) ? SsnFilter : account;
                request.Criteria.CustomFilters.Remove(request.Criteria.CustomFilters.Where(x => x.Key.Contains(SsnFilter)).FirstOrDefault());
            }
            var requestHandler = new Microsoft.Dynamics.Commerce.Runtime.Services.StoreOperationService();
            return request.RequestContext.Runtime.Execute<SearchJournalTransactionsServiceResponse>(request, request.RequestContext, requestHandler, skipRequestTriggers: true);
        }

        private string GetCustomerId(SearchJournalTransactionsServiceRequest request)
        {
            QueryResultSettings queryResultSettings = QueryResultSettings.AllRecords;
            queryResultSettings.Paging = new PagingInfo(10);

            var custRequest = new GetCustomerByIdRequest(request.Criteria.CustomFilters.FirstOrDefault().SearchValues.FirstOrDefault().Value.StringValue) { QueryResultSettings = queryResultSettings };
            var account = request.RequestContext.Execute<EntityDataServiceResponse<CustTable>>(custRequest, null);
            return account.FirstOrDefault().AccountNumber;
        }
    }
}
