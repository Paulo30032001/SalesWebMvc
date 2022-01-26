namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Department(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

    }
}
