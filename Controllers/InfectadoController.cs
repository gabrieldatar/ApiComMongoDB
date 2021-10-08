using Microsoft.AspNetCore.Mvc;
using ApiComMongoDB.Data;
using MongoDB.Driver;
using ApiComMongoDB.Data.Collections;
using ApiComMongoDB.Models;

namespace ApiComMongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController: ControllerBase
    {
        ApiComMongoDB.Data.MongoDB _mongoDB;

        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(ApiComMongoDB.Data.MongoDB mongoDB)
        {
            _mongoDB=mongoDB;
            _infectadosCollection=_mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody]InfectadoDto dto)
        {
            var infectado=new Infectado(dto.DataNascimento,dto.Sexo,dto.latitude,dto.Longitude);

            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201,"Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados=_infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();

            return Ok(infectados);
        }            
    }
}