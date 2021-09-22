###Andrei AspNetSandbox project for backend course

name | value
--- | ---
language | c#
database | postgres
deployed | https://asp-sandbox-update.herokuapp.com/

#Image with working app

![alt text](https://github.com/[DirleaAndrei]/[AspNetSandbox]/blob/[master]/images/andrei.jpg?raw=true)

# How to run in Docker from the commandline

## Run this commands in [AspNetSanbox](AspNetSandbox) project

Build in container
```
docker build -t asp_net_with_db .
```

to run

```
docker run -d -p 8081:80 --name web_container_andrei asp_net_with_db
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
heroku container:push -a asp-sandbox-update web
```

Release the container
```
heroku container:release -a asp-sandbox-update web
```