using System;
using System.Collections;
using Persistance.Dao;
using Persistance.Model;

namespace Persistance.Managers
{
    public static class PricesBranchOfficesManager
    {
        public static bool Save(PricesBranchOffices model)
        {
            // Guardo sql 
            Hashtable hParams = new Hashtable();
            const string sQuery = "Insert into tabla  (Code,NameProduct,DateAdd,ProductCode,TradeName,BranchOfficeName,Departament,Price) values (?Code,?NameProduct,?DateAdd,?ProductCode,?TradeName,?BranchOfficeName?Departament,?Price)";
            hParams.Add("?Code", model.Code);
            hParams.Add("?NameProduct", model.NameProduct);
            hParams.Add("?DateAdd", model.DateAdd);
            hParams.Add("?ProductCode", model.ProductCode);
            hParams.Add("?TradeName", model.TradeName);
            hParams.Add("?BranchOfficeName", model.BranchOfficeName);
            hParams.Add("?Departament", model.Departament);
            hParams.Add("?Price", model.Price);
            hParams.Add("?TradeName", model.TradeName);

            return Query.Execute(sQuery, hParams); 
        }
    }
}