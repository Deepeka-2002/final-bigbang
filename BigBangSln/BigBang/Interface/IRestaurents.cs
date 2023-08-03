using BigBang.Models;

namespace BigBang.Interface
{
    public interface IRestaurents
    {
        Task<List<Restaurents>> GetRestaurents();
        IEnumerable<Restaurents> Filterpackage(int packageId);
        Task<List<Restaurents>> AddRestaurent(Restaurents apps);

        Task<List<Restaurents>?> UpdateRestaurentById(int id, Restaurents apps);
        Task<List<Restaurents>?> DeleteRestaurentById(int id);
    }
}
