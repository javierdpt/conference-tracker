using ConferenceTracker.Model;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTracker.Data.Infrastructure
{
    public class TestDataHelper
    {
        public static PersonNameGenerator PersonNameGenerator = new PersonNameGenerator();

        public static Attendee GetRandomAttendee()
        {
            return new Attendee
            {
                Id = Guid.NewGuid(),
                FirstName = PersonNameGenerator.GenerateRandomFirstName(),
                LastName = PersonNameGenerator.GenerateRandomLastName(),
                Company = PersonNameGenerator.GenerateRandomLastName(),
                Email = null,
                PhoneNumber = "5555555555"
            };
        }

        public static Speaker GetRandomSpeaker()
        {
            return new Speaker
            {
                Id = Guid.NewGuid(),
                Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu",
                FirstName = PersonNameGenerator.GenerateRandomFirstName(),
                LastName = PersonNameGenerator.GenerateRandomLastName()
            };
        }

        public static Session GetSession(List<Speaker> speakers)
        {
            var random = new Random();
            var min = new[] { 0, 10, 20, 30, 40, 50 };
            var dur = new[] { 15, 20, 30, 40, 50, 60, 70 };
            return new Session
            {
                Title = SessionsTitles[random.Next(0, SessionsTitles.Length - 1)],
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostr",
                Speakers = speakers.Shuffle().Take(random.Next(1, 4)).ToList(),
                Time = new DateTime(2020, 2, 29, random.Next(9, 18), min[random.Next(0, min.Length - 1)], 0),
                Duration = TimeSpan.FromMinutes(dur[random.Next(0, dur.Length - 1)]),
                Attendees = Enumerable
                     .Range(1, random.Next(0, 20))
                     .Select(i => GetRandomAttendee())
                     .ToList(),
                Location = RoomNames[random.Next(0, RoomNames.Length - 1)],
                Capacity = random.Next(30, 60)
            };
        }

        private static readonly string[] SessionsTitles =
        {
            "Introduction to Python for .NET Developers",
            "Building powerful web applications with Blazor",
            "Blazor: Blazing A Trail For.NET Web Developers",
            "Entity Framework performance monitoring and tuning",
            "Building Solutions with Angular and.Net Core WebApi",
            "Troubleshooting Web Performance Issues",
            "Diving into the world of Digital Health",
            "Empowering your Associates and Creating Loyal Customers Through Outstanding Mobile Experiences",
            "Artificial Intelligence(AI) Democratized with the Microsoft Power Platform",
            "UX Design for Developers - The Secret Sauce of great products",
            "Understanding Probabilistic Data Structures with 112, 092 UFO Sightings",
            "360° AI + RPA Automated Trading Systems(ATS) Using C# Strategy Code Generator",
            "Take Command of Azure PowerShell & PowerShell 7",
            "Introduction to Microsoft Enterprise Mobility + Security(EMS)",
            "Fun with Databricks and Spark",
            "Made Sooo Easy..Azure Logic Apps with  Data Factory, Blob Storage and Service Bus",
            "Introduction to Event Sourcing and CQRS",
            "Monitoring production applications in Azure with Application Insights",
            "Introduction to the AWS Cloud Development Kit(CDK), Deploy a containerized app to AWS in less than 5 mins!",
            "Azure Functions with Event hubs.",
            "Streaming at scale solutions with Azure SQL",
            "Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs",
            "Delivering Real - Time Data with Azure",
            "Building Solutions in the Cloud",
            "You're A Book, You're A Brand: Storytelling and Personal Branding",
            "Top 5 Soft Skills to Boost Your Career",
            "Take Charge of Your Career",
            "Dare to change, the path of the peak performance professional",
            "For Love or Money, Developing your Career",
            "Art of pitching",
            "A Practical Approach to DevOps",
            "State of DevOps in 2020",
            "SQL is not an excuse to avoid DevOps",
            "Improve your AWS security posture using AWS Config, AWS Cloudtrail, and AWS Guarduty",
            "The Skeptic's Guide to Git",
            "Working with Docker Containers in WSL",
            "DevOps Chalk Board Session",
            "DevOps is more than Dev and Ops",
            "Git Good with Advanced Git",
            "Streaming Architectures by Example",
            "Robotic Process Automation: Building and Operating Bots",
            "Create Custom Alexa Skills with NodeJS",
            "A Developer's Introduction to Electronics",
            "How to get started programming with microcontrollers",
            "A Lap around the Azure Data Platform",
            "Deep Learning Basics",
            "Explainable, Interpretable, and Transparent Machine Learning",
            "Amazing Algorithms for Solving Problems in Software",
            "Azure Machine Learning Workspaces for Beginners",
            "Automated Machine Learning(AutoML)",
            "Volunteering in Technology",
            "PowerShell From The Trenches - Workflows...Of Course Yes!",
            "The Developer's Spectrum - From Junior to Lead",
            "Algorand Blockchain Basics - Decentralized and For Developers",
            "Bitcoin Programming with Python",
            "Pop OS - Perfecting Linux for today’s Software Developers",
            "Using Azure Cognitive Services in your applications to provide Vision, Speech, Search and Understanding",
            "Building an \"in-house\" Mobile User Interface Test Farm - with Xamarin.UITest",
            "Getting Started on Bayesian Analysis-- with PyMC3",
            "You are more than a coder – You are a problem solver!",
            "SQL 2019 For Developers",
            "Having Trouble Finding Tech Talent ? Start an apprenticeship program.",
            "Programmer burnout: how to recognize and avoid it",
            "What's an Apprenticeship in Tech Like?",
            "Become a Civic Hacker Today!",
            "Azure Data Factory for Beginners",
            "Prototyping Without Design Skills in Adobe XD",
            "Designing Serverless Applications Using AWS Lambda",
            "Intro to Kubernetes with Azure Kubernetes Service",
            "Using Service Fabric As a Platform",
            "Using Azure and Xamarin to Scale a 1 Million + User Platform",
            "Containerization For Software Developers",
            "Static Sites and Serverless Functions - A Dynamic Combination",
            "Survey Data - Clean Up and Analysis",
            "Power BI and Excel: Both part of your BI Strategy in the same platform",
            "SQL Server Windowing Functions",
            "SQL Server In - Memory OLTP",
            "Fundamentals of Relational Db Design",
            "Unit Testing SQL Server",
            "GraphQL - API for modern apps",
            "Reactive Programming with Angular & RxJS",
            "Harness the Power of Visual Studio Code",
            "Angular 9 for the Enterprise",
            "My Browser Does What ?",
            "C# and Xamarin to build, publish iOS, Android apps",
            "Run the world from the palm of your hand",
            "Mobile App Testing - Tips and Tricks",
            "Mobile DevOps Made Easy with VS AppCenter",
            "Building Efficient Xamarin apps with GraphQL"
        };

        private static readonly string[] RoomNames =
        {
            "ASP .Net/Core 1 - Room 2060",
            "Auditorium - Room 1124",
            "Azure/Cloud 1 - Room 2071",
            "Azure/Cloud 2 - Room 2072",
            "Career/Business Dev - Room 1133",
            "DevOps 1 - Room 2057",
            "DevOps 2 - Room 2078",
            "IoT - Room 2061",
            "Machine Learning - Room 2067",
            "Open Topics 1 - Room 2081",
            "Open Topics 2 - Room 2082",
            "Open Topics 3 - Room 2097",
            "SQL/BI - Room 2064",
            "Web Development 1 - Room 2056",
            "Xamarin/Mobile - Room 2065"
        };
    }
}