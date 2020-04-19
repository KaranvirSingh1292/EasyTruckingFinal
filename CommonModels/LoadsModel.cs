using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonModels
{
    public class LoadsModel
    {

        public int Id { get; set; }

        public string Items { get; set; }

        [Display(Name = "Pickup Location")]
        public string Pickup_Location { get; set; }

        [Display(Name = "Drop Location")]
        public string Drop_Location { get; set; }

        public decimal Pay { get; set; }

        [Display(Name = "Dispatcher")]
        public int DispatcherID { get; set; }

        [Display(Name = "Driver")]
        public int DriverID { get; set; }

        [Display(Name = "Available From")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created_On { get; set; }

        [Display(Name = "Accepted On")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Accepted_On { get; set; }

        [Display(Name = "Dispatcher Name")]
        public string Dispatcher_Name { get; set; }

        [Display(Name = "Driver Name")]
        public string Driver_Name { get; set; }

    }
}
