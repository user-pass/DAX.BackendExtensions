using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using DAX.Runtime.Extensions.CRTExtensions.DataModel;
    public sealed class DeleteInvitationRequest: Request
    {
        public DeleteInvitationRequest(Invitation deleteInvitationRecord)
        {
            this.DeleteInvitationRecord = deleteInvitationRecord;
        }

        public Invitation DeleteInvitationRecord { get; private set; }
    }
}
