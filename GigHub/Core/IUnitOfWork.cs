using GigHub.Core.Repositories;

namespace GigHub.Persistance
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowRepository Follow { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        INotificationRepository Notifications { get; }

        void Complete();
    }
}