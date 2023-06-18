using System.Collections.Immutable;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
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
        var weeklySchedule = new DailySchedule(1,
            ImmutableArray<Range<TimeOnly>>.Empty
                .Add(new Range<TimeOnly>(new TimeOnly(10, 0), new TimeOnly(12, 0)))
                .Add(new Range<TimeOnly>(new TimeOnly(14, 0), new TimeOnly(18, 0)))
        );
        new ShouldBeAbleToSetAppointmentProperly()
            .WithExamples(new ExampleTable("doctor speciality", "weekly schedule", "appointment time",
                    "appointment duration")
                {
                    {
                        DoctorSpeciality.GeneralPractitioner,
                        new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
                            .Add(weeklySchedule)),
                        DateTime.Parse("2023-12-18 11:00"),
                        TimeSpan.FromMinutes(10)
                    },
                    {
                        DoctorSpeciality.GeneralPractitioner,
                        new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
                            .Add(weeklySchedule)),
                        DateTime.Parse("2023-12-18 15:00"),
                        TimeSpan.FromMinutes(10)
                    },
                }
            ).BDDfy<SetAppointmentFeature>();
    }

    [Fact]
    public void The_appointment_time_must_be_within_the_working_hours_of_the_clinic()
    {
        new SetAppointmentProperlyShouldBeThrowException()
            .WithExamples(new ExampleTable("doctor speciality", "weekly schedule", "appointment time",
                    "appointment duration", "exception code")
                {
                    {
                        DoctorSpeciality.GeneralPractitioner,
                        new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
                            .Add(new DailySchedule(1,
                                ImmutableArray<Range<TimeOnly>>.Empty
                                    .Add(new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(19, 0)))
                            ))),
                        DateTime.Parse("2023-12-10 8:59"),
                        TimeSpan.FromMinutes(15),
                        AppointmentExceptionCodes.MustBeWithinWorkingHourOfClinic
                    },
                    {
                        DoctorSpeciality.Specialist,
                        new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
                            .Add(new DailySchedule(1,
                                ImmutableArray<Range<TimeOnly>>.Empty
                                    .Add(new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(19, 0)))
                            ))),
                        DateTime.Parse("2023-12-10 19:01"),
                        TimeSpan.FromMinutes(30),
                        AppointmentExceptionCodes.MustBeWithinWorkingHourOfClinic
                    },
                }
            ).BDDfy<SetAppointmentFeature>();
    }

    [Fact]
    public void The_appointment_duration_should_be_appropriate_to_the_type_of_doctors_speciality()
    {
        new SetAppointmentProperlyShouldBeThrowException()
            .WithExamples(new ExampleTable("doctor speciality", "weekly schedule", "appointment time",
                    "appointment duration", "exception code")
                {
                    {
                        DoctorSpeciality.GeneralPractitioner,
                        new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
                            .Add(new DailySchedule(1,
                                ImmutableArray<Range<TimeOnly>>.Empty
                                    .Add(new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(19, 0)))
                            ))),
                        DateTime.Parse("2023-12-10 9:00"),
                        TimeSpan.FromMinutes(16),
                        AppointmentExceptionCodes.MustBeAppropriateToTheDoctorSpeciality
                    },
                    {
                        DoctorSpeciality.GeneralPractitioner,
                        new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
                            .Add(new DailySchedule(1,
                                ImmutableArray<Range<TimeOnly>>.Empty
                                    .Add(new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(19, 0)))
                            ))),
                        DateTime.Parse("2023-12-10 19:00"),
                        TimeSpan.FromMinutes(4),
                        AppointmentExceptionCodes.MustBeAppropriateToTheDoctorSpeciality
                    },
                }
            ).BDDfy<SetAppointmentFeature>();
    }

    [Fact]
    public void The_appointment_should_be_during_doctors_presents()
    {
        var weeklySchedule = new WeeklySchedule(ImmutableArray<DailySchedule>.Empty
            .Add(new DailySchedule(1,
                ImmutableArray<Range<TimeOnly>>.Empty
                    .Add(new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(12, 0)))
                    .Add(new Range<TimeOnly>(new TimeOnly(14, 0), new TimeOnly(18, 0)))
            )));
        new SetAppointmentProperlyShouldBeThrowException()
            .WithExamples(new ExampleTable("doctor speciality", "weekly schedule", "appointment time",
                    "appointment duration", "exception code")
                {
                    {
                        DoctorSpeciality.GeneralPractitioner,
                        weeklySchedule,
                        DateTime.Parse("2023-12-10 9:00"),
                        TimeSpan.FromMinutes(15),
                        AppointmentExceptionCodes.MustBeADuringTheDoctorsPresents
                    },
                    {
                        DoctorSpeciality.Specialist,
                        weeklySchedule,
                        DateTime.Parse("2023-12-18 13:00"),
                        TimeSpan.FromMinutes(10),
                        AppointmentExceptionCodes.MustBeADuringTheDoctorsPresents
                    },
                }
            ).BDDfy<SetAppointmentFeature>();
    }
}