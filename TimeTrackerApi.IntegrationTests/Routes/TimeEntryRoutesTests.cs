namespace TimeTrackerApi.IntegrationTests.Routes;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TimeTrackerApi.IntegrationTests.Util;
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
        var existingTimeEntry = await SetupTimeEntry();

        var response = await httpClient.GetAsync("/timeEntries");

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var responseBody = await response.Content.ReadFromJsonAsync<IEnumerable<TimeEntry>>(jsonSerializerOptions);

        Assert.IsNotNull(responseBody);
        Assert.AreEqual(1, responseBody.Count());
        Assert.AreEqual(existingTimeEntry.Id, responseBody.First().Id);
    }

    [TestMethod]
    public async Task PostFailsOnInvalidBody()
    {
        var timeEntry = TestHelper.GetTimeEntry();
        timeEntry.PauseHours = 100;

        var response = await httpClient.PostAsJsonAsync("/timeEntry", timeEntry, jsonSerializerOptions);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    public async Task PostAddsToDb()
    {
        var timeEntry = TestHelper.GetTimeEntry();

        var postResponse = await httpClient.PostAsJsonAsync("/timeEntry", timeEntry, jsonSerializerOptions);
        Assert.AreEqual(HttpStatusCode.OK, postResponse.StatusCode);

        var postBody = await postResponse.Content.ReadFromJsonAsync<TimeEntry>(jsonSerializerOptions);
        Assert.IsNotNull(postBody);
        Assert.AreEqual(timeEntry.Date, postBody.Date);
        //TODO: the response should be a record that is easily comparable.

        var getResponse = await httpClient.GetAsync("/timeEntry/" + postBody.Id);
        Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [TestMethod]
    public async Task PoutChangesInDb()
    {
        var existingTimeEntry = await SetupTimeEntry();

        var timeEntry = new TimeEntry();
        timeEntry.Map(existingTimeEntry);
        timeEntry.PauseHours++;

        var putResponse = await httpClient.PutAsJsonAsync("/timeEntry/" + existingTimeEntry.Id, timeEntry, jsonSerializerOptions);
        Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);

        var putBody = await putResponse.Content.ReadFromJsonAsync<TimeEntry>(jsonSerializerOptions);
        Assert.IsNotNull(putBody);
        Assert.AreEqual(timeEntry.PauseHours, putBody.PauseHours);
        //TODO: the response should be a record that is easily comparable.

        var getResponse = await httpClient.GetAsync("/timeEntry/" + putBody.Id);
        Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
        var getBody = await getResponse.Content.ReadFromJsonAsync<TimeEntry>(jsonSerializerOptions);
        Assert.IsNotNull(putBody);
        Assert.AreEqual(timeEntry.PauseHours, getBody.PauseHours);
    }

    [TestMethod]
    public async Task DeleteRemovesFromDb()
    {
        var createdTimeEntry = await SetupTimeEntry();

        var getResponseBeforeDelete = await httpClient.GetAsync("/timeEntry/" + createdTimeEntry.Id);
        Assert.AreEqual(HttpStatusCode.OK, getResponseBeforeDelete.StatusCode);

        var deleteResponse = await httpClient.DeleteAsync("/timeEntry/" + createdTimeEntry.Id);
        Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);

        var getResponseAfterDelete = await httpClient.GetAsync("/timeEntry/" + createdTimeEntry.Id);
        Assert.AreEqual(HttpStatusCode.NotFound, getResponseAfterDelete.StatusCode);
    }
}
