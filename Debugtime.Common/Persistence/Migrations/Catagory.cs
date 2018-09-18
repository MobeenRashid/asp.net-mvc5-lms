using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Persistence.Migrations
{
    public class Catagory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catagory",
                c => new
                {
                    CatagoryId = c.String(nullable: false, maxLength: 128),
                    CatagoryTitle = c.String(nullable: false),
                    Slag = c.String(nullable: false),
                })
                .PrimaryKey(t => t.CatagoryId);

            AddColumn("dbo.Courses", "CatagoryId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Courses", "CatagoryId");
            AddForeignKey("dbo.Courses", "CatagoryId", "dbo.Catagory", "CatagoryId");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CatagoryId", "dbo.Catagory");
            DropIndex("dbo.Courses", new[] { "CatagoryId" });
            DropColumn("dbo.Courses", "CatagoryId");
            DropTable("dbo.Catagory");
        }
    }
}
