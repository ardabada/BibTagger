using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BibCore.FaceApi.Models
{
    public class FaceLandmarks : IFaceApiData
    {
        public FacePoint ContourChin { get; set; }
        public FacePoint ContourLeft1 { get; set; }
        public FacePoint ContourLeft2 { get; set; }
        public FacePoint ContourLeft3 { get; set; }
        public FacePoint ContourLeft4 { get; set; }
        public FacePoint ContourLeft5 { get; set; }
        public FacePoint ContourLeft6 { get; set; }
        public FacePoint ContourLeft7 { get; set; }
        public FacePoint ContourLeft8 { get; set; }
        public FacePoint ContourLeft9 { get; set; }
        public FacePoint ContourRight1 { get; set; }
        public FacePoint ContourRight2 { get; set; }
        public FacePoint ContourRight3 { get; set; }
        public FacePoint ContourRight4 { get; set; }
        public FacePoint ContourRight5 { get; set; }
        public FacePoint ContourRight6 { get; set; }
        public FacePoint ContourRight7 { get; set; }
        public FacePoint ContourRight8 { get; set; }
        public FacePoint ContourRight9 { get; set; }
        public FacePoint LeftEyeBottom { get; set; }
        public FacePoint LeftEyeCenter { get; set; }
        public FacePoint LeftEyeLeftCorner { get; set; }
        public FacePoint LeftEyeLowerLeftQuarter { get; set; }
        public FacePoint LeftEyeLowerRightQuarter { get; set; }
        public FacePoint LeftEyePupil { get; set; }
        public FacePoint LeftEyeRightCorner { get; set; }
        public FacePoint LeftEyeTop { get; set; }
        public FacePoint LeftEyeUpperLeftQuarter { get; set; }
        public FacePoint LeftEyeUpperRightQuarter { get; set; }
        public FacePoint LeftEyebrowLeftCorner { get; set; }
        public FacePoint LeftEyebrowLowerLeftQuarter { get; set; }
        public FacePoint LeftEyebrowLowerMiddle { get; set; }
        public FacePoint LeftEyebrowLowerRightQuarter { get; set; }
        public FacePoint LeftEyebrowRightCorner { get; set; }
        public FacePoint LeftEyebrowUpperLeftQuarter { get; set; }
        public FacePoint LeftEyebrowUpperMiddle { get; set; }
        public FacePoint LeftEyebrowUpperRightQuarter { get; set; }
        public FacePoint MouthLeftCorner { get; set; }
        public FacePoint MouthLowerLipBottom { get; set; }
        public FacePoint MouthLowerLipLeftContour1 { get; set; }
        public FacePoint MouthLowerLipLeftContour2 { get; set; }
        public FacePoint MouthLowerLipLeftContour3 { get; set; }
        public FacePoint MouthLowerLipRightContour1 { get; set; }
        public FacePoint MouthLowerLipRightContour2 { get; set; }
        public FacePoint MouthLowerLipRightContour3 { get; set; }
        public FacePoint MouthLowerLipTop { get; set; }
        public FacePoint MouthRightCorner { get; set; }
        public FacePoint MouthUpperLipBottom { get; set; }
        public FacePoint MouthUpperLipLeftContour1 { get; set; }
        public FacePoint MouthUpperLipLeftContour2 { get; set; }
        public FacePoint MouthUpperLipLeftContour3 { get; set; }
        public FacePoint MouthUpperLipRightContour1 { get; set; }
        public FacePoint MouthUpperLipRightContour2 { get; set; }
        public FacePoint MouthUpperLipRightContour3 { get; set; }
        public FacePoint MouthUpperLipTop { get; set; }
        public FacePoint NoseContourLeft1 { get; set; }
        public FacePoint NoseContourLeft2 { get; set; }
        public FacePoint NoseContourLeft3 { get; set; }
        public FacePoint NoseContourLowerMiddle { get; set; }
        public FacePoint NoseContourRight1 { get; set; }
        public FacePoint NoseContourRight2 { get; set; }
        public FacePoint NoseContourRight3 { get; set; }
        public FacePoint NoseLeft { get; set; }
        public FacePoint NoseRight { get; set; }
        public FacePoint NoseTip { get; set; }
        public FacePoint RightEyeBottom { get; set; }
        public FacePoint RightEyeCenter { get; set; }
        public FacePoint RightEyeLeftCorner { get; set; }
        public FacePoint RightEyeLowerLeftQuarter { get; set; }
        public FacePoint RightEyeLowerRightQuarter { get; set; }
        public FacePoint RightEyePupil { get; set; }
        public FacePoint RightEyeRightCorner { get; set; }
        public FacePoint RightEyeTop { get; set; }
        public FacePoint RightEyeUpperLeftQuarter { get; set; }
        public FacePoint RightEyeUpperRightQuarter { get; set; }
        public FacePoint RightEyebrowLeftCorner { get; set; }
        public FacePoint RightEyebrowLowerLeftQuarter { get; set; }
        public FacePoint RightEyebrowLowerMiddle { get; set; }
        public FacePoint RightEyebrowLowerRightQuarter { get; set; }
        public FacePoint RightEyebrowRightCorner { get; set; }
        public FacePoint RightEyebrowUpperLeftQuarter { get; set; }
        public FacePoint RightEyebrowUpperMiddle { get; set; }
        public FacePoint RightEyebrowUpperRightQuarter { get; set; }

        public FaceLandmarks() { }
        public FaceLandmarks(JToken json)
        {
            this.Read(json);
        }

        public List<FacePoint> ToList()
        {
            return new List<FacePoint>()
            {
                ContourChin,
                ContourLeft1,
                ContourLeft2,
                ContourLeft3,
                ContourLeft4,
                ContourLeft5,
                ContourLeft6,
                ContourLeft7,
                ContourLeft8,
                ContourLeft9,
                ContourRight1,
                ContourRight2,
                ContourRight3,
                ContourRight4,
                ContourRight5,
                ContourRight6,
                ContourRight7,
                ContourRight8,
                ContourRight9,
                LeftEyeBottom,
                LeftEyeCenter,
                LeftEyeLeftCorner,
                LeftEyeLowerLeftQuarter,
                LeftEyeLowerRightQuarter,
                LeftEyePupil,
                LeftEyeRightCorner,
                LeftEyeTop,
                LeftEyeUpperLeftQuarter,
                LeftEyeUpperRightQuarter,
                LeftEyebrowLeftCorner,
                LeftEyebrowLowerLeftQuarter,
                LeftEyebrowLowerMiddle,
                LeftEyebrowLowerRightQuarter,
                LeftEyebrowRightCorner,
                LeftEyebrowUpperLeftQuarter,
                LeftEyebrowUpperMiddle,
                LeftEyebrowUpperRightQuarter,
                MouthLeftCorner,
                MouthLowerLipBottom,
                MouthLowerLipLeftContour1,
                MouthLowerLipLeftContour2,
                MouthLowerLipLeftContour3,
                MouthLowerLipRightContour1,
                MouthLowerLipRightContour2,
                MouthLowerLipRightContour3,
                MouthLowerLipTop,
                MouthRightCorner,
                MouthUpperLipBottom,
                MouthUpperLipLeftContour1,
                MouthUpperLipLeftContour2,
                MouthUpperLipLeftContour3,
                MouthUpperLipRightContour1,
                MouthUpperLipRightContour2,
                MouthUpperLipRightContour3,
                MouthUpperLipTop,
                NoseContourLeft1,
                NoseContourLeft2,
                NoseContourLeft3,
                NoseContourLowerMiddle,
                NoseContourRight1,
                NoseContourRight2,
                NoseContourRight3,
                NoseLeft,
                NoseRight,
                NoseTip,
                RightEyeBottom,
                RightEyeCenter,
                RightEyeLeftCorner,
                RightEyeLowerLeftQuarter,
                RightEyeLowerRightQuarter,
                RightEyePupil,
                RightEyeRightCorner,
                RightEyeTop,
                RightEyeUpperLeftQuarter,
                RightEyeUpperRightQuarter,
                RightEyebrowLeftCorner,
                RightEyebrowLowerLeftQuarter,
                RightEyebrowLowerMiddle,
                RightEyebrowLowerRightQuarter,
                RightEyebrowRightCorner,
                RightEyebrowUpperLeftQuarter,
                RightEyebrowUpperMiddle,
                RightEyebrowUpperRightQuarter
            };
        }

        public void Read(JToken json)
        {
            this.ContourChin = new FacePoint(json["contour_chin"]);
            this.ContourLeft1 = new FacePoint(json["contour_left1"]);
            this.ContourLeft2 = new FacePoint(json["contour_left2"]);
            this.ContourLeft3 = new FacePoint(json["contour_left3"]);
            this.ContourLeft4 = new FacePoint(json["contour_left4"]);
            this.ContourLeft5 = new FacePoint(json["contour_left5"]);
            this.ContourLeft6 = new FacePoint(json["contour_left6"]);
            this.ContourLeft7 = new FacePoint(json["contour_left7"]);
            this.ContourLeft8 = new FacePoint(json["contour_left8"]);
            this.ContourLeft9 = new FacePoint(json["contour_left9"]);
            this.ContourRight1 = new FacePoint(json["contour_right1"]);
            this.ContourRight2 = new FacePoint(json["contour_right2"]);
            this.ContourRight3 = new FacePoint(json["contour_right3"]);
            this.ContourRight4 = new FacePoint(json["contour_right4"]);
            this.ContourRight5 = new FacePoint(json["contour_right5"]);
            this.ContourRight6 = new FacePoint(json["contour_right6"]);
            this.ContourRight7 = new FacePoint(json["contour_right7"]);
            this.ContourRight8 = new FacePoint(json["contour_right8"]);
            this.ContourRight9 = new FacePoint(json["contour_right9"]);
            this.LeftEyeBottom = new FacePoint(json["left_eye_bottom"]);
            this.LeftEyeCenter = new FacePoint(json["left_eye_center"]);
            this.LeftEyeLeftCorner = new FacePoint(json["left_eye_left_corner"]);
            this.LeftEyeLowerLeftQuarter = new FacePoint(json["left_eye_lower_left_quarter"]);
            this.LeftEyeLowerRightQuarter = new FacePoint(json["left_eye_lower_right_quarter"]);
            this.LeftEyePupil = new FacePoint(json["left_eye_pupil"]);
            this.LeftEyeRightCorner = new FacePoint(json["left_eye_right_corner"]);
            this.LeftEyeTop = new FacePoint(json["left_eye_top"]);
            this.LeftEyeUpperLeftQuarter = new FacePoint(json["left_eye_upper_left_quarter"]);
            this.LeftEyeUpperRightQuarter = new FacePoint(json["left_eye_upper_right_quarter"]);
            this.LeftEyebrowLeftCorner = new FacePoint(json["left_eyebrow_left_corner"]);
            this.LeftEyebrowLowerLeftQuarter = new FacePoint(json["left_eyebrow_lower_left_quarter"]);
            this.LeftEyebrowLowerMiddle = new FacePoint(json["left_eyebrow_lower_middle"]);
            this.LeftEyebrowLowerRightQuarter = new FacePoint(json["left_eyebrow_lower_right_quarter"]);
            this.LeftEyebrowRightCorner = new FacePoint(json["left_eyebrow_right_corner"]);
            this.LeftEyebrowUpperLeftQuarter = new FacePoint(json["left_eyebrow_upper_left_quarter"]);
            this.LeftEyebrowUpperMiddle = new FacePoint(json["left_eyebrow_upper_middle"]);
            this.LeftEyebrowUpperRightQuarter = new FacePoint(json["left_eyebrow_upper_right_quarter"]);
            this.MouthLeftCorner = new FacePoint(json["mouth_left_corner"]);
            this.MouthLowerLipBottom = new FacePoint(json["mouth_lower_lip_bottom"]);
            this.MouthLowerLipLeftContour1 = new FacePoint(json["mouth_lower_lip_left_contour1"]);
            this.MouthLowerLipLeftContour2 = new FacePoint(json["mouth_lower_lip_left_contour2"]);
            this.MouthLowerLipLeftContour3 = new FacePoint(json["mouth_lower_lip_left_contour3"]);
            this.MouthLowerLipRightContour1 = new FacePoint(json["mouth_lower_lip_right_contour1"]);
            this.MouthLowerLipRightContour2 = new FacePoint(json["mouth_lower_lip_right_contour2"]);
            this.MouthLowerLipRightContour3 = new FacePoint(json["mouth_lower_lip_right_contour3"]);
            this.MouthLowerLipTop = new FacePoint(json["mouth_lower_lip_top"]);
            this.MouthRightCorner = new FacePoint(json["mouth_right_corner"]);
            this.MouthUpperLipBottom = new FacePoint(json["mouth_upper_lip_bottom"]);
            this.MouthUpperLipLeftContour1 = new FacePoint(json["mouth_upper_lip_left_contour1"]);
            this.MouthUpperLipLeftContour2 = new FacePoint(json["mouth_upper_lip_left_contour2"]);
            this.MouthUpperLipLeftContour3 = new FacePoint(json["mouth_upper_lip_left_contour3"]);
            this.MouthUpperLipRightContour1 = new FacePoint(json["mouth_upper_lip_right_contour1"]);
            this.MouthUpperLipRightContour2 = new FacePoint(json["mouth_upper_lip_right_contour2"]);
            this.MouthUpperLipRightContour3 = new FacePoint(json["mouth_upper_lip_right_contour3"]);
            this.MouthUpperLipTop = new FacePoint(json["mouth_upper_lip_top"]);
            this.NoseContourLeft1 = new FacePoint(json["nose_contour_left1"]);
            this.NoseContourLeft2 = new FacePoint(json["nose_contour_left2"]);
            this.NoseContourLeft3 = new FacePoint(json["nose_contour_left3"]);
            this.NoseContourLowerMiddle = new FacePoint(json["nose_contour_lower_middle"]);
            this.NoseContourRight1 = new FacePoint(json["nose_contour_right1"]);
            this.NoseContourRight2 = new FacePoint(json["nose_contour_right2"]);
            this.NoseContourRight3 = new FacePoint(json["nose_contour_right3"]);
            this.NoseLeft = new FacePoint(json["nose_left"]);
            this.NoseRight = new FacePoint(json["nose_right"]);
            this.NoseTip = new FacePoint(json["nose_tip"]);
            this.RightEyeBottom = new FacePoint(json["right_eye_bottom"]);
            this.RightEyeCenter = new FacePoint(json["right_eye_center"]);
            this.RightEyeLeftCorner = new FacePoint(json["right_eye_left_corner"]);
            this.RightEyeLowerLeftQuarter = new FacePoint(json["right_eye_lower_left_quarter"]);
            this.RightEyeLowerRightQuarter = new FacePoint(json["right_eye_lower_right_quarter"]);
            this.RightEyePupil = new FacePoint(json["right_eye_pupil"]);
            this.RightEyeRightCorner = new FacePoint(json["right_eye_right_corner"]);
            this.RightEyeTop = new FacePoint(json["right_eye_top"]);
            this.RightEyeUpperLeftQuarter = new FacePoint(json["right_eye_upper_left_quarter"]);
            this.RightEyeUpperRightQuarter = new FacePoint(json["right_eye_upper_right_quarter"]);
            this.RightEyebrowLeftCorner = new FacePoint(json["right_eyebrow_left_corner"]);
            this.RightEyebrowLowerLeftQuarter = new FacePoint(json["right_eyebrow_lower_left_quarter"]);
            this.RightEyebrowLowerMiddle = new FacePoint(json["right_eyebrow_lower_middle"]);
            this.RightEyebrowLowerRightQuarter = new FacePoint(json["right_eyebrow_lower_right_quarter"]);
            this.RightEyebrowRightCorner = new FacePoint(json["right_eyebrow_right_corner"]);
            this.RightEyebrowUpperLeftQuarter = new FacePoint(json["right_eyebrow_upper_left_quarter"]);
            this.RightEyebrowUpperMiddle = new FacePoint(json["right_eyebrow_upper_middle"]);
            this.RightEyebrowUpperRightQuarter = new FacePoint(json["right_eyebrow_upper_right_quarter"]);
        }
    }
}
