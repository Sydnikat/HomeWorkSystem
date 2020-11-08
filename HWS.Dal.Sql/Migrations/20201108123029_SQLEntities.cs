using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWS.Dal.Sql.Migrations
{
    public partial class SQLEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MongoUsers",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MongoUsers", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Owner_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x._id);
                    table.ForeignKey(
                        name: "FK_Groups_MongoUsers_Owner_id",
                        column: x => x.Owner_id,
                        principalTable: "MongoUsers",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupStudentJoin",
                columns: table => new
                {
                    StudentId = table.Column<long>(nullable: false),
                    GroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStudentJoin", x => new { x.GroupId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_GroupStudentJoin_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupStudentJoin_MongoUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "MongoUsers",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTeacherJoin",
                columns: table => new
                {
                    TeacherId = table.Column<long>(nullable: false),
                    GroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTeacherJoin", x => new { x.GroupId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_GroupTeacherJoin_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeacherJoin_MongoUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "MongoUsers",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MaxFileSize = table.Column<int>(nullable: false),
                    GroupId = table.Column<long>(nullable: false),
                    SubmissionDeadline = table.Column<DateTime>(nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(nullable: false),
                    MaximumNumberOfStudents = table.Column<int>(nullable: false),
                    CurrentNumberOfStudents = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x._id);
                    table.ForeignKey(
                        name: "FK_Homeworks_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(nullable: false),
                    HomeworkId = table.Column<long>(nullable: false),
                    SubmissionDeadline = table.Column<DateTime>(nullable: false),
                    TurnInDate = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    Student = table.Column<Guid>(nullable: false),
                    ReservedBy = table.Column<Guid>(nullable: false),
                    Grade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x._id);
                    table.ForeignKey(
                        name: "FK_Assignment_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    GroupId = table.Column<long>(nullable: true),
                    HomeworkId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x._id);
                    table.ForeignKey(
                        name: "FK_Comments_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkGranderJoin",
                columns: table => new
                {
                    GraderId = table.Column<long>(nullable: false),
                    HomeworkId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkGranderJoin", x => new { x.HomeworkId, x.GraderId });
                    table.ForeignKey(
                        name: "FK_HomeworkGranderJoin_MongoUsers_GraderId",
                        column: x => x.GraderId,
                        principalTable: "MongoUsers",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkGranderJoin_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkStudentJoin",
                columns: table => new
                {
                    StudentId = table.Column<long>(nullable: false),
                    HomeworkId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkStudentJoin", x => new { x.HomeworkId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_HomeworkStudentJoin_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkStudentJoin_MongoUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "MongoUsers",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_HomeworkId",
                table: "Assignment",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GroupId",
                table: "Comments",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_HomeworkId",
                table: "Comments",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Owner_id",
                table: "Groups",
                column: "Owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudentJoin_StudentId",
                table: "GroupStudentJoin",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeacherJoin_TeacherId",
                table: "GroupTeacherJoin",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkGranderJoin_GraderId",
                table: "HomeworkGranderJoin",
                column: "GraderId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_GroupId",
                table: "Homeworks",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkStudentJoin_StudentId",
                table: "HomeworkStudentJoin",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "GroupStudentJoin");

            migrationBuilder.DropTable(
                name: "GroupTeacherJoin");

            migrationBuilder.DropTable(
                name: "HomeworkGranderJoin");

            migrationBuilder.DropTable(
                name: "HomeworkStudentJoin");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "MongoUsers");
        }
    }
}
