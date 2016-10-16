using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDeveloperExamen.Model
{


    [Table("Supplier")]
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Compania")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre de Contacto")]
        public string ContactName { get; set; }

        [StringLength(40)]
        [Display(Name = "Contacto")]
        public string ContactTitle { get; set; }

        [StringLength(40)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [StringLength(40)]
        [Display(Name = "Pa�s")]
        public string Country { get; set; }

        [StringLength(30)]
        [Display(Name = "Tel�fono")]
        public string Phone { get; set; }

        [StringLength(30)]
        public string Fax { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
