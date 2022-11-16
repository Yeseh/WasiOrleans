
namespace WasiOrleans.Interfaces;

public interface IWasiModuleStorage
{
    Task<List<string>> ListModulesAsync();

    Task<string> GetModuleAsync();
}