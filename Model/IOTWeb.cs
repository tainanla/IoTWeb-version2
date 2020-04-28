namespace OneNETDataReceiver.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
   
    public partial class IOTWeb : DbContext
    {
        public IOTWeb()
            : base("name=IOTWeb")
        {
        }
        
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<CompanyInfo> CompanyInfo { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<HistoryValue> HistoryValue { get; set; }
        public virtual DbSet<HistoryValueServerSide> HistoryValueServerSide { get; set; }
        public virtual DbSet<Laser> Laser { get; set; }
        public virtual DbSet<OneNetProduct> OneNetProduct { get; set; }
        public virtual DbSet<Renjianxin_ButtonClick> Renjianxin_ButtonClick { get; set; }
        public virtual DbSet<Renjianxin_HealthData> Renjianxin_HealthData { get; set; }
        public virtual DbSet<SMSDeviceInfo> SMSDeviceInfo { get; set; }
        public virtual DbSet<SysLog> SysLog { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Renjianxin_ButtonClick>()
                .Property(e => e.ButtonName)
                .IsFixedLength();

            modelBuilder.Entity<Renjianxin_HealthData>()
                .Property(e => e.UserID)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.IMEI)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.ObservedDevice)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.Area)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.Person1)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.Phone1)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.Person2)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.Phone2)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.Person3)
                .IsFixedLength();

            modelBuilder.Entity<SMSDeviceInfo>()
                .Property(e => e.Phone3)
                .IsFixedLength();
        }
    }
}
