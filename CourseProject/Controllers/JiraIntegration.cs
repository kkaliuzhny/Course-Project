using Atlassian.Jira;

namespace CourseProject.Controllers
{
    public class JiraIntegration
    {
        private readonly Jira jira;
        public JiraIntegration(string jiraBaseUrl, string username, string apiToken)
        {
            jira = Jira.CreateRestClient(jiraBaseUrl, username, apiToken);
        }
        public async Task<JiraUser> CreateUser(string username, string password, string emailAddress, string displayName)
        {
            
            var newUser = new JiraUserCreationInfo
            {
                Username = username,
                Password = password,
                Email = emailAddress,
                DisplayName = displayName,
            };

            
           return  await jira.Users.CreateUserAsync(newUser);
        }
        public async Task<Issue> CreateJiraTicketAsync(string projectKey, string priority,string summary, JiraUser jiraUser)
        {
            var issue = jira.CreateIssue(projectKey);
            issue.Priority = priority;
            issue.Reporter = jiraUser.DisplayName;
            await issue.SaveChangesAsync();
            return issue;
        }

    }
}
