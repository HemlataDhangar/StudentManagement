using Domain;
using System.Linq;

namespace DataAccess.Abstract.ICourseDL
{
    public interface ICourseDataLayer
    {
        IQueryable<Course> FindAll();
    }
}
