# Project screenshots

- the page view:

<img src="https://raw.githubusercontent.com/RobertNeat/identity_rest_api/master/foxes_list_page.png" width="500px" heigth="auto"/>



- creating the admin account:

It is made through registering the new user in the browser in the "register page" with "@example.com" domain.
To use account you have to restart the app

<img src="https://raw.githubusercontent.com/RobertNeat/identity_rest_api/master/register_page.png" width="500px" heigth="auto"/>

<img src="https://raw.githubusercontent.com/RobertNeat/identity_rest_api/master/admin_jwt_authentication.png" width="500px" heigth="auto"/>


- creating the user account:

It is mage through registering the new user in the browser in the "register page" (with domain any other than the "@example.com").
To use account you have to restart the app.

<img src="https://raw.githubusercontent.com/RobertNeat/identity_rest_api/master/register_page.png" width="500px" heigth="auto"/>

<img src="https://raw.githubusercontent.com/RobertNeat/identity_rest_api/master/user_jwt_authentication.png" width="500px" heigth="auto"/>

- creating new fox in the list:

To create the new fox you have to be validated by JWT token with admin role

<img src="https://raw.githubusercontent.com/RobertNeat/identity_rest_api/master/admin_add_new_fox.png" width="500px" heigth="auto"/>

- loving/hating the fox:

To love/hate the fox you have to be validated by JWT token (any valid token)

<img src="https://raw.githubusercontent.com/RobertNeat/identity_rest_api/master/user-admin_love_fox.png" width="500px" heigth="auto"/>

- getting all the foxes list (no token):

<img src="https://raw.githubusercontent.com/RobertNeat/identity_rest_api/master/get_all_foxes.png" width="500px" heigth="auto"/>


# Special dependencies:

- .net 6

- added packages (*only if the project don't run):

```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.0
dotnet add package Microsoft.AspNetCore.Identity.UI --version 6.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0
```

- the project may use the LiteDB database (it was based on the project that used database and this don't, because the list of foxes is "in memory")

