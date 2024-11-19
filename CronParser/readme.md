# CronParser

## Overview

The `CronParser` project is a library for parsing and expanding cron expressions. It provides functionality to display cron expressions in a human-readable format and to expand sub-expressions into their individual components.

## Features

- Parse cron expressions
- Expand sub-expressions (e.g., `1-5` to `1, 2, 3, 4, 5`)
- Display cron expressions in a human-readable format

## Getting Started

### Prerequisites

- .NET 8.0 SDK

### Installation

1. Clone the repository:
git clone https://github.com/your-repo/CronParser.git


3. Restore the dependencies:
2. Navigate to the project directory:


### Usage

To use the `CronParser` library, you need to set up dependency injection and resolve the required services.

Inject the ISubCronExpander and ICronDisplayable services and use them to parse and display cron expressions.

```csharp
using Deliveroo.CronParser.Setup; using Microsoft.Extensions.DependencyInjection;
var serviceProvider = CronParserSetup.Setup();
var cronParser = serviceProvider.GetService(); 
var cronExpression = "* * * * *"; 
cronParser.ParseAndDisplay(cronExpression, command);
```
    