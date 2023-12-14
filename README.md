# Warehouse Management System (WMS) - PRN221 Project

<div align="center">
    <img src="https://github.com/maotou-spy/PRN221_Fall23_3W/blob/main/Database/img/wms-logo.png" alt="Warehouse Management System LOGO" />
</div>

## Table of Contents

<details>
  <summary>WMS Contents</summary>

1. [Overview](#overview)
2. [Technologies Used](#technologies-used)
3. [Features](#features)
4. [Setup Instructions](#setup-instructions)
5. [Contributing](#contributing)
6. [License](#license)
</details>

## Overview

This project is part of the PRN221 course, "Advanced Cross-Platform Application Programming With .NET," Block 3W, during the Fall semester of 2023. The Warehouse Management System (WMS) is designed to efficiently manage various aspects of warehouse operations, including products, categories, inventory movements, storage areas, partners, and user accounts with different roles (admin, manager, storekeeper).

- JS, Razor Page, Bootstrap to build up the front-end
- C# .Net 6, ASP.NET Core, Entity Framework (EF) to build up the back-end
- MS SQL Server to store data

## Technologies Used

<!-- Front-end -->
<div align="center">
  <a href="https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-8.0&tabs=visual-studio" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/Razor%20Page-blue.svg?style=for-the-badge"/></a> 
  <a href="https://html.com/html5" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/HTML5-E34F26.svg?style=for-the-badge&logo=html5&logoColor=white&labelColor=383838"/></a>
  <a href="https://css3.com" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/CSS3-1572B6.svg?style=for-the-badge&logo=css3&logoColor=white&labelColor=2A2A2A"/></a>
  <a href="https://getbootstrap.com" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/Bootstrap-563D7C.svg?style=for-the-badge&logo=bootstrap&logoColor=white&labelColor=563D7C"/></a>
  <a href="https://www.chartjs.org" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/chart.js-F5788D.svg?style=for-the-badge&logo=chart.js&logoColor=white"/></a>
  <a href="#" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/JavaScript-F7DF1E.svg?style=for-the-badge&logo=javascript&logoColor=black"/></a>
</div>
<!-- Back-end -->
<div align="center">
  <a href="https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/dotnet- 6 -red?style=for-the-badge&logo=java&logoColor=white&labelColor=2C2D72"/></a>
  <a href="https://learn.microsoft.com/en-us/aspnet/entity-framework" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/Entity_Framework_(EF)- 6 -red?style=for-the-badge"/></a>
  <a href="https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/ASP_.Net_Core- -green?style=for-the-badge"/></a>
</div>
<!-- Database -->
<div align="center">
  <a href="https://www.microsoft.com/en-us/sql-server" target="blank" rel="noreferrer"><img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white"/></a> 
</div>

## Features

1. **Product Management**

   - Add, edit, and Active/InActive products.
   - Associate products with categories.

2. **Category Management**

   - Create, modify, and Active/InActive product categories.

3. **Inventory Movements**

   - Track and manage the inflow and outflow of products.
   - Record transactions for accurate inventory management.

4. **Storage Area Management**

   - Define and manage storage areas within the warehouse.
   - Assign products to specific storage locations.

5. **Partner Management**

   - Maintain a list of partners involved in warehouse operations.
   - Track partner information and interactions.

6. **User Account Management**
   - Admin: Full access to view all view and manage user accounts.
   - Manager: Access to most features, excluding administrative capabilities.
   - Warehouse Keeper: Limited access for inventory management.

## Setup Instructions

1. **Clone the Repository**

```bash
git clone https://github.com/maotou-spy/PRN221_Fall23_3W.git
```

2. **Install Dependencies**

```bash
cd PRN221_Fall23_3W
dotnet restore
```

3. **Database Setup**

- Create a SQL Server database and update the connection string in appsettings.json.
- Run EF migrations to create the database schema.

```bash
dotnet ef database update
```

4. **Run the Application**

```bash
Copy code
dotnet run
```

5. **Access the Application**
   Open your web browser and navigate to http://localhost:5070.

## Contributing

Meet the Team:

- [Bùi Hữu Phúc](https://github.com/maotou-spy)
- [Bùi Hữu Đức](https://github.com/baemgao)
- [Lê Đình Duy An](https://github.com/Anxiousz)
- [Nguyễn Văn Tiến](https://github.com/nvtiendev)
- [Trương Lê Hiếu Trọng](https://github.com/Code-dudu)

## License [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

This project is licensed under the MIT License.
