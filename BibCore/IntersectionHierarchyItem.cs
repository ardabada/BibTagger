using System.Collections.Generic;
using System.Drawing;

namespace BibCore
{
    public class IntersectionHierarchyItem
    {
        public IntersectionHierarchyItem()
        {
            Intersection = new List<IntersectionHierarchyItem>();
        }
        public IntersectionHierarchyItem(Rectangle rect) : this()
        {
            Source = rect;
        }

        public Rectangle Source { get; set; }
        public List<IntersectionHierarchyItem> Intersection { get; set; }

        public bool HasIntersection
        {
            get { return Intersection.Count > 0; }
        }

        public Rectangle Union
        {
            get
            {
                Rectangle result = Source;

                foreach (var section in Intersection)
                    result = Rectangle.Union(result, section.Union);

                return result;
            }
        }
    }
}
