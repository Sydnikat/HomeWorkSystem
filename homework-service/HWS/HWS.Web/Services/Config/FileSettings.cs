using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Config
{
    public class FileSettings : IFileSettings
    {
        public string FileDirectory { get; set; }
        public string TimeStampFormat { get; set; }
    }

    public interface IFileSettings
    {
        public string FileDirectory { get; set; }
        public string TimeStampFormat { get; set; }
    }
}
