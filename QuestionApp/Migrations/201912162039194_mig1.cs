namespace QuestionApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuizLibraries", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.QuizLibraries", "EndDate", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.QuizLibraries", "EndDate", c => c.String());
            AlterColumn("dbo.QuizLibraries", "StartDate", c => c.String());
        }
    }
}
