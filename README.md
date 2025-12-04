1. I'm assuming that https://hacker-news.firebaseio.com/v0/beststories.json returns best stories sorted by their score in descending order.
2. There is no error handling, e.g. user requests best 3 stories, but API returns details only for 2 of them.
There should be returned some extra structure with ID of the best story and details retrieval status.
Some other error scenarios should also be taken into account. 
3. I'm assuming that conversion to unix time can cause problems and in case of errors returned time would be in original format as it comes from hacker-news.firebaseio.com API.
