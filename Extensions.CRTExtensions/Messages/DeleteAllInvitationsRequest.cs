using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAX.Runtime.Extensions.CRTExtensions.Messages
{
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    public sealed class DeleteAllInvitationsRequest: Request
    {
        public DeleteAllInvitationsRequest()
        {
        }

        public string DeleteAllInvitations { get; private set; }
    }
}
