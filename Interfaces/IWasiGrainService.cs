namespace WasiOrleans.Interfaces;

internal interface IWasiGrainService
{
    Task ListModules();

    Task GetModule(string moduleName);
}