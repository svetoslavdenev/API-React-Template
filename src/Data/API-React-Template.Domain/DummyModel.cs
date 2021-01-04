namespace APIReactTemplate.Domain
{
    using APIReactTemplate.Domain.Common.Models;

    public class DummyModel : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
