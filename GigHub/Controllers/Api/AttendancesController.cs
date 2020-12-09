using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exist = _context.Attendance.Any(a => a.AttendeeId == userId
            && a.GigId == dto.GigId);

            if (exist)
                return BadRequest("Attendance already exists");

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendance.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _context.Attendance.SingleOrDefault(a => a.AttendeeId == userId
            && a.GigId == id);

            if (attendance == null)
                return NotFound();

            _context.Attendance.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
