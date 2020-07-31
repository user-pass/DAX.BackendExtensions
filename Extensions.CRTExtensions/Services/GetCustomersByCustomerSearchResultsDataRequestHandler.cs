//namespace DAX.Runtime.Extensions.CRTExtensions.Services
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using Microsoft.Dynamics.Commerce.Runtime;
//    using Microsoft.Dynamics.Commerce.Runtime.Data;
//    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
//    using CustomerAttribute = Microsoft.Dynamics.Commerce.Runtime.DataModel.CustomerAttribute;
//    using CustomerAttributeWithIdentificationNumber = DataModels.CustomerAttributeWithIdentificationNumber;
//    using Customer = Microsoft.Dynamics.Commerce.Runtime.DataModel.Customer;
//    using QueryResultSettings = Microsoft.Dynamics.Commerce.Runtime.DataModel.QueryResultSettings;

//    public sealed class GetCustomersByCustomerSearchResultsDataRequestHandler : SingleRequestHandler<GetGlobalCustomersByCustomerSearchResultsDataRequest, EntityDataServiceResponse<Customer>>
//    {

//        protected override EntityDataServiceResponse<Customer> Process(GetGlobalCustomersByCustomerSearchResultsDataRequest request)
//        {
//            ThrowIf.Null(request, "request");

//            // Execute original customer search logic.
//            var requestHandler = new Microsoft.Dynamics.Commerce.Runtime.DataServices.SqlServer.CustomerSqlServerDataService();
//            var searchResults = request.RequestContext.Runtime.Execute<EntityDataServiceResponse<Customer>>(request, request.RequestContext, requestHandler, skipRequestTriggers: false).PagedEntityCollection;

//            // Get the desired attributes for each search result, and marshal any retrieved attribute data into extension properties.
//            this.GetRelevantAttributes(searchResults, request.RequestContext);

//            return new EntityDataServiceResponse<Customer>(searchResults.AsPagedResult());
//        }

//        private void GetRelevantAttributes(IEnumerable<Customer> customers, RequestContext context)
//        {
//            // Identify relevant async and traditional customer account numbers.
//            List<string> customerIdentificationNums = new List<string>();
//            List<string> asyncCustomerIdentificationNums = new List<string>();
//            foreach (Customer customer in customers)
//            {
//                if (customer.IsAsyncCustomer)
//                {
//                    asyncCustomerIdentificationNums.Add(customer.IdentificationNumber);
//                }
//                else
//                {
//                    customerIdentificationNums.Add(customer.IdentificationNumber);
//                }
//            }

//            const string AttributeViewDatabaseSchema = "crt";
//            const string AccountNumberColumnName = "ACCOUNTNUM";
//            const string DesiredAttribute = "Marketing opt in";
//            const string DataAreaIdVarName = "@dataAreaId";
//            const string AttrNameVarName = "@attrName";
//            const string WhereClause = "DATAAREAID = " + DataAreaIdVarName + " AND Name = " + AttrNameVarName;

//            var relevantAttributes = new List<CustomerAttributeWithIdentificationNumber>();
//            var generatedWhereClauses = new List<string>();

//            using (DatabaseContext databaseContext = new DatabaseContext(context))
//            {
//                //// Customer Attributes

//                if (customerIdentificationNums.Any())
//                {
//                    var customerAttributesQuery = new SqlPagedQuery(QueryResultSettings.AllRecords)
//                    {
//                        DatabaseSchema = AttributeViewDatabaseSchema,
//                        From = "CUSTOMERATTRIBUTEVIEW",
//                        Where = WhereClause,
//                        OrderBy = AccountNumberColumnName
//                    };

//                    customerAttributesQuery.AddInClause(customerIdentificationNums, AccountNumberColumnName, generatedWhereClauses);
//                    customerAttributesQuery.Where = WhereClause + " AND " + generatedWhereClauses.FirstOrDefault();

//                    customerAttributesQuery.Parameters[DataAreaIdVarName] = context.GetChannelConfiguration().InventLocationDataAreaId;
//                    customerAttributesQuery.Parameters[AttrNameVarName] = DesiredAttribute;

//                    relevantAttributes.AddRange(databaseContext.ReadEntity<CustomerAttributeWithIdentificationNumber>(customerAttributesQuery));
//                }

//                //// Async Customer Attributes

//                if (asyncCustomerIdentificationNums.Any())
//                {
//                    var asyncCustomerAttributesQuery = new SqlPagedQuery(QueryResultSettings.AllRecords)
//                    {
//                        DatabaseSchema = AttributeViewDatabaseSchema,
//                        From = "ASYNCCUSTOMERATTRIBUTEVIEW",
//                        Where = WhereClause,
//                        OrderBy = AccountNumberColumnName
//                    };

//                    generatedWhereClauses.Clear();
//                    asyncCustomerAttributesQuery.AddInClause(asyncCustomerIdentificationNums, AccountNumberColumnName, generatedWhereClauses);
//                    asyncCustomerAttributesQuery.Where = WhereClause + " AND " + generatedWhereClauses.FirstOrDefault();

//                    asyncCustomerAttributesQuery.Parameters[DataAreaIdVarName] = context.GetChannelConfiguration().InventLocationDataAreaId;
//                    asyncCustomerAttributesQuery.Parameters[AttrNameVarName] = DesiredAttribute;

//                    relevantAttributes.AddRange(databaseContext.ReadEntity<CustomerAttributeWithIdentificationNumber>(asyncCustomerAttributesQuery));
//                }
//            }

//            // Create a dictionary of these attributes to improve look-up efficiency.
//            Dictionary<string, CustomerAttribute> attributeMap = new Dictionary<string, CustomerAttribute>();
//            foreach (CustomerAttributeWithIdentificationNumber attribute in relevantAttributes)
//            {
//                attributeMap[attribute.IdentificationNumber] = attribute;
//            }

//            // Add each retrieved attribute to the appropriate customer record.
//            foreach (Customer customer in customers)
//            {
//                string identificationNum = customer.IdentificationNumber;
//                if (attributeMap.ContainsKey(identificationNum))
//                {
//                    CustomerAttribute attribute = attributeMap[identificationNum];
//                    customer.SetProperty(attribute.Name, attribute.AttributeValue.GetPropertyValue());
//                }
//            }
//        }
//    }
//}
