using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gigId"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto) 
        {
            var userId = User.Identity.GetUserId();
            
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId)) 
            {
                return BadRequest("The attendance already exists");
            }

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();        
        }

        [HttpDelete]
        public IHttpActionResult CancelAttend(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _context.Attendances.Single(g => g.AttendeeId == userId && g.GigId == id);
            
            if (attendance == null) return NotFound();
            
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(id); // Devolver ok junto con el id removido, esto es una convesion.
        }
    }
}
