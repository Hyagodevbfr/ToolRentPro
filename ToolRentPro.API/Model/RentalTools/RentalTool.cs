namespace ToolRentPro.API.Model.RentalTools;

public class RentalTool
{
    public Guid Id { get; set; }

    protected RentalTool()
    {
        Id = Guid.NewGuid();
    }
}
