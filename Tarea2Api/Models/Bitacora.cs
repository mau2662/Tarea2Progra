//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tarea2Api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bitacora
    {
        public long Codigo { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaHora { get; set; }
        public long CodigoCotizacion { get; set; }
    
        public virtual Cotizacion Cotizacion { get; set; }
    }
}
