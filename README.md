# Car Speed Data App
## Welcome!

<p>Happy to see you here and checking out my project. It is a asp.Net Core Web Api project with React.ts frontend. Here are a few steps to open the application and see it working!</p>

<p>My name: Judīte Senkāne</p>

### Step By Step Guide
1. Clone this project and open the folders on your computer
1. Open the [CarSpeedDataApp.sln](https://github.com/judite-senkane/car-speed-data-web-api/blob/car-speed-data/CarSpeedDataApp/CarSpeedDataApp.sln) file in Visual Studio
1. Open the [car-speed-data-app](https://github.com/judite-senkane/car-speed-data-web-api/tree/car-speed-data) in your Visual Studio Code
1. Make sure you have MS SQL Server downloaded on your computer
1. In Visual Studio, make sure that these packages are installed: 

		-Microsoft.AspNet.WebApi.Cors (added to CarSpeedDataApp project)
		-Microsoft.AspNetCore.Http.Features (added to CarSpeedDataApp.Core project)
		-Microsoft.EntityFrameworkCore (added to CarSpeedDataApp.Data project)
		-Microsoft.EntityFrameworkCore.Design (added to CarSpeedDataApp.Data and CarSpeedDataApp projects)
		-Microsoft.EntityFrameworkCore.Tools (added to CarSpeedDataApp.Data project)
		-Microsoft.EntityFrameworkCore.SqlServer (added to CarSpeedDataApp.Data project)

1. In Visual Studio Package Manager Console choose CarSpeedDataApp.Data as the default project and add the following commands:

		-add-migration (to create a database for this project on your MS Sql Server)
		-update-database (to create tables in the sql server)

1. In Visual Studio Code, run ```npm install``` in the terminal
1. Now start the Visual Studio Web Api project. Ignore the Swagger window that appears
1. Start the frontend in Visual Studio Code by running ```npm start``` in the terminal
1. Now you can navigate to UploadFile page and upload your first file with the data. I have a sample data in the repository. The upload for the sample data should take 2-3 minutes. There will be a message with "File uploaded." once it is completed.
1. Enjoy the website!

