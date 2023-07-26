using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<TicketDetail> DetailTicket { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Homelet> Homelet { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<PhoneImage> PhoneImage { get; set; }
        public DbSet<Promote> Promote { get; set; }
        public DbSet<PromoteDetail> PromoteDetail { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<WarehouseTicket> WarehouseTicket { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach(var entityType in builder.Model.GetEntityTypes())
            {
                string tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    tableName = tableName.Substring(6);
                    entityType.SetTableName(tableName);
                }
            }
            builder.Entity<Address>(opt => 
            {
                opt.HasMany(ad => ad.Orders)
                .WithOne(o => o.Address)
                .HasForeignKey(o => o.AddressId)
                .HasConstraintName("FK_Address_Order");
            });
            builder.Entity<AppUser>(opt =>
            {
                opt.HasMany(u => u.Addresses).WithOne(a => a.User)
                 .HasForeignKey(a => a.UserId)
                 .HasConstraintName("FK_User_Address");
                opt.HasMany(u => u.Carts).WithOne(a => a.User)
                 .HasForeignKey(a => a.UserId)
                 .HasConstraintName("FK_User_Cart");
                opt.HasMany(u => u.WarehouseTicket).WithOne(a => a.User)
                 .HasForeignKey(a => a.UserId)
                 .HasConstraintName("FK_User_WarehouseTicket");
                opt.HasIndex(u => u.Email).IsUnique();
                opt.HasIndex(u => u.PhoneNumber).IsUnique();
            });
            builder.Entity<WarehouseTicket>(opt =>
            {
                opt.HasMany(wh => wh.TicketDetails).WithOne(ticket => ticket.WarehouseTicket)
                    .HasForeignKey(ticket => ticket.WarehouseTicketId)
                    .HasConstraintName("FK_WarehouseTicket_TicketDetail");
            });
            builder.Entity<Supplier>(opt =>
            {
                opt.HasMany(sup => sup.WarehouseTickets).WithOne(wh => wh.Supplier)
                    .HasForeignKey(wh => wh.SupplierId)
                    .HasConstraintName("FK_Supplier_WarehouseTicket");
            });
            builder.Entity<Brand>(opt =>
            {
                opt.HasMany(brand => brand.Phones).WithOne(p => p.Brand)
                    .HasForeignKey(wh => wh.BrandId)
                    .HasConstraintName("FK_Brand_Phone");
                opt.HasIndex(b => b.Name).IsUnique();
            });
            builder.Entity<Phone>(opt =>
            {
                opt.HasMany(phone => phone.PromoteDetails).WithOne(p => p.Phone)
                    .HasForeignKey(p => p.PhoneId)
                    .HasConstraintName("FK_Phone_PromoteDetail");
                opt.HasMany(phone => phone.PhoneImages).WithOne(p => p.Phone)
                    .HasForeignKey(p => p.PhoneId)
                    .HasConstraintName("FK_Phone_PhoneImages");
                opt.HasMany(phone => phone.TicketDetails).WithOne(p => p.Phone)
                    .HasForeignKey(p => p.PhoneId)
                    .HasConstraintName("FK_Phone_TicketDetails");
                opt.HasMany(phone => phone.Carts).WithOne(p => p.Phone)
                    .HasForeignKey(p => p.PhoneId)
                    .HasConstraintName("FK_Phone_Carts");
                opt.HasMany(phone => phone.OrderDetails).WithOne(p => p.Phone)
                    .HasForeignKey(p => p.PhoneId)
                    .HasConstraintName("FK_Phone_OrderDetails");
                opt.HasCheckConstraint<Phone>("CK_Phone_Price_ImportSold", "ImportPrice <= SoldPrice");
                opt.HasCheckConstraint<Phone>("CK_Phone_Quantity", "Quantity >= 0");
            });
            builder.Entity<Promote>(opt =>
            {
                opt.HasMany(promote => promote.PromoteDetail).WithOne(p => p.Promote)
                    .HasForeignKey(p => p.PromoteId)
                    .HasConstraintName("FK_Promote_PromoteDetail");
            });
            builder.Entity<Order>(opt =>
            {
                opt.HasMany(order => order.OrderDetail).WithOne(od => od.Order)
                    .HasForeignKey(od => od.OrderId)
                    .HasConstraintName("FK_Order_OrderDetail");
                opt.HasMany(order => order.OrderStatus).WithOne(os => os.Order)
                    .HasForeignKey(os => os.OrderId)
                    .HasConstraintName("FK_Order_OrderStatus");
            });
            builder.Entity<Status>(opt =>
            {
                opt.HasMany(order => order.OrderStatus).WithOne(od => od.Status)
                    .HasForeignKey(od => od.StatusId)
                    .HasConstraintName("FK_Status_OrderStatus");
            });
            builder.Entity<Status>(opt =>
            {
                opt.HasMany(order => order.OrderStatus).WithOne(od => od.Status)
                    .HasForeignKey(od => od.StatusId)
                    .HasConstraintName("FK_Status_OrderStatus");
            });
            builder.Entity<Province>(opt =>
            {
                opt.HasMany(province => province.District).WithOne(d => d.Province)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Province_District");
            });
            builder.Entity<District>(opt =>
            {
                opt.HasMany(district => district.Homelet).WithOne(h => h.District)
                    .HasForeignKey(h => h.DistrictId)
                    .HasConstraintName("FK_District_Homelet");
            });
            builder.Entity<Homelet>(opt =>
            {
                opt.HasMany(homelet => homelet.Address).WithOne(a => a.Homelet)
                    .HasForeignKey(a => a.HomeletId)
                    .HasConstraintName("FK_Homelet_Address");
                opt.HasMany(homelet => homelet.Supplier).WithOne(sup => sup.Homelet)
                    .HasForeignKey(sup => sup.HomeletId)
                    .HasConstraintName("FK_Homelet_Supplier");
            });


        }
    }
}
