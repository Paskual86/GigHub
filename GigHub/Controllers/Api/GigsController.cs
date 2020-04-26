using GigHub.Persistance;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Cancel(int id) 
        {
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null || gig.IsCanceled) return NotFound();

            if (gig.ArtistId != userId) return BadRequest();

            gig.Cancel();
            
            _unitOfWork.Complete();

            return Ok();
        }
        
    }
}