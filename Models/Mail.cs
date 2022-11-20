using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMTP_Tester.Models
{
    public class Mail
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Host")]
        public string Host
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Port")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Port must be numeric")]
        public string Port
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide To Eamil")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid To Email")]
        public string To
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide From Eamil")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid From Email")]
        public string From
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Password")]
        public string Password
        {
            get;
            set;
        }
    }
}