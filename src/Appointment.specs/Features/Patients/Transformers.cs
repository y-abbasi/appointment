using TechTalk.SpecFlow.Assist;

namespace Appointment.specs.Features.Patients;

[Binding]
public class Transformers
{
    [StepArgumentTransformation]
    public PatientModel ToDefine(Table table)
    {
        return table.CreateInstance<PatientModel>() with { Id = Guid.NewGuid().ToString() };
    }
}