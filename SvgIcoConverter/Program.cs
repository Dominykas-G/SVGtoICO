using System.Drawing.Imaging;
using Svg;
using ImageMagick;

namespace SvgConverter
{
    internal class Settings
    {
        public string FileName { set; get; } = string.Empty;
        public string Path { set; get; } = string.Empty;
        public string FileName16px { set; get; } = string.Empty;
    }

    internal class Program
    {
        private static void Save(string inputPath, string outputPath, int dimension)
        {
            // https://github.com/svg-net/SVG/issues/312
            // From .svg get .bmp file
            SvgDocument doc = SvgDocument.Open(inputPath);
            doc.Height = dimension;
            doc.Width = dimension;

            doc.ShapeRendering = SvgShapeRendering.GeometricPrecision;

            using (var bitmap = doc.Draw())
            {
                bitmap?.SetResolution(300.0f, 300.0f);
                bitmap?.Save(outputPath, ImageFormat.Bmp);
            }
        }
        public static void MakeIco(Settings settings)
        {
            var images = new MagickImageCollection();

            int[] dims = { 16, 32, 48, 128, 256 }; // dimensions of the .ico file

            if (settings.FileName16px != string.Empty && File.Exists(settings.FileName16px))
            {
                try
                {
                    MagickImage image = new(settings.FileName16px);
                    images.Add(image);

                    // Remove the first 16 px int since it is no longer needed.
                    int[] newDims = new int[dims.Length - 1];
                    Array.Copy(dims, 1, newDims, 0, dims.Length - 1);
                    dims = newDims;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            if ((Directory.Exists(settings.Path) == false || settings.Path == string.Empty) && File.Exists(settings.FileName))
            {
                settings.Path = Path.GetDirectoryName(settings.FileName);
            }

            // Saving .bmp files
            foreach (int dim in dims)
            {
                string outputPath = settings.Path + "\\" + Path.GetFileNameWithoutExtension(settings.FileName) + "_" + dim + "." + MagickFormat.Bmp;
                try
                {
                    Save(settings.FileName, outputPath, dim);
                    MagickImage image = new(outputPath);
                    images.Add(image);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            try
            {
                images.Write(settings.Path + "\\" + Path.GetFileNameWithoutExtension(settings.FileName) + "." + MagickFormat.Ico);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            // Deleting .bmp files
            foreach (int dim in dims)
            {
                string outputPath = settings.Path + "\\" + Path.GetFileNameWithoutExtension(settings.FileName) + "_" + dim + "." + MagickFormat.Bmp;
                try
                {
                    File.Delete(outputPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        static void Main(string[] args)
        {
            string file = string.Empty;
            string directory = string.Empty;
            string file16px = string.Empty;

            Settings settings = new();

            foreach (string arg in args)
            {
                if (arg.StartsWith("-f"))
                {
                    file = arg.Replace("-f ", "");
                }
                else if (arg.StartsWith("-d"))
                {
                    directory = arg.Replace("-d ", "");
                }
                else if (arg.StartsWith("-i"))
                {
                    file16px = arg.Replace("-i ", "");
                }
                else
                {
                    Console.WriteLine($"unknown flag {arg}");
                }
            }

            if (File.Exists(file))
            {
                settings.FileName = file;
            }

            if (directory != string.Empty)
            {
                System.IO.Directory.CreateDirectory(directory);
                settings.Path = directory;
            }

            if (File.Exists(file16px))
            {
                settings.FileName16px = file16px;
            }

            if (File.Exists(settings.FileName))
            {
                MakeIco(settings);
            }

            else
            {
                Console.WriteLine("Input file does not exist");
            }
        }
    }
}