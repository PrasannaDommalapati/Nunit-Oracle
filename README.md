# Oracle DB Testing with NUnit and Dapper

This repository contains NUnit tests to verify Oracle database connectivity and queries using Dapper.

## Prerequisites

Before running the tests, ensure you have the following installed:

- **.NET SDK (6.0 or later)** – [Download Here](https://dotnet.microsoft.com/en-us/download)
- **Oracle Database** (or an accessible Oracle DB instance)
- **NuGet Packages**:
  - `NUnit`
  - `NUnit3TestAdapter`
  - `Microsoft.NET.Test.Sdk`
  - `Dapper`
  - `Oracle.ManagedDataAccess.Core`
  - `Microsoft.Extensions.Configuration`
  - `Microsoft.Extensions.Configuration.Json`
  - `Microsoft.Extensions.Configuration.Binder`

## Setup Instructions

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/your-repo/oracle-db-tests.git
cd oracle-db-tests
```

### 2️⃣ Configure Database Connection
1. Open `appsettings.json`.
2. Update the **OracleDB connection string**:

```json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=your_user;Password=your_password;Data Source=your_oracle_db"
  }
}
```

### 3️⃣ Install Dependencies
Run the following command in the project root:
```sh
dotnet restore
```

### 4️⃣ Run the Tests
Execute the NUnit tests using:
```sh
dotnet test
```

You should see output similar to:
```
Passed! - Test_OracleConnection_ShouldBeSuccessful
Passed! - Test_SelectQuery_ShouldReturnResults
```

## Test Structure

| Test Name | Description |
|-----------|-------------|
| `Test_OracleConnection_ShouldBeSuccessful` | Checks if the Oracle DB connection opens successfully. |
| `Test_SelectQuery_ShouldReturnResults` | Executes a `SELECT` query to verify data retrieval. |
| `Test_InsertQuery_ShouldInsertData` | Inserts a row into the database and validates success. |
| `Test_DeleteQuery_ShouldDeleteData` | Deletes a test row and verifies deletion. |

## Troubleshooting

- **Connection Errors?**
  - Verify that the Oracle DB is running and accessible.
  - Ensure credentials and Data Source are correct in `appsettings.json`.
  
- **Test Failures?**
  - Check if required database tables exist and contain data.
  - Ensure your Oracle instance allows external connections.
  
## License
This project is licensed under the MIT License.

