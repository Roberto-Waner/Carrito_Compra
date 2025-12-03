namespace Capa_Presentacion.Models
{
    public class FormFieldViewModel
    {
        public string? Container_Label { get; set; }
        public string? Input_Type { get; set; }
        public string? Form_Id { get; set; }
        //public string? Label_Id { get; set; }
        //public string? Input_Id { get; set; }
        public string? Input_Placeholder { get; set; }
        //public string? Select_Id { get; set; }
        public Dictionary<string, string>? Select_Options { get; set; }
    }
}
