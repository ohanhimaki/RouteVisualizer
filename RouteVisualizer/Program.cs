using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Xml.Serialization;
using RouteVisualizer.Classes;
using System.Linq;
using Accord.Video;
using Accord;
using FFMediaToolkit.Encoding;
using FFMediaToolkit.Graphics;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net;
using System.Globalization;

namespace RouteVisualizer {
    class Program {
        static void Main (string[] args) {
            string testifilu = @"C:\coding\RouteVisualizer\testifilut\20200530T1640300300PT1H24M56.099SPyoraily.tcx";
            //string testifilu = @"C:\coding\RouteVisualizer\testifilut\2020-06-14T1508340300PT1H57M49905SPyoraily - Copy.tcx";
  
            TrainingCenterDatabase TrainingCenterDatabase = new TrainingCenterDatabase();

            string fullFile = "";
            try
            {
   

                using (StreamReader sr = File.OpenText(testifilu))
                {
                    fullFile = sr.ReadToEnd();
         
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var testi1 = new XmlSerializer(typeof(TrainingCenterDatabase));
            TrainingCenterDatabase i;

            using(Stream reader = new FileStream(testifilu, FileMode.Open))
            {
                i = (TrainingCenterDatabase)testi1.Deserialize(reader);
            }

            var maxLon = i.Activities.Activity.Lap.Track.Where(i => i.Position!= null).Aggregate((i1, i2) => i1.Position.LongitudeDegrees > i2.Position.LongitudeDegrees? i1 : i2).Position.LongitudeDegrees;
            var minLon = i.Activities.Activity.Lap.Track.Where(i => i.Position != null).Aggregate((i1, i2) => i1.Position.LongitudeDegrees < i2.Position.LongitudeDegrees ? i1 : i2).Position.LongitudeDegrees;
            var maxLat = i.Activities.Activity.Lap.Track.Where(i => i.Position != null).Aggregate((i1, i2) => i1.Position.LatitudeDegrees > i2.Position.LatitudeDegrees ? i1 : i2).Position.LatitudeDegrees;
            var minLat = i.Activities.Activity.Lap.Track.Where(i => i.Position != null).Aggregate((i1, i2) => i1.Position.LatitudeDegrees < i2.Position.LatitudeDegrees ? i1 : i2).Position.LatitudeDegrees;

            Console.WriteLine(maxLat + " " + maxLon);
            Console.WriteLine(minLat + " " + maxLon);
            Console.WriteLine(maxLat + " " + minLon);
            Console.WriteLine(minLat + " " + minLon);

            var size = 300;
            var padding = 30;
          

            var totalLat = maxLat - minLat;
            var totalLon = maxLon / 2 - minLon / 2;


            decimal multiplier;
            decimal lonMultiplier;
            decimal latMultiplier;
            decimal centerLon = maxLat - (totalLat/2)*(decimal)1.1;
            decimal centerLat = maxLon - (totalLon); 
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            var zoomLevel = 11.43;


            string openboxApiUrl = String.Format(@"https://api.mapbox.com/styles/v1/mapbox/outdoors-v11/static/{0},{1},{2},0,0/330x330?access_token={3}", 
                centerLat.ToString(nfi), centerLon.ToString(nfi), zoomLevel.ToString(nfi), args[0]);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(openboxApiUrl), @"C:\coding\RouteVisualizer\testifilut\image35.png");
                // OR 
                //client.DownloadFileAsync(new Uri(openboxApiUrl), @"c:\temp\image35.png");
            }


            var bitmap = new Bitmap(size + padding, size + padding);
            Graphics g = Graphics.FromImage(bitmap);

            var taustakuva = Image.FromFile(@"C:\coding\RouteVisualizer\testifilut\image35.png");

            g.DrawImage(taustakuva, 0, 0);

            if (totalLat > totalLon)
            {
                multiplier= (size - 1) / totalLat;
                lonMultiplier = totalLat / totalLon;
                latMultiplier = 1;
            } else
            {
                multiplier = (size - 1) / totalLon;
                lonMultiplier = 1;
                latMultiplier = totalLon/ totalLat;
            }



            var testlocation = (int)((maxLat - minLat) * multiplier);

            //bitmap.SetPixel(10, 10, Color.BlueViolet);
            //bitmap.SetPixel(10+ testlocation, 10, Color.BlueViolet);
            //bitmap.SetPixel(10, 10, Color.BlueViolet);
            //bitmap.SetPixel(10, 10, Color.BlueViolet);

            Brush blackPen = Brushes.Black;
            Brush redPen = Brushes.Red; 

            ////Vihreätausta debukkaukseen
            //var tmpx = 0;
            //var tmpy = 0;
            //while (tmpx < size+padding)
            //{
            //    tmpy = 0;
            //    while (tmpy < size+padding)
            //    {

            //        g.DrawEllipse(new Pen(Color.Green), tmpx, tmpy, 1, 1);
            //        tmpy  +=1;
            //    }
            //    tmpx +=1;
            //}

      

            foreach (TrainingCenterDatabaseActivitiesActivityLapTrackpoint track in  i.Activities.Activity.Lap.Track)
            {
                if(track.Position != null)
                {
                    var xCoords = (int)(padding / 2) + (int)(((size*lonMultiplier)-size)/4) + (int)((track.Position.LongitudeDegrees - minLon) * multiplier / 2);
                    var yCoords =(int)(padding/2 ) + (int)(size*latMultiplier  - 1) - (int)((track.Position.LatitudeDegrees - minLat) * multiplier);
                    g.FillEllipse(blackPen, xCoords, yCoords, 4,4);
                    //bitmap.SetPixel((int)((track.Position.LongitudeDegrees - minLon) * multiplier / 2), (size - 1) - (int)((track.Position.LatitudeDegrees - minLat) * multiplier), Color.BlueViolet);
                    //bitmap.SetPixel((int)((track.Position.LongitudeDegrees - minLon) * multiplier/2), (size - 1) - (int)((track.Position.LatitudeDegrees-minLat)*multiplier),  Color.BlueViolet);
                }
            }


            //bitmap.Save("m.bmp");
            var i2 = 0;
            var HeartRateTmp = "";
            var distanceTmp = "";

            var startTime = i.Activities.Activity.Lap.StartTime;
            var endTime = startTime.AddSeconds(300);
            //var endTime = startTime.AddSeconds((int)i.Activities.Activity.Lap.TotalTimeSeconds);
            var filenamePrefix = i.Activities.Activity.Lap.StartTime.ToString("yyyyMMddHHmmss");

            while (startTime < endTime)
            {
                TimeSpan span = startTime - endTime;
                Console.WriteLine(span.TotalSeconds);

                var newestTrackpoint =  i.Activities.Activity.Lap.Track.Where(n => n.Time <= startTime).Aggregate((i1, i2) => i1.Time > i2.Time ? i1 : i2);
                var editbitmap = new Bitmap(bitmap);
                Graphics gedit = Graphics.FromImage(editbitmap);

                HeartRateTmp = newestTrackpoint.HeartRateBpm?.Value.ToString("#") ?? HeartRateTmp;
                distanceTmp = (newestTrackpoint.DistanceMeters / 1000).ToString("#.##") ?? distanceTmp;
                if (newestTrackpoint.Position != null)
                {

                    var xCoords = (int)(padding / 2) + (int)(((size * lonMultiplier) - size) / 4) + (int)((newestTrackpoint.Position.LongitudeDegrees - minLon) * multiplier / 2);
                    var yCoords = (int)(padding / 2) + (int)(size * latMultiplier - 1) - (int)((newestTrackpoint.Position.LatitudeDegrees - minLat) * multiplier);
                    gedit.FillEllipse(redPen, xCoords, yCoords, 8, 8);

                    //gedit.FillEllipse(redPen, (int)(padding / 2) + (int)((newestTrackpoint.Position.LongitudeDegrees - minLon) * multiplier / 2), (int)(padding / 2) + (size - 1) - (int)((newestTrackpoint.Position.LatitudeDegrees - minLat) * multiplier), 8, 8);
                    gedit.SmoothingMode = SmoothingMode.AntiAlias;
                    gedit.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gedit.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gedit.DrawString(HeartRateTmp, new Font("Tahoma", 8), Brushes.Black, new PointF(0, 12));
                    gedit.DrawString(distanceTmp, new Font("Tahoma", 8), Brushes.Black, new PointF(0, 0));

                    //gedit.Flush();

                    editbitmap.Save(string.Format(@"C:\coding\RouteVisualizer\testifilut\testi3\{0}-{1}.png", filenamePrefix, i2));

                    //bitmap.SetPixel((int)((track.Position.LongitudeDegrees - minLon) * multiplier / 2), (size - 1) - (int)((track.Position.LatitudeDegrees - minLat) * multiplier), Color.BlueViolet);
                    //bitmap.SetPixel((int)((track.Position.LongitudeDegrees - minLon) * multiplier/2), (size - 1) - (int)((track.Position.LatitudeDegrees-minLat)*multiplier),  Color.BlueViolet);
                    i2++;
                }
                Console.WriteLine(startTime);

                startTime = startTime.AddSeconds(1);
                Console.WriteLine(startTime);

            }

            //foreach (TrainingCenterDatabaseActivitiesActivityLapTrackpoint track in i.Activities.Activity.Lap.Track)
            //{
            //    var editbitmap = new Bitmap(bitmap);
            //    Graphics gedit = Graphics.FromImage(editbitmap);

            //    HeartRateTmp = track.HeartRateBpm?.Value.ToString() ?? HeartRateTmp;
            //    if (track.Position != null)
            //    {


            //        gedit.FillEllipse(redPen, (int)(padding / 2) + (int)((track.Position.LongitudeDegrees - minLon) * multiplier / 2), (int)(padding / 2) + (size - 1) - (int)((track.Position.LatitudeDegrees - minLat) * multiplier), 8, 8);
            //        gedit.SmoothingMode = SmoothingMode.AntiAlias;
            //        gedit.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //        gedit.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //        gedit.DrawString(HeartRateTmp, new Font("Tahoma", 18), Brushes.Black, new PointF(0, 0));

            //        //gedit.Flush();

            //        editbitmap.Save(string.Format(@"C:\coding\RouteVisualizer\testifilut\testi2\{0}.png",i2));

            //        //bitmap.SetPixel((int)((track.Position.LongitudeDegrees - minLon) * multiplier / 2), (size - 1) - (int)((track.Position.LatitudeDegrees - minLat) * multiplier), Color.BlueViolet);
            //        //bitmap.SetPixel((int)((track.Position.LongitudeDegrees - minLon) * multiplier/2), (size - 1) - (int)((track.Position.LatitudeDegrees-minLat)*multiplier),  Color.BlueViolet);
            //        i2++;
            //    }
            //}



            // Create image.
            //Image newImage = System.Drawing.Image.FromFile

            // Create point for upper-left corner of image.
            //PointF ulCorner = new PointF(100.0F, 100.0F);
            //    var writer = new Accord.Video.
            //    AForge.Video.VideoFileWriter writer = new VideoFileWriter();
            //    writer.Open("myfile.avi", width, height, 25, VideoCodec.MPEG4, 1000000);
            //    // ... here you'll need to load your bitmaps
            //    writer.WriteVideoFrame(image);
            //}
            //writer.Close();


            //var settings = new VideoEncoderSettings(width: 1920, height: 1080);
            //var file = new MediaBuilder(@"C:\coding\RouteVisualizer\testifilut\newvideo.mp4").WithVideo(settings).Create();
            //while (file.Video.FramesCount < 300)
            //{
            //    BitmapData bData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            //    file.Video.AddFrame(new ImageData(new Span<byte>(), ImagePixelFormat.Bgra32, 1920,1080));
            //}


            Console.WriteLine ("Hello World!");
        }
    }
}