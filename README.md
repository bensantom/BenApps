# Reddit

This is a sample web application for code evaluation developed in .Net Core 6.0 and MVC

Steps to follow
Open the solution file RedditApp.sln in Visual Studio. 
Please set Reddit.Web as a startup project in Visual Studio and run the solution. 
A UI screen will be launched with a blue Sign-in button. 
Please click and it will be redirected to the Reddit Authorization page. 
When you accept authorization, it will take you to the portal screen. 
By default, a portal screen will be loaded with the 100 latest posts from wallstreetbets subreddit, if everything is good, and it refreshes every minute. 
You have an option to change the subreddit or number of posts on the text box field location on the top of the screen, hit search to get the result.

Note: If you would like to change ClientId/SecretKey/RedirectUrl, please go to the appsettings.json file location under RedditApp\Reddit.Web
The working model is available here:  https://test.mrbenb.com/
