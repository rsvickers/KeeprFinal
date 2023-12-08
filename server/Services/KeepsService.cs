




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

    internal Keep GetKeepsById(int keepId, string userId)
    {
        Keep keep = _keepsRepository.GetKeepsById(keepId);

        if (keep == null)
        {
            throw new Exception($"{keepId} is an invalid ID");
        }
        return keep;
    }

    // internal Keep GetKeepsByIdAndAddVisits(int keepId, string userId)
    // {GetKeepsById(keepId, userId);
    // Keep.Visits++
    // }


    internal Keep UpdateKeep(int keepId, string userId, Keep keepData)
    {
        Keep keepToUpdate = GetKeepsById(keepId, userId);

        if (keepToUpdate.CreatorId != userId)
        {
            throw new Exception("NOT YOUR KEEP");
        }

        keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
        keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;
        keepToUpdate.Img = keepData.Img ?? keepToUpdate.Img;

        Keep keep = _keepsRepository.UpdateKeep(keepToUpdate);
        return keep;
    }
}
