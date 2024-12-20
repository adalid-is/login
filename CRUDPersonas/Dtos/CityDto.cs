using System;
using System.Collections.Generic;

namespace CRUDPersonas.Dto;

public partial class CityDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int IdProvince { get; set; }
    public ProvinceDto Province { get; set; }
}
