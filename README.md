# Software Orchestration & Distribution Platform

[![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Enabled-blue?style=for-the-badge&logo=docker)](https://www.docker.com/)
[![Ansible](https://img.shields.io/badge/Ansible-Automation-red?style=for-the-badge&logo=ansible)](https://www.ansible.com/)
[![n8n](https://img.shields.io/badge/n8n-Workflow-orange?style=for-the-badge&logo=n8n)](https://n8n.io/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean-green?style=for-the-badge)](https://github.com/MaedehShahcheraghi)

> An enterprise-grade, containerized orchestration platform designed to automate software distribution and configuration management across multi-VM environments. 

This system leverages a modern architecture to ensure scalability, reliability, and idempotent deployments.

---

## 🏗️ System Architecture

This project follows **Clean Architecture** principles, ensuring a strict separation of concerns between business logic, application rules, and infrastructure components.

* **Core (Domain & Application):** Enterprise logic, system entities, and core service abstractions.
* **Infrastructure (Persistence & External):** SQL Server integration (EF Core), MinIO Object Storage, and dynamic n8n workflow integrations.
* **Presentation (Web API & Razor Dashboard):** Secure RESTful API endpoints and a centralized management console.

---

## 🛠️ Tech Stack

| Category | Technology |
| :--- | :--- |
| **Framework** | .NET 8 (C#) |
| **Automation** | n8n & Ansible Control Node |
| **Storage** | MinIO (Secure Object Storage) |
| **Database** | SQL Server 2022 |
| **Virtualization** | Docker Containers & Oracle VirtualBox / VMware |
| **OS Target** | Ubuntu Linux Nodes |

---

## 🌟 Key Features

* **Automated Package Distribution:** Dynamic deployment and execution of local `.deb` packages fetched from MinIO storage directly to remote target machines.
* **Workflow Orchestration:** Complex installation lifecycles and step-by-step state triggers orchestrated via secure webhooks in n8n.
* **Secure Key-Based Execution:** Passwordless SSH-key authentication ensuring zero-knowledge storage of target server credentials inside the core database.
* **Centralized Storage Ecosystem:** Secure asset handling, binary tracking, and direct URL generation using MinIO bucket storage.
* **Granular Lifecycle Monitoring:** Full-scale tracking and persistence of installation statuses (`Pending`, `In Progress`, `Success`, `Failed`).

---

## 🚀 Getting Started

### 📌 Prerequisites for Target Hosts

To register and manage a target host in the **Orchestration Platform**, the configured SSH user must have passwordless `sudo` privileges. This allows the Ansible control node to execute installation tasks and handle internal packages (`dpkg`) seamlessly.

#### 1. Configure Passwordless Sudo
Log in to your target virtual machine and run the following command once using your deployment user. This creates an isolated and secure sudoers rule for the platform:

```bash
echo "$USER ALL=(ALL) NOPASSWD:ALL" | sudo tee /etc/sudoers.d/orchestration-platform && sudo chmod 0440 /etc/sudoers.d/orchestration-platform