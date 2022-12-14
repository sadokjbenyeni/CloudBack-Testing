// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.1.0.0
//      SpecFlow Generator Version:3.1.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace CloudBacktesting.PaymentService.Specs.Features.PaymentMethod
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("PaymentMethod creation feature")]
    public partial class PaymentMethodCreationFeatureFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "PaymentMethod.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "PaymentMethod creation feature", "\t\tAll customers can create / update / delete the payment methods ", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        public virtual void TestTearDown()
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
#line hidden
#line 5
 testRunner.Given("Morgan is authentificated with roles \'Administrator\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 6
 testRunner.Given("Chang is authentificated with roles \'Client\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 7
 testRunner.Given("\'Chang\' payment account has been created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang creates a new Credit card payment method")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("paymentMethod")]
        [NUnit.Framework.CategoryAttribute("creation")]
        [NUnit.Framework.CategoryAttribute("success")]
        [NUnit.Framework.CategoryAttribute("creditCard")]
        public virtual void ChangCreatesANewCreditCardPaymentMethod()
        {
            string[] tagsOfScenario = new string[] {
                    "v1",
                    "paymentMethod",
                    "creation",
                    "success",
                    "creditCard"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang creates a new Credit card payment method", null, new string[] {
                        "v1",
                        "paymentMethod",
                        "creation",
                        "success",
                        "creditCard"});
#line 10
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 11
 testRunner.Given("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Holder",
                            "Numbers",
                            "Network",
                            "ExpirationYear",
                            "ExpirationMonth",
                            "Cryptogram"});
                table2.AddRow(new string[] {
                            "Chang\'s Company",
                            "4050 1197 6948 4808",
                            "Visa",
                            "2021",
                            "03",
                            "359"});
#line 12
 testRunner.When("\'Chang\' creates a new payment method with:", ((string)(null)), table2, "When ");
#line hidden
#line 15
 testRunner.Then("Creation of payment method creation is successful", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang creates a new Credit card payment method with already created others cards")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("paymentMethod")]
        [NUnit.Framework.CategoryAttribute("creation")]
        [NUnit.Framework.CategoryAttribute("success")]
        [NUnit.Framework.CategoryAttribute("creditCard")]
        public virtual void ChangCreatesANewCreditCardPaymentMethodWithAlreadyCreatedOthersCards()
        {
            string[] tagsOfScenario = new string[] {
                    "v1",
                    "paymentMethod",
                    "creation",
                    "success",
                    "creditCard"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang creates a new Credit card payment method with already created others cards", null, new string[] {
                        "v1",
                        "paymentMethod",
                        "creation",
                        "success",
                        "creditCard"});
#line 18
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 19
 testRunner.Given("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 20
 testRunner.And("5 credit cards has been already created by \'Chang\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "Holder",
                            "Numbers",
                            "Network",
                            "ExpirationYear",
                            "ExpirationMonth",
                            "Cryptogram"});
                table3.AddRow(new string[] {
                            "Chang\'s Company",
                            "4050 1197 6948 4808",
                            "Visa",
                            "2021",
                            "03",
                            "359"});
#line 21
 testRunner.When("\'Chang\' creates a new payment method with:", ((string)(null)), table3, "When ");
#line hidden
#line 24
 testRunner.Then("Creation of payment method creation is successful", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The Chang\'s creation payment method request is failed with wrong credit card form" +
            "at")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("paymentMethod")]
        [NUnit.Framework.CategoryAttribute("creation")]
        [NUnit.Framework.CategoryAttribute("failed")]
        [NUnit.Framework.CategoryAttribute("creditCard")]
        public virtual void TheChangsCreationPaymentMethodRequestIsFailedWithWrongCreditCardFormat()
        {
            string[] tagsOfScenario = new string[] {
                    "v1",
                    "paymentMethod",
                    "creation",
                    "failed",
                    "creditCard"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The Chang\'s creation payment method request is failed with wrong credit card form" +
                    "at", null, new string[] {
                        "v1",
                        "paymentMethod",
                        "creation",
                        "failed",
                        "creditCard"});
#line 27
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 28
 testRunner.Given("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "Holder",
                            "Numbers",
                            "Network",
                            "ExpirationYear",
                            "ExpirationMonth",
                            "Cryptogram"});
                table4.AddRow(new string[] {
                            "Chang\'s Company",
                            "5555 5555 5555 5555",
                            "Visa",
                            "2030",
                            "12",
                            "111"});
#line 29
 testRunner.When("\'Chang\' creates a new payment method with:", ((string)(null)), table4, "When ");
#line hidden
#line 32
 testRunner.Then("Creation of payment method creation is not successful", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang browses credit cards list empty")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("paymentMethod")]
        [NUnit.Framework.CategoryAttribute("browseAll")]
        [NUnit.Framework.CategoryAttribute("success")]
        [NUnit.Framework.CategoryAttribute("creditCard")]
        public virtual void ChangBrowsesCreditCardsListEmpty()
        {
            string[] tagsOfScenario = new string[] {
                    "v1",
                    "paymentMethod",
                    "browseAll",
                    "success",
                    "creditCard"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang browses credit cards list empty", null, new string[] {
                        "v1",
                        "paymentMethod",
                        "browseAll",
                        "success",
                        "creditCard"});
#line 35
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 36
 testRunner.Given("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 37
 testRunner.When("\'Chang\' browses all payment method", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 38
 testRunner.Then("result of request is empty", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang browses credit cards list contains the specific credit card")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("paymentMethod")]
        [NUnit.Framework.CategoryAttribute("browseAll")]
        [NUnit.Framework.CategoryAttribute("success")]
        [NUnit.Framework.CategoryAttribute("creditCard")]
        public virtual void ChangBrowsesCreditCardsListContainsTheSpecificCreditCard()
        {
            string[] tagsOfScenario = new string[] {
                    "v1",
                    "paymentMethod",
                    "browseAll",
                    "success",
                    "creditCard"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang browses credit cards list contains the specific credit card", null, new string[] {
                        "v1",
                        "paymentMethod",
                        "browseAll",
                        "success",
                        "creditCard"});
#line 41
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 42
 testRunner.Given("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "Holder",
                            "Numbers",
                            "Network",
                            "ExpirationYear",
                            "ExpirationMonth",
                            "Cryptogram"});
                table5.AddRow(new string[] {
                            "Chang\'s Company",
                            "4050 1197 6948 4808",
                            "Visa",
                            "2021",
                            "03",
                            "359"});
#line 43
 testRunner.And("\'Chang\' created payment method with:", ((string)(null)), table5, "And ");
#line hidden
#line 46
 testRunner.When("\'Chang\' browses all payment method", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 47
 testRunner.Then("the credit card has been return by the request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang browses credit cards list all credit cards")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("paymentMethod")]
        [NUnit.Framework.CategoryAttribute("browseAll")]
        [NUnit.Framework.CategoryAttribute("success")]
        [NUnit.Framework.CategoryAttribute("creditCard")]
        public virtual void ChangBrowsesCreditCardsListAllCreditCards()
        {
            string[] tagsOfScenario = new string[] {
                    "v1",
                    "paymentMethod",
                    "browseAll",
                    "success",
                    "creditCard"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang browses credit cards list all credit cards", null, new string[] {
                        "v1",
                        "paymentMethod",
                        "browseAll",
                        "success",
                        "creditCard"});
#line 50
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 51
 testRunner.Given("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 52
 testRunner.And("3 credit cards has been already created by \'Chang\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 53
 testRunner.When("\'Chang\' browses all payment method", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 54
 testRunner.Then("the result of the request contains 3 credit cards created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang browses a specific credit cards")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("paymentMethod")]
        [NUnit.Framework.CategoryAttribute("browse")]
        [NUnit.Framework.CategoryAttribute("success")]
        [NUnit.Framework.CategoryAttribute("creditCard")]
        public virtual void ChangBrowsesASpecificCreditCards()
        {
            string[] tagsOfScenario = new string[] {
                    "v1",
                    "paymentMethod",
                    "browse",
                    "success",
                    "creditCard"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang browses a specific credit cards", null, new string[] {
                        "v1",
                        "paymentMethod",
                        "browse",
                        "success",
                        "creditCard"});
#line 57
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 58
 testRunner.Given("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "Holder",
                            "Numbers",
                            "Network",
                            "ExpirationYear",
                            "ExpirationMonth",
                            "Cryptogram"});
                table6.AddRow(new string[] {
                            "Chang\'s Company",
                            "4050 1197 6948 4808",
                            "Visa",
                            "2021",
                            "03",
                            "359"});
#line 59
 testRunner.And("\'Chang\' created payment method with:", ((string)(null)), table6, "And ");
#line hidden
#line 62
 testRunner.When("\'Chang\' browses \'Chang\'s Company\' payment method", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 63
 testRunner.Then("only this credit cards has been returned", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Chang browses with wrong payment method identifier and result is not found")]
        [NUnit.Framework.CategoryAttribute("v1")]
        [NUnit.Framework.CategoryAttribute("paymentMethod")]
        [NUnit.Framework.CategoryAttribute("browse")]
        [NUnit.Framework.CategoryAttribute("failed")]
        [NUnit.Framework.CategoryAttribute("creditCard")]
        public virtual void ChangBrowsesWithWrongPaymentMethodIdentifierAndResultIsNotFound()
        {
            string[] tagsOfScenario = new string[] {
                    "v1",
                    "paymentMethod",
                    "browse",
                    "failed",
                    "creditCard"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Chang browses with wrong payment method identifier and result is not found", null, new string[] {
                        "v1",
                        "paymentMethod",
                        "browse",
                        "failed",
                        "creditCard"});
#line 66
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 67
 testRunner.Given("the webapi is online", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "Holder",
                            "Numbers",
                            "Network",
                            "ExpirationYear",
                            "ExpirationMonth",
                            "Cryptogram"});
                table7.AddRow(new string[] {
                            "Chang\'s Company",
                            "4050 1197 6948 4808",
                            "Visa",
                            "2021",
                            "03",
                            "359"});
#line 68
 testRunner.And("\'Chang\' created payment method with:", ((string)(null)), table7, "And ");
#line hidden
#line 71
 testRunner.When("\'Chang\' browses wrong id payment method", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 72
 testRunner.Then("the api return an not found result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
