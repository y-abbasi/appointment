﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Appointment.specs.Features.Appointments
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class SetAppointmentFeature : object, Xunit.IClassFixture<SetAppointmentFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "SetAppointment.feature"
#line hidden
        
        public SetAppointmentFeature(SetAppointmentFeature.FixtureData fixtureData, Appointment_specs_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Appointments", "SetAppointment", "Simple calculator for adding **two** numbers\n\nLink to a feature: [Calculator]($pr" +
                    "ojectname$/Features/SetAppointment.feature)", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Appointment sets properly")]
        [Xunit.TraitAttribute("FeatureTitle", "SetAppointment")]
        [Xunit.TraitAttribute("Description", "Appointment sets properly")]
        [Xunit.InlineDataAttribute("2023-12-10 10:00", "5", "GeneralPractitioner", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-13 10:00", "15", "GeneralPractitioner", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-10 12:00", "15", "GeneralPractitioner", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-10 15:00", "5", "GeneralPractitioner", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-13 19:00", "15", "GeneralPractitioner", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-10 10:00", "10", "Specialist", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-13 10:00", "30", "Specialist", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-10 12:00", "10", "Specialist", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-10 15:00", "30", "Specialist", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-13 19:00", "10", "Specialist", new string[0])]
        public void AppointmentSetsProperly(string appointmentTime, string appointmentDuration, string doctorSpeciality, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("AppointmentTime", appointmentTime);
            argumentsOfScenario.Add("AppointmentDuration", appointmentDuration);
            argumentsOfScenario.Add("DoctorSpeciality", doctorSpeciality);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Appointment sets properly", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 6
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table1.AddRow(new string[] {
                            "John"});
#line 7
        testRunner.Given("There is a registered patient with the following properties", ((string)(null)), table1, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "DoctorSpeciality"});
                table2.AddRow(new string[] {
                            "Smith",
                            string.Format("{0}", doctorSpeciality)});
#line 10
        testRunner.And("A Doctor has been defined with the following properties", ((string)(null)), table2, "And ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "DayOfWeek",
                            "DaySchedules"});
                table3.AddRow(new string[] {
                            "Sunday",
                            "10:00:00-12:00:00, 15:00:00-19:00:00"});
                table3.AddRow(new string[] {
                            "Wednesday",
                            "10:00:00-12:00:00, 15:00:00-19:00:00"});
#line 13
        testRunner.And("With the following weekly schedule", ((string)(null)), table3, "And ");
#line hidden
#line 17
        testRunner.And("I have registered the doctor \'Smith\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "Patient",
                            "Doctor",
                            "AppointmentTime",
                            "AppointmentDuration"});
                table4.AddRow(new string[] {
                            "John",
                            "Smith",
                            string.Format("{0}", appointmentTime),
                            string.Format("{0}", appointmentDuration)});
#line 18
        testRunner.When("I set appointment with the following properties", ((string)(null)), table4, "When ");
#line hidden
#line 21
        testRunner.Then("I can find an appointment with above info", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Appointment duration should be appropriate to the doctor speciality")]
        [Xunit.TraitAttribute("FeatureTitle", "SetAppointment")]
        [Xunit.TraitAttribute("Description", "Appointment duration should be appropriate to the doctor speciality")]
        [Xunit.InlineDataAttribute("2023-12-10 10:00", "4", "GeneralPractitioner", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-13 10:00", "16", "GeneralPractitioner", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-10 10:00", "9", "Specialist", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-13 10:00", "31", "Specialist", new string[0])]
        public void AppointmentDurationShouldBeAppropriateToTheDoctorSpeciality(string appointmentTime, string appointmentDuration, string doctorSpeciality, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("AppointmentTime", appointmentTime);
            argumentsOfScenario.Add("AppointmentDuration", appointmentDuration);
            argumentsOfScenario.Add("DoctorSpeciality", doctorSpeciality);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Appointment duration should be appropriate to the doctor speciality", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 36
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table5.AddRow(new string[] {
                            "John"});
#line 37
        testRunner.Given("There is a registered patient with the following properties", ((string)(null)), table5, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "DoctorSpeciality"});
                table6.AddRow(new string[] {
                            "Smith",
                            string.Format("{0}", doctorSpeciality)});
#line 40
        testRunner.And("A Doctor has been defined with the following properties", ((string)(null)), table6, "And ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "DayOfWeek",
                            "DaySchedules"});
                table7.AddRow(new string[] {
                            "Sunday",
                            "10:00:00-12:00:00, 15:00:00-19:00:00"});
                table7.AddRow(new string[] {
                            "Wednesday",
                            "10:00:00-12:00:00, 15:00:00-19:00:00"});
#line 43
        testRunner.And("With the following weekly schedule", ((string)(null)), table7, "And ");
#line hidden
#line 47
        testRunner.And("I have registered the doctor \'Smith\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "Patient",
                            "Doctor",
                            "AppointmentTime",
                            "AppointmentDuration"});
                table8.AddRow(new string[] {
                            "John",
                            "Smith",
                            string.Format("{0}", appointmentTime),
                            string.Format("{0}", appointmentDuration)});
#line 48
        testRunner.When("I set appointment with the following properties", ((string)(null)), table8, "When ");
#line hidden
#line 51
        testRunner.Then("Exception with the code \'BR-AP-101\' should be thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="The number of Doctor`s overlapping appointments should not exceeded the allowed n" +
            "umber of total overlapping appointment at the day")]
        [Xunit.TraitAttribute("FeatureTitle", "SetAppointment")]
        [Xunit.TraitAttribute("Description", "The number of Doctor`s overlapping appointments should not exceeded the allowed n" +
            "umber of total overlapping appointment at the day")]
        [Xunit.InlineDataAttribute("2023-12-10 10:00", "15", "GeneralPractitioner", "2", new string[0])]
        [Xunit.InlineDataAttribute("2023-12-10 15:00", "30", "Specialist", "3", new string[0])]
        public void TheNumberOfDoctorSOverlappingAppointmentsShouldNotExceededTheAllowedNumberOfTotalOverlappingAppointmentAtTheDay(string appointmentTime, string appointmentDuration, string doctorSpeciality, string numberOfRegisteredAppointment, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("AppointmentTime", appointmentTime);
            argumentsOfScenario.Add("AppointmentDuration", appointmentDuration);
            argumentsOfScenario.Add("DoctorSpeciality", doctorSpeciality);
            argumentsOfScenario.Add("NumberOfRegisteredAppointment", numberOfRegisteredAppointment);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The number of Doctor`s overlapping appointments should not exceeded the allowed n" +
                    "umber of total overlapping appointment at the day", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 60
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table9.AddRow(new string[] {
                            "John"});
#line 61
        testRunner.Given("There is a registered patient with the following properties", ((string)(null)), table9, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table10.AddRow(new string[] {
                            "Bob"});
#line 64
        testRunner.And("There is a registered patient with the following properties", ((string)(null)), table10, "And ");
#line hidden
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table11.AddRow(new string[] {
                            "Sara"});
#line 67
        testRunner.And("There is a registered patient with the following properties", ((string)(null)), table11, "And ");
#line hidden
                TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table12.AddRow(new string[] {
                            "Alex"});
#line 70
        testRunner.And("There is a registered patient with the following properties", ((string)(null)), table12, "And ");
#line hidden
                TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table13.AddRow(new string[] {
                            "Emma"});
#line 73
        testRunner.And("There is a registered patient with the following properties", ((string)(null)), table13, "And ");
#line hidden
                TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "DoctorSpeciality"});
                table14.AddRow(new string[] {
                            "Smith",
                            string.Format("{0}", doctorSpeciality)});
#line 76
        testRunner.And("A Doctor has been defined with the following properties", ((string)(null)), table14, "And ");
#line hidden
                TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                            "DayOfWeek",
                            "DaySchedules"});
                table15.AddRow(new string[] {
                            "Sunday",
                            "10:00:00-12:00:00, 15:00:00-19:00:00"});
                table15.AddRow(new string[] {
                            "Wednesday",
                            "10:00:00-12:00:00, 15:00:00-19:00:00"});
#line 79
        testRunner.And("With the following weekly schedule", ((string)(null)), table15, "And ");
#line hidden
#line 83
        testRunner.And("I have registered the doctor \'Smith\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                            "Patient",
                            "Doctor",
                            "AppointmentTime",
                            "AppointmentDuration"});
                table16.AddRow(new string[] {
                            "John",
                            "Smith",
                            string.Format("{0}", appointmentTime),
                            string.Format("{0}", appointmentDuration)});
                table16.AddRow(new string[] {
                            "Bob",
                            "Smith",
                            string.Format("{0}", appointmentTime),
                            string.Format("{0}", appointmentDuration)});
                table16.AddRow(new string[] {
                            "Sara",
                            "Smith",
                            string.Format("{0}", appointmentTime),
                            string.Format("{0}", appointmentDuration)});
                table16.AddRow(new string[] {
                            "Alex",
                            "Smith",
                            string.Format("{0}", appointmentTime),
                            string.Format("{0}", appointmentDuration)});
#line 84
        testRunner.And(string.Format("\'{0}\' overlapping appointments with the following properties has already been reg" +
                            "istered", numberOfRegisteredAppointment), ((string)(null)), table16, "And ");
#line hidden
                TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                            "Patient",
                            "Doctor",
                            "AppointmentTime",
                            "AppointmentDuration"});
                table17.AddRow(new string[] {
                            "Emma",
                            "Smith",
                            string.Format("{0}", appointmentTime),
                            string.Format("{0}", appointmentDuration)});
#line 90
        testRunner.When("I set appointment with the following properties", ((string)(null)), table17, "When ");
#line hidden
#line 93
        testRunner.Then("Exception with the code \'BR-AP-105\' should be thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                SetAppointmentFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                SetAppointmentFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
