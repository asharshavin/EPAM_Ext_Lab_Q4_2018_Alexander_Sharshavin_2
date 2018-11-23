using My_Calc.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace My_Calc.Models
{
    public enum Operation
    {
        [Display(Name = "Add", ResourceType = typeof(CalcResources))]
        Add,
        [Display(Name = "Sub", ResourceType = typeof(CalcResources))]
        Sub,
        [Display(Name = "Mult", ResourceType = typeof(CalcResources))]
        Mult,
        [Display(Name = "Div", ResourceType = typeof(CalcResources))]
        Div
    }
}