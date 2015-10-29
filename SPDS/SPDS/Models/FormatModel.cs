using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSSQLModel;
using SPDS.Models.DbModels;

namespace SPDS.Models
{
    public class FormatModel
    {
        private IDalRetrieve dal;



        public string[,] GetDataPointsByDataSet(Dataset d)
        {
            DataPoint[] data = dal.GetDataPointsByDataSet(d).ToArray();
            string[,] convertedlist = new string[data.Length, 5];

            convertedlist[0, 0] = "ProjectileCharge";
            convertedlist[0, 1] = "EqEnergy";
            convertedlist[0, 2] = "StoppingPower";
            convertedlist[0, 3] = "ConvertetData";
            convertedlist[0, 4] = "Error";

            for (int i = 1; i < data.Length; i++)
            {
                convertedlist[i, 0] = data[i].ProjectileCharge.ToString();
                convertedlist[i, 1] = data[i].EqEnergy.ToString();
                convertedlist[i, 2] = data[i].StoppingPower.ToString();
                convertedlist[i, 3] = data[i].ConvertetData.ToString();
                convertedlist[i, 4] = data[i].Error.ToString();
            }

            return convertedlist;
        }

        public string[] GetProjectileList()
        {
            List<Projectile> projectiles = dal.GetallProjectiles();
            string[] convertedlist = new string[projectiles.Count];

            foreach (var p in projectiles)
            {

            }
            return convertedlist;
        }

    }
}