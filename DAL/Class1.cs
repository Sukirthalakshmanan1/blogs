using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdminInfo
    {
        [Key]
        [Required()]
        public string EmailId { get; set; }
        [Required()]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters only")]
        public string pass { get; set; }

    }
        public class EmpInfo
    {
        [Key]
        [Required()]
        public string EmailId { get;set; }
        [MaxLength(20, ErrorMessage = "Not allowed above 20 chars")]
        [MinLength(2, ErrorMessage = "Not allowed below 2 chars")]
        public string Name { get; set; }
        
        

        public DateTime DateOfJoining { get; set; }
        public int PassCode { get; set; }

        //1 Book---M Enrollments(members)
        public virtual ICollection<BlogInfo> I { get; set; }
        //  [DataType(DataType.DateTime,ErrorMessage ="not valid date")]


    }

    public class BlogInfo
    {
        [Key]
        [Required]
        public int BlogId { get; set; } 
        public string Title { get; set; }

        public string subject { get; set; }

        public DateTime DateOfCreation { get; set; }
        [Required()]
        public string BlogUrl { get; set; }
        [EmailAddress]
        public string EmpEmailId { get; set; }



        //1 Book---M Enrollments(members)
        public virtual ICollection<EmpInfo> e { get; set; }
        //  [DataType(DataType.DateTime,ErrorMessage ="not valid date")]


    }



    public class EmpDBInitializer : DropCreateDatabaseIfModelChanges<MyContext1>
    {
        protected override void Seed(MyContext1 context)
        {
            IList<AdminInfo> defaultStandards = new List<AdminInfo>();

            defaultStandards.Add(new AdminInfo() { EmailId="suki123@gmail.com",pass="suki123" });


            context.AdminTable.AddRange(defaultStandards);

            base.Seed(context);
        }
    }

    public class MyContext1 : DbContext
    {
        public MyContext1() : base("MyContext1")
        {
            //createdatabase if not exists
            //drop create always
            //drop create if model changes

            Database.SetInitializer<MyContext1>(new DropCreateDatabaseIfModelChanges<MyContext1>());
            Database.SetInitializer(new EmpDBInitializer());
        }

        public virtual DbSet<EmpInfo> EmpTable { get; set; }

        public virtual DbSet<AdminInfo> AdminTable { get; set; }

        public virtual DbSet<BlogInfo> BlogTable { get; set; }


    }
}
