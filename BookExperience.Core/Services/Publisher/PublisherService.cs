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

            int didExists = DidPublisherExists(publisher.Name.ToLower());

            if (didExists > 0)
            {
                return didExists;
            }

            this.data.Publishers.Add(publisher);
            this.data.SaveChanges();

            return publisher.Id;
        }

        public int DidPublisherExists(string name)
        {
            List<Publisher> publishers = this.data.Publishers.ToList();

            foreach (var publisher in publishers)
            {
                if (name.ToLower() == publisher.Name.ToLower())
                {
                    return publisher.Id;
                }
            }
            return 0;
        }
    }
}
