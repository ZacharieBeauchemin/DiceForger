using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Core.Repositories;
using Model;

namespace Api.Controllers;


[Route("[controller]")]
[ApiController]
public class CardsController: ControllerBase {
    private IDFCardsRepository _cardsRepository;

    public CardsController(IDFCardsRepository cardsRepository) {
        _cardsRepository = cardsRepository;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IEnumerable<DFCard>> GetAll() {
        return await _cardsRepository.GetCardsAsync();
    }
}