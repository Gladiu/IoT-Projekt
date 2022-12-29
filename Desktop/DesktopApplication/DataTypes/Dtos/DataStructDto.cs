namespace DataTypes.Dtos
{
    public class DataStructDto
    {
        public string? name { get; set; }

        public IEnumerable<string>? units { get; set; }

        public float? value { get; set; }

        public string? defaultUnit { get; set; }
    }
}
