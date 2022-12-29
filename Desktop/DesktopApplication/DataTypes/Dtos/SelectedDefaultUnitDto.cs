using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes.Dtos
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
