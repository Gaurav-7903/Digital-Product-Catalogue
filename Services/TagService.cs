using Digital_Product_Catalogue.Data;
using Digital_Product_Catalogue.Models;
using Digital_Product_Catalogue.ServiceContract;

namespace Digital_Product_Catalogue.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;


        public TagService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public Tag AddTag(string tagName)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.Name == tagName);
            if (tag == null)
            {
                tag = new Tag { Name = tagName };
                _context.Tags.Add(tag);
                _context.SaveChanges();
            }
            return tag;
        }

        public Tag GetTagById(int tagId)
        {
            if (tagId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tagId));
            }
            Tag tag = _context.Tags.Where(tag => tag.Id == tagId).FirstOrDefault();
            if (tag == null)
            {
                throw new ArgumentOutOfRangeException(nameof(tagId));
            }
            return tag;

        }
    }
}
