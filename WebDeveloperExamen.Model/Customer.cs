using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDeveloperExamen.Model
{

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [StringLength(40)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [StringLength(40)]
        [Display(Name = "Pa�s")]
        public string Country { get; set; }

        [StringLength(20)]
        [Display(Name = "Tel�fono")]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
