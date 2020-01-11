namespace YiFlag.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class YiFlagContext : DbContext
    {
        public YiFlagContext()
            : base("name=YiFlagContext")
        {
        }

        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<SysAdmin> SysAdmin { get; set; }
        public virtual DbSet<SysAdminRoles> SysAdminRoles { get; set; }
        public virtual DbSet<SysMenue> SysMenue { get; set; }
        public virtual DbSet<SysMenueFunction> SysMenueFunction { get; set; }
        public virtual DbSet<SysRoles> SysRoles { get; set; }
        public virtual DbSet<SysRolesMenueFuction> SysRolesMenueFuction { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysAdminMenue> SysAdminMenue { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysAdmin>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<SysAdmin>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<SysAdmin>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenue>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenue>()
                .Property(e => e.Icon)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenue>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenue>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenue>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<SysRoles>()
                .Property(e => e.RolesName)
                .IsUnicode(false);

            modelBuilder.Entity<SysRoles>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Pwd)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Tel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SysAdminMenue>()
                .Property(e => e.Remark)
                .IsUnicode(false);
        }
    }
}
