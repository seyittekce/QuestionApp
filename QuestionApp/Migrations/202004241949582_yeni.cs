namespace QuestionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yeni : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideAndTexts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(),
                        Yazilar = c.String(),
                        dateTime = c.DateTime(nullable: false),
                        Yazar = c.String(),
                        Link = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Answers", "Pass", c => c.Boolean(nullable: false));
            AddColumn("dbo.Answers", "Status", c => c.String());
            AddColumn("dbo.Answers", "TotalPoint", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "Type", c => c.String());
            AddColumn("dbo.Answers", "ClassID", c => c.Int(nullable: false));
            AddColumn("dbo.Classes", "MailSend", c => c.Boolean(nullable: false));
            AddColumn("dbo.Classes", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Classes", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuizLibraries", "Comment", c => c.String());
            AlterColumn("dbo.Classes", "QuizLib", c => c.String());
            AlterColumn("dbo.Students", "FirsName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false));
            DropColumn("dbo.QuizLibraries", "MailSend");
            DropColumn("dbo.QuizLibraries", "StartDate");
            DropColumn("dbo.QuizLibraries", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuizLibraries", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuizLibraries", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuizLibraries", "MailSend", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Students", "LastName", c => c.String());
            AlterColumn("dbo.Students", "FirsName", c => c.String());
            AlterColumn("dbo.Classes", "QuizLib", c => c.Int(nullable: false));
            DropColumn("dbo.QuizLibraries", "Comment");
            DropColumn("dbo.Classes", "EndDate");
            DropColumn("dbo.Classes", "StartDate");
            DropColumn("dbo.Classes", "MailSend");
            DropColumn("dbo.Answers", "ClassID");
            DropColumn("dbo.Answers", "Type");
            DropColumn("dbo.Answers", "TotalPoint");
            DropColumn("dbo.Answers", "Status");
            DropColumn("dbo.Answers", "Pass");
            DropTable("dbo.VideAndTexts");
        }
    }
}
