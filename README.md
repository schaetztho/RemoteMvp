# RemoteMvp

This project shows how to decouple a Windows-Forms based application from the application logic using a TCP/IP connection.
For simplicity technologies such JSON, XML, REST etc. are not used.
The code relies on a TCP/IP server receiving commands (key/value pairs as string) from any client and executing some simple business logics (e.g. user registration and password check).

Although everything is in one solution the application may be distributed as two specific projects (cliend and server, each of them using MvpLib).

Modern web-application or the C#-based BLAZOR are working similar (in very basic theory!!!) but using more secure and standardized technologies (such as ASP.NET Server, SignalR instead of TCP/IP, etc.) 
