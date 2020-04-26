using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistance;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            
            if (_unitOfWork.Attendances.GetAttendance(dto.GigId, userId).Any()) 
            {
                return BadRequest("The attendance already exists");
            }

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendances.Create(attendance);
            _unitOfWork.Complete();
            return Ok();        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult CancelAttend(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(id, userId).FirstOrDefault();
            
            if (attendance == null) return NotFound();

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id); // Devolver ok junto con el id removido, esto es una convesion.
        }
    }
}
