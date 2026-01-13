namespace Capa_Presentacion.Models
{
    public class FormFieldViewModel
    {
        public string? IdInput { get; set; }
        public string? Name { get; set; }
        public string? Placeholder { get; set; }
        public string? CustomColumnClass { get; set; }
        //Laber
        public string? IdLabel { get; set; }
        //public bool IncludLabel { get; set; }
        public string? Container_Label { get; set; }
        //Input
        public string? Input_Type { get; set; }
        public bool AutoComplete { get; set; }
        public bool Input_IsRequired { get; set; }
        //Textarea
        public string? ColTexTarea { get; set; }
        public string? RowTexTarea { get; set; }
        //Select
        public Dictionary<string, string>? Select_Options { get; set; }
    }
}
