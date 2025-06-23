# best-stories
Best Stories
 project designed to retrieve the best n stories from the Hacker News API: https://hacker-news.firebaseio.com/v0/beststories.json 

Projects
DevCodingTest-StoriesAPI
This repository contains the complete codebase to fulfill all project requirements.
By default, the project runs on port 7005 at the following route:
https://localhost:7005/weatherforecast

API Details
The API includes a single controller action:
GET GetStories, which accepts a parameter best={int}.

- The "best" parameter specifies the number of stories the API should return.
- If the "best" parameter is not provided, the API returns 10 stories by default.

TODO:
- Implement security to restrict access to API resources.
- Add logging to track all activity and capture errors.
- Integrate a caching library to improve API performance.