namespace TimeTrackerApi.IntegrationTests.Routes;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TimeTrackerApi.Dtos;

[TestClass]
public class MonthRoutesTests : IntegrationTest
{
    [TestMethod]
    public async Task GetMonthsByMonthAndYearReturnsOk()
    {
        await SetupTimeEntry();

        var response = await httpClient.GetAsync("/month?month=3&year=2020");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var responseBody = await response.Content.ReadFromJsonAsync<MonthResponse>(jsonSerializerOptions);

        Assert.IsNotNull(responseBody);
        Assert.AreEqual("March", responseBody.MonthString);
        Assert.AreEqual(3, responseBody.Month);
        Assert.AreEqual(2020, responseBody.Year);
        Assert.AreEqual(31, responseBody.Days.Count());
        Assert.AreEqual(new DateOnly(2020, 3, 4), responseBody.Days.First(day => day.TimeEntries.Any()).TimeEntries.First().Date);
    }
}
