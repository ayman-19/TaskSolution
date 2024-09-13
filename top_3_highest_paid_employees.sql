
   WITH GetTop3Rows AS (
    SELECT 
      "EmployeeId",
    "Name",
    "Department",
    "Salary",
        DENSE_RANK() OVER (PARTITION BY "Department" ORDER BY "Salary" DESC) AS "GetSalary"
    FROM 
        "Employees" 
)
SELECT 
    "EmployeeId",
    "Name",
    "Department",
    "Salary"
FROM 
    GetTop3Rows
WHERE 
    "GetSalary" <= 3
ORDER BY 
    "Department", "GetSalary";