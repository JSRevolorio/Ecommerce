using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ecommerce_proyect.Models
{
    public partial class DbContexEcommerce : DbContext
    {
        public DbContexEcommerce()
        {
        }

        public DbContexEcommerce(DbContextOptions<DbContexEcommerce> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<EmpleadoRol> EmpleadoRols { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FacturaPago> FacturaPagos { get; set; }
        public virtual DbSet<Impuesto> Impuestos { get; set; }
        public virtual DbSet<Inventario> Inventarios { get; set; }
        public virtual DbSet<MetodoPago> MetodoPagos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<RecuperarContraseña> RecuperarContraseñas { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Tienda> Tiendas { get; set; }
        public virtual DbSet<TipoCliente> TipoClientes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        optionsBuilder.UseSqlServer("Server=LAPTOP-A3PDHI1E\\SQLEXPRESS; Database=EcommerceService; Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdTipoClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdTipoCliente)
                    .HasConstraintName("FK__Cliente__IdTipoC__5629CD9C");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__Compra__IdEmplea__7F2BE32F");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK__Compra__IdProvee__7E37BEF6");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK__DetalleCo__IdCom__02FC7413");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleCo__IdPro__03F0984C");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("FK__DetalleFa__IdFac__68487DD7");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleFa__IdPro__693CA210");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<EmpleadoRol>(entity =>
            {
                entity.HasOne(d => d.Empleados)
                    .WithMany(p => p.EmpleadoRols)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__EmpleadoR__IdEmp__08B54D69");

                entity.HasOne(d => d.Rols)
                    .WithMany(p => p.EmpleadoRols)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__EmpleadoR__IdRol__09A971A2");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Factura__IdClien__619B8048");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__Factura__IdEmple__6383C8BA");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("FK__Factura__IdImpue__6477ECF3");

                entity.HasOne(d => d.IdTiendaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdTienda)
                    .HasConstraintName("FK__Factura__IdTiend__628FA481");
            });

            modelBuilder.Entity<FacturaPago>(entity =>
            {
                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.FacturaPagos)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("FK__FacturaPa__IdFac__76969D2E");

                entity.HasOne(d => d.IdMetodoPagoNavigation)
                    .WithMany(p => p.FacturaPagos)
                    .HasForeignKey(d => d.IdMetodoPago)
                    .HasConstraintName("FK__FacturaPa__IdMet__75A278F5");
            });

            modelBuilder.Entity<Impuesto>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Inventari__IdPro__6EF57B66");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK__Inventari__IdPro__70DDC3D8");

                entity.HasOne(d => d.IdTiendaNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.IdTienda)
                    .HasConstraintName("FK__Inventari__IdTie__6FE99F9F");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.PorcentajeDescuento).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Categorias)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Producto__IdCate__4BAC3F29");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<RecuperarContraseña>(entity =>
            {
                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.RecuperarContraseñas)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Recuperar__IdCli__797309D9");
            });

            modelBuilder.Entity<Tienda>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasOne(d => d.IdEmpleadoRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdEmpleadoRol)
                    .HasConstraintName("FK__Usuario__IdEmple__0C85DE4D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
