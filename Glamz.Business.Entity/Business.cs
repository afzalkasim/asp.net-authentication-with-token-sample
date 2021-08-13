using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Glamz.Business.Entity
{
    public class Business : BaseEntity
    {
        [Required]
        [Key]
        public int BusinessId { get; set; }
        [Required]
        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string WebPage { get; set; }

        public string FacebookLink { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        public string TwitterLink { get; set; }

        public string InstagramLink { get; set; }

        public string LandlineNumber { get; set; }
    }
}
