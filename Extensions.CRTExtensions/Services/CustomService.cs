﻿namespace DAX.Runtime.Extensions.CRTExtensions.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using Microsoft.Dynamics.Retail.Diagnostics;
    using Messages;
    using Microsoft.Dynamics.Commerce.Runtime.Data;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.DataServices.Messages;
    using System.Linq;
    using DataModels;

    public class CustomService : IRequestHandler
    {
        public IEnumerable<Type> SupportedRequestTypes
        {
            get
            {
                return new[]
                {
                        typeof(GetInvitationRequest),
                        typeof(GetAllInvitationsRequest),
                        typeof(DeleteInvitationRequest),
                        typeof(DeleteAllInvitationsRequest),
                        typeof(InsertInvitationRequest),
                        typeof(UpdateInvitationRequest),
                        typeof(GetAllLanguagesRequest),
                        typeof(GetGratitudeRequest),
                        typeof(GetCustomerByIdRequest),
                        typeof(GetTenderTypesRequest),
                    };
            }
        }

        public Response Execute(Request request)
        {

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            Type reqType = request.GetType();

            if (reqType == typeof(GetInvitationRequest))
            {
                return this.GetInvitation((GetInvitationRequest)request);
            }
            if (reqType == typeof(GetAllInvitationsRequest))
            {
                return this.GetAllInvitations((GetAllInvitationsRequest)request);
            }
            if (reqType == typeof(DeleteInvitationRequest))
            {
                return this.DeleteInvitation((DeleteInvitationRequest)request);
            }
            if (reqType == typeof(DeleteAllInvitationsRequest))
            {
                return this.DeleteAllInvitations((DeleteAllInvitationsRequest)request);
            }
            if (reqType == typeof(UpdateInvitationRequest))
            {
                return this.UpdateInvitation((UpdateInvitationRequest)request);
            }
            if (reqType == typeof(InsertInvitationRequest))
            {
                return this.InsertInvitation((InsertInvitationRequest)request);
            }
            if (reqType == typeof(GetAllLanguagesRequest))
            {
                return this.GetAllLanguages((GetAllLanguagesRequest)request);
            }
            if (reqType == typeof(GetGratitudeRequest))
            {
                return this.GetGratitude((GetGratitudeRequest)request);
            }
            if(reqType == typeof(GetCustomerByIdRequest))
            {
                return this.GetCustomerById((GetCustomerByIdRequest)request);
            }
            if(reqType == typeof(GetTenderTypesRequest))
            {
                return this.GetTenderTypes((GetTenderTypesRequest)request);
            }
            else
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Request '{0}' is not supported.", reqType);
                RetailLogger.Log.GenericWarningEvent(message);
                throw new NotSupportedException(message);
            }
        }



        private EntityDataServiceResponse<DataModels.Invitation> GetAllInvitations(GetAllInvitationsRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "dbo",
                    Select = new ColumnSet("RECID", "MESSAGETEXT", "LANGUAGEID"),
                    From = "INVITATIONTABLE"
                };

                var result = databaseContext.ReadEntity<DataModels.Invitation>(query);

                return new EntityDataServiceResponse<DataModels.Invitation>(result);
            }

        }

        private SingleEntityDataServiceResponse<bool> DeleteAllInvitations(DeleteAllInvitationsRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                try
                {
                    var query = new SqlQuery()
                    {
                        QueryString = "TRUNCATE TABLE [AxDB].[dbo].[INVITATIONTABLE]"
                    };
                    databaseContext.ExecuteNonQuery(query);

                    return new SingleEntityDataServiceResponse<bool>(true);
                }
                catch (Exception)
                {
                    return new SingleEntityDataServiceResponse<bool>(false);
                }
            }
        }

        private SingleEntityDataServiceResponse<bool> DeleteInvitation(DeleteInvitationRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                try
                {

                    var query = new SqlQuery()
                    {
                        QueryString = "DELETE FROM [AxDB].[dbo].[INVITATIONTABLE] WHERE RECID = @recId"
                    };
                    query.Parameters["@recId"] = request.DeleteInvitationRecord.Id;

                    databaseContext.ExecuteNonQuery(query);

                    return new SingleEntityDataServiceResponse<bool>(true);
                }
                catch (Exception)
                {
                    return new SingleEntityDataServiceResponse<bool>(false);
                }
            }
        }

        private SingleEntityDataServiceResponse<bool> InsertInvitation(InsertInvitationRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                try
                {
                    var query = new SqlQuery()
                    {
                        QueryString = "INSERT INTO [AxDB].[dbo].[INVITATIONTABLE] (MESSAGETEXT, LANGUAGEID) VALUES (@message, @language)"
                    };
                    query.Parameters["@message"] = request.InsertInvitationRecord.Message;
                    query.Parameters["@language"] = request.InsertInvitationRecord.Language;

                    databaseContext.ExecuteNonQuery(query);

                    return new SingleEntityDataServiceResponse<bool>(true);
                }
                catch (Exception)
                {
                    return new SingleEntityDataServiceResponse<bool>(false);
                }
            }
        }

        private SingleEntityDataServiceResponse<bool> UpdateInvitation(UpdateInvitationRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                try
                {
                    var query = new SqlQuery()
                    {
                        QueryString = "UPDATE [AxDB].[dbo].[INVITATIONTABLE] SET MESSAGETEXT = @message, LANGUAGEID = @language WHERE RECID = @recId"
                    };
                    query.Parameters["@recId"] = request.UpdateInvitationRecord.Id;
                    query.Parameters["@message"] = request.UpdateInvitationRecord.Message;
                    query.Parameters["@language"] = request.UpdateInvitationRecord.Language;

                    databaseContext.ExecuteNonQuery(query);

                    return new SingleEntityDataServiceResponse<bool>(true);

                }
                catch (Exception)
                {
                    return new SingleEntityDataServiceResponse<bool>(false);
                }
            }
        }

        private EntityDataServiceResponse<DataModels.Invitation> GetInvitation(GetInvitationRequest request)
        {
            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "dbo",
                    Select = new ColumnSet("RECID", "MESSAGETEXT", "LANGUAGEID"),
                    From = "INVITATIONTABLE",
                    Where = "LANGUAGEID = @languageId"
                };
                query.Parameters["@languageId"] = request.Invitation.Language;

                var result = databaseContext.ReadEntity<DataModels.Invitation>(query);

                return new EntityDataServiceResponse<DataModels.Invitation>(result);
            }
        }

        /////////////////////////////////////////LANGUAGE///////////////////////////////////////////////////

        private EntityDataServiceResponse<DataModels.Language> GetAllLanguages(GetAllLanguagesRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ax",
                    Select = new ColumnSet("RECID", "LANGUAGEID"),
                    From = "LANGUAGETABLE"
                };

                var result = databaseContext.ReadEntity<DataModels.Language>(query);

                return new EntityDataServiceResponse<DataModels.Language>(result);
            }

        }

        /////////////////////////////////////////GRATITUDE///////////////////////////////////////////////////
        private EntityDataServiceResponse<DataModels.Gratitude> GetGratitude(GetGratitudeRequest request)
        {
            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ext",
                    Select = new ColumnSet("STORENUMBER", "RECEIPTMESSAGE", "RECID"),
                    From = "GRATITUDE",
                    Where = "STORENUMBER = @storeNumber"
                };
                query.Parameters["@storeNumber"] = request.StoreNumber;
                var result = databaseContext.ReadEntity<DataModels.Gratitude>(query);

                return new EntityDataServiceResponse<DataModels.Gratitude>(result);
            }
        }

        ////////////////////////////////////////CUSTOMER_FILTER///////////////////////////////////////////////

        private EntityDataServiceResponse<CustTable> GetCustomerById(GetCustomerByIdRequest request)
        {
            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ax",
                    Select = new ColumnSet("IDENTIFICATIONNUMBER", "RECID", "ACCOUNTNUM"),
                    From = "CUSTTABLE",
                    Where = "IDENTIFICATIONNUMBER = @idNumber"
                };
                query.Parameters["@idNumber"] = request.CustomerIdNumber;

                var result = databaseContext.ReadEntity<CustTable>(query);

                return new EntityDataServiceResponse<CustTable>(result);
            }
        }


        //////////////////////////////////////////POS_REQUEST_HANDLER//////////////////////////////////////////

        private EntityDataServiceResponse<TenderTypeModel> GetTenderTypes(GetTenderTypesRequest request)
        {
            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ext",
                    Select = new ColumnSet("NAME", "TENDERTYPEID", "DRAFTNEEDED"),
                    From = "TENDERTYPETABLE",
                };

                var result = databaseContext.ReadEntity<TenderTypeModel>(query);

                return new EntityDataServiceResponse<TenderTypeModel>(result);
            }

        }
    }
}

