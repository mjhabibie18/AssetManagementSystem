using AssetManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
        {

        }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<ConditionItem> ConditionItems { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Account-Employee
            //One to One
            modelBuilder.Entity<Account>()
                .HasOne(Account => Account.Employee)
                .WithOne(Employee => Employee.Account)
                .HasForeignKey<Account>(Account => Account.Id);

            //Employee-Role
            //One to Many
            modelBuilder.Entity<Employee>()
                .HasOne(Employee => Employee.Role)
                .WithMany(Role => Role.Employees);

            //Department -Employee
            //One to Many
            modelBuilder.Entity<Employee>()
                .HasOne(Employee => Employee.Department)
                .WithMany(Department => Department.Employees);

            //Empolyee-Transaction
            //one to Many
            modelBuilder.Entity<Transaction>()
                .HasOne(Transaction => Transaction.Employee)
                .WithMany(Employee => Employee.Transactions);

            //Transaction -TransactionItem
            //one to Many
            modelBuilder.Entity<TransactionItem>()
                .HasOne(TransactionItem => TransactionItem.Transaction)
                .WithMany(Transaction => Transaction.TransactionItems);

            //transactionItem-item
            //one to many
            modelBuilder.Entity<TransactionItem>()
                .HasOne(TransactionItem => TransactionItem.Item)
                .WithMany(Item => Item.TransactionItems);

            //category-item
            //one to many
            modelBuilder.Entity<Item>()
                .HasOne(Item => Item.Category)
                .WithMany(Category => Category.Items);

            //Item-ConditionItem
            //one to many
            modelBuilder.Entity<ConditionItem>()
                .HasOne(ConditionItem => ConditionItem.Item)
                .WithMany(Item => Item.ConditionItems);

            //condition-conditionitem
            //one to many
            modelBuilder.Entity<ConditionItem>()
                .HasOne(ConditionItem => ConditionItem.Condition)
                .WithMany(Condition => Condition.ConditionItems);

            modelBuilder.Entity<Employee>()
                .HasIndex(Employee => Employee.Email)
                .IsUnique();
        }
    }
}
