using Microsoft.EntityFrameworkCore;
using StudentCrud.Middleware;
using StudentCrud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCrudTest
{
    [TestClass]
    public class StudentsTest
    {
        [TestMethod]
        public void generateGuidTest()
        {
            StudentsManager studentsManager = new StudentsManager();
            string newId = studentsManager.generateGuid();

            Assert.IsNotNull(newId);
        }

        [DataTestMethod]
        [DataRow("001")]
        [DataRow("002")]
        public void studentExistsTest(string id) 
        {
            // Create instances of students
            var student1 = new Student { Id = "001", Name = "a", Age = 20 };
            var student2 = new Student { Id = "002", Name = "b", Age = 21 };
            var student3 = new Student { Id = "003", Name = "c", Age = 22 };

            // Create an instance of CollegeContext
            CollegeContext context = new CollegeContext { };
            
            // Add students to the Students DbSet
            context.Students.Add(student1);
            context.Students.Add(student2);
            context.Students.Add(student3);
            
            StudentsManager studentsManager = new StudentsManager();
            bool exists = studentsManager.StudentExists(id, context);

            Assert.IsTrue(exists); 
        }
    }
}
