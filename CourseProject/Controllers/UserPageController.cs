using CourseProject.Data;
using CourseProject.Models;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Salesforce.Common;
using Salesforce.Force;
using System.Net;
using static System.Net.WebRequestMethods;

namespace CourseProject.Controllers;

public class UserPageController : Controller
{
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