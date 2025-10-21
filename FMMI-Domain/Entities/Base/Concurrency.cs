using System.ComponentModel.DataAnnotations;


namespace FMMI_Domain.Entities.Base;

public class Concurrency : BaseIdEntity
{

    [ConcurrencyCheck]
    public virtual string ConcurrencyStamp { get; set; }
}
