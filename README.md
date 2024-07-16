# Product Inventory Management System

The Product Inventory Management System is a web-based application developed using ASP.NET Core to streamline and manage product data efficiently. It incorporates key features such as routing, database connections, CRUD operations, stored procedures, and Excel file export functionalities, ensuring robust and scalable inventory management.




## Tech Stack

**Client:** Html , CSS

**Server:** Dot Net (.NET) Core Framework

## Prerequisite Software Required
- Visual Studio IDE
- .NET CORE Version 8 


## Required Packages
- Microsoft.EntityFrameworkCore (8.0.6)
- Microsoft.EntityFrameworkCore.SQLServer (8.0.6)
- Microsoft.EntityFrameworkCore.Tools (8.0.6)
- EPPlus (7.2.1)

## Features

CRUD OPERATIONS
- GET Product LIST  ("/Products")
- GET Product BY ID ("/Products/{id}")
- ADD A New Product ("/Products/Create")
- DELETE A Product  ("/Products/Delete/{id}")
- UPDATE A Product  ("/Products/Edit/{id}")
## HTTP METHODS USED

- GET 
- POST 
- PUT
- DELETE


## DATABASE Schema
Microsoft SQL Server was used to Host Database

- Database name : **ProductDB**
- Tables Count  : 1
- Table Name    : **Products**
Columns in Products Table 
    ProductID (int, Primary Key, Identity)
    Name (nvarchar(100))
    Category (nvarchar(100))
    Price (decimal)
    StockQuantity (int)
## Installation


```bash
git clone https://github.com/shrirang010/Product-Inventory-Management-System.git

cd Product-Inventory-Management-System
```

## Screenshots
- Get Product List
![image](https://github.com/user-attachments/assets/b8e15365-3990-4029-a83b-98ad656c01cd)

- Get Product By id
![image](https://github.com/user-attachments/assets/7729d0ef-e07d-4ae0-be89-6d571ed3efcc)

- Add Product
  ![image](https://github.com/user-attachments/assets/4e044269-464b-4bb3-8379-d325dee9eaca)

- Delete Product
![image](https://github.com/user-attachments/assets/ad89c317-c031-4ecb-a7fc-e9760783a129)

- Edit Product
    ![image](https://github.com/user-attachments/assets/581a04d4-99e9-4366-b120-0afe030f0bba)

