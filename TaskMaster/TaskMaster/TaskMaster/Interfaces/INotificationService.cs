using System;

namespace TaskMaster.Interfaces
{
    public interface INotificationService
    {
        void LoadNotifications(string name, string textdesc, int id, DateTime whenToStart);
        long NotifyTimeInMilliseconds(DateTime notifyTime);
        void CancelNotification(int id);
    }
}
