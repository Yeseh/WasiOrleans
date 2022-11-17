using WasiOrleans.Interfaces;

public class LocalFileWasiStorage : IWasiModuleStorage
{
    public Task<string> GetModuleAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> ListModulesAsync()
    {
        throw new NotImplementedException();
    }
}