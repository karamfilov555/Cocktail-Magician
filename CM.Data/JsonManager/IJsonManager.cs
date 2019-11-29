using System.Collections.Generic;

namespace CM.Data.JsonManager
{
    public interface IJsonManager
    {
        List<T> ExtractTypesFromJson<T>(string directory);
    }
}