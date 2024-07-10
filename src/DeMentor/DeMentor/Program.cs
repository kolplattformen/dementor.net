using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

var cookieContainer = new CookieContainer();
var httpClientHandler = new HttpClientHandler { CookieContainer = cookieContainer, AllowAutoRedirect = false };
var client = new HttpClient(httpClientHandler);
client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36"); ;


// read the json file credentials.json and set the values to the variables
var json = File.ReadAllText("credentials.json");
var credentials = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

var username = credentials["username"];
var password = credentials["password"];



// START LOGIN


var response = await client.GetAsync("https://hub.infomentor.se/");

while(response.StatusCode == HttpStatusCode.Found)
{
    var location = response.Headers.Location;
    if (location.ToString().StartsWith("/"))
    {
        location = new Uri( "https://hub.infomentor.se" + location);
    }
    response = await client.GetAsync(location);
}

var content = await response.Content.ReadAsStringAsync();

// get the value of oauth_token from content
var oauthToken = Regex.Match(content, "<input type=\"hidden\" name=\"oauth_token\" value=\"(.*?)\" />").Groups[1].Value;
oauthToken = WebUtility.HtmlDecode(oauthToken);


response = await client.PostAsync("https://infomentor.se/swedish/production/mentor/", new FormUrlEncodedContent(new Dictionary<string, string>
{
    { "oauth_token", oauthToken }

}));


while (response.StatusCode == HttpStatusCode.Found)
{
    var location = response.Headers.Location;
    response = await client.GetAsync(location);
}

content = await response.Content.ReadAsStringAsync();


// get the value of viewstate from content
var viewState = Regex.Match(content, "<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\"(.*?)\" />").Groups[1].Value;
var viewstateGenerator = Regex.Match(content, "<input type=\"hidden\" name=\"__VIEWSTATEGENERATOR\" id=\"__VIEWSTATEGENERATOR\" value=\"(.*?)\" />").Groups[1].Value;
var eventValidation = Regex.Match(content, "<input type=\"hidden\" name=\"__EVENTVALIDATION\" id=\"__EVENTVALIDATION\" value=\"(.*?)\" />").Groups[1].Value;


// HTML decode e viewstate
viewState = WebUtility.HtmlDecode(viewState);
viewstateGenerator = WebUtility.HtmlDecode(viewstateGenerator);
eventValidation = WebUtility.HtmlDecode(eventValidation);

response = await client.PostAsync("https://infomentor.se/swedish/production/mentor/", new FormUrlEncodedContent(new Dictionary<string, string>
{
    { "login_ascx$txtNotandanafn", username },
    { "login_ascx$txtLykilord", password },
    { "login_ascx$btnLogin", "Logga in" },
    { "__VIEWSTATE", viewState },
    { "__VIEWSTATEGENERATOR", viewstateGenerator },
    { "__EVENTVALIDATION", eventValidation },
    {"__EVENTTARGET", ""},
    {"__EVENTARGUMENT", ""},

    
}));

while (response.StatusCode == HttpStatusCode.Found)
{
    var location = response.Headers.Location;
    response = await client.GetAsync(location);
}

content = await response.Content.ReadAsStringAsync();

 oauthToken = Regex.Match(content, "<input type=\"hidden\" name=\"oauth_token\" value=\"(.*?)\" />").Groups[1].Value;
 oauthToken = WebUtility.HtmlDecode(oauthToken);

response = await client.PostAsync("https://infomentor.se/swedish/production/mentor/", new FormUrlEncodedContent(new Dictionary<string, string>
{
    { "oauth_token", oauthToken }

}));

while (response.StatusCode == HttpStatusCode.Found)
{
    var location = response.Headers.Location;
    if (location.ToString().StartsWith("/"))
    {
        location = new Uri("https://hub.infomentor.se" + location);
    }
    response = await client.GetAsync(location.ToString());
}

content = await response.Content.ReadAsStringAsync();

var time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
response = await client.PostAsync($"https://hub.infomentor.se/authentication/authentication/isauthenticated/?_={time}", null);

content = await response.Content.ReadAsStringAsync();

// END LOGIN


// NOTIFICATIONS
response = await client.PostAsync("https://hub.infomentor.se/NotificationApp/NotificationApp/appData", null);
//response = await client.PostAsync("https://hub.infomentor.se/NotificationApp/NotificationApp/GetNotifications", null);
var notificationsJson = await response.Content.ReadAsStringAsync();

// NEWS
response = await client.PostAsync("https://hub.infomentor.se/Communication/News/GetNewsList", null);
var newsListJson = await response.Content.ReadAsStringAsync();

// DOCUMENTS
response = await client.PostAsync("https://hub.infomentor.se/Communication/Documents/GetDocumentsList", null);
var documentsListJson = await response.Content.ReadAsStringAsync();

// LINKS
response = await client.PostAsync("https://hub.infomentor.se/Communication/Links/GetLinksList", null);
var linksListJson = await response.Content.ReadAsStringAsync();

// CALENDAR
response = await client.PostAsync("https://hub.infomentor.se/calendarv2/calendarv2/getentries", new StringContent("{\"startDate\":\"2024/05/20\",\"endDate\":\"2024/07/07\"}"));
var calendarJson = await response.Content.ReadAsStringAsync();

// TIMETABLE TODO: NOT WORKING, Needs more headers?
response = await client.PostAsync("https://hub.infomentor.se/TimeTable/TimeTable/GetTimeTable", new FormUrlEncodedContent( new Dictionary<string, string>
{
    { "UTCOffset", "-120"},
    { "star", "2024/05/20" },
    { "end", "2024/07/07" }
}));
var timetableJson = await response.Content.ReadAsStringAsync();

// CLASSLIST
response = await client.PostAsync("https://hub.infomentor.se/classlist/classlist/appData?_=090720241513", null);
var classListJson = await response.Content.ReadAsStringAsync();

// CLASSLISTITEM - STAFFMEMBER TODO: NOT WORKING, Needs more headers?
response = await client.PostAsync("https://hub.infomentor.se/ClassList/classlist/GetStaff", new StringContent("{\"id\":\"744028\",\"establishmentId\":\"0000012345\"}"));
var classListStaffJson = await response.Content.ReadAsStringAsync();

// CLASSLISTITEM - PUPIL TODO: NOT WORKING, Needs more headers?
response = await client.PostAsync("https://hub.infomentor.se/ClassList/classlist/GetPupil", new StringContent("{\"id\":\"953908\",\"establishmentId\":null}"));
var classListPupilJson = await response.Content.ReadAsStringAsync();



// ATTENDANCE



var a = 2;
