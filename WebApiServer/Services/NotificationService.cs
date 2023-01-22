using System.Collections.Generic;
using Deadlindar.Repositories;
using ValueObjects;

namespace WebAPI.Server.Services
{
    public class NotificationService: INotificationService
    {
        private INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository repository)
        {
            _notificationRepository = repository;
        }

        public IEnumerable<Notification> GetByLogin(string login)
        {
            return _notificationRepository.GetByLogin(login);
        }

        public void Add(string login, Notification notification)
        {
            _notificationRepository.Add(login, notification);
        }

        public bool Read(string login, Notification notification)
        {
            return _notificationRepository.Read(login, notification);
        }

        public bool Delete(string login, Notification notification)
        {
            return _notificationRepository.Delete(login, notification);
        }
    }
}