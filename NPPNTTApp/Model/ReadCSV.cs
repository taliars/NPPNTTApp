using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NPPNTTApp.Model
{
    public static class ReadCSV
    {
        public static  List<BaseClass> ReadFile()
        {
            List<BaseClass> outPutList = new List<BaseClass>();

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("csv файлы", "*.csv"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string[] lines = File.ReadAllLines(dialog.FileName);

                var data = from l in lines.Skip(1)
                            let split = l.Split(';')
                             select new BaseClass
                             {
                                 Date = DateTime.Parse(split[0]),
                                 ObjectA = split[1],
                                 TypeA = split[2],
                                 ObjectB = split[3],
                                 TypeB = split[4],
                                 Direction = (Direction)Enum.Parse(typeof(Direction), split[5]),
                                 Color = split[6],
                                 Intensity = int.Parse(split[7]),
                                 LatitudeA = double.Parse(split[8], CultureInfo.InvariantCulture),
                                 LongitudeA = double.Parse(split[9], CultureInfo.InvariantCulture),
                                 LatitudeB = double.Parse(split[10], CultureInfo.InvariantCulture),
                                 LongitudeB = double.Parse(split[11], CultureInfo.InvariantCulture),
                             };

                outPutList = data.ToList();
            }

            return outPutList;
        }

        public static async Task<List<BaseClass>> Get(
            IProgress<BaseProgressData> progress, 
            CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<BaseClass> outPutList = new List<BaseClass>();

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("csv файлы", "*.csv"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                using (var reader = File.OpenText(dialog.FileName))
                {
                    var fileText = await reader.ReadToEndAsync();
                    string[] linesa =  fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                    string[] some = linesa.Skip(1).ToArray();

                    List<BaseClass> data = new List<BaseClass>();

                    foreach (var item in some)
                    {
                        if (string.IsNullOrEmpty(item)) break;
                        
                        string[] split = item.Split(';');

                        BaseClass baseClass = new BaseClass
                        {
                            Date = DateTime.Parse(split[0]),
                            ObjectA = split[1],
                            TypeA = split[2],
                            ObjectB = split[3],
                            TypeB = split[4],
                            Direction = (Direction)Enum.Parse(typeof(Direction), split[5]),
                            Color = split[6],
                            Intensity = int.Parse(split[7]),
                            LatitudeA = double.Parse(split[8], CultureInfo.InvariantCulture),
                            LongitudeA = double.Parse(split[9], CultureInfo.InvariantCulture),
                            LatitudeB = double.Parse(split[10], CultureInfo.InvariantCulture),
                            LongitudeB = double.Parse(split[11], CultureInfo.InvariantCulture),
                        };

                        data.Add(baseClass);
                    }

                    //var data = from l in linesa.Skip(1)
                    //           let split = l.Split(';')
                    //           select new BaseClass
                    //           {
                    //               //Date = DateTime.Parse(split[0]),
                    //               ObjectA = split[1],
                    //               TypeA = split[2],
                    //               ObjectB = split[3],
                    //               TypeB = split[4],
                    //               Direction = (Direction)Enum.Parse(typeof(Direction), split[5]),
                    //               Color = split[6],
                    //               Intensity = int.Parse(split[7]),
                    //               LatitudeA = double.Parse(split[8], CultureInfo.InvariantCulture),
                    //               LongitudeA = double.Parse(split[9], CultureInfo.InvariantCulture),
                    //               LatitudeB = double.Parse(split[10], CultureInfo.InvariantCulture),
                    //               LongitudeB = double.Parse(split[11], CultureInfo.InvariantCulture),
                    //           };

                    for (int i = 0; i < data.Count(); i++)
                    {
                        progress?.Report(new BaseProgressData(i + 1, data.Count()));
                        await Task.Delay(900);
                    }

                    outPutList = data.ToList();
                }
            }

            return outPutList;
        }
    }
}
