namespace SmartPlug.Services
{
    public interface IParseArduinoDataService
    {
        double? GetToogleAc(int plugNumber, string arduinoString);
        double? GetWh(string arduinoString);
    }
}