using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces;

public interface ITranslationClient
{
    Task<string> GetTranslationFromLtToEn(string products);
    Task<string> TranslateFromEnToLt(string text);
}
