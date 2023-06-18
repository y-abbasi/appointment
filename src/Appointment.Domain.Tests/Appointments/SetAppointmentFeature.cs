using System.Collections.Immutable;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.Domain.Patiens;
using Appointment.Domain.Tests.Doctors;
using TestStack.BDDfy;

namespace Appointment.Domain.Tests.Appointments;

[Story(Title = "set appointment feature",
    AsA = "appointment manager",
    IWant = "to set appointment for patient")]
public class SetAppointmentFeature
{
    [Fact]
    public void Appointment_sets_properly()
    {
        new ShouldBeAbleToSetAppointmentProperly()
            .WithExamples(new ExampleTable("doctor speciality", "weekly schedule", "appointment time",
                    "appointment duration")
                {
                    {
                        DoctorSpeciality.GeneralPractitioner,
                        new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
                            .Add(new DailySchedule(1,
                                ImmutableArray<Range<TimeOnly>>.Empty
                                    .Add(new Range<TimeOnly>(new TimeOnly(10, 0), new TimeOnly(19, 0)))
                            ))),
                        DateTime.Parse("2023-12-10"),
                        TimeSpan.FromMinutes(10)
                    },
                }
            ).BDDfy<SetAppointmentFeature>();
    }

}