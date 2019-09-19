# W2V.Posts
A. Posts score calculation: (based on http://ryancompton.net/2016/08/07/upvotes-over-time-by-subreddit-or-why-the_donald-is-always-on-the-front-page-of-reddit/)
anking on reddit is determined using a combination of upvotes and the age of the post at the time of each vote (cf. here, here, and here for some good explanations). 
In short, the ranking of a submission is set by the rating function

f(n,t)=45000log10(n)+t
where n is upvotes and t is the number of seconds which elapsed between the post’s creation time and 7:46:43 am December 8th, 2005.

More recent posts have a larger t which translates to a better ranking. 
Thus, the best way to get your post to the front page is to upvote aggressively when the post is very young.

B. Top List endpoint: https://localhost:<port>/api/posts/TopPosts/?numOfPosts=x
The top list endpoint get the number of posts as a prameter and display y posts sorted decending by Score(see 1), when 0 <= y <= x. 


C. Instructions on how to run the project (I assume that docker installed on machine.)
1. Please run following command 'docker pull redis' on command line for install Redis container (https://hub.docker.com/_/redis)
2. than run redis from command line 'docker run -d -p 8080:6379 redis'
3. Run project from IDE (https://github.com/ItzikAscapa23/W2V.Posts), sorry I don't have enough time to do that with Docker :(

