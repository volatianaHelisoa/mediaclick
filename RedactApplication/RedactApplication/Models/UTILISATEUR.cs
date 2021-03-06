//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RedactApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UTILISATEUR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UTILISATEUR()
        {
            this.COMMANDEs = new HashSet<COMMANDE>();
            this.COMMANDEs1 = new HashSet<COMMANDE>();
            this.FACTUREs = new HashSet<FACTURE>();
            this.FACTUREs1 = new HashSet<FACTURE>();
            this.NOTIFICATIONs = new HashSet<NOTIFICATION>();
            this.NOTIFICATIONs1 = new HashSet<NOTIFICATION>();
            this.TEMPLATEs = new HashSet<TEMPLATE>();
            this.USER_THEME = new HashSet<USER_THEME>();
            this.UserRoles = new HashSet<UserRole>();
            this.REDACT_THEME = new HashSet<REDACT_THEME>();
        }
    
        public System.Guid userId { get; set; }
        public string userNom { get; set; }
        public string userPrenom { get; set; }
        public string userMail { get; set; }
        public string userMotdepasse { get; set; }
        public Nullable<System.Guid> token { get; set; }
        public Nullable<System.DateTime> dateToken { get; set; }
        public string logoUrl { get; set; }
        public string redactSkype { get; set; }
        public string redactVolume { get; set; }
        public string redactModePaiement { get; set; }
        public string redactReferenceur { get; set; }
        public string redactThemes { get; set; }
        public string redactNiveau { get; set; }
        public string redactPhone { get; set; }
        public string redactTarif { get; set; }
        public string redactVolumeRestant { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMANDE> COMMANDEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMANDE> COMMANDEs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACTURE> FACTUREs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACTURE> FACTUREs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION> NOTIFICATIONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION> NOTIFICATIONs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TEMPLATE> TEMPLATEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_THEME> USER_THEME { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REDACT_THEME> REDACT_THEME { get; set; }
    }
}
