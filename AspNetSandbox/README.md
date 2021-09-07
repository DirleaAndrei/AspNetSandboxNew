## How to run in Docker from the commandline

Build in container
```
docker build -t asp_net .
```

to run

```
docker run -d -p 8081:80 --name web_container_andrei asp_net
```

to stop container
```
docker stop web_container_andrei
```

to remove container
```
docker rm web_container_andrei
```

## Deploy to heroku

1. Create heroku account
2. Create application
3. Make sure application works locally in Docker


Login to heroku
```
heroku login
heroku container:login
```

Push container
```
heroku container:push -a asp-net-sandbox-first web
```

Release the container
```
heroku container:release -a asp-net-sandbox-first web
```