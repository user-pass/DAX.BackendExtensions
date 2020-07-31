using Microsoft.Dynamics.Commerce.Runtime.ComponentModel.DataAnnotations;
using Microsoft.Dynamics.Commerce.Runtime.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAX.Runtime.Extensions.CRTExtensions.DataModels
{
    public class CustTable : CommerceEntity
    {
        private const string recId = "RECID";
        private const string identificationNumber = "IDENTIFICATIONNUMBER";
        private const string accountNumber = "ACCOUNTNUM";


        public CustTable() : base("CustTable")
        {
        }

        [Key]
        [DataMember]
        [Column(recId)]
        public int Id
        {
            get { return (int)this[recId]; }
            set { this[recId] = value; }
        }

        [DataMember]
        [Column(identificationNumber)]
        public string IdentificationNumber
        {
            get { return (string)this[identificationNumber]; }
            set { this[identificationNumber] = value; }
        }

        [DataMember]
        [Column(accountNumber)]
        public string AccountNumber
        {
            get { return (string)this[accountNumber]; }
            set { this[accountNumber] = value; }
        }

    }
}
