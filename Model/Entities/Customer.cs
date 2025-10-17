using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Model.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Column("customer_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]

        public string LastName { get; set; }
        [Column("email")]

        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }

        [Column("address")]
        public string Address { get; set; }


        [Column("phonenumber")]
        public string PhoneNumber { get; set; }

    }
}

