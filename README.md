# Hacker News API - Best Stories

This project is a RESTful API that retrieves the details of the first n "best stories" from the Hacker News API. It exposes an endpoint that returns an array of the best stories, sorted by their score in descending order.

## How to Run

To run the application, please follow these steps:

1. Make sure you have [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
2. Clone this repository.
3. Open a terminal or command prompt and navigate to the project's root directory.
4. Run the following command to build the project:
   ```shell
   dotnet build
   dotnet run
5. Move to http://localhost:{port}/api/stories/{n}

## Assumptions

The following assumptions were made while implementing the application:
1. The application retrieves the story IDs from the Hacker News API's beststories.json endpoint and then fetches the details of each story using the item/{storyId}.json endpoint.

## Implementation Details
The project is implemented using ASP.NET Core with C# and follows the RESTful principles. The application uses the HttpClient to make requests to the Hacker News API and retrieves the best story IDs and their details. The retrieved data is mapped to the Story model and returned as a JSON response.

The project is structured with the following classes:

1. Program.cs: Contains the entry point of the application.
2. Controllers/StoriesController.cs: Implements the API endpoint to retrieve the best stories.
3. Services/HackerNewsService.cs: Provides methods to fetch data from the Hacker News API.
4. Models/Story.cs: Represents the model for a story.

## Potential Enhancements and Changes
Given more time, the following enhancements or changes could be considered:

1. Implementing a more comprehensive caching strategy: Currently, the application uses in-memory caching to improve performance. However, for a production environment, a distributed caching mechanism, such as Redis, could be utilized for better scalability and resilience.

2. Implementing pagination: If the number of best stories is large, it might be useful to implement pagination in the API to retrieve stories in smaller chunks and improve the response time.

3. Adding error handling and logging: Enhance the application to handle and log errors effectively, providing appropriate responses and logging useful information for troubleshooting and monitoring purposes.

4. Implementing additional features: Depending on the requirements, additional features such as filtering, searching, or sorting options could be added to enhance the functionality of the API.
