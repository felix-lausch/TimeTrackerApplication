namespace TimeTrackerApi.IntegrationTests.Routes;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

[TestClass]
public class BaseRouteTests : IntegrationTest
{
    [TestMethod]
    public async Task GetBaseReturnsMessage()
    {
        var response = await httpClient.GetAsync("/");

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("TimeTracker API is reachable.", await response.Content.ReadAsStringAsync());
    }
}
