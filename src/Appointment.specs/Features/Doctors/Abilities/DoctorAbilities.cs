using Appointment.specs.Features.Doctors.Models;
using Suzianna.Core.Screenplay;
using Suzianna.Rest.Screenplay.Interactions;

namespace Appointment.specs.Features.Doctors.Abilities;

public class DoctorAbilities : IDoctorAbilities
{
    public IPerformable DefineDoctor(DefineDoctor command)
    {
        return Post.DataAsJson(command).To($"api/doctors");
    }
}