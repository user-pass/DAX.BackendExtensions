namespace DAX.Runtime.Extensions.CRTExtensions.DataModel
{

    using Microsoft.Dynamics.Commerce.Runtime.ComponentModel.DataAnnotations;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using System.Runtime.Serialization;

    public class Gratitude : CommerceEntity
    {
        private const string storeNumber = "STORENUMBER";
        private const string receiptMessage = "RECEIPTMESSAGE";
        private const string recId = "RECID";

        public Gratitude() : base("Gratitude")
        {
        }

        [DataMember]
        [Column(storeNumber)]
        public string StoreNumber
        {
            get { return (string)this[storeNumber]; }
            set { this[storeNumber] = value; }
        }

        [DataMember]
        [Column(receiptMessage)]
        public string ReceiptMessage
        {
            get { return (string)this[receiptMessage]; }
            set { this[receiptMessage] = value; }
        }

        [Key]
        [DataMember]
        [Column(recId)]
        public int RecId
        {
            get { return (int)this[recId]; }
            set { this[recId] = value; }
        }

    }
}
