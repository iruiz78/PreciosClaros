using System;

namespace Persistance.Model
{
    public class PricesBranchOffices
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public DateTime DateAdd { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
        public string TradeName { get; set; }
        public string ProductCode { get; set; }
        public string BranchOfficeName { get; set; }
        public string Departament { get; set; }
    }
}