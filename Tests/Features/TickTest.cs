using LightBDD.Framework.Scenarios;
using LightBDD.MsTest3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Features;

[TestClass]
public partial class TickTest : BaseTestFrame
{
    [Scenario]
    [TestMethod]
    public async Task Things_happen()
    {
        await Runner.RunScenarioAsync(
                _ => Begin(),
                _ => Wait(),
                _ => Stop(),
                _ => Three_ticks_were_sent()

                );

    }

}

