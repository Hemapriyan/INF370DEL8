﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpartanFireWebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SpartanFireDBEntities1 : DbContext
    {
        public SpartanFireDBEntities1()
            : base("name=SpartanFireDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AssignDelivery> AssignDeliveries { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<AuditLogType> AuditLogTypes { get; set; }
        public virtual DbSet<BankingDetail> BankingDetails { get; set; }
        public virtual DbSet<CheckEquipment> CheckEquipments { get; set; }
        public virtual DbSet<CheckEquipmentLine> CheckEquipmentLines { get; set; }
        public virtual DbSet<CheckEquipmentStatu> CheckEquipmentStatus { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientFeedback> ClientFeedbacks { get; set; }
        public virtual DbSet<CompanyInfo> CompanyInfoes { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<DeliveryLine> DeliveryLines { get; set; }
        public virtual DbSet<DeliveryStatu> DeliveryStatus { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeRequest> EmployeeRequests { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Functionality> Functionalities { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationType> LocationTypes { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
        public virtual DbSet<ProductOrderLine> ProductOrderLines { get; set; }
        public virtual DbSet<ProductOrderReturn> ProductOrderReturns { get; set; }
        public virtual DbSet<ProductOrderReturnLine> ProductOrderReturnLines { get; set; }
        public virtual DbSet<ProductOrderStatu> ProductOrderStatus { get; set; }
        public virtual DbSet<ProductStatu> ProductStatus { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceItem> ServiceItems { get; set; }
        public virtual DbSet<ServiceItemSize> ServiceItemSizes { get; set; }
        public virtual DbSet<ServiceItemType> ServiceItemTypes { get; set; }
        public virtual DbSet<ServiceLine> ServiceLines { get; set; }
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }
        public virtual DbSet<ServiceRequestStatu> ServiceRequestStatus { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierOrder> SupplierOrders { get; set; }
        public virtual DbSet<SupplierOrderLine> SupplierOrderLines { get; set; }
        public virtual DbSet<SupplierOrderReturn> SupplierOrderReturns { get; set; }
        public virtual DbSet<SupplierOrderReturnLine> SupplierOrderReturnLines { get; set; }
        public virtual DbSet<SupplierStatu> SupplierStatus { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<VAT> VATs { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<WorkDone> WorkDones { get; set; }
        public virtual DbSet<WriteOfProduct> WriteOfProducts { get; set; }
    }
}
