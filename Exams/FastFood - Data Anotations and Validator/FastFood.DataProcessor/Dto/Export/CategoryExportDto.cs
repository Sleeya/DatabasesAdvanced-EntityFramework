using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Export
{
    [XmlType("Category")]
    public class CategoryExportDto
    {
        public string Name { get; set; }
        public CategoryItemExportDto MostPopularItem { get; set; }
    }
}
