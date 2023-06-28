using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarea2Api.Entities
{
    public class CotizacionEnt
    {

        public int Codigo { get; set; }
        public string Matricula { get; set; }
        public int Porcentaje { get; set; }

        public decimal PrecioFinal { get; set; }


    }
}