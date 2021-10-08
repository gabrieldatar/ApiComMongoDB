using Microsoft.AspNetCore.Mvc;
using ApiComMongoDB.Data;
using MongoDB.Driver;
using ApiComMongoDB.Data.Collections;
using ApiComMongoDB.Models;
using System;

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
            var infectado=new Infectado(dto.DataNascimento,dto.Sexo,dto.Latitude,dto.Longitude);

            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201,"Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados=_infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();

            return Ok(infectados);
        }

        // O ideial seria o HttpPatch
        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody]InfectadoDto dto)
        {
            _infectadosCollection.UpdateOne(Builders<Infectado>.Filter.Where(_ =>_.DataNascimento==dto.DataNascimento),Builders<Infectado>.Update.Set("sexo",dto.Sexo));

            return Ok("Atualizado com sucesso");
        }

        [HttpDelete("{dataNasc}")]
        public ActionResult DeleteInfectado(DateTime dataNasc)
        {
            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ =>_.DataNascimento==dataNasc));

            return Ok("Deletado com sucesso");
        }
    }
}