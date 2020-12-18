#👓 Project Introduction

PlanShift is my defense project for ASP.NET Core course at SoftUni (September-2020). The website's purvose is to help small to medium sized businesses, like restaurants and bars to manage their shifts in the organization.

### ✏️ Overview
PlanShift is a website in which a user can create a profile and further create businesses, every business has it's owner and it's groups, but every user can participate in as many businesses and groups as he wants. 

The business management is seperate in three administration groups, created with the creation of a business every person in this groups has certain powers in the website. 

The three groups and their responsibilities are:


"Hr Managers": can create groups and invite new people to a group, which are already users or not, as an automatic invitation email is send to the non-users, added to the given group imedietely after registration. They can also remove people from a given group. 


"Schedule Managers": responsible for schedule creation, they can create or delete shifts, accept shift applications and shift requests.


"Admins": everything from the two groups above.


If a user is not participating in the groups mentioned above, but any other one, they can apply for shifts for the groups they are participating in, apply for shfit swaps with colleagues or accept shift swaps towards their shifts.


There is also group chat integrated.


### 🔨 Built With
- ASP.NET 5.0
- ASP.NET view components
- MSSQL Server
- Cache
- Sessions
- SignalR
- HangFire
- SendGrid
- Bootstrap
- AJAX real-time Requests
- jQuery
- Moq
- AutoMapper
- FullCalendar
- MomentJS


### Deployed: https://planshift.azurewebsites.net/
- Azure MSSQL Server
- Application Insights

## DB Diagram
https://ibb.co/4t8b4d8
