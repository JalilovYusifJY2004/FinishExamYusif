﻿namespace FinalExamYusif.Areas.Admin.ViewModels.Service
{
    public class UpdateServiceVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Image { get; set; }
    }
}
