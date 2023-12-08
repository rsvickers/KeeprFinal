



namespace KeeprFinal.Services;

public class KeepsService
{
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
        _keepsRepository = keepsRepository;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _keepsRepository.CreateKeep(keepData);
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = _keepsRepository.GetKeeps();
        return keeps;
    }
}
