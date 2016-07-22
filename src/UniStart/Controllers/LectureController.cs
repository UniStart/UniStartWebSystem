namespace UniStart.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Unistart.Models;
    using UniStart.Data.Repositories;
    using System.Web.Http;

    // TODO: Make the endpoints async
    public class LectureController : ApiController
    {
        private readonly IRepository<Lecture> lecturesRepository;
        
        public LectureController(IRepository<Lecture> lectureRepository)
        {
            lecturesRepository = lectureRepository;
        }

        [HttpGet]
        public IEnumerable<Lecture> GetAll()
        {
            var allLectures = lecturesRepository.All();

            return allLectures;
        }


        [HttpGet]
        public Lecture GetGameById(int id)
        {
            Lecture lecture = lecturesRepository.All()
                .First(a => a.Id == id);

            return lecture;
        }

        [HttpGet]
        public IHttpActionResult SearchLectureByName(string name)
        {
            var lectures = lecturesRepository.All()
                .Where(lec => lec.Title == name).ToList();

            if (lectures.Count == 0)
            {
                return BadRequest("Lectures not found");
            }

            return Ok(lectures);
        }

        [HttpPost]
        public void CreateLecture(Lecture lecture)
        {
            lecturesRepository.Add(lecture);
            lecturesRepository.SaveChanges();
        }

        [HttpPost]
        public IHttpActionResult UpdateLecture(Lecture updatedLecture)
        {
            int id = updatedLecture.Id;
            var oldLecture = lecturesRepository.All().First(l => l.Id == id);

            if (oldLecture == null)
            {
                return BadRequest("Such lecture doesn't exist");
            }

            oldLecture = updatedLecture;
            lecturesRepository.SaveChanges();

            return Ok(updatedLecture);
        }

        [HttpPost]
        public IHttpActionResult DeleteLectureById(int id)
        {
            var lecture = lecturesRepository.All().First(l => l.Id == id);
            
            if (lecture == null)
            {
                return BadRequest("Such lecture doesn't exist");
            }

            lecture.isDeleted = true;

            return Ok($"Lecture with {id} is deleted");
        }
    }
}