﻿namespace GamePlay.Attributes
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {
        private readonly int _MaxFileSize;
        public MaxFileSizeAttribute(int MaxFileSize)
        {
            _MaxFileSize = MaxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
               
                if (file.Length >_MaxFileSize)
                {
                    return new ValidationResult($"maximum allowed size is {_MaxFileSize} bytes");
                }
            }
            return ValidationResult.Success;
        }
    }
}
