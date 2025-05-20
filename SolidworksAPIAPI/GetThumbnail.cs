using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAPICodePack.Shell;

namespace SolidworksAPIAPI
{
    public class GetThumbnail
    {
        public GetThumbnail() { }
        public void GetAllImg(string Folderpath, String FilePath)
        {

            if (File.Exists(FilePath))
            {
                ShellFile shellFile = ShellFile.FromFilePath(FilePath);
                Bitmap bmp = shellFile.Thumbnail.Bitmap;
                int w = (int)(bmp.Width * 1);
                int h = (int)(bmp.Height * 1);
                bmp.Save(Path.Combine(Folderpath, Path.ChangeExtension(Path.GetFileName(FilePath), ".png")), System.Drawing.Imaging.ImageFormat.Png);
                bmp.Dispose();
            }

        }

    }
}

