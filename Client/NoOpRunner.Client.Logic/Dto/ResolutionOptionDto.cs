namespace NoOpRunner.Client.Logic.Dto
{
    public class ResolutionOptionDto : DropdownDto
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public override string Value 
        { 
            get => $"{Width}x{Height}"; 
            set => base.Value = value; 
        }
    }
}
