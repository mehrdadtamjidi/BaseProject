using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace Data.Migrations
{
    public partial class SampleInsertToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder sb = new("INSERT INTO [dbo].[Roles]([Name],[Title],[IsDelete] ,[CreateDate],[LastUpdateDate])");
            sb.Append(" VALUES('Admin', 'Admin', 0, getDate(), getDate())  ,  ");
            sb.Append(" ('User', 'User', 0, getDate(), getDate())    ");
            migrationBuilder.Sql(sb.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            StringBuilder sb = new("Delete from Roles where Name in ('Admin','User')");
            migrationBuilder.Sql(sb.ToString());
        }
    }
}
