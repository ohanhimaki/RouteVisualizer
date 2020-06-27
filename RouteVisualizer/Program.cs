using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Xml.Serialization;
using RouteVisualizer.Classes;
using System.Linq;
//using static System.Net.Mime.MediaTypeNames;

namespace RouteVisualizer {
    class Program {
        static void Main (string[] args) {
            string testifilu = @"C:\coding\RouteVisualizer\testifilut\2020-06-14T1508340300PT1H57M49905SPyoraily - Copy.tcx";
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

            var bitmap = new Bitmap(size+padding, size+padding);
            Graphics g = Graphics.FromImage(bitmap);
            
            

            var biggermultiplier = (maxLat - minLat > maxLon - minLon) ? maxLat - minLat : maxLon - minLon;
            var multiplier =  (size-1) / biggermultiplier;
            

            var testlocation = (int)((maxLat - minLat) * multiplier);

            //bitmap.SetPixel(10, 10, Color.BlueViolet);
            //bitmap.SetPixel(10+ testlocation, 10, Color.BlueViolet);
            //bitmap.SetPixel(10, 10, Color.BlueViolet);
            //bitmap.SetPixel(10, 10, Color.BlueViolet);

            Pen blackPen = new Pen(Color.Black);

            foreach (TrainingCenterDatabaseActivitiesActivityLapTrackpoint track in  i.Activities.Activity.Lap.Track)
            {
                if(track.Position != null)
                {

                    g.DrawEllipse(blackPen, (int)(padding/2)+(int)((track.Position.LongitudeDegrees - minLon) * multiplier / 2), (int)(padding / 2) + (size - 1) - (int)((track.Position.LatitudeDegrees - minLat) * multiplier), 4,4);
                    //bitmap.SetPixel((int)((track.Position.LongitudeDegrees - minLon) * multiplier / 2), (size - 1) - (int)((track.Position.LatitudeDegrees - minLat) * multiplier), Color.BlueViolet);
                    //bitmap.SetPixel((int)((track.Position.LongitudeDegrees - minLon) * multiplier/2), (size - 1) - (int)((track.Position.LatitudeDegrees-minLat)*multiplier),  Color.BlueViolet);
                }
            }


            bitmap.Save("m.bmp");
            // Create image.
            //Image newImage = System.Drawing.Image.FromFile

            // Create point for upper-left corner of image.
            //PointF ulCorner = new PointF(100.0F, 100.0F);





            Console.WriteLine ("Hello World!");
        }
    }
}