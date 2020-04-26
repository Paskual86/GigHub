using AutoMapper;
using GigHub.App_Start;
using GigHub.Core.Dtos;
using GigHub.Persistance;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _context = new ApplicationDbContext();
            _unitOfWork = unitOfWork;
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = configuration.CreateMapper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NotificationDto> GetNewNotifications() 
        {
            var notifications = _unitOfWork.Notifications.GetNewNotifications(User.Identity.GetUserId());
            return notifications.Select(n => _mapper.Map<NotificationDto>(n));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult MarkAsRead() 
        {
            _unitOfWork.Notifications.MarkAsRead(User.Identity.GetUserId());
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
