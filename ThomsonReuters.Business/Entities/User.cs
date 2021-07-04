using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ThomsonReuters.Business.DomainObjects;

namespace ThomsonReuters.Business.Entities
{
    [Table("User")]
    public class User : IAggregateRoot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

    }
}
