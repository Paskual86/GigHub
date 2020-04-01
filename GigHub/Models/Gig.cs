using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    /// <summary>
    /// Gig Class
    /// </summary>
    public class Gig
    {
        public int Id { get; set; }
        
        public ApplicationUser Artist { get; set; }
        [Required]
        public string ArtistId { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }

        public bool IsCanceled { get; private set; }
        public ICollection<Attendance> Attendances { get; internal set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dateTime"></param>
        /// <param name="genre"></param>
        /// <param name="venue"></param>
        public Gig(string user, DateTime dateTime, byte genre, string venue)
        {
            this.ArtistId = user;
            DateTime = dateTime;
            this.GenreId = genre;
            Venue = venue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dateTime"></param>
        /// <param name="genre"></param>
        /// <param name="venue"></param>
        /// <returns></returns>
        public static Gig New(string user, DateTime dateTime, byte genre, string venue)
        {
            return new Gig(user, dateTime, genre, venue);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Cancel()
        {
            IsCanceled = true;
            var notification = Notification.GigCanceled(this);
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newVenue"></param>
        /// <param name="newDateTime"></param>
        /// <param name="newGenre"></param>
        public void Modify(string newVenue, DateTime newDateTime, byte newGenre)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            Venue = newVenue;
            DateTime = newDateTime;
            GenreId = newGenre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);

            }
        }
    }
}