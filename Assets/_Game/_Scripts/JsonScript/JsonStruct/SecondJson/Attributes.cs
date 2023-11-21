using _Game._Scripts;

public class Attributes : JsonEntity<IdNameValue>
{
    public Attributes(string nameEntity) : base(nameEntity)
    {
        data.Add(new IdNameValue(Config.JSON_ENTITY_NAME));
    }
}