using System;

namespace ApiComMongoDB.Models
{
    public class InfectadorDto
    {
        public DateTime DataNascimento{get;set;}

        public string Sexo{get;set;}

        public double latitude{get;set;}

        public double Longitude{get;set;}
    }
}