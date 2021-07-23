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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Cuenta = new HashSet<Cuenta>();
            this.Informe = new HashSet<Informe>();
            this.Ruta = new HashSet<Ruta>();
        }
    
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public int FK_bodegaID { get; set; }
        public int FK_personaID { get; set; }
        public int FK_rolID { get; set; }
        public bool activoSN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cuenta> Cuenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informe> Informe { get; set; }
        public virtual Bodega Bodega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ruta> Ruta { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Rol Rol { get; set; }
    }
}