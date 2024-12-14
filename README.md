# BKS API

This project serves as the backend API for the BKS system. Follow these steps to set up and run the application.

---

## Prerequisites

Ensure the following are installed and running:
- [.NET SDK](https://dotnet.microsoft.com/download) (compatible with the project version)
- [Redis](https://redis.io/download) (default port: **6379**)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (to restore the database)

---

## Setup Instructions

1. **Clone the Repository**  
   Clone the project
   ```bash
   git clone <repository-url>
   
2. **Restore the Database**  
  Restore `codigobookingdb` backup file from the repo into microsoft ssms

3. **Redis**  
  Please have redis run on default port `6379` or please change the desired port in appsettings.json
  
4. **Run the Project**  
   Navigate to the `bks.api` folder and run this command in terminal to run the api:
   ```bash
  
   dotnet run
   
