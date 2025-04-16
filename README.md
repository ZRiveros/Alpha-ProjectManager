# 🌐 Alpha – Project Management Portal

A clean and modern project dashboard built using **ASP.NET Core Razor Pages**.

This is a web-based project management tool built using **ASP.NET Core Razor Pages**, designed to help users create, view, update, and manage projects. The project follows a clean UI inspired by a Figma design.

---

## 🚀 Features

- User registration and login with **ASP.NET Identity**
- Project listing and filtering by status (**All / Started / Completed**)
- Create, edit, and delete projects with modal-based UI
- Rich text editor for project descriptions using **Quill.js**
- Form validation with **JavaScript**
- Secure routes protected via **[Authorize]** attribute
- Service layer with **Entity Framework Core** (Code-First)

---

## 🔧 Technologies Used

- ASP.NET Core Razor Pages  
- Entity Framework Core  
- Microsoft Identity (Individual Accounts)  
- SQL Server / LocalDB  
- HTML, CSS (custom), Bootstrap Icons  
- JavaScript (modals, form validation)  
- Quill.js (rich text editor)  

---

## 📁 Folder Structure

- `Pages/Account` – Login, Register, Logout pages  
- `Pages/Portal` – Project dashboard and functionality  
- `Models` – Entity classes (e.g. Project)  
- `Services` – Business logic and database operations  
- `wwwroot` – Static assets (CSS, JS, images)  
- `Docs/screenshots` – Demo screenshots  

---

## 🖼️ Screenshots

### 🔐 Login  
![Login](Docs/screenshots/Login.png)

### 📝 Register  
![Register](Docs/screenshots/Register.png)

### 📋 Project List  
![Projects](Docs/screenshots/Projectsite.png)

### ➕ Add Project  
![Add Project](Docs/screenshots/AddProject.png)

### ✏️ Edit Project  
![Edit Project](Docs/screenshots/EditProject.png)

### 🔍 Filtering Projects  
![Filtering](Docs/screenshots/FilteringProjects.png)

### 🚪 Logout  
![Logout](Docs/screenshots/Logout.png)

---

## 🛠️ Setup

1. **Clone the repository**
```bash
git clone https://github.com/YourUsername/Alpha-ProjectManager.git
