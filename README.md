# Reddit

This is sample application for code test.
.Net Core 6.0

Steps to follow
---------------
Open the solution file RedditApp.sln in Visual Studio.
Please set Reddit.Web as startup project in Visual Studio and run the solution.
A UI screen will be launched with a blue Sign In button.
Please click and it will be redirected to Reddit Autorization page.
When you accept authorization, it will take you to the portal screen.
By default, portal screen will be loaded with 100 latest posts from wallstreetbets subreddit if everything is good.
You have an option to change subreddit or number of post on the text box fields location on the top of the screen, hit search to get the result.

Note: If you would like to change ClientId/SecretKey/RedirectUrl, please go to the appsettings.json file location under RedditApp\Reddit.Web
