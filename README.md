# web-voting-sys
## Description
This project is an electronic voting system that enables voters to vote securely from remote sites (rather than centralized polling sites). The system is configurable, allowing users to specify candidates, a date range for voting, and who can vote. The winning number of votes can be determined without waiting until all votes are casted, like in presidential elections.
We built a web-based implementation that is robust, user-friendly, and deployable. The system will eventually include a user interface showing the voting results (e.g., the final votes each candidate received and the winning candidate and possibly the vote statistics by geographic locations or political regions) for which visualizations can be employed to enhance the understanding of results. However, this has not been implemented yet in this prototype.
## Local Usage
1. Ensure the appropriate software is installed. The project was built using Visual Studio and will likely not function using another IDE.
1. Next, open the project and run/build it (Ctrl + F5).
1. The webpage should open correctly, but some of the functionality may not work until database migrations are performed.
   * First, try to make an account by clicking the "Register" button along the banner. If you get an SqlException, click the appropriate button to perform the necessary migration.
   * Next, try to create a poll. If you get another SqlException because database migrations need to be performed, then go to a Command Prompt in Windows and navigate to the project directory (contains Startup.cs). Run the following commands:
     * "dotnet ef database drop --context PollContext"
     * "y"
     * "dotnet ef database update --context PollContext"
   * You should now be able to successfully use the web page.
## General Usage
Once we are confident in our design and have thoroughly tested the site locally, then we will make the site live by using GitHub Pages to host. 
## Team
The team was comprised of Kyler Little, Lance Hoffmann, Jeff Kremer, Tony Yockey, and Robert Belter.
