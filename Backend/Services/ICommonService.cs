using Backend.DTOs;

namespace Backend.Services
{
    public interface ICommonService<T, TI, TU>
    {
        //lista para errores
        public List<string> Errors { get; }

        Task <IEnumerable<T>> Get();
        Task <T> GetbyId(int id);
        Task <T> Add(TI beerInsertDto);
        Task<T> Update(int id , TU beerUpdateDto);
        Task<T> Delete(int id);
        Task<IEnumerable<BeerDto>> SearchByName(string name);
        //manejo de errores
        bool Validate(TI dto);
        bool Validate(TU dto);
        Task<IEnumerable<BeerDto>> SearchByNameStoredProcedure(string name);
    }
}
