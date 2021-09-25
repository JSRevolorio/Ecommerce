using Ecommerce_proyect.Models;
using Ecommerce_proyect.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce_proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private DbContexEcommerce context;

        public FacturaController(DbContexEcommerce context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetFactura()
        {
            try
            {
                var facturas = context.Facturas.Include(factura => factura.FacturaPagos).ThenInclude(pagos => pagos.IdMetodoPagoNavigation).Include(factuDe => factuDe.DetalleFacturas).Include(factura => factura.IdClienteNavigation).Include(factura => factura.IdEmpleadoNavigation);


                List<FacturaPresentacionView> facturaPresentacionViews = new List<FacturaPresentacionView>();

                facturas.ToList().ForEach(factura =>
                {

                    var cliente = context.Clientes.Find(factura.IdCliente);
                    var tienda = context.Tiendas.Find(factura.IdTienda);
                    var empleado = context.Empleados.Find(factura.IdEmpleado);
                    var monto = factura.FacturaPagos.Select(metodo => metodo.Monto).Sum();
                    var productos = context.DetalleFacturas.Where(productos => productos.IdFactura == factura.Id).ToList();


                    List<DetalleFacturaView> detalleFacturaViews = new List<DetalleFacturaView>();

                    productos.ForEach(pro =>
                    {
                        detalleFacturaViews.Add(new DetalleFacturaView()
                        {
                            IdProducto = pro.IdProducto,
                            Cantidad = pro.Cantidad,
                            PrecioConIva = pro.PrecioConIva,

                        });

                    });

                    facturaPresentacionViews.Add(new FacturaPresentacionView()
                    {
                        Id = factura.Id,
                        Serie = factura.Serie,
                        NumeroFactura = factura.Numero,
                        Fecha = factura.Fecha,
                        NombreCliente = cliente.Nombre + " " + cliente.Apellido,
                        Nit = cliente.Nit,
                        NombreTienda = tienda.Nombre,
                        DireccionTienda = tienda.Direccion,
                        NombreEmpleado = empleado.Nombre,
                        Productos = detalleFacturaViews,
                        Monto = monto,

                    });

                });

                return StatusCode((int)HttpStatusCode.OK, facturaPresentacionViews);
            }

            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }
        }

        [HttpPost]
        public IActionResult CreateFactura([FromBody] FacturaView facturaView)
        {
            try
            {
                //    DetalleFacturaView detalleFacturaView = new DetalleFacturaView();
                //    var producto = context.Productos.Find(detalleFacturaView.IdProducto);
                //    var cantidadProductoInventario = context.Inventarios.Where(inventario => inventario.IdProducto == producto.Id).Select(canti => canti.Cantidad);
                //    var cantidadProductosAFacturar = facturaView.facturaDetalles.Where(producto => producto.IdProducto == ;



                if (facturaView.facturaDetalles.Count > 0)
                {

                    Factura factura = new Factura()
                    {
                        Serie = facturaView.Serie,
                        Numero = facturaView.Numero,
                        Fecha = facturaView.Fecha,
                        IdCliente = facturaView.IdCliente,
                        IdTienda = facturaView.IdTienda,
                        IdEmpleado = facturaView.IdEmpleado,
                        Descuento = facturaView.Descuento,
                        IdImpuesto = 1,
                        Estado = 1
                    };


                    context.Facturas.Add(factura);
                    context.SaveChanges();

                    facturaView.Id = factura.Id;

                    decimal monto = 0;




                    facturaView.facturaDetalles.ForEach(producto =>
                    {


                        var precio = context.Productos.Where(pd => pd.Id == producto.IdProducto).Select(precio => precio.PrecioConIva).FirstOrDefault();



                        DetalleFactura detalleFactura = new DetalleFactura()
                        {
                            IdFactura = factura.Id,
                            IdProducto = producto.IdProducto,
                            PrecioConIva = precio,
                            Cantidad = producto.Cantidad
                        };

                        context.DetalleFacturas.Add(detalleFactura);
                        context.SaveChanges();

                        var inventario = context.Inventarios.Where(inventar => inventar.IdProducto == producto.IdProducto).FirstOrDefault();
                        inventario.Cantidad = inventario.Cantidad - producto.Cantidad;
                        context.Inventarios.Update(inventario);

                        monto += Convert.ToDecimal(precio) * producto.Cantidad;
                    });

                    FacturaPago facturaPago = new FacturaPago()
                    {
                        IdFactura = factura.Id,
                        IdMetodoPago = facturaView.IdMetodoPagos,
                        Monto = monto,
                        Referencia = "23",

                    };

                    context.FacturaPagos.Add(facturaPago);
                    context.SaveChanges();


                    return StatusCode((int)HttpStatusCode.Created, facturaView);

                }


                return StatusCode((int)HttpStatusCode.Created, facturaView);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { mensaje = ex.Message });

            }

        }


    }
}
