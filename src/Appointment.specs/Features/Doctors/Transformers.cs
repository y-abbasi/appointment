using Appointment.specs.Features.Doctors.Models;
using TechTalk.SpecFlow.Assist;

namespace Appointment.specs.Features.Doctors;

[Binding]
public class Transformers
{
    [StepArgumentTransformation]
    public DoctorModel DoctorModel(Table table)
    {
        return table.CreateInstance<DoctorModel>() with { Id = Guid.NewGuid() };
    }
    
}