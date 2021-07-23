//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaAccesoDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Factura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Factura()
        {
            this.LineaPedido = new HashSet<LineaPedido>();
        }
    
        public int facturaID { get; set; }
        public System.DateTime fecha { get; set; }
        public byte descuento { get; set; }
        public decimal total { get; set; }
        public decimal saldo { get; set; }
        public int FK_cuentaID { get; set; }
        public int FK_informeID { get; set; }
        public bool activoSN { get; set; }
    
        public virtual Cuenta Cuenta { get; set; }
        public virtual Informe Informe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LineaPedido> LineaPedido { get; set; }
    }
}
