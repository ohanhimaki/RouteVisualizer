using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RouteVisualizer.Classes
{


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2", IsNullable = false)]
    public partial class TrainingCenterDatabase
    {

        private TrainingCenterDatabaseActivities activitiesField;

        private TrainingCenterDatabaseAuthor authorField;

        /// <remarks/>
        public TrainingCenterDatabaseActivities Activities
        {
            get
            {
                return this.activitiesField;
            }
            set
            {
                this.activitiesField = value;
            }
        }

        /// <remarks/>
        public TrainingCenterDatabaseAuthor Author
        {
            get
            {
                return this.authorField;
            }
            set
            {
                this.authorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseActivities
    {

        private TrainingCenterDatabaseActivitiesActivity activityField;

        /// <remarks/>
        public TrainingCenterDatabaseActivitiesActivity Activity
        {
            get
            {
                return this.activityField;
            }
            set
            {
                this.activityField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseActivitiesActivity
    {

        private System.DateTime idField;

        private string notesField;

        private TrainingCenterDatabaseActivitiesActivityLap lapField;

        private string sportField;

        /// <remarks/>
        public System.DateTime Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string Notes
        {
            get
            {
                return this.notesField;
            }
            set
            {
                this.notesField = value;
            }
        }

        /// <remarks/>
        public TrainingCenterDatabaseActivitiesActivityLap Lap
        {
            get
            {
                return this.lapField;
            }
            set
            {
                this.lapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Sport
        {
            get
            {
                return this.sportField;
            }
            set
            {
                this.sportField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseActivitiesActivityLap
    {

        private TrainingCenterDatabaseActivitiesActivityLapTrackpoint[] trackField;

        private decimal distanceMetersField;

        private decimal totalTimeSecondsField;

        private decimal caloriesField;

        private TrainingCenterDatabaseActivitiesActivityLapAverageHeartRateBpm averageHeartRateBpmField;

        private TrainingCenterDatabaseActivitiesActivityLapMaximumHeartRateBpm maximumHeartRateBpmField;

        private string intensityField;

        private string triggerMethodField;

        private System.DateTime startTimeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Trackpoint", IsNullable = false)]
        public TrainingCenterDatabaseActivitiesActivityLapTrackpoint[] Track
        {
            get
            {
                return this.trackField;
            }
            set
            {
                this.trackField = value;
            }
        }

        /// <remarks/>
        public decimal DistanceMeters
        {
            get
            {
                return this.distanceMetersField;
            }
            set
            {
                this.distanceMetersField = value;
            }
        }

        /// <remarks/>
        public decimal TotalTimeSeconds
        {
            get
            {
                return this.totalTimeSecondsField;
            }
            set
            {
                this.totalTimeSecondsField = value;
            }
        }

        /// <remarks/>
        public decimal Calories
        {
            get
            {
                return this.caloriesField;
            }
            set
            {
                this.caloriesField = value;
            }
        }

        /// <remarks/>
        public TrainingCenterDatabaseActivitiesActivityLapAverageHeartRateBpm AverageHeartRateBpm
        {
            get
            {
                return this.averageHeartRateBpmField;
            }
            set
            {
                this.averageHeartRateBpmField = value;
            }
        }

        /// <remarks/>
        public TrainingCenterDatabaseActivitiesActivityLapMaximumHeartRateBpm MaximumHeartRateBpm
        {
            get
            {
                return this.maximumHeartRateBpmField;
            }
            set
            {
                this.maximumHeartRateBpmField = value;
            }
        }

        /// <remarks/>
        public string Intensity
        {
            get
            {
                return this.intensityField;
            }
            set
            {
                this.intensityField = value;
            }
        }

        /// <remarks/>
        public string TriggerMethod
        {
            get
            {
                return this.triggerMethodField;
            }
            set
            {
                this.triggerMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseActivitiesActivityLapTrackpoint
    {

        private decimal distanceMetersField;

        private System.DateTime timeField;

        private TrainingCenterDatabaseActivitiesActivityLapTrackpointHeartRateBpm heartRateBpmField;

        private TrainingCenterDatabaseActivitiesActivityLapTrackpointPosition positionField;

        private decimal altitudeMetersField;

        private bool altitudeMetersFieldSpecified;

        /// <remarks/>
        public decimal DistanceMeters
        {
            get
            {
                return this.distanceMetersField;
            }
            set
            {
                this.distanceMetersField = value;
            }
        }

        /// <remarks/>
        public System.DateTime Time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
            }
        }

        /// <remarks/>
        public TrainingCenterDatabaseActivitiesActivityLapTrackpointHeartRateBpm HeartRateBpm
        {
            get
            {
                return this.heartRateBpmField;
            }
            set
            {
                this.heartRateBpmField = value;
            }
        }

        /// <remarks/>
        public TrainingCenterDatabaseActivitiesActivityLapTrackpointPosition Position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }

        /// <remarks/>
        public decimal AltitudeMeters
        {
            get
            {
                return this.altitudeMetersField;
            }
            set
            {
                this.altitudeMetersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AltitudeMetersSpecified
        {
            get
            {
                return this.altitudeMetersFieldSpecified;
            }
            set
            {
                this.altitudeMetersFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseActivitiesActivityLapTrackpointHeartRateBpm
    {

        private decimal valueField;

        /// <remarks/>
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseActivitiesActivityLapTrackpointPosition
    {

        private decimal latitudeDegreesField;

        private decimal longitudeDegreesField;

        /// <remarks/>
        public decimal LatitudeDegrees
        {
            get
            {
                return this.latitudeDegreesField;
            }
            set
            {
                this.latitudeDegreesField = value;
            }
        }

        /// <remarks/>
        public decimal LongitudeDegrees
        {
            get
            {
                return this.longitudeDegreesField;
            }
            set
            {
                this.longitudeDegreesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseActivitiesActivityLapAverageHeartRateBpm
    {

        private decimal valueField;

        /// <remarks/>
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseActivitiesActivityLapMaximumHeartRateBpm
    {

        private decimal valueField;

        /// <remarks/>
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseAuthor
    {

        private string nameField;

        private TrainingCenterDatabaseAuthorBuild buildField;

        private string langIDField;

        private string partNumberField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public TrainingCenterDatabaseAuthorBuild Build
        {
            get
            {
                return this.buildField;
            }
            set
            {
                this.buildField = value;
            }
        }

        /// <remarks/>
        public string LangID
        {
            get
            {
                return this.langIDField;
            }
            set
            {
                this.langIDField = value;
            }
        }

        /// <remarks/>
        public string PartNumber
        {
            get
            {
                return this.partNumberField;
            }
            set
            {
                this.partNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseAuthorBuild
    {

        private TrainingCenterDatabaseAuthorBuildVersion versionField;

        /// <remarks/>
        public TrainingCenterDatabaseAuthorBuildVersion Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
    public partial class TrainingCenterDatabaseAuthorBuildVersion
    {

        private byte versionMajorField;

        private byte versionMinorField;

        private byte buildMajorField;

        private byte buildMinorField;

        /// <remarks/>
        public byte VersionMajor
        {
            get
            {
                return this.versionMajorField;
            }
            set
            {
                this.versionMajorField = value;
            }
        }

        /// <remarks/>
        public byte VersionMinor
        {
            get
            {
                return this.versionMinorField;
            }
            set
            {
                this.versionMinorField = value;
            }
        }

        /// <remarks/>
        public byte BuildMajor
        {
            get
            {
                return this.buildMajorField;
            }
            set
            {
                this.buildMajorField = value;
            }
        }

        /// <remarks/>
        public byte BuildMinor
        {
            get
            {
                return this.buildMinorField;
            }
            set
            {
                this.buildMinorField = value;
            }
        }
    }



}
