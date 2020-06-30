namespace DAX.Runtime.Extensions.CRTExtensions.DataModels
{
    using Microsoft.Dynamics.Commerce.Runtime.ComponentModel.DataAnnotations;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using System.Runtime.Serialization;

    public class Invitation : CommerceEntity
    {
        private const string recId = "RECID";
        private const string message = "MESSAGETEXT";
        private const string language = "LANGUAGEID";

        public Invitation() : base("Invitation")
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
        [Column(language)]
        public string Language
        {
            get { return (string)this[language]; }
            set { this[language] = value; }
        }

        [DataMember]
        [Column(message)]
        public string Message
        {
            get { return (string)this[message]; }
            set { this[message] = value; }
        }

    }
}

