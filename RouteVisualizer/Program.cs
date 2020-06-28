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
using Geolocation;

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

            var coordminmin = new Coordinate((double)minLat, (double)minLon);
            var coordminmax = new Coordinate((double)minLat, (double)maxLon);
            var coordmaxmin = new Coordinate((double)maxLat, (double)minLon);
            var coordmaxmax = new Coordinate((double)maxLat, (double)maxLon);

            double distancemaxlat = GeoCalculator.GetDistance(coordmaxmax, coordmaxmin, 1, DistanceUnit.Meters);
            double distanceminlat = GeoCalculator.GetDistance(coordminmax, coordminmin, 1, DistanceUnit.Meters);

            double distancemaxlon = GeoCalculator.GetDistance(coordmaxmax, coordminmax, 3, DistanceUnit.Meters);
            double distanceminlon = GeoCalculator.GetDistance(coordmaxmin, coordminmin, 3, DistanceUnit.Meters);


            var size = 300;
            var padding = 30;
          

            var totalLat = (decimal)distancemaxlat;
            var totalLon = (decimal)distancemaxlon;
            var imgRatio = totalLat / totalLon;
            var totalLatDegrees = maxLat - minLat;
            var totalLonDegrees = maxLon / 2 - minLon / 2;



            decimal multiplier;
            decimal lonMultiplier;
            decimal latMultiplier;
            decimal centerLon = maxLat - (totalLatDegrees / 2);
            decimal centerLat = maxLon - (totalLonDegrees); 
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";


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
            decimal multiplierFormMapboxZoom = (decimal)846.074929615;
           var zoomLevel = (decimal)11.67;


            string openboxApiUrl = String.Format(@"https://api.mapbox.com/styles/v1/mapbox/outdoors-v11/static/{0},{1},{2},0,0/{3}x{4}?access_token={5}",
                centerLat.ToString(nfi), centerLon.ToString(nfi),
                zoomLevel.ToString(nfi), (int)(size * imgRatio),
                size, args[0]);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(openboxApiUrl), @"C:\coding\RouteVisualizer\testifilut\image35.png");
            }


          var bitmap = new Bitmap((int)(size * imgRatio), size );
            Graphics g = Graphics.FromImage(bitmap);

            var taustakuva = Image.FromFile(@"C:\coding\RouteVisualizer\testifilut\image35.png");
            g.DrawImage(taustakuva, 0, 0);



        
            Brush blackPen = Brushes.Black;
            Brush redPen = Brushes.Red;
            Brush greenPen = Brushes.Green;

   

            foreach (TrainingCenterDatabaseActivitiesActivityLapTrackpoint track in  i.Activities.Activity.Lap.Track)
            {
                if(track.Position != null)
                {
                 var tmpMinLon = new Coordinate((double)track.Position.LatitudeDegrees, (double)minLon);
                    var tmpMinLat = new Coordinate((double)minLat, (double)track.Position.LongitudeDegrees);
                    var tmpCurrentCoords = new Coordinate((double)track.Position.LatitudeDegrees, (double)track.Position.LongitudeDegrees);
                    
                    var xCoords =   (int)((decimal)GeoCalculator.GetDistance(tmpMinLon,tmpCurrentCoords,2,DistanceUnit.Meters)  * multiplier);
                    var yCoords = (int)( ((decimal)distancemaxlon - (decimal)GeoCalculator.GetDistance(tmpMinLat, tmpCurrentCoords, 2, DistanceUnit.Meters)) * multiplier);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.FillEllipse(blackPen, xCoords, yCoords, 4,4);
               }
            }


            var i2 = 0;
            var HeartRateTmp = "";
            var distanceTmp = "";

            var startTime = i.Activities.Activity.Lap.StartTime;
            //var endTime = startTime.AddSeconds(300); //kehitys
            var endTime = startTime.AddSeconds((int)i.Activities.Activity.Lap.TotalTimeSeconds);
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
                    var tmpMinLon = new Coordinate((double)newestTrackpoint.Position.LatitudeDegrees, (double)minLon);
                    var tmpMinLat = new Coordinate((double)minLat, (double)newestTrackpoint.Position.LongitudeDegrees);
                    var tmpCurrentCoords = new Coordinate((double)newestTrackpoint.Position.LatitudeDegrees, (double)newestTrackpoint.Position.LongitudeDegrees);

                    var xCoords = (int)((decimal)GeoCalculator.GetDistance(tmpMinLon, tmpCurrentCoords, 2, DistanceUnit.Meters) * multiplier);
                    var yCoords = (int)(((decimal)distancemaxlon - (decimal)GeoCalculator.GetDistance(tmpMinLat, tmpCurrentCoords, 2, DistanceUnit.Meters)) * multiplier);
                    gedit.SmoothingMode = SmoothingMode.AntiAlias;
                   gedit.InterpolationMode = InterpolationMode.HighQualityBicubic;
                   gedit.PixelOffsetMode = PixelOffsetMode.HighQuality;
                   gedit.FillEllipse(greenPen, xCoords, yCoords, 8, 8);
                    g.FillEllipse(redPen, xCoords, yCoords, 4, 4);

                    gedit.DrawString(HeartRateTmp, new Font("Tahoma", 8), Brushes.Black, new PointF(0, 12));
                    gedit.DrawString(distanceTmp, new Font("Tahoma", 8), Brushes.Black, new PointF(0, 0));

                    editbitmap.Save(string.Format(@"C:\coding\RouteVisualizer\testifilut\testi3\{0}-{1}.png", filenamePrefix, i2));

                   i2++;
                }
                Console.WriteLine(startTime);

                startTime = startTime.AddSeconds(1);
                Console.WriteLine(startTime);

            }




            Console.WriteLine ("Hello World!");
        }
    }
}