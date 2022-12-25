namespace BookExperience.Infrastrucutre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static BookExperience.Infrastrucutre.Data.DataConstants;
    public class Category
    {
        public int Id { get; set; }

        /// <summary>
        /// category name
        /// </summary>
        [StringLength(maximumLength: CategoryMaxLength)]
        public string Name { get; set; } = null!;
    }
}
