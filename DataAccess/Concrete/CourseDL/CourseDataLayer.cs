using DataAccess.Abstract.ICourseDL;
using Domain;
using Infrastructure;
using System.Linq;

namespace DataAccess.Concrete.CourseDL
{
    public class CourseDataLayer : ICourseDataLayer
    {
        private readonly BaseRepository<Course> _baseRepository;
        public CourseDataLayer(BaseRepository<Course> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public IQueryable<Course> FindAll()
        {
            return _baseRepository.FindAll();
        }
    }
}
