using System.ComponentModel.DataAnnotations;
using SPDS.Models.DbModels;
using MSSQLModel;
using System.Collections.Generic;
using System;
using System.Web;

namespace SPDS.Models
{
    public class submitmodel
    {

        [Required(ErrorMessage ="Field Required")]
        public string _targetMaterial { get; set; }

        [Required(ErrorMessage = "Field Required")]
        public string _projectile { get; set; }

        
        public string _comment { get; set; }


        public HttpPostedFileBase _uploadedfile { get; set; }

        public string _manualString { get; set; }




    }
}