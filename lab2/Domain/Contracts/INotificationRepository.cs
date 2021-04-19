using System.Collections.Generic;
using System.Threading.Tasks;
using EfModels.Models;
using lab2.Domain.DTOs;

namespace lab2.Domain.Contracts
{
    public interface INotificationRepository
    {
        public Task<List<NotificacionDTO>> GetAllNotifications(bool isActive = true);
        public Task<bool> CloseNotificacion(int IdNotification);
        public Task<NotificacionDTO> CreateNewNotification(Notificacion notification);
    }
}