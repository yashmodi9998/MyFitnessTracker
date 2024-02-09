namespace MyFitnessTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersphonenumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserDatas", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDatas", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
