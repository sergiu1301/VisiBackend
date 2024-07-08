using System.ComponentModel.DataAnnotations;

namespace VisiProject.API.Requests;

public class RoleRequest
{
    [StringLength(256)]
    public string Name { get; set; }

    [StringLength(256)]
    public string Description { get; set; }
}