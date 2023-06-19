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
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Appointment must be in valid time")]
        [Xunit.TraitAttribute("FeatureTitle", "SetAppointment")]
        [Xunit.TraitAttribute("Description", "Appointment must be in valid time")]
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
        public void AppointmentMustBeInValidTime(string appointmentTime, string appointmentDuration, string doctorSpeciality, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("AppointmentTime", appointmentTime);
            argumentsOfScenario.Add("AppointmentDuration", appointmentDuration);
            argumentsOfScenario.Add("DoctorSpeciality", doctorSpeciality);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Appointment must be in valid time", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
