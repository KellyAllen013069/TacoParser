using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog Logger = new TacoLogger();
        const string CsvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            try
            {
                // Objective: Find the two Taco Bells that are the farthest apart from one another.
                // Some of the TODO's are done for you to get you started. 

                Logger.LogInfo("Log initialized");

                // Use File.ReadAllLines(path) to grab all the lines from your csv file. 
                // Optional: Log an error if you get 0 lines and a warning if you get 1 line
                var lines = File.ReadAllLines(CsvPath);
                if (lines.Length == 0) Logger.LogError("File is empty.");
                if (lines.Length == 1) Logger.LogWarning("File contains only one line.");

                // This will display the first item in your lines array
                Logger.LogInfo($"Lines: {lines[0]}");

                // Create a new instance of your TacoParser class
                var parser = new TacoParser();

                // Use the Select LINQ method to parse every line in lines collection
                var locations = lines.Select(parser.Parse).ToArray();


                // Complete the Parse method in TacoParser class first and then START BELOW ----------

                // DONE: Create two `ITrackable` variables with initial values of `null`. 
                // These will be used to store your two Taco Bells that are the farthest from each other.
                ITrackable locA = null;
                ITrackable locB = null;

                // DONE: Create a `double` variable to store the distance
                double longestDistance = 0.0;
                // DONE: Add the Geolocation library to enable location comparisons: using GeoCoordinatePortable;
                // Look up what methods you have access to within this library.

                // NESTED LOOPS SECTION----------------------------

                // FIRST FOR LOOP -
                // DONE: Create a loop to go through each item in your collection of locations.
                // This loop will let you select one location at a time to act as the "starting point" or "origin" location.
                // Naming suggestion for variable: `locA`

                // DONE: Once you have locA, create a new Coordinate object called `corA` with your locA's latitude and longitude.

                // SECOND FOR LOOP -
                // DONE: Now, Inside the scope of your first loop, create another loop to iterate through locations again.
                // This allows you to pick a "destination" location for each "origin" location from the first loop. 
                // Naming suggestion for variable: `locB`

                // DONE: Once you have locB, create a new Coordinate object called `corB` with your locB's latitude and longitude.

                // DONE: Now, still being inside the scope of the second for loop, compare the two locations using `.GetDistanceTo()` method, which returns a double.
                // If the distance is greater than the currently saved distance, update the distance variable and the two `ITrackable` variables you set above.
                
                foreach (var loc in locations)
                {
                    var corA = new GeoCoordinate(loc.Location.Latitude, loc.Location.Longitude);
                    foreach (var dest in locations)
                    {
                        var corB = new GeoCoordinate(dest.Location.Latitude, dest.Location.Longitude);
                        var distance = corA.GetDistanceTo(corB);
                        if (distance > longestDistance)
                        {
                            longestDistance = distance;
                            locA = loc;
                            locB = dest;
                        }
                    }
                }
                // NESTED LOOPS SECTION COMPLETE ---------------------

                // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
                // Display these two Taco Bell locations to the console.

                Logger.LogInfo(
                    $"The two Taco Bells that are farthest away from one another are: {locA?.Name} and {locB?.Name}\n"
                    + $"They are {(longestDistance / 1000.0):F2} kilometers away from each other.");
            }
            catch (Exception e)
            {
                Logger.LogError("Error", e);
            }

        }
    }
}
