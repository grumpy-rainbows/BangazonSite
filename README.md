# Welcome to Bangazon!

## Overview

This version of Bangazon implements the Identity framework, and extends the base User object with the `ApplicationUser` model.
It shows how to remove a model's property from the automatic model binding in a controller method by using `ModelState.Remove()`.

### Installing

To clone down our repo into a directory, Open your terminal and enter:

`git clone git@github.com:grumpy-rainbows/BangazonSite.git1`
To open, you'll need Visual Studio installed.

`cd BangazonSite`
`start BangazonSite.sln`
Next, go to Tools > NuGet Package Manager > Package Manager Console. Once open, type in and enter Update-Database in the PMC. Once the database has updated, go to View, select SQL Server Object Explorer. Select your local database and refresh.

### Instructions

You will be presented with a list of the top twenty (20) recently added items on the Bangazon platform. Click on a product to see a more detailed view of each product. But before you do, you need to log in. You may log in as an existing user or create a new account. If you want to log in as an administrator -> Username: admin@admin.com, Password: Admin8*. Once logged in, you can view products and their details, add them to your cart, create a new product to sell, view product categories, and check out your profile settings.

### Built With

C# - The language we used
Azure Data Studio - Cloud for database
ASP.NET MVC - MVC Framework for Web app
Entity Framework - Object Relational Mapper
Identity Framework - Authentication and user related data in ASP.NET MVC

### Authors

* Samuel Britt
* Meag Mueller
* Ali Abdulle
* Selam Gebrekidan
