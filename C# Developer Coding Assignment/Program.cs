using System;
using System.Collections.Generic;
using System.Linq;
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
            try
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
                    Console.WriteLine("Available tags: [BE], [FE], [QA], [Urgent]");
                    Console.WriteLine("Enter the notification title:");
                    string title = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.WriteLine("Error: Title cannot be empty.");
                        continue;
                    }

                    // Parse the notification title to identify relevant tags
                    var relevantChannels = ParseNotificationTitle(title, channels);

                    // Check if the title only contains tags and no other content
                    var titleWithoutTags = Regex.Replace(title, @"\[(.*?)\]", "").Trim();
                    if (relevantChannels.Count > 0 && string.IsNullOrWhiteSpace(titleWithoutTags))
                    {
                        Console.WriteLine("Error: Title contains tags but no actual content.");
                        continue;
                    }

                    // Check if any relevant channels were found
                    if (relevantChannels.Count > 0)
                    {
                        // Format and display the list of relevant notification channels
                        var formattedChannels = string.Join(", ", relevantChannels);
                        Console.WriteLine($"Receive channels: {formattedChannels}");
                    }
                    else
                    {
                        Console.WriteLine("No relevant channels found or invalid tags used.");
                    }

                    // Prompt user to continue or exit
                    Console.WriteLine("\nPress Enter to analyze another title or type 'exit' to close the program.");
                    string input = Console.ReadLine();
                    if (input.Trim().ToLower() == "exit")
                    {
                        Console.WriteLine("Exiting the program. Goodbye!");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static List<string> ParseNotificationTitle(string title, List<NotificationChannel> channels)
        {
            // List to store the tags of relevant channels
            var relevantTags = new HashSet<string>();

            // Regex pattern to match tags enclosed in square brackets
            var regex = new Regex(@"\[(.*?)\]");

            // Find all tags in the title
            var matches = regex.Matches(title);

            if (matches.Count == 0)
            {
                Console.WriteLine("Error: No tags found in the title.");
            }

            foreach (Match match in matches)
            {
                string tag = match.Groups[1].Value;

                // Check if the tag matches any of the defined channels
                bool tagFound = false;
                foreach (var channel in channels)
                {
                    if (channel.Tag.Equals(tag, StringComparison.OrdinalIgnoreCase))
                    {
                        relevantTags.Add(channel.Tag);
                        tagFound = true;
                        break;
                    }
                }

                if (!tagFound)
                {
                    Console.WriteLine($"Error: Invalid tag '{tag}' found in the title.");
                }
            }

            return relevantTags.ToList();
        }
    }
}
