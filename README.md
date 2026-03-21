# Software Orchestration & Distribution Platform

[![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Enabled-blue?style=for-the-badge&logo=docker)](https://www.docker.com/)
[![Ansible](https://img.shields.io/badge/Ansible-Automation-red?style=for-the-badge&logo=ansible)](https://www.ansible.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean-green?style=for-the-badge)](https://github.com/MaedehShahcheraghi)

> An enterprise-grade, containerized orchestration platform designed to automate software distribution and configuration management across multi-VM environments. 

This system leverages a modern **Microservices** architecture to ensure scalability, reliability, and idempotent deployments.

---

## System Architecture

This project follows **Clean Architecture** principles, ensuring a strict separation of concerns between business logic, application rules, and infrastructure.

* **Core (Domain & Application):** Enterprise logic, entities, and service interfaces.
* **Infrastructure (Persistence & External):** SQL Server integration (EF Core), MinIO Object Storage, and n8n workflow triggers.
* **Presentation (Web API & Razor Dashboard):** RESTful endpoints and a centralized management console.

---

## Tech Stack

| Category | Technology |
| :--- | :--- |
| **Framework** | .NET 8 (C#) |
| **Automation** | n8n & Ansible |
| **Storage** | MinIO (Object Storage) |
| **Database** | SQL Server 2022 |
| **Virtualization** | Docker & VMware Workstation |
| **OS Target** | Ubuntu Linux Nodes |

---

## Key Features

* **Automated Deployment:** One-click software installation on remote Linux nodes using Ansible playbooks.
* **Workflow Orchestration:** Complex installation sequences and lifecycles managed via n8n workflows.
* **Centralized Storage:** Securely store, manage, and version software binaries (`.deb`, `.rpm`, `.tar.gz`) in MinIO.
* **Detailed Audit Logs:** Comprehensive tracking and monitoring of every installation step and its status (Pending, Success, Failed).
* **Dockerized Environment:** Fully containerized services for consistent and reproducible deployment across different host systems.

---

## Getting Started

### Prerequisites

Ensure the following dependencies are installed on your host machine before proceeding:
* Docker Desktop / Docker Engine
* .NET 8 SDK
* SQL Server 2022 (Local or Containerized)

### Installation & Setup

**1. Clone the repository:**
```bash
git clone [https://github.com/MaedehShahcheraghi/OrchestrationPlatform.git](https://github.com/MaedehShahcheraghi/OrchestrationPlatform.git)
cd OrchestrationPlatform