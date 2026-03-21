# 🚀 Software Orchestration & Distribution Platform

[![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Enabled-blue?style=for-the-badge&logo=docker)](https://www.docker.com/)
[![Ansible](https://img.shields.io/badge/Ansible-Automation-red?style=for-the-badge&logo=ansible)](https://www.ansible.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean-green?style=for-the-badge)](https://github.com/MaedehShahcheraghi)

An enterprise-grade, containerized orchestration platform designed to automate software distribution and configuration management across multi-VM environments. [cite_start]This system leverages a modern **Microservices** architecture to ensure scalability, reliability, and idempotent deployments[cite: 36, 44].

---

## 🏗 System Architecture
[cite_start]This project follows **Clean Architecture** principles, ensuring a strict separation of concerns between business logic, application rules, and infrastructure[cite: 44, 49].

- [cite_start]**Core (Domain & Application):** Enterprise logic, entities, and service interfaces[cite: 46].
- [cite_start]**Infrastructure (Persistence & External):** SQL Server integration (EF Core), MinIO Object Storage, and n8n workflow triggers[cite: 48, 51].
- [cite_start]**Presentation (Web API & Razor Dashboard):** RESTful endpoints and a centralized management console[cite: 44].

---

## 🛠 Tech Stack
| Category | Technology |
| :--- | :--- |
| **Framework** | [cite_start].NET 8 (C#) [cite: 49] |
| [cite_start]**Automation** | n8n & Ansible [cite: 37, 48] |
| **Storage** | [cite_start]MinIO (Object Storage) [cite: 48, 51] |
| **Database** | [cite_start]SQL Server 2022 [cite: 48, 51] |
| **Virtualization** | [cite_start]Docker & VMware Workstation [cite: 48, 49] |
| **OS Target** | [cite_start]Ubuntu Linux Nodes [cite: 50] |

---

## 🌟 Key Features
- [cite_start]**📦 Automated Deployment:** One-click software installation on remote Linux nodes using Ansible playbooks[cite: 44, 45].
- [cite_start]**🔄 Workflow Orchestration:** Complex installation sequences managed via n8n workflows[cite: 46].
- [cite_start]**☁️ Centralized Storage:** Securely store and version software binaries (deb, rpm, tar.gz) in MinIO[cite: 51].
- [cite_start]**📋 Detailed Audit Logs:** Comprehensive tracking of every installation step and status[cite: 44].
- [cite_start]**🐳 Dockerized Environment:** Fully containerized services for consistent deployment[cite: 36, 46].

---

## 🚦 Getting Started

### Prerequisites
- **Docker Desktop**
- **.NET 8 SDK**
- **SQL Server 2022**

### Installation
1. **Clone the repository:**
   ```bash
   git clone [https://github.com/MaedehShahcheraghi/OrchestrationPlatform.git](https://github.com/MaedehShahcheraghi/OrchestrationPlatform.git)