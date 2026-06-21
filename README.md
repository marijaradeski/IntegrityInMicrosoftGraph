📊 IntegrityInMicrosoftGraph

A small research-oriented .NET 9 console application that experiments with file transfer performance and integrity validation using Microsoft Graph (OneDrive).

🧠 Overview

This project was built as a personal experiment to better understand how file uploads and downloads behave in real cloud storage scenarios.

Instead of just using OneDrive as storage, the idea was to treat it like a test environment for benchmarking and integrity validation.

Each run of the application:

generates a file locally
uploads it to OneDrive via Microsoft Graph
downloads it back
compares both versions using SHA-256 hashing
measures upload/download performance

🎯 Goal of the project

The main focus is not production usage, but measurement and observation:

How does file size affect upload speed?
Does file type make any difference?
Is data preserved correctly during transfer?
What is the real latency of Microsoft Graph in practice?

The application is structured like a small “lab tool” for running repeatable experiments.

⚙️ How it works

User enters file size (KB)
User selects file type (txt, jpg, png, zip, etc.)
The system generates a random file locally
SHA-256 hash is calculated (baseline)
File is uploaded to OneDrive using Microsoft Graph
File is downloaded back to local disk
SHA-256 hash is calculated again
Both hashes are compared
Upload/download time and speed are measured

🧱 Architecture

The project follows a simple layered structure:

UI Layer
ConsoleMenu
Handles user input and output formatting
Core Layer
Runner
Orchestrates the experiment flow
Services Layer
FileService → generates test files
GraphService → handles Microsoft Graph upload/download
HashService → computes SHA-256 hashes
BenchmarkCalculator → calculates transfer speed
FileComparer → compares file content
Authentication
GraphAuthenticator
Uses MSAL Device Code Flow for personal Microsoft accounts

🔐 Integrity verification

Integrity is checked using SHA-256 hashing:

Hash before upload = original state
Hash after download = received state

If both match, the file is considered unchanged.

If not, it indicates corruption or transfer mismatch.

📊 Benchmarking

Each experiment records:

Upload time (ms)
Download time (ms)
File size (bytes / KB)
Upload speed (MB/s)
Download speed (MB/s)
Hash match result
File equality check

This allows comparison between:

small vs large files
different file types
multiple repeated runs
🛠 Tech stack
.NET 9 (Console Application)
Microsoft Graph SDK
MSAL (Device Code Flow authentication)
SHA-256 cryptography (System.Security.Cryptography)
Manual dependency injection (no external DI container)

▶️ Running the project

Build the solution
Run the console app
Authenticate with Microsoft account (device code flow)
Enter:
file size
file type
Wait for results

📁 Output behavior

Files are stored in OneDrive under a test folder:

/test/original.bin

Downloaded files are saved locally for comparison.

📌 Notes

This project is not optimized for production workloads
It is designed for experimentation and learning
Network speed and Microsoft Graph latency affect results
Binary files are used for consistent benchmarking

📈 Possible improvements

Some ideas for future expansion:

Export results to CSV for analysis
Run batch experiments automatically (e.g. 100 runs per file size)
Add charts for performance visualization
Add retry logic for unstable network conditions
Compare OneDrive vs other storage providers

🧠 Personal takeaway

The most interesting part of this project was realizing how much real-world variance exists in something as simple as file upload speed. Even small changes in file size or network conditions can significantly affect results.

It also made clear how important it is to separate:

experiment logic
data collection
and presentation layer

⚠️ Biggest Challange

The most difficult part of this project was setting up the Azure app registration and getting the authentication to work properly. Because I used a personal Microsoft account, I ran into several configuration issues. Understanding how to properly connect everything with Microsoft Graph and resolve authentication problems took most of my time.
