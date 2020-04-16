namespace DAX.RetailServer.Extensions.Invitation
{
    using System;
    using System.Composition;
    using System.Runtime.InteropServices;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Retail.RetailServerLibrary;
    using DAX.Runtime.Extensions.CRTExtensions.DataModel;
    using System.Collections.Generic;


    [Export(typeof(IEdmModelExtender))]
    [ComVisible(false)]
    public class EdmModelExtender : IEdmModelExtender
    {

        public void ExtendModel(CommerceModelBuilder builder)
        {
            builder.BuildEntitySet<Invitation>("InvitationController");

            var action = builder.BindEntitySetAction<Invitation>("GetAllInvitations");
            action.ReturnsCollectionFromEntitySet<Invitation>("InvitationController");

            action = builder.BindEntitySetAction<Invitation>("GetInvitation");
            action.Returns<string>();

            action = builder.BindEntitySetAction<Invitation>("DeleteInvitation");
            action.Parameter<Invitation>("deleteInvitationRecord");
            action.ReturnsCollectionFromEntitySet<Invitation>("InvitationController");

            action = builder.BindEntitySetAction<Invitation>("DeleteAllInvitations");
            action.ReturnsCollectionFromEntitySet<Invitation>("InvitationController");

            action = builder.BindEntitySetAction<Invitation>("InsertInvitation");
            action.Parameter<Invitation>("insertInvitationRecord");
            action.ReturnsCollectionFromEntitySet<Invitation>("InvitationController");

            action = builder.BindEntitySetAction<Invitation>("UpdateInvitation");
            action.Parameter<Invitation>("updateInvitationRecord");
            action.ReturnsCollectionFromEntitySet<Invitation>("InvitationController");

            //ThrowIf.Null(builder, "builder");
            //builder.BuildEntitySet<SampleDataModel.StoreDayHours>("StoreHours");        
            //action = builder.BindEntityAction<SampleDataModel.StoreDayHours>("UpdateStoreDayHours");
            //action.ReturnsFromEntitySet<SampleDataModel.StoreDayHours>("StoreHours");

        }
    }
}
