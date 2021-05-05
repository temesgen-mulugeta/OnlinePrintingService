using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlinePrintingService.Models
{
    public class UserModel
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }
    }
}