// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Quanthouse.LicensingManager.Server.Specs.CloudBacktesting_SubscriptionService_Specs.Features.SubscriptionRequest
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Subscription validation request feature")]
    public partial class SubscriptionValidationRequestFeatureFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SubscriptionValidationRequest.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Subscription validation request feature", "\t\tChang can create a new subscription after register on the site", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 4
#line 5
 testRunner.Given("Morgan is authentificated with roles \'Admin\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 6
 testRunner.And("Chang is authentificated with roles \'Client\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 7
 testRunner.And("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
 testRunner.And("\'Chang\' subscription account has been created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
 testRunner.And("\'Mutualized\' subscription has been created for \'Chang\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang browses all subscription request")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("subscription")]
        [NUnit.Framework.CategoryAttribute("request")]
        [NUnit.Framework.CategoryAttribute("creation")]
        public virtual void ChangBrowsesAllSubscriptionRequest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang browses all subscription request", null, new string[] {
                        "v1",
                        "subscription",
                        "request",
                        "creation"});
#line 12
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
this.FeatureBackground();
#line 13
 testRunner.When("\'Chang\' sends GET request these subscriptions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 14
 testRunner.Then("subscriptions are returned for \'Chang\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang browses description for specific subscription")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("subscription")]
        [NUnit.Framework.CategoryAttribute("request")]
        [NUnit.Framework.CategoryAttribute("creation")]
        public virtual void ChangBrowsesDescriptionForSpecificSubscription()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang browses description for specific subscription", null, new string[] {
                        "v1",
                        "subscription",
                        "request",
                        "creation"});
#line 17
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
this.FeatureBackground();
#line 18
 testRunner.When("\'Chang\' sends GET request subscription", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "field",
                        "value"});
            table3.AddRow(new string[] {
                        "Status",
                        "Pending"});
            table3.AddRow(new string[] {
                        "Subscriber",
                        "Chang"});
            table3.AddRow(new string[] {
                        "Type",
                        "Mutualized"});
            table3.AddRow(new string[] {
                        "IsSystemValidated",
                        "true"});
            table3.AddRow(new string[] {
                        "OrderId",
                        "1"});
#line 19
 testRunner.Then("The subscription required that:", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Morgan browses the Chang\'s mutualized subscription account")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("subscription")]
        [NUnit.Framework.CategoryAttribute("request")]
        [NUnit.Framework.CategoryAttribute("creation")]
        [NUnit.Framework.CategoryAttribute("validationRequest")]
        [NUnit.Framework.CategoryAttribute("browse")]
        [NUnit.Framework.CategoryAttribute("data")]
        [NUnit.Framework.CategoryAttribute("readModel")]
        public virtual void MorganBrowsesTheChangsMutualizedSubscriptionAccount()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Morgan browses the Chang\'s mutualized subscription account", null, new string[] {
                        "v1",
                        "subscription",
                        "request",
                        "creation",
                        "validationRequest",
                        "browse",
                        "data",
                        "readModel",
                        "ignore"});
#line 28
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
this.FeatureBackground();
#line 29
 testRunner.When("\'Morgan\' sends GET admin request with \'Mutualized\' subscription", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("only \'Mutualized\' subscription has been return at \'Chang\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Morgan browses subscription requests open")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("subscription")]
        [NUnit.Framework.CategoryAttribute("request")]
        [NUnit.Framework.CategoryAttribute("creation")]
        [NUnit.Framework.CategoryAttribute("validationRequest")]
        [NUnit.Framework.CategoryAttribute("browse")]
        [NUnit.Framework.CategoryAttribute("data")]
        [NUnit.Framework.CategoryAttribute("readModel")]
        public virtual void MorganBrowsesSubscriptionRequestsOpen()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Morgan browses subscription requests open", null, new string[] {
                        "v1",
                        "subscription",
                        "request",
                        "creation",
                        "validationRequest",
                        "browse",
                        "data",
                        "readModel"});
#line 33
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
this.FeatureBackground();
#line 34
 testRunner.Given("populates repositry with subscription account and requests", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 35
 testRunner.When("\'Morgan\' sends GET request on subscription request which are being created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.Then("all subscription request has been return", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
