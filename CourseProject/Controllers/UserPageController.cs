using Atlassian.Jira;
using CloudinaryDotNet;
using CourseProject.Data;
using CourseProject.Models;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Salesforce.Common;
using Salesforce.Force;
using System.Drawing.Printing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using static System.Net.WebRequestMethods;

namespace CourseProject.Controllers;

public class UserPageController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly HttpClient _httpClient;


    public UserPageController(UserManager<User> userManager)
    {
        _userManager = userManager;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://kkaliuzhny.atlassian.net");
       
        string token = Environment.GetEnvironmentVariable("JiraApiToken");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"kkaliuzhny@gmail.com:"+token)));
    }

    [HttpGet]
    public IActionResult ShowPage()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CreateAccount()
    {
        return View();
    }

    [HttpGet]
    public IActionResult NewTicket(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateTicket(string Summary, string Priority, string returnUrl)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var newUser = new
        {
            displayName = user.UserName,
            emailAddress = user.Email,
            name = user.UserName,
            password = user.PasswordHash,
            products = new[] { "jira-software" }

        };
        var response = await _httpClient.PostAsync("rest/api/3/user",
             new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));
        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException($"Failed to create user: {responseContent}");
        }
        var createdUser = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
        string createdUserEmail = createdUser.accountId;

        var requestBody = new
        {
            accountId = createdUserEmail
        };
        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var addUserToGroupResponse = await _httpClient.PostAsync($"rest/api/3/group/user?groupname=jira-users", content);


        if (!addUserToGroupResponse.IsSuccessStatusCode)
        {
            var groupResponseContent = await addUserToGroupResponse.Content.ReadAsStringAsync();
            if (groupResponseContent.Contains("Cannot add user. User is already a member"))
            {
            }
            else
            {
                throw new InvalidOperationException($"Failed to assign user to group: {groupResponseContent}");
            }
        }
        var ticketData = new
        {
            fields = new
            {
                project = new { key = "SCRUM" },
                issuetype = new { name = "Bug" },
                summary = Summary,
                priority = new { name = Priority },
                customfield_10037 = "title", 
                customfield_10038 = returnUrl,
                reporter = new { accountId = createdUserEmail }
            }
        };
        var json = JsonConvert.SerializeObject(ticketData);
        var ticketCreationResponse = await _httpClient.PostAsync("rest/api/3/issue",
            new StringContent(json, Encoding.UTF8, "application/json"));
        if (!ticketCreationResponse.IsSuccessStatusCode)
        {
            var errorResponse = await ticketCreationResponse.Content.ReadAsStringAsync();
            throw new Exception($"Error creating ticket: {errorResponse}");
        }
        var ticketResponseContent = await ticketCreationResponse.Content.ReadAsStringAsync();
        var createdTicket = JsonConvert.DeserializeObject<dynamic>(ticketResponseContent);
        string ticketUrl =  $"https://kkaliuzhny.atlassian.net/browse/{createdTicket.key}";
        ViewData["ticketUrl"] = ticketUrl;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ShowTickets()
    {
        int page = 1;
        int ticketsAmountOnPage = 10;
        int totalTicketsAmount = 0;
        var user = await _userManager.GetUserAsync(HttpContext.User);
        string email = user.Email;
        string encodedEmail = Uri.EscapeDataString(email);
        var query = $"rest/api/3/search?jql=reporter=\"{encodedEmail}\"&startAt={(page - 1) * ticketsAmountOnPage}&maxResults={ticketsAmountOnPage}";
        var response = await _httpClient.GetAsync(query);
        JArray tickets = null;
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonResponse);
            tickets = (JArray)jsonObject["issues"];
            totalTicketsAmount = (int)jsonObject["total"];
        }
        ViewBag.Page = page;
        ViewBag.PageSize = ticketsAmountOnPage;
        ViewBag.TotalPagesAmount = (int)Math.Ceiling((double)totalTicketsAmount / ticketsAmountOnPage);
        return View(tickets);
    }


        private async Task<string> GetAccountTokens()
        {
            string consumerKey = Environment.GetEnvironmentVariable("consumerSalesforceKey");
            string consumerSecret = Environment.GetEnvironmentVariable("consumerSalesforceSecret");
            string username = "klimk@gmail.com";
            string password = Environment.GetEnvironmentVariable("salesforcePassword");
       
            var authClient = new AuthenticationClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            await authClient.UsernamePasswordAsync(
                consumerKey,
                consumerSecret,
                username,
                password
            );
            var accessToken = authClient.AccessToken;
            var instanceUrl = authClient.InstanceUrl;
            return accessToken;
        }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(SalesforceUserAccountModel salesforceUser)
    {
        var accessToken = GetAccountTokens();
        var instanceUrl = "https://bntu-dev-ed.develop.my.salesforce.com";
        string apiVersion = "v62.0";
        var client = new ForceClient(instanceUrl, await accessToken,apiVersion);


        var account = new
        {
            Name = $"{salesforceUser.FirstName}&{salesforceUser.LastName}"
        };
     
        try
        {
            var accountId = await client.CreateAsync("Account", account);
            var contact = new
            {
                salesforceUser.FirstName,
                salesforceUser.LastName,
                salesforceUser.Email,
                AccountId = accountId.Id
            };

            var contactId = await client.CreateAsync("Contact", contact);
            return View("SuccessfulAuthorization");
        }

        catch (ForceException ex)
        {

            if (ex.Message.Contains("Use one of these records?"))
            {
                ViewBag.ErrorMessage = "You have already created an account and a contact";
              
            }
        }
        return View();
    }

  

}