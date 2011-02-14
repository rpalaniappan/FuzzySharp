using Fuzzy.Collections;
using Fuzzy.Entities;
using Fuzzy.Rules.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuzzy.Tests.Rules
{
    [TestClass]
    public class SetupInterface
    {
        [TestMethod]
        public void SetupRuleBuilder_ShouldCreateAlgorithm()
        {
            var list = new FuzzyValues();
            list.AddFuzzyValue("NB", new FuzzySet());
            list.AddFuzzyValue("NS", new FuzzySet());
            list.AddFuzzyValue("Z", new FuzzySet());
            list.AddFuzzyValue("PS", new FuzzySet());
            list.AddFuzzyValue("PB", new FuzzySet());

            list.AddFuzzyValue("P", new FuzzySet());
            list.AddFuzzyValue("N", new FuzzySet());
            list.AddFuzzyValue("ZE", new FuzzySet());

            list.AddFuzzyValue("VL", new FuzzySet());
            list.AddFuzzyValue("LOW", new FuzzySet());
            list.AddFuzzyValue("MED", new FuzzySet());
            list.AddFuzzyValue("HIGH", new FuzzySet());
            list.AddFuzzyValue("VH", new FuzzySet());

            var builder = new RuleBuilder(list, Functions.Functions.Min, null, null);

            builder
                .If("error").Is("NB").And().If("deltaerror").Is("N").Then("output").Is("HIGH").Else()
                .If("error").Is("NB").And().If("deltaerror").Is("ZE").Then("output").Is("VH").Else()
                .If("error").Is("NB").And().If("deltaerror").Is("P").Then("output").Is("VH").Else()
                .If("error").Is("NS").And().If("deltaerror").Is("N").Then("output").Is("HIGH").Else()
                .If("error").Is("NS").And().If("deltaerror").Is("ZE").Then("output").Is("HIGH").Else()
                .If("error").Is("NS").And().If("deltaerror").Is("P").Then("output").Is("MED").Else()
                .If("error").Is("Z").And().If("deltaerror").Is("N").Then("output").Is("MED").Else()
                .If("error").Is("Z").And().If("deltaerror").Is("ZE").Then("output").Is("MED").Else()
                .If("error").Is("Z").And().If("deltaerror").Is("P").Then("output").Is("MED").Else()
                .If("error").Is("PS").And().If("deltaerror").Is("N").Then("output").Is("MED").Else()
                .If("error").Is("PS").And().If("deltaerror").Is("ZE").Then("output").Is("LOW").Else()
                .If("error").Is("PS").And().If("deltaerror").Is("P").Then("output").Is("LOW").Else()
                .If("error").Is("PB").And().If("deltaerror").Is("N").Then("output").Is("LOW").Else()
                .If("error").Is("PB").And().If("deltaerror").Is("ZE").Then("output").Is("VL").Else()
                .If("error").Is("PB").And().If("deltaerror").Is("P").Then("output").Is("VL");

            Assert.AreEqual(builder.FuzzyAlgorithm.Rules.Count, 15);
            Assert.AreEqual(builder.FuzzyAlgorithm.Rules[0].Conditions.Count, 2);
            Assert.AreEqual(builder.FuzzyAlgorithm.Rules[0].Conditions[0].Variable, "error");
            Assert.AreEqual(builder.FuzzyAlgorithm.Rules[0].Conditions[1].Variable, "deltaerror");
        }
    }
}
