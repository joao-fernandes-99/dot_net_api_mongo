using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class infectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<infectado> _infectadosCollection;

        public infectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<infectado>(typeof(infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] infectadoDto dto){
            var infectado = new infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201, "Infectado Adcionado com Sucesso!!!!");
        }

        [HttpGet]
        public ActionResult ObterInfectador(){
            var infectados = _infectadosCollection.Find(Builders<infectado>.Filter.Empty).ToList();

            return Ok(infectados);
        }
    }
}