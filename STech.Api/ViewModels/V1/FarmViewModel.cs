using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SarveenTech.API.ViewModels.V1
{
    public class FarmViewModel
    {
        public FarmViewModel()
        {
            
        }

        //public int Id { get; set; }

        [Required]
        [StringLength(512, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        //[Required(ErrorMessage ="Enter your email. email address is rquired ...")]
        //[StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[EmailAddress]
        //public string Email { get; set; }
        //public DateTime? CreationDateTime { get; set; }
        //public bool IsDeleted { get; set; } = false;
        //public bool IsEnabled { get; set; } = true;

     
    }
}
