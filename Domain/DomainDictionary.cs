namespace Domain
{
    public abstract class DomainDictionaryEntry
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

    //Interface for marking domain dictionary entries. This arking is used in BaseRepository to avoid domain dictionary
    //entries creating in DB
    public interface IDomainDictionary
    {

    }
}
