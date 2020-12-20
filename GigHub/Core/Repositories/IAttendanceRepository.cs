using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int gigId, string userId);

        void Add(Attendance attendance);

        void Remove(Attendance attendance);

        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}
