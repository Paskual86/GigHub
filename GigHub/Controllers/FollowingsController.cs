using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;


namespace GigHub.Controllers
{

    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gigId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IHttpActionResult Attend(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(a => a.FollowerId == userId && a.FolloweeId == dto.FolloweeId))
            {
                return BadRequest("Following already exists");
            }

            var following = new Following()
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}
