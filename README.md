# ?? Orchestration & Software Distribution Platform

[![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Enabled-blue)](https://www.docker.com/)
[![Ansible](https://img.shields.io/badge/Ansible-Automation-red)](https://www.ansible.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

An enterprise-grade, containerized orchestration platform designed to automate software distribution and configuration management across multi-VM environments. This system leverages a modern Microservices architecture to ensure scalability, reliability, and idempotent deployments.

---

## ?? System Architecture
This project follows **Clean Architecture** principles, ensuring a strict separation of concerns between business logic, application rules, and infrastructure.

- **Core (Domain & Application):** Enterprise logic, entities, and service interfaces.
- **Infrastructure (Persistence & External):** SQL Server integration (EF Core), MinIO Object Storage, and n8n workflow triggers.
- **Presentation (Web API & Razor Dashboard):** RESTful endpoints and a centralized management console.



## ?? Tech Stack
* [cite_start]**Framework:** .NET 8 (C#) [cite: 7, 19]
* [cite_start]**Orchestration & Automation:** n8n & Ansible [cite: 7, 14]
* [cite_start]**Storage:** MinIO (Object Storage for Software Packages) [cite: 7, 21]
* [cite_start]**Database:** SQL Server 2022 [cite: 18, 21]
* [cite_start]**Containerization:** Docker & Docker Compose [cite: 6, 20]
* [cite_start]**Environment:** VMware Workstation (Multi-VM Ubuntu Nodes) [cite: 19, 20]

## ?? Key Features
- **Automated Deployment:** One-click software installation on remote Linux nodes using Ansible playbooks.
- **Workflow Orchestration:** Complex installation sequences managed via n8n workflows.
- **Centralized Storage:** Securely store and version software binaries (deb, rpm, tar.gz) in MinIO.
- **Detailed Audit Logs:** Comprehensive tracking of every installation step and status (Pending, Success, Failed).
- **Dockerized Environment:** Fully containerized services for consistent deployment across different host systems.

## ?? Getting Started (Development)

### Prerequisites
- Docker Desktop
- .NET 8 SDK
- [cite_start]SQL Server 2022 (Local or Containerized) [cite: 18]

### Installation
1. **Clone the repository:**
   ```bash
   git clone [https://github.com/your-username/OrchestrationPlatform.git](https://github.com/your-username/OrchestrationPlatform.git)