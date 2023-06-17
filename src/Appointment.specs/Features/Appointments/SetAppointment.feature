Feature: SetAppointment
Simple calculator for adding **two** numbers

Link to a feature: [Calculator]($projectname$/Features/SetAppointment.feature)

    Scenario Outline: Appointment must be in valid time
        Given There is a registered patient with the following properties
          | Name |
          | John |
        And A Doctor has been defined with the following properties
          | Name  | DoctorSpeciality   |
          | Smith | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | Times        |
          | Sun       | 10-12, 15-19 |
          | Wen       | 10-12, 15-19 |
        And I have registered the doctor 'Smith'
        When I set appointment with the following properties
          | Pateint | Doctor | AppointmentTime   | AppointmentDuration   |
          | John    | Smith  | <AppointmentTime> | <AppointmentDuration> |
        Then I can find an appointment with above info

        Examples:
          | AppointmentTime  | AppointmentDuration | DoctorSpeciality    |
          | 2023-12-10 10:00 | 5                   | GeneralPractitioner |
          | 2023-12-14 10:00 | 15                  | GeneralPractitioner |
          | 2023-12-10 12:00 | 15                  | GeneralPractitioner |
          | 2023-12-10 15:00 | 5                   | GeneralPractitioner |
          | 2023-12-14 19:00 | 15                  | GeneralPractitioner |
          | 2023-12-10 10:00 | 10                  | Specialist          |
          | 2023-12-14 10:00 | 30                  | Specialist          |
          | 2023-12-10 12:00 | 10                  | Specialist          |
          | 2023-12-10 15:00 | 30                  | Specialist          |
          | 2023-12-14 19:00 | 10                  | Specialist          |