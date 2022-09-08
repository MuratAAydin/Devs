using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Technology : Entity
{
    public Technology()
    {
    }

    public Technology(int id, int programmingLanguageId, string name) : this()
    {
        Id = id;
        ProgrammingLanguageId = programmingLanguageId;
        Name = name;
    }

    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }
    public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }
}