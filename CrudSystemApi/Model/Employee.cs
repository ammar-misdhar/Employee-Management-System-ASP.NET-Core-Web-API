using System.ComponentModel.DataAnnotations;

namespace CrudSystemApi.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNo {  get; set; }
        public string EmailAddress { get; set; }
    }
}
