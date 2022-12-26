namespace BookExperience.Core.Services.Publisher
{
	using BookExperience.Infrastrucutre.Data;
	using BookExperience.Infrastrucutre.Data.Models;

	public class PublisherService : IPublisherService
	{
        private readonly ApplicationDbContext data;
        public PublisherService(ApplicationDbContext data)
        {
            this.data = data;
		}
		public int Create(string name)
		{
			if (name is null)
			{
				return 0;
			}

			Publisher publisher = new Publisher()
			{
				Name = name
			};

            this.data.Publishers.Add(publisher);
            this.data.SaveChanges();

            return publisher.Id;
        }
	}
}
