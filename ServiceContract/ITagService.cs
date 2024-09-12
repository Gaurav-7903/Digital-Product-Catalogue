using Digital_Product_Catalogue.Models;

namespace Digital_Product_Catalogue.ServiceContract
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();

        Tag AddTag(string tagName);
        Tag GetTagById(int tagId);
    }
}
