using WasiOrleans.Interfaces;
using Orleans.Concurrency;
using Orleans.Runtime;

[Reentrant]
public class WasiGrainService : GrainService, IWasiGrainService, IDisposable
{
    private Wasmtime.Engine _engine = null!;
    private Wasmtime.Store _store = null!;
    private Wasmtime.Linker _linker = null!;
    private readonly IWasiModuleStorage _moduleStorage;
    private readonly IGrainFactory _grainFactory;

    private List<Wasmtime.Module> _modules = new();

    public WasiGrainService(IWasiModuleStorage wasiModuleStorage, IGrainFactory grainFactory)
    {
        _moduleStorage = wasiModuleStorage;
        _grainFactory = grainFactory;
    }

    public override async Task Init(IServiceProvider serviceProvider)
    {
        _engine = new();
        _linker = new(_engine);
        _store = new(_engine);
        _modules = new();
        
        var modules =  await _moduleStorage.ListModulesAsync();

        foreach (var path in modules)
        {
            Wasmtime.Module.FromFile(_engine, path);
        }

        await base.Init(serviceProvider);
    }

    public async Task<IWasiHttpWorkerGrain> GetModule(string modulename)
    {
        var module = _modules.FirstOrDefault(x => x.Name == modulename);

        if (module == null) { throw new ArgumentException("Module not found"); }

        var grain = _grainFactory.GetGrain<IWasiHttpWorkerGrain>(0);
        await grain.SetModule(module);

        return grain; 
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