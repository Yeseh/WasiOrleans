namespace WasiOrleans.Interfaces;

public interface IWasiHttpWorkerGrain : IGrainWithIntegerKey
{
    Task<HttpResponse> RequestAsync(HttpRequest req);

    Task SetModule(Wasmtime.Module module);
}