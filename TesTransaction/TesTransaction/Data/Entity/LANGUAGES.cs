//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TesTransaction.Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class LANGUAGES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LANGUAGES()
        {
            this.TICKET_MESSAGE = new HashSet<TICKET_MESSAGE>();
        }
    
        public int idLanguage { get; set; }
        public string nameLanguage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TICKET_MESSAGE> TICKET_MESSAGE { get; set; }
    }
}
