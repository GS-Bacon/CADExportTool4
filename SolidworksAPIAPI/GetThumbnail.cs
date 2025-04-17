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
        public void GetAllImg(string Folderpath)
        {
            string[] files = Directory.GetFiles(Folderpath);
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    ShellFile shellFile =ShellFile.FromFilePath(file);
                    Bitmap bmp = shellFile.Thumbnail.Bitmap;
                    int w = (int)(bmp.Width * 2);
                    int h = (int)(bmp.Height * 2);
                    bmp.Save(Path.ChangeExtension(file,".png"), System.Drawing.Imaging.ImageFormat.Png);
                    bmp.Dispose();
                }

            }

        }
    }
}
