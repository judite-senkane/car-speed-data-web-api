# Car Speed Data App
## Welcome!

<p>Happy to see you here and checking out my project. It is a asp.Net Core Web Api project with React.ts frontend. It uses the local Microsoft SQL Server and Entity Framework. Here are a few steps to open the application and see it working!</p>

<p>My name: Judīte Senkāne</p>

### Step By Step Guide
1. Clone this project and open the folders on your computer
1. Open the CarSpeedDataApp.sln file in CarSpeedDataApp project folder in Visual Studio
1. Open the car-speed-data-app-react folder in your Visual Studio Code
1. Make sure you have MS SQL Server downloaded on your computer.
1. In Visual Studio (or other IDE) Package Manager Console choose CarSpeedDataApp.Data as the default project and add the following commands:

		-add-migration (to create a database for this project on your MS SQL Server)
		-update-database (to create tables in the sql server)

1. In Visual Studio Code, run ```npm install``` in the terminal
1. Now run the Visual Studio Web Api project. Ignore the Swagger window that appears
1. Start the frontend in Visual Studio Code by running ```npm start``` in the terminal
1. Now you can navigate to UploadFile page and upload your first file with the data. I have a sample data file in the repository. The upload for the sample data should take 2-3 minutes. There will be a message with "File uploaded." once it is completed.
1. Enjoy the website!

