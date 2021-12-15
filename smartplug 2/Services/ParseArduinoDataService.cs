using System.Globalization;

namespace SmartPlug.Services
{
    public class ParseArduinoDataService: IParseArduinoDataService
    {
        public double? GetToogleAc(int plugNumber, string arduinoString)
        {
            var inputStrings = arduinoString.Split("\r\n");
            foreach (var inputString in inputStrings)
            {
                if (inputString.Contains("t" + plugNumber))
                {
                    if (double.TryParse(inputString.Substring(3), NumberStyles.Any, CultureInfo.InvariantCulture, out var wh))
                    {
                        return wh;
                    }                    
                }                
            }

            return null;
        }

        public double? GetWh(string arduinoString)
        {
            var inputStrings = arduinoString.Split("\r\n");
            foreach (var inputString in inputStrings)
            {
                if (!inputString.Contains("w")) continue;
                if (double.TryParse(inputString[2..],
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var wh))
                {
                    return wh;
                }
            }

            return null;        
        }
    }
}