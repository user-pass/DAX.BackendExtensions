namespace DAX.Runtime.Extensions.CRTExtensions.DataModels
{
    using Microsoft.Dynamics.Commerce.Runtime.ComponentModel.DataAnnotations;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using System.Runtime.Serialization;
    public class TenderTypeModel : CommerceEntity
    {
        private const string tenderTypeId = "TENDERTYPEID";
        private const string name = "NAME";
        private const string draftNeeded = "DRAFTNEEDED";

        public TenderTypeModel() : base("TenderTypeModel")
        {
        }

        [DataMember]
        [Column(name)]
        public string Name
        {
            get { return (string)this[name]; }
            set { this[name] = value; }
        }

        [Key]
        [DataMember]
        [Column(tenderTypeId)]
        public string TenderTypeModelId
        {
            get { return (string)this[tenderTypeId]; }
            set { this[tenderTypeId] = value; }
        }

        [DataMember]
        [Column(draftNeeded)]
        public bool DraftNeeded
        {
            get { return (bool)this[draftNeeded]; }
            set { this[draftNeeded] = value; }
        }


    }
}
