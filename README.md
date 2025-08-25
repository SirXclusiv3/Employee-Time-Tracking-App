# Employee-Time-Tracking-App
It is a console App that monitores directory where employees drop their clock in/clock-out files. then it calculates total working hours over time and late arrivals . I t also generates weekly reports automatically 


# Employee Time Tracking System

## 📌 Overview
This is a C# console application that helps an admin manage employees’ work hours.  
The system allows clocking in and out, storing logs, and generating different reports such as weekly, monthly, overtime, and late arrivals.  

It also supports exporting reports to **CSV format** for easier data analysis.

---

## ⚙️ Requirements
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or newer  
- .NET 6.0 (or higher)  

---

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/employee-time-tracking.git
```

### 2. Open in Visual Studio
- Open the `Project.sln` file in Visual Studio.  

### 3. Build and Run
- Run the project with `F5`.  
- Log in with the default admin account:  
  ```
  Username: admin
  Password: admin123
  ```

---

## 🛠 Features
- 🔑 **Admin login system** (default: `admin` / `admin123`)  
- 👨‍💼 **Add employees** (Full-time or Part-time)  
- 🕒 **Clock In / Clock Out** with input validation  
- 📊 **Reports**:
  - Weekly report  
  - Monthly report  
  - Overtime report  
  - Late arrivals report  
- 📤 **Export reports to CSV**  
- ⚡ **Auto-report generation** (runs in a background thread)  
- 📂 **File watching** to detect logs in a specified directory  

---

## 📂 Project Structure
```
Project/
│-- Project.sln                # Solution file
│-- Project/Project.csproj     # Project file
│-- Program.cs                 # Main entry point
│-- Admin.cs                   # Admin logic
│-- Employee.cs                # Employee base class
│-- FullTimeEmployee.cs        # Full-time employee class
│-- PartTimeEmployee.cs        # Part-time employee class
│-- TimeLog.cs                 # Represents time entries
│-- TimeLogManager.cs          # Manages logs and validation
│-- ReportGenerator.cs         # Handles report creation/export
│-- FileChecker.cs             # Monitors file directory for logs
│-- AutoReportThread.cs        # Background auto-report thread
│-- Exceptions/                # Custom exception classes
```

---

## 🧑‍💻 OOP Principles Used
- **Encapsulation** → Employees manage their own clock-in/out times.  
- **Inheritance** → `FullTimeEmployee` and `PartTimeEmployee` inherit from `Employee`.  
- **Polymorphism** → Different employee types override behaviors as needed.  
- **Abstraction** → Interfaces/abstract classes separate report generation logic.  

---

## 📸 Sample Run (Console)
```
=== Employee Time Tracking System ===
Username: admin
Password: admin123

Main Menu:
1. Add Employee
2. Clock In
3. Clock Out
4. Generate Report
5. Export Report to CSV
6. Exit
```

---

## 🤝 Contributing
Pull requests are welcome. For significant changes, please open an issue first to discuss what you’d like to improve.  

---

## 📜 License
This project is licensed under the MIT License.  
