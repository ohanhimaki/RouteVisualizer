using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using RouteVisualizer.Classes;
using System.Linq;

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




            Console.WriteLine ("Hello World!");
        }
    }
}