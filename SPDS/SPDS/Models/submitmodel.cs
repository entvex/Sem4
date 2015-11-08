using System.ComponentModel.DataAnnotations;
using SPDS.Models.DbModels;
using MSSQLModel;
using System.Collections.Generic;
using System;

namespace SPDS.Models
{
    public class submitmodel
    {

        [Required(ErrorMessage ="Field Required")]
        public string _targetMaterial { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string _projectile { get; set; }

        
        public string _comment { get; set; }



    }
}