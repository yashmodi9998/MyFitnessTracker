namespace MyFitnessTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainExercises",
                c => new
                    {
                        ExerciseID = c.Int(nullable: false, identity: true),
                        ExerciseName = c.String(nullable: false),
                        Description = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.ExerciseID);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        WorkoutID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        WorkoutDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        ExerciseID = c.Int(nullable: false),
                        SubExerciseID = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Reps = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.WorkoutID)
                .ForeignKey("dbo.MainExercises", t => t.ExerciseID, cascadeDelete: true)
                .ForeignKey("dbo.SubExercises", t => t.SubExerciseID, cascadeDelete: true)
                .ForeignKey("dbo.UserDatas", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ExerciseID)
                .Index(t => t.SubExerciseID);
            
            CreateTable(
                "dbo.SubExercises",
                c => new
                    {
                        SubExerciseID = c.Int(nullable: false, identity: true),
                        ExerciseID = c.Int(nullable: false),
                        SubExerciseName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SubExerciseID)
                .ForeignKey("dbo.MainExercises", t => t.ExerciseID, cascadeDelete: false)
                .Index(t => t.ExerciseID);
            
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        JoinDate = c.DateTime(),
                        FitnessGoal = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Workouts", "UserID", "dbo.UserDatas");
            DropForeignKey("dbo.Workouts", "SubExerciseID", "dbo.SubExercises");
            DropForeignKey("dbo.SubExercises", "ExerciseID", "dbo.MainExercises");
            DropForeignKey("dbo.Workouts", "ExerciseID", "dbo.MainExercises");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SubExercises", new[] { "ExerciseID" });
            DropIndex("dbo.Workouts", new[] { "SubExerciseID" });
            DropIndex("dbo.Workouts", new[] { "ExerciseID" });
            DropIndex("dbo.Workouts", new[] { "UserID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserDatas");
            DropTable("dbo.SubExercises");
            DropTable("dbo.Workouts");
            DropTable("dbo.MainExercises");
        }
    }
}
