namespace TimeTrackerApi.IntegrationTests.Routes;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TimeTrackerApi.Models;

[TestClass]
public class TimeEntryRoutesTests : IntegrationTest
{
    [TestMethod]
    public async Task GetAllWithoutEntriesReturnsEmpty()
    {
        var response = await httpClient.GetAsync("/timeEntries");

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var responseBody = await response.Content.ReadFromJsonAsync<IEnumerable<TimeEntry>>(jsonSerializerOptions);

        Assert.IsNotNull(responseBody);
        Assert.AreEqual(0, responseBody.Count());
    }

    [TestMethod]
    public async Task GetAllReturnsEntries()
    {
        await CreateTimeEntry();

        var response = await httpClient.GetAsync("/timeEntries");

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var responseBody = await response.Content.ReadFromJsonAsync<IEnumerable<TimeEntry>>(jsonSerializerOptions);

        Assert.IsNotNull(responseBody);
        Assert.AreEqual(1, responseBody.Count());
    }
}
