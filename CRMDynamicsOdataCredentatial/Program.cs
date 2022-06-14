using System.Net;
using System.Net.Http.Headers;

// SUMMARY: CRM Dynamics 2015 or later on premise OData acces
// if youra using dynamics crm on premise with active directory and dont active directory factory service or Azure AD
// u can use Azure AD with on-premise for credentatial (recomended) or active diretory factory service
// this is the 3. option that use credendatial 
// planning to create .net core library for remote user and role based soon..


string userName = "yourUserName";
string password = "yourPasword";
string domain = "domain";
string baseAdd = "yourODataUrl"; // something like http://<domainbaseUrl>/<domain>/api/data/v8.0/accounts

getNewHttpClient(userName, password, domain, baseAdd);

var credentials = new NetworkCredential(userName, password);

var client = getNewHttpClient(userName, password, domain, baseAdd);
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
var response = client.GetAsync("accounts?$top=1").Result;
if (response.IsSuccessStatusCode)
{
    var yourcustomobjects = response.Content.ReadAsStringAsync();
}


 HttpClient getNewHttpClient(string userName, string password, string domainName, string webAPIBaseAddress)
{
    HttpClient client = new HttpClient(new HttpClientHandler() { Credentials = new NetworkCredential(userName, password, domainName) });
    client.BaseAddress = new Uri(webAPIBaseAddress);
    client.Timeout = new TimeSpan(0, 2, 0);
    return client;
}