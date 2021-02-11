using Newtonsoft.Json;

namespace FaceRecognitionApiExample.Apis
{
    public partial class Emotions
    {
        [JsonProperty("emotions")]
        public Emotion[] EmotionsEmotions { get; set; }

        [JsonProperty("faceAnnotations")]
        public FaceAnnotation[] FaceAnnotations { get; set; }
    }

    public partial class Emotion
    {
        [JsonProperty("joyLikelihood")]
        public string JoyLikelihood { get; set; }

        [JsonProperty("sorrowLikelihood")]
        public string SorrowLikelihood { get; set; }

        [JsonProperty("angerLikelihood")]
        public string AngerLikelihood { get; set; }

        [JsonProperty("surpriseLikelihood")]
        public string SurpriseLikelihood { get; set; }

        [JsonProperty("underExposedLikelihood")]
        public string UnderExposedLikelihood { get; set; }

        [JsonProperty("blurredLikelihood")]
        public string BlurredLikelihood { get; set; }

        [JsonProperty("headwearLikelihood")]
        public string HeadwearLikelihood { get; set; }
    }

    public partial class FaceAnnotation
    {
        [JsonProperty("landmarks")]
        public Landmark[] Landmarks { get; set; }

        [JsonProperty("boundingPoly")]
        public BoundingPoly BoundingPoly { get; set; }

        [JsonProperty("fdBoundingPoly")]
        public BoundingPoly FdBoundingPoly { get; set; }

        [JsonProperty("rollAngle")]
        public double RollAngle { get; set; }

        [JsonProperty("panAngle")]
        public double PanAngle { get; set; }

        [JsonProperty("tiltAngle")]
        public double TiltAngle { get; set; }

        [JsonProperty("detectionConfidence")]
        public double DetectionConfidence { get; set; }

        [JsonProperty("landmarkingConfidence")]
        public double LandmarkingConfidence { get; set; }

        [JsonProperty("joyLikelihood")]
        public string JoyLikelihood { get; set; }

        [JsonProperty("sorrowLikelihood")]
        public string SorrowLikelihood { get; set; }

        [JsonProperty("angerLikelihood")]
        public string AngerLikelihood { get; set; }

        [JsonProperty("surpriseLikelihood")]
        public string SurpriseLikelihood { get; set; }

        [JsonProperty("underExposedLikelihood")]
        public string UnderExposedLikelihood { get; set; }

        [JsonProperty("blurredLikelihood")]
        public string BlurredLikelihood { get; set; }

        [JsonProperty("headwearLikelihood")]
        public string HeadwearLikelihood { get; set; }
    }

    public partial class BoundingPoly
    {
        [JsonProperty("vertices")]
        public Vertex[] Vertices { get; set; }

        [JsonProperty("normalizedVertices")]
        public object[] NormalizedVertices { get; set; }
    }

    public partial class Vertex
    {
        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }
    }

    public partial class Landmark
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }
    }

    public partial class Position
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }
    }
}
