namespace QuestionApp.Migrations
{
    using System.Data.Entity.Migrations;
    public partial class _1122asd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    StudentID = c.Int(nullable: false),
                    QuestionID = c.Int(nullable: false),
                    LibraryID = c.Int(nullable: false),
                    Answer = c.String(),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.Classes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Students = c.String(),
                    QuizLib = c.Int(nullable: false),
                    CratedDate = c.String(),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.LogErrors",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Action = c.String(),
                    Date = c.String(),
                    ErrorCode = c.String(),
                    ErrorStatus = c.String(),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.Logs",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Action = c.String(),
                    Date = c.String(),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.Questions",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Questions = c.String(),
                    Answers = c.String(),
                    Explanation = c.String(),
                    Type = c.String(),
                    CreatedDate = c.String(),
                    answers1 = c.String(),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.QuizLibraries",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Questions = c.String(),
                    MailSend = c.Boolean(nullable: false),
                    StartDate = c.String(),
                    EndDate = c.String(),
                    CreatedDate = c.String(),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.Settings",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Host = c.String(),
                    Port = c.Int(nullable: false),
                    User_Name = c.String(),
                    User_password = c.String(),
                    site_adress = c.String(),
                    Layout = c.String(),
                    Logo_Background = c.String(),
                    Navbar_Background = c.String(),
                    Sidebar_Background = c.String(),
                    Logo = c.String(),
                    First = c.Int(nullable: false),
                    Seccond = c.Int(nullable: false),
                    Preloader = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.Students",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FirsName = c.String(),
                    LastName = c.String(),
                    NickName = c.String(),
                    Password = c.String(),
                    Mail = c.String(),
                    PhoneNumber = c.String(),
                    Clasess = c.String(),
                    TotalPoint = c.String(),
                    CreatedDate = c.String(),
                    Verified = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.Users",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    LastName = c.String(),
                    NickName = c.String(),
                    Password = c.String(),
                    Email = c.String(),
                    AuthLevel = c.String(),
                    PhoneNumber = c.String(),
                    CreatedDate = c.String(),
                    First = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);
        }
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Students");
            DropTable("dbo.Settings");
            DropTable("dbo.QuizLibraries");
            DropTable("dbo.Questions");
            DropTable("dbo.Logs");
            DropTable("dbo.LogErrors");
            DropTable("dbo.Classes");
            DropTable("dbo.Answers");
        }
    }
}
