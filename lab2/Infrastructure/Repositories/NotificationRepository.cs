using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfModels;
using EfModels.Models;
using lab2.Domain.Contracts;
using lab2.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace lab2.Infrastructure.Repositories
{
    public class NotificationRepository: INotificationRepository
    {
        private MyDbContext _db;
        private IMapper _mapper;
        public NotificationRepository(MyDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }
        public async Task<List<NotificacionDTO>> GetAllNotifications(bool isActive = true)
        {
            List<Notificacion> notificacions = _db.Notificacions.Where(x=>x.IsActive == isActive).ToList();
            return _mapper.Map<List<Notificacion>,List<NotificacionDTO>>(notificacions);
        }

        public async Task<bool> CloseNotificacion(int IdNotification)
        {
            Notificacion notificacion = _db.Notificacions.FirstOrDefault(x=>x.Id == IdNotification);
            notificacion.IsActive = false;
            _db.Notificacions.Attach(notificacion);
            _db.Entry(notificacion).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<NotificacionDTO> CreateNewNotification(Notificacion notification)
        {
            await _db.AddAsync(notification);
            await _db.SaveChangesAsync();
            return _mapper.Map<NotificacionDTO>(notification);
        }
    }
}