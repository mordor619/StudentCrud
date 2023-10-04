using Microsoft.EntityFrameworkCore;
using StudentCrud.Model;

namespace StudentCrud.Middleware
{
    public class StudentsManager
    {
        public StudentsManager() { }

        public string generateGuid() 
        {
            return Guid.NewGuid().ToString(); 
        }

        public bool StudentExists(string id, CollegeContext _context)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
