using _Game._Scripts;

public class What : JsonEntity<IdNameValue>
{
    public What(string nameEntity) : base(nameEntity)
    {
        data.Add(new IdNameValue(Config.JSON_ENTITY_COVER));
        data.Add(new IdNameValue(Config.JSON_ENTITY_SOUND));
        data.Add(new IdNameValue(Config.JSON_ENTITY_COMMENT));
    }
}