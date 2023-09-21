namespace WebAPITest.Domain.Models
{
    public class DomainDictionaryEntry
    {
        public DomainDictionaryEntry(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
    }
}
