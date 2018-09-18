namespace Debugtime.Common.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactionStatementTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statements",
                c => new
                    {
                        TransactionId = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Transactions", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.TransactionId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BuyerId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                        Order = c.String(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.BuyerId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Statements", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Transactions", "BuyerId", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "CourseId" });
            DropIndex("dbo.Transactions", new[] { "BuyerId" });
            DropIndex("dbo.Statements", new[] { "TransactionId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Statements");
        }
    }
}
