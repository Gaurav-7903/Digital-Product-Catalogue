using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.Models;

namespace Digital_Product_Catalogue.ViewModel
{
    public class TagIndexViewModel
    {
        public TagRequest TagRequest { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
