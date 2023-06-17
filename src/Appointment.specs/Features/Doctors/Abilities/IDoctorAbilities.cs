using Appointment.specs.Features.Doctors.Models;
using Suzianna.Core.Screenplay;

namespace Appointment.specs.Features.Doctors.Abilities;

public interface IDoctorAbilities
{
    IPerformable DefineDoctor(DefineDoctor build);
}