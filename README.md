# Core Cooking

This is a simple cooking recipe website using .Net Core 2.0. It uses Azure storage as the datastore by storing json files. This is done so it can be hosted for free.

It was written to explore Asp.net Core MVC and Boostrap 4.

Technologies used:
- Core 2.1 
- Asp.Net MVC 
- Azure
- Azure Storage
- Bootstrap 4
- Cookie based Authentication
- ImageSharp

# Setup Dev Environment
* The project runs in Visual Studio 2017
1. You will need to manually add a appsettings.UserSecrets.json file to the CoreCooking.Models project root.
  (This is because user secrets don't work on )

2. Populate this file with a link to the database.
