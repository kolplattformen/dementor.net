# dementor.net
An attempt to create an API for accessing InfoMentor school API

## Why?
As Skolplattformen's time is comming to an end, InfoMentor will be the new system for pupils and parents in Stockholm.
This is an attempt to figure out the InfoMentor API. 

This is not intended to be more than a lab. The results from this work will hopefully end up in Öppna Skolplattformen and Öppna Elevappen.

## Why C#?
Because that's what I am most confortable with and I like the tooling in Visual Studio.

## Login credentials
We have received a test account from InfoMentor (thanks!) to start working. We can not share it here and the login procedure to the Stockholm setup will most certainly differ from the one we have. 

If you have credentials that work and will try it out, create a file called credentials.json alongside the Program.cs file. The file content should look like 
```json
{
  "username": "username",
  "password": "password"
}
```