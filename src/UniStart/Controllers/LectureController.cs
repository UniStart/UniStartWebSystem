namespace UniStart.Controllers
{
    using System.Linq;
    using Unistart.Models;
    using UniStart.Data.Repositories;
    using System.Web.Http;
    using UniStart.Common;
    
    // TODO: Add ModelState is valid    
    [JsonConfiguration]
    public class LectureController : ApiController
    {
        private readonly IRepository<Lecture> lecturesRepository;
        
        public LectureController(IRepository<Lecture> lectureRepository)
        {
            lecturesRepository = lectureRepository;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var allLectures = lecturesRepository.All()
                .Where(l => l.IsDeleted == false);

            return Ok(allLectures.ToList());
        }

        [HttpGet]
        public IHttpActionResult GetLectureById(int id)
        {
            Lecture lecture = lecturesRepository.All()
                .FirstOrDefault(a => a.Id == id);

            if (lecture == null)
            {
                return BadRequest(GlobalConstants.NonExistingLecture);
            }
            return Ok(lecture);
        }

        [HttpGet]
        public IHttpActionResult SearchLectureByName(string name)
        {
            var lectures = 
                lecturesRepository.All()
                .Where(
                    lec => lec.Title.ToLower().Contains(name) 
                    && lec.IsDeleted == false)
                    .ToList();

            if (lectures.Count == 0)
            {
                return BadRequest(GlobalConstants.NonExistingLecture);
            }

            return Ok(lectures);
        }

        [HttpPost]
        public IHttpActionResult CreateLecture(Lecture lecture)
        {
            if (lecture == null)
            {
                return BadRequest("Lecture can't be null.");
            }

            lecturesRepository.Add(lecture);
            lecturesRepository.SaveChanges();

            return Ok(lecture);
        }

        [HttpPost]
        public IHttpActionResult UpdateLecture(int id, Lecture updatedLecture)
        {
            if (id != updatedLecture.Id)
            {
                return BadRequest("Wrong lecture id.");
            }

            var oldLecture = lecturesRepository
                .All().FirstOrDefault(l => l.Id == id);

            // If we can't find such lecture, create a new one
            if (oldLecture == null)
            {
                lecturesRepository.Add(updatedLecture);
                lecturesRepository.SaveChanges();

                return Ok(updatedLecture);
            }

            MapLectureFields(oldLecture, updatedLecture);

            lecturesRepository.Update(oldLecture);
            lecturesRepository.SaveChanges();

            return Ok(updatedLecture);
        }

        [HttpPost]
        public IHttpActionResult DeleteLecture(int id)
        {
            var lecture = lecturesRepository.All()
                .FirstOrDefault(l => l.Id == id);
            
            if (lecture == null)
            {
                return BadRequest(GlobalConstants.NonExistingLecture);
            }

            lecture.IsDeleted = true;

            lecturesRepository.Update(lecture);
            lecturesRepository.SaveChanges();

            return Ok($"Lecture with Id = {id} is deleted");
        }
        
        private void MapLectureFields(Lecture oldLecture, Lecture updatedLecture)
        {
            oldLecture.Title = updatedLecture.Title;
            oldLecture.DatePublished = updatedLecture.DatePublished;
            oldLecture.IsDeleted = updatedLecture.IsDeleted;
            oldLecture.Presentation = updatedLecture.Presentation;
            oldLecture.TextFile = updatedLecture.TextFile;
            oldLecture.VideoUrl = updatedLecture.VideoUrl;
        }
    }
}
