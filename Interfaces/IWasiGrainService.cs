namespace WasiOrleans.Interfaces;

internal interface IWasiGrainService
{
    Task ListModules();

    Task<IWasiHttpWorkerGrain> GetModule(string moduleName);
}