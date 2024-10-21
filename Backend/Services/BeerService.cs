using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;

        public List<string> Errors { get; }

        public BeerService(IRepository<Beer> beerRepository,
            IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }


        public async Task<BeerDto> GetbyId(int id)
        {
            var beer = await _beerRepository.GetbyId (id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }
            return null;
        }
        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = _mapper.Map<Beer>(beerInsertDto);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);
            return beerDto;
        }
        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetbyId(id);

            if (beer != null)
            {
                beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto, beer);

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }


            return null;
        }
        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _beerRepository.GetbyId(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                
                return beerDto;
            }

            return null;
        }
        public bool Validate(BeerInsertDto beerInsertDto)
        {
            if (_beerRepository.Search(b => b.Name == beerInsertDto.Name).Count()>0)
            {
                Errors.Add("No puede existir una cerveza con un nombre y aexistente");
                return false;
            };
            return true;
        }
        public bool Validate(BeerUpdateDto beerUpdateDto)
        {
            if (_beerRepository.Search(b => b.Name == beerUpdateDto.Name 
            && beerUpdateDto.Id != b.BeerId).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre y aexistente");
                return false;
            };
            return true;
        }

        public async Task<IEnumerable<BeerDto>> SearchByNameStoredProcedure(string name)
        {
            var beers = await _beerRepository.SearchByNameStoredProcedure(name);
            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }

        public async Task<IEnumerable<BeerDto>> SearchByName(string name)
        {
            var beers = await _beerRepository.Search(b => b.Name.Contains(name)).ToListAsync();
            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }
    }
}
