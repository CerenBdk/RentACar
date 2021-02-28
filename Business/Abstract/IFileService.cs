using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFileService
    {
        string Add(IFormFile image);
        string CreateNewFileName(string fileName);
        void Delete(string path);
    }
}
