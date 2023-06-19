Feature: SetAppointment
Simple calculator for adding **two** numbers

Link to a feature: [Calculator]($projectname$/Features/SetAppointment.feature)

    Scenario Outline: Appointment sets properly
        Given There is a registered patient with the following properties
          | Name |
          | John |
        And A Doctor has been defined with the following properties
          | Name  | DoctorSpeciality   |
          | Smith | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | DaySchedules                         |
          | Sunday    | 10:00:00-12:00:00, 15:00:00-19:00:00 |
          | Wednesday | 10:00:00-12:00:00, 15:00:00-19:00:00 |
        And I have registered the doctor 'Smith'
        When I set appointment with the following properties
          | Patient | Doctor | AppointmentTime   | AppointmentDuration   |
          | John    | Smith  | <AppointmentTime> | <AppointmentDuration> |
        Then I can find an appointment with above info

        Examples:
          | AppointmentTime  | AppointmentDuration | DoctorSpeciality    |
          | 2023-12-10 10:00 | 5                   | GeneralPractitioner |
          | 2023-12-13 10:00 | 15                  | GeneralPractitioner |
          | 2023-12-10 12:00 | 15                  | GeneralPractitioner |
          | 2023-12-10 15:00 | 5                   | GeneralPractitioner |
          | 2023-12-13 19:00 | 15                  | GeneralPractitioner |
          | 2023-12-10 10:00 | 10                  | Specialist          |
          | 2023-12-13 10:00 | 30                  | Specialist          |
          | 2023-12-10 12:00 | 10                  | Specialist          |
          | 2023-12-10 15:00 | 30                  | Specialist          |
          | 2023-12-13 19:00 | 10                  | Specialist          |

    Scenario Outline: Appointment time should be within working hours of clinic
        Given There is a registered patient with the following properties
          | Name |
          | John |
        And A Doctor has been defined with the following properties
          | Name  | DoctorSpeciality   |
          | Smith | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | DaySchedules                         |
          | Sunday    | 10:00:00-12:00:00, 15:00:00-19:00:00 |
          | Wednesday | 10:00:00-12:00:00, 15:00:00-19:00:00 |
        And I have registered the doctor 'Smith'
        When I set appointment with the following properties
          | Patient | Doctor | AppointmentTime   | AppointmentDuration |
          | John    | Smith  | <AppointmentTime> | 10                  |
        Then Exception with the code 'BR-AP-100' should be thrown

        Examples:
          | AppointmentTime  | DoctorSpeciality    |
          | 2023-12-10 08:59 | GeneralPractitioner |
          | 2023-12-13 19:01 | Specialist          |

    Scenario Outline: Appointment duration should be appropriate to the doctor speciality
        Given There is a registered patient with the following properties
          | Name |
          | John |
        And A Doctor has been defined with the following properties
          | Name  | DoctorSpeciality   |
          | Smith | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | DaySchedules                         |
          | Sunday    | 10:00:00-12:00:00, 15:00:00-19:00:00 |
          | Wednesday | 10:00:00-12:00:00, 15:00:00-19:00:00 |
        And I have registered the doctor 'Smith'
        When I set appointment with the following properties
          | Patient | Doctor | AppointmentTime   | AppointmentDuration   |
          | John    | Smith  | <AppointmentTime> | <AppointmentDuration> |
        Then Exception with the code 'BR-AP-101' should be thrown

        Examples:
          | AppointmentTime  | AppointmentDuration | DoctorSpeciality    |
          | 2023-12-10 10:00 | 4                   | GeneralPractitioner |
          | 2023-12-13 10:00 | 16                  | GeneralPractitioner |
          | 2023-12-10 10:00 | 9                   | Specialist          |
          | 2023-12-13 10:00 | 31                  | Specialist          |

    Scenario Outline: Appointment time should be during the doctor`s presents
        Given There is a registered patient with the following properties
          | Name |
          | John |
        And A Doctor has been defined with the following properties
          | Name  | DoctorSpeciality   |
          | Smith | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | DaySchedules                         |
          | Sunday    | 10:00:00-12:00:00, 15:00:00-18:00:00 |
          | Wednesday | 10:00:00-12:00:00, 15:00:00-18:00:00 |
        And I have registered the doctor 'Smith'
        When I set appointment with the following properties
          | Patient | Doctor | AppointmentTime   | AppointmentDuration   |
          | John    | Smith  | <AppointmentTime> | <AppointmentDuration> |
        Then Exception with the code 'BR-AP-102' should be thrown

        Examples:
          | AppointmentTime  | AppointmentDuration | DoctorSpeciality    |
          | 2023-12-11 10:00 | 10                  | GeneralPractitioner |
          | 2023-12-13 09:59 | 10                  | GeneralPractitioner |
          | 2023-12-13 12:01 | 10                  | GeneralPractitioner |
          | 2023-12-13 18:01 | 10                  | GeneralPractitioner |

    Scenario Outline: Patient`s appointment in one day should be less than two
        Given There is a registered patient with the following properties
          | Name |
          | John |
        And A Doctor has been defined with the following properties
          | Name  | DoctorSpeciality   |
          | Smith | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | DaySchedules                         |
          | Sunday    | 10:00:00-12:00:00, 15:00:00-18:00:00 |
        And I have registered the doctor 'Smith'
        And A Doctor has been defined with the following properties
          | Name | DoctorSpeciality   |
          | Mary | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | DaySchedules                         |
          | Sunday    | 10:00:00-12:00:00, 15:00:00-18:00:00 |
        And I have registered the doctor 'Mary'
        And A Doctor has been defined with the following properties
          | Name | DoctorSpeciality   |
          | Sam  | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | DaySchedules                         |
          | Sunday    | 10:00:00-12:00:00, 15:00:00-18:00:00 |
        And I have registered the doctor 'Sam'
        And An appointment with the following properties has already been registered
          | Patient | Doctor | AppointmentTime        | AppointmentDuration   |
          | John    | Smith  | <FirstAppointmentTime> | <AppointmentDuration> |
        And An appointment with the following properties has already been registered
          | Patient | Doctor | AppointmentTime         | AppointmentDuration   |
          | John    | Mary   | <SecondAppointmentTime> | <AppointmentDuration> |
        When I set appointment with the following properties
          | Patient | Doctor | AppointmentTime        | AppointmentDuration   |
          | John    | Sam    | <ThirdAppointmentTime> | <AppointmentDuration> |
        Then Exception with the code 'BR-AP-103' should be thrown

        Examples:
          | FirstAppointmentTime | SecondAppointmentTime | ThirdAppointmentTime | AppointmentDuration | DoctorSpeciality    |
          | 2023-12-10 10:00     | 2023-12-10 10:30      | 2023-12-10 11:00     | 10                  | GeneralPractitioner |

    Scenario Outline: The number of Doctor`s overlapping appointments should not exceeded the allowed number of total overlapping appointment at the day
        Given There is a registered patient with the following properties
          | Name |
          | John |
        And There is a registered patient with the following properties
          | Name |
          | Bob  |
        And There is a registered patient with the following properties
          | Name |
          | Sara |
        And There is a registered patient with the following properties
          | Name |
          | Alex |
        And There is a registered patient with the following properties
          | Name |
          | Emma |
        And A Doctor has been defined with the following properties
          | Name  | DoctorSpeciality   |
          | Smith | <DoctorSpeciality> |
        And With the following weekly schedule
          | DayOfWeek | DaySchedules                         |
          | Sunday    | 10:00:00-12:00:00, 15:00:00-19:00:00 |
          | Wednesday | 10:00:00-12:00:00, 15:00:00-19:00:00 |
        And I have registered the doctor 'Smith'
        And '<NumberOfRegisteredAppointment>' overlapping appointments with the following properties has already been registered
          | Patient | Doctor | AppointmentTime   | AppointmentDuration   |
          | John    | Smith  | <AppointmentTime> | <AppointmentDuration> |
          | Bob     | Smith  | <AppointmentTime> | <AppointmentDuration> |
          | Sara    | Smith  | <AppointmentTime> | <AppointmentDuration> |
          | Alex    | Smith  | <AppointmentTime> | <AppointmentDuration> |
        When I set appointment with the following properties
          | Patient | Doctor | AppointmentTime   | AppointmentDuration   |
          | Emma    | Smith  | <AppointmentTime> | <AppointmentDuration> |
        Then Exception with the code 'BR-AP-105' should be thrown

        Examples:
          | AppointmentTime  | AppointmentDuration | DoctorSpeciality    | NumberOfRegisteredAppointment |
          | 2023-12-10 10:00 | 15                  | GeneralPractitioner | 2                             |
          | 2023-12-10 15:00 | 30                  | Specialist          | 3                             |