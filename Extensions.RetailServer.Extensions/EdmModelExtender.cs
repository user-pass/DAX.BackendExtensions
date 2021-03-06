﻿namespace DAX.RetailServer.Extensions.Controllers
{
    using System;
    using System.Composition;
    using System.Runtime.InteropServices;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Retail.RetailServerLibrary;
    using DAX.Runtime.Extensions.CRTExtensions.DataModels;
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

            //action = builder.BindEntitySetAction<Invitation>("GetInvitation");
            //action.Parameter<Invitation>("getInvitationRecord");
            //action.ReturnsCollectionFromEntitySet<Invitation>("InvitationController");

            action = builder.BindEntitySetAction<Invitation>("DeleteInvitation");
            action.Parameter<Invitation>("deleteInvitationRecord");
            action.Returns<bool>();

            action = builder.BindEntitySetAction<Invitation>("DeleteAllInvitations");
            action.Returns<bool>();

            action = builder.BindEntitySetAction<Invitation>("InsertInvitation");
            action.Parameter<Invitation>("insertInvitationRecord");
            action.Returns<bool>();

            action = builder.BindEntitySetAction<Invitation>("UpdateInvitation");
            action.Parameter<Invitation>("updateInvitationRecord");
            action.Returns<bool>();

            ////////////////////Language////////////////////////////////////

            builder.BuildEntitySet<Language>("LanguageController");
            action = builder.BindEntitySetAction<Language>("GetAllLanguages");
            action.ReturnsCollectionFromEntitySet<Language>("LanguageController");


            ////////////////////General/////////////////////////////////////////

            builder.BuildEntitySet<TenderTypeModel>("TenderTypeController");
            action = builder.BindEntitySetAction<TenderTypeModel>("GetTenderTypes");
            action.ReturnsCollectionFromEntitySet<TenderTypeModel>("TenderTypeController");
        }
    }
}
