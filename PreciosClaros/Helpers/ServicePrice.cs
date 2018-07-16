using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PreciosClaros.Helpers
{
    public class ServicePrice
    {
        public bool GetPriceAll()
        {
            try
            {
                const int CountMaxBranchOffice = 30;
                const int CountMaxBranchOfficeProduct = 100;
                int countSuc = 0;
                int countBranchOfficeProduct = 0;

                // Url Cordoba falta reemplazos de geo por 8 zonas
                var result = Common.Helpers.Utility.GetDownloadDocument(string.Format(Common.Helpers.Setting.GetUrlBranchOffice, CountMaxBranchOffice, countSuc));
                dynamic SucursalesPorZona = JsonConvert.DeserializeObject(result);
                if (SucursalesPorZona == null) return false;
                var totalZone = (int) SucursalesPorZona.total;

                while (countSuc< totalZone)
                {
                    // Proceso todo.
                    //  ver de realizarlo por linq si se puedo con el obj dynamic
                    string idsSuc = string.Empty;
                    foreach (var suc in SucursalesPorZona.sucursales)
                    {
                        idsSuc += suc.id + ",";
                    }

                    // Busco sucursales por zonas.
                    dynamic ListProductByBranchOffice = Common.Helpers.Utility.GetDownloadDocument(string.Format(
                        Common.Helpers.Setting.GetUrlBranchOfficeProducts, idsSuc, countBranchOfficeProduct, CountMaxBranchOfficeProduct));

                    var totalListProductByBranchOffice = (int)ListProductByBranchOffice.total;
                    while (countBranchOfficeProduct < totalListProductByBranchOffice)
                    {
                        //Listado de precios por producto 
                        for (int i = 0; i < CountMaxBranchOfficeProduct; i++)
                        {
                            SaveProducts(ListProductByBranchOffice.productos[i].id, idsSuc);
                        }

                        countBranchOfficeProduct = countBranchOfficeProduct + CountMaxBranchOfficeProduct;
                        if (CountMaxBranchOffice == countSuc)
                        {
                            ListProductByBranchOffice = Common.Helpers.Utility.GetDownloadDocument(string.Format(
                                Common.Helpers.Setting.GetUrlBranchOfficeProducts, idsSuc, countBranchOfficeProduct, CountMaxBranchOfficeProduct));
                        }
                        if (ListProductByBranchOffice == null) countBranchOfficeProduct = totalListProductByBranchOffice + 1;
                    }

                    // Es para recorrer el total de registros que dice que tienen.
                    countSuc = countSuc+CountMaxBranchOffice;
                    if (CountMaxBranchOffice == countSuc)
                    {
                        SucursalesPorZona = JsonConvert.DeserializeObject(Common.Helpers.Utility.GetDownloadDocument(string.Format(Common.Helpers.Setting.GetUrlBranchOffice,
                            CountMaxBranchOffice, countSuc)));

                    }
                    if (SucursalesPorZona == null) countSuc = totalZone+1;
                }
                return true;
            }
            catch (Exception e)
            {
                Common.Helpers.Logs.LogFile("GetPriceAll:" + e.ToString());
                Console.WriteLine(e);
            }
            return false;
        }

        private static void SaveProducts(string code, string idsBranchoffice)
        {
            dynamic product = JsonConvert.DeserializeObject(Common.Helpers.Utility.GetDownloadDocument(string.Format(
                Common.Helpers.Setting.GetUrlPriceProduct, code, idsBranchoffice)));

            if (product == null) return;

            var model = new Persistance.Model.PricesBranchOffices
            {
                DateAdd = DateTime.Now,
                NameProduct = product.producto.nombre,
                ProductCode = product.producto.id
            };
            for (int j = 0; j < 30; j++)
            {
                model.BranchOfficeName = product.sucursales[j].sucursalNombre;
                model.Departament = product.sucursales[j].localidad;
                model.TradeName = product.sucursales[j].banderaDescripcion;

                // Guardar el prodcutos con la suc.
                Persistance.Managers.PricesBranchOfficesManager.Save(model);
            }
        }
    }
}
