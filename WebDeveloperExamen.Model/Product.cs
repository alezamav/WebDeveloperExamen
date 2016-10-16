using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDeveloperExamen.Model
{

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string ProductName { get; set; }

        [Display(Name = "Código de Proveedor")]
        public int SupplierId { get; set; }

        [Display(Name = "Precio Unitario")]
        public decimal? UnitPrice { get; set; }

        [StringLength(30)]
        [Display(Name = "Paquete")]
        public string Package { get; set; }

        public bool IsDiscontinued { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItem { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
