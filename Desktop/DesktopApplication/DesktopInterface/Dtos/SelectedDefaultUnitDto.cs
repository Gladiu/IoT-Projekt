
namespace DesktopInterface.Dtos
{
    public class SelectedDefaultUnitDto
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }

        public SelectedDefaultUnitDto(string? name, string? unit)
        {
            this.Name = name;
            this.Unit = unit;
        }
    }
}
