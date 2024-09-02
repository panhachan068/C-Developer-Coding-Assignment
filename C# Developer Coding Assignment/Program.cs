using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace C__Developer_Coding_Assignment
{
    class Program
    {
        // Define a class to represent notification channels
        public class NotificationChannel
        {
            public string Name { get; set; }
            public string Tag { get; set; }

            public NotificationChannel(string name, string tag)
            {
                Name = name;
                Tag = tag;
            }
        }

        static void Main(string[] args)
        {
            // Define the available notification channels
            var channels = new List<NotificationChannel>
            {
                new NotificationChannel("Backend", "BE"),
                new NotificationChannel("Frontend", "FE"),
                new NotificationChannel("Quality Assurance", "QA"),
                new NotificationChannel("Urgent", "Urgent")
            };

            while (true)
            {
                // Take user input for the notification title
                Console.WriteLine("Enter the notification title:");
                string title = Console.ReadLine();

                // Parse the notification title to identify relevant tags
                var relevantChannels = ParseNotificationTitle(title, channels);

                // Display the list of relevant notification channels
                Console.WriteLine("Relevant Notification Channels:");
                foreach (var channel in relevantChannels)
                {
                    Console.WriteLine(channel);
                }

                // Prompt user to continue or exit
                Console.WriteLine("\nPress Enter to analyze another title or type 'exit' to close the program.");
                string input = Console.ReadLine();
                if (input.Trim().ToLower() == "exit")
                {
                    break;
                }
            }
        }

        static List<string> ParseNotificationTitle(string title, List<NotificationChannel> channels)
        {
            // List to store the names of relevant channels
            var relevantChannels = new List<string>();

            // Regex pattern to match tags enclosed in square brackets
            var regex = new Regex(@"\[(.*?)\]");

            // Find all tags in the title
            var matches = regex.Matches(title);

            foreach (Match match in matches)
            {
                string tag = match.Groups[1].Value;

                // Check if the tag matches any of the defined channels
                foreach (var channel in channels)
                {
                    if (channel.Tag.Equals(tag, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!relevantChannels.Contains(channel.Name))
                        {
                            relevantChannels.Add(channel.Name);
                        }
                    }
                }
            }

            return relevantChannels;
        }
    }
}
