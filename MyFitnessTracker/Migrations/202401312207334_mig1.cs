namespace MyFitnessTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainExercises", "SubExercise_SubExerciseID", c => c.Int());
            AddColumn("dbo.SubExercises", "MainExercise_ExerciseID", c => c.Int());
            AlterColumn("dbo.MainExercises", "ExerciseName", c => c.String());
            AlterColumn("dbo.SubExercises", "SubExerciseName", c => c.String());
            CreateIndex("dbo.MainExercises", "SubExercise_SubExerciseID");
            CreateIndex("dbo.SubExercises", "MainExercise_ExerciseID");
            AddForeignKey("dbo.MainExercises", "SubExercise_SubExerciseID", "dbo.SubExercises", "SubExerciseID");
            AddForeignKey("dbo.SubExercises", "MainExercise_ExerciseID", "dbo.MainExercises", "ExerciseID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubExercises", "MainExercise_ExerciseID", "dbo.MainExercises");
            DropForeignKey("dbo.MainExercises", "SubExercise_SubExerciseID", "dbo.SubExercises");
            DropIndex("dbo.SubExercises", new[] { "MainExercise_ExerciseID" });
            DropIndex("dbo.MainExercises", new[] { "SubExercise_SubExerciseID" });
            AlterColumn("dbo.SubExercises", "SubExerciseName", c => c.String(nullable: false));
            AlterColumn("dbo.MainExercises", "ExerciseName", c => c.String(nullable: false));
            DropColumn("dbo.SubExercises", "MainExercise_ExerciseID");
            DropColumn("dbo.MainExercises", "SubExercise_SubExerciseID");
        }
    }
}
