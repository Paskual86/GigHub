using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gig"></param>
        /// <param name="type"></param>
        private Notification(Gig gig, NotificationType type)
        {
            Gig = gig ?? throw new ArgumentNullException("Gig");
            Type = type;
            DateTime = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gig"></param>
        /// <param name="type"></param>
        /// <param name="originalDateTime"></param>
        /// <param name="originalVenue"></param>
        private Notification(Gig gig, NotificationType type, DateTime originalDateTime, string originalVenue):this(gig, type)
        {
            OriginalDateTime = originalDateTime;
            OriginalVenue = originalVenue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gig"></param>
        /// <returns></returns>
        public static Notification GigCreated(Gig gig) 
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gig"></param>
        /// <param name="originalDateTime"></param>
        /// <param name="originalVenue"></param>
        /// <returns></returns>
        public static Notification GigUpdated(Gig gig, DateTime originalDateTime, string originalVenue)
        {
            return new Notification(gig, NotificationType.GigUpdated, originalDateTime, originalVenue);
        }

        /// <summary>
        /// Cancel
        /// </summary>
        /// <param name="gig"></param>
        /// <param name="originalDateTime"></param>
        /// <param name="originalVenue"></param>
        /// <returns></returns>
        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }
    }
}