using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibCore.FaceApi.Models
{
    /// <summary>
    /// Face attributes
    /// </summary>
    [Flags]
    public enum FaceAttributesValues
    {
        All,
        Gender,
        Age,
        Smiling,
        Headpose,
        Blur,
        Facequality,
        Eyestatus,
        Ethnicity
    }

    public enum Gender
    {
        Unknown,
        Male,
        Female
    }
}
