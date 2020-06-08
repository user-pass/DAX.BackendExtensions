using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.Commerce.Runtime.ComponentModel.DataAnnotations;
using Microsoft.Dynamics.Commerce.Runtime.DataModel;
using System.Runtime.Serialization;

namespace DAX.Runtime.Extensions.CRTExtensions.DataModel
{
    public class Language: CommerceEntity
    {
        private const string recId = "RECID";
        private const string languageId = "LANGUAGEID";

        public Language() : base("Language")
        {
        }
        
        [DataMember]
        [Column(recId)]
        public long RecId
        {
            get { return (long)this[recId]; }
            set { this[recId] = value; }
        }

        [Key]
        [DataMember]
        [Column(languageId)]
        public string LanguageId
        {
            get { return (string)this[languageId]; }
            set { this[languageId] = value; }
        }

       
    }
}
