using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(128, ErrorMessage = "First Name cannot exceed 128 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(1024)]
        public string HashedPassword { get; set; }

        public bool IsLocked { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(128, ErrorMessage = "Last Name cannot exceed 128 characters")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(16, ErrorMessage = "Phone Number cannot exceed 16 characters")]
        public string PhoneNumber { get; set; }

        [Url(ErrorMessage = "Invalid profile picture URL")]
        public string ProfilePictureUrl { get; set; }

        //[Required(ErrorMessage = "Salt is required")]
        [MaxLength(1024)]
        public string Salt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = [];
        public ICollection<Purchase> Purchases { get; set; } = [];
    }
}
