namespace Aplication.Models.Response.Base;

public class SelectBaseResponse 
{ 
    public List<SelectBase>? LSelectBase { get; set; }
}

public class SelectBase
{
    public string? Description { get; set; } = null!;
    public int Value { get; set; }
}