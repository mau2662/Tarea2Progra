using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Tarea2Api.Entities;
using Tarea2Api.Models;

namespace Tarea2Api.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarVehiculos")]
        public List<VehiculosEnt> ConsultarVehiculos()
        {
            try
            {
                using (var bd = new PracticaLNEntities())
                {
                    var datos = (from x in bd.Vehiculos
                                 select x).ToList();
                    if (datos.Count > 0)
                    {
                        var resp = new List<VehiculosEnt>();
                        foreach (var item in datos)
                        {
                            resp.Add(new VehiculosEnt
                            {
                                Matricula = item.Matricula,
                                PrecioInicial = item.PrecioInicial,
                                Fabricacion = item.Fabricacion

                            });
                        }
                        return resp;
                    }
                    else
                    {
                        return new List<VehiculosEnt>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        [HttpGet]
        [Route("api/ConsultaMatricula")]
        public VehiculosEnt ConsultaMatricula(string matricula)
        {
            try
            {
                using (var bd = new PracticaLNEntities())
                {
                    var vehiculo = (from x in bd.Vehiculos
                                    where x.Matricula == matricula
                                    select x).FirstOrDefault();

                    if (vehiculo != null)
                    {
                        var vehiculoEntidad = new VehiculosEnt
                        {
                            Matricula = vehiculo.Matricula,
                            PrecioInicial = vehiculo.PrecioInicial,
                            Fabricacion = vehiculo.Fabricacion,

                        };

                        return vehiculoEntidad;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        [HttpPost]
        [Route("api/CotizacionCarros")]
        public int CotizacionCarros(CotizacionEnt entidad)
        {
            try
            {
                using (var bd = new PracticaLNEntities())
                {
                    var vehiculo = (from x in bd.Vehiculos
                                    where x.Matricula == entidad.Matricula
                                    select x).FirstOrDefault();

                    if (vehiculo != null)
                    {
                        if (vehiculo.Fabricacion > 2010)
                        {
                            if (entidad.Porcentaje >= 1 && entidad.Porcentaje <= 3)
                            {
                             
                                bool cotizacionExistente = bd.Cotizacion.Any(c => c.Matricula == entidad.Matricula && c.Porcentaje == entidad.Porcentaje);

                                if (!cotizacionExistente)
                                {
                                    Cotizacion tabla = new Cotizacion();
                                    tabla.Matricula = entidad.Matricula;
                                    tabla.Porcentaje = entidad.Porcentaje;
                                    var descuento = vehiculo.PrecioInicial * entidad.Porcentaje / 100;
                                    tabla.PrecioFinal = vehiculo.PrecioInicial - descuento;
                                    bd.Cotizacion.Add(tabla);
                                    bd.SaveChanges();
                                    var codigo = tabla.Codigo;

                                    Bitacora bitacora = new Bitacora();
                                    bitacora.Descripcion = entidad.Matricula;
                                    bitacora.FechaHora = DateTime.Now;
                                    bitacora.CodigoCotizacion = codigo;
                                    bd.Bitacora.Add(bitacora);

                                    return bd.SaveChanges();
                                }
                                else
                                {
                                    return 0; //La cotizacion ya existe
                                }
                            }
                            else
                            {
                                return 0;  //El porcentaje no esta entre 1 y 3
                            }
                        }
                        else
                        {
                            return 0;  //El anno de fabricacion es menor del 2010
                        }
                    }
                    else
                    {
                        return 0;  //El carro no existe
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0; 
            }
        }


    }

}





