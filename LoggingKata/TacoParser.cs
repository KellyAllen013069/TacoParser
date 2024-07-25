using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog _logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            try
            {
                _logger.LogInfo("Begin parsing");

                // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
                var cells = line.Split(',');

                // If your array's Length is less than 3, something went wrong
                if (cells.Length < 3)
                {
                    // Log error message and return null
                    _logger.LogError("A line does not contain the correct data");
                    return null;
                }

                // DONE: Grab the latitude from your array at index 0
                // You're going to need to parse your string as a `double`
                // which is similar to parsing a string as an `int`
                var hasLat = double.TryParse(cells[0], out double lat);
                if (!hasLat)
                {
                    _logger.LogError("Cannot obtain latitude.");
                    return null;
                }


                // DONE: Grab the longitude from your array at index 1
                // You're going to need to parse your string as a `double`
                // which is similar to parsing a string as an `int`
                var hasLong = double.TryParse(cells[1], out double lng);
                if (!hasLong)
                {
                    _logger.LogError("Cannot obtain latitude.");
                    return null;
                }

                // DONE: Grab the name from your array at index 2
                var name = cells[2];

                // DONE: Create a TacoBell class
                // that conforms to ITrackable

                // DONE: Create an instance of the Point Struct
                // DONE: Set the values of the point correctly (Latitude and Longitude)
                var point = new Point()
                {
                    Latitude = lat,
                    Longitude = lng
                };
                
               

                // DONE: Create an instance of the TacoBell class
                // DONE: Set the values of the class correctly (Name and Location)
                var tb = new TacoBell()
                {
                    Name = name,
                    Location = point
                };

                // DONE: Then, return the instance of your TacoBell class,
                // since it conforms to ITrackable

                return tb;
            }
            catch (Exception e)
            {
                _logger.LogError("Error", e);
                return null;
            }
        }
    }
}
