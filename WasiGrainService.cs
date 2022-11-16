using WasiOrleans.Interfaces;
using Orleans.Concurrency;
using Orleans.Runtime;

[Reentrant]
public class WasiGrainService : GrainService, IWasiGrainService, System.IDisposable
{
    private Wasmtime.Engine _engine = null!;
    private Wasmtime.Store _store = null!;
    private Wasmtime.Linker _linker = null!;

    private readonly IWasiModuleStorage _moduleStorage;

    public WasiGrainService(IWasiModuleStorage wasiModuleStorage)
        : base()
    {
        _moduleStorage = wasiModuleStorage;
    }

    public Task GetModule(string moduleName)
    {
        throw new NotImplementedException();
    }

    public override async Task Init(IServiceProvider serviceProvider)
    {
        _engine = new();
        _linker = new(_engine);
        _store = new(_engine);

        var modules =  await _moduleStorage.ListModulesAsync();

        await base.Init(serviceProvider);
        return Task.CompletedTask;
    }

    new public void Dispose()
    {
        if (_store is not null) { _store.Dispose(); }
        if (_linker is not null) { _linker.Dispose(); } 
        if (_engine is not null) { _engine.Dispose(); } 
        base.Dispose();
    }

    public Task ListModules()
    {
        throw new NotImplementedException();
    }
}