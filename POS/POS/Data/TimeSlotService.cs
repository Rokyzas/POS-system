using POS.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using static POS.Models.Booking;

namespace POS.Data
{
    public class TimeSlotService
    {
        private readonly AppDbContext _context;
        public TimeSlotService(AppDbContext context)
        {
            _context = context;
        }
        public List<TimeSlot> FilterByStaffAndDay(DbSet<TimeSlot> list, DateTime time, int staffID)
        {
            DateTime dayStart = time.Date; // Start of the specific day (00:00:00)
            DateTime dayEnd = dayStart.AddDays(1).AddTicks(-1); // End of the specific day (23:59:59)

            var timeslots = list
                .Where(t =>
                    t.StaffID == staffID &&
                    t.StartDate >= dayStart &&
                    t.EndDate <= dayEnd
                )
                .OrderBy(t => t.StartDate)
                .ToList();
            return timeslots;
        }

        public List<DateTime> FindAvailableSlots(List<TimeSlot> list, DateTime time)
        {
            DateTime dayStart = time.Date; // Start of the specific day (00:00:00)
            DateTime dayEnd = dayStart.AddDays(1).AddTicks(-1); // End of the specific day (23:59:59)

            var intervals = new List<DateTime>();
            DateTime previousEndTime = dayStart;

            foreach (var timeslot in list)
            {
                var booking = _context.booking
                    .Where(t =>
                    t.TimeSlotID == timeslot.id
                    )
                    .ToList();
                if (timeslot.StartDate > previousEndTime)
                {
                    if (booking.Count()!=0 && booking.ElementAt(0).Status != BookingStatus.Cancelled)
                    {
                        intervals.Add(previousEndTime);
                        intervals.Add(timeslot.StartDate);
                    }
                }
                previousEndTime = timeslot.EndDate;
            }

            // Handle the interval after the last timeslot until the end of the day
            if (previousEndTime < dayEnd)
            {
                intervals.Add(previousEndTime);
                intervals.Add(dayEnd);
            }

            return intervals;
        }

        public bool CheckIfWithinTimeInterval(TimeSlot timeSlot, List<DateTime> interval) {
            var check = false;
            for (int i = 0; i < interval.Count(); i+=2)
            {
                if(timeSlot.StartDate >= interval.ElementAt(i))
                {
                    if (timeSlot.EndDate <= interval.ElementAt(i + 1)) { 
                        check = true; break;
                    }
                }
            }
            return check;
        }

    }
}
