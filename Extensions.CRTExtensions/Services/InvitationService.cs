namespace DAX.Runtime.Extensions.CRTExtensions.Services
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

    public class InvitationService : IRequestHandler
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
                        typeof(UpdateInvitationRequest)
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
            else
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Request '{0}' is not supported.", reqType);
                RetailLogger.Log.GenericWarningEvent(message);
                throw new NotSupportedException(message);
            }
        }

        private SingleEntityDataServiceResponse<string> GetInvitation(GetInvitationRequest request)
        {
            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ext",
                    Select = new ColumnSet("message"),
                    From = "INVITATIONTABLE",
                    Where = "language = @language"
                };
                query.Parameters["@language"] = request.RequestContext.GetChannelConfiguration().CompanyLanguageId.ToString();

                var result = databaseContext.ReadEntity<DataModel.Invitation>(query);

                return new SingleEntityDataServiceResponse<string>(result?.Results?.FirstOrDefault()?.Message);
            }
        }

        private EntityDataServiceResponse<DataModel.Invitation> GetAllInvitations(GetAllInvitationsRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ext",
                    Select = new ColumnSet("recId", "message", "language"),
                    From = "INVITATIONTABLE"
                };

                var result = databaseContext.ReadEntity<DataModel.Invitation>(query);

                return new EntityDataServiceResponse<DataModel.Invitation>(result);
            }
            
        }

        private EntityDataServiceResponse<DataModel.Invitation> DeleteAllInvitations(DeleteAllInvitationsRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var truncateQuery = new SqlQuery()
                {
                    QueryString = "TRUNCATE TABLE [AxDB].[ext].[INVITATIONTABLE]"
                };
                databaseContext.ExecuteNonQuery(truncateQuery);

                var selectQuery = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ext",
                    Select = new ColumnSet("recId", "message", "language"),
                    From = "INVITATIONTABLE"
                };

                var result = databaseContext.ReadEntity<DataModel.Invitation>(selectQuery);

                return new EntityDataServiceResponse<DataModel.Invitation>(result);
            }
        }

        private EntityDataServiceResponse<DataModel.Invitation> DeleteInvitation(DeleteInvitationRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var deleteQuery = new SqlQuery()
                {                   
                    QueryString = "DELETE FROM [AxDB].[ext].[INVITATIONTABLE] WHERE recId = @recId"
                };
                deleteQuery.Parameters["@recId"] = request.DeleteInvitationRecord.Id;
                databaseContext.ExecuteNonQuery(deleteQuery);

                var selectQuery = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ext",
                    Select = new ColumnSet("recId", "message", "language"),
                    From = "INVITATIONTABLE"
                };

                var result = databaseContext.ReadEntity<DataModel.Invitation>(selectQuery);

                return new EntityDataServiceResponse<DataModel.Invitation>(result);
            }
        }

        private EntityDataServiceResponse<DataModel.Invitation> InsertInvitation(InsertInvitationRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlQuery()
                {
                    QueryString = "INSERT INTO [AxDB].[ext].[INVITATIONTABLE] (message, language) VALUES (@message, @language)"
                };
                query.Parameters["@message"] = request.InsertInvitationRecord.Message;
                query.Parameters["@language"] = request.InsertInvitationRecord.Language;

                databaseContext.ExecuteNonQuery(query);

                return new EntityDataServiceResponse<DataModel.Invitation>();
            }
        }

        private EntityDataServiceResponse<DataModel.Invitation> UpdateInvitation(UpdateInvitationRequest request)
        {

            ThrowIf.Null(request, "request");
            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    QueryString = "UPDATE ext.INVITATIONTABLE SET message = @message, language = @language WHERE recId = @recId"
                };
                query.Parameters["@recId"] = request.UpdateInvitationRecord.Id;
                query.Parameters["@message"] = request.UpdateInvitationRecord.Message;
                query.Parameters["@language"] = request.UpdateInvitationRecord.Language;

                databaseContext.ExecuteNonQuery(query);

                return new EntityDataServiceResponse<DataModel.Invitation>();
            }
        }
    }
}

