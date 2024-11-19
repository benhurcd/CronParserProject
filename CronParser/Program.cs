// See https://aka.ms/new-console-template for more information
using Deliveroo.CronParser;
using Deliveroo.CronParser.Setup;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider provider = CronParserSetup.Setup();

ICronParser? parser = provider.GetService<ICronParser>();
if(parser == null)
{
    Console.WriteLine("Failed to get parser or displayable");
    return;
}
if(args == null || args.Length < 6)
{
    Console.WriteLine("Please provide a valid cron expression and command");
    return;
}

parser.ParseAndDisplay(string.Join(" ", args.Take(5)), string.Join(" ", args.Skip(5).ToArray()));