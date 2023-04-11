
using SvgDemoWinForms;

namespace SvgDemoTests
{
    public static class Tests
    {
        public static SizeModel Size1 = new SizeModel() { Width = 50, Height = 60 };
        public static SizeModel Size2 = new SizeModel() { Width = 10, Height = 80 };
        public static PointModel Pt1 = new PointModel() { X = 3, Y = 10 };
        public static PointModel Pt2 = new PointModel() { X = 20, Y = 5 };
        public static PointModel Pt3 = new PointModel() { X = 4, Y = 5 };

        public static DocumentModel Doc = new DocumentModel()
        {
            Elements = new List<ElementModel>()
            {
                new EllipseModel()
                {
                    Position = Pt1,
                    Size = Size1,
                },
                new RectModel()
                {
                    Position = Pt2,
                    Size = Size2
                },
                new PolygonModel()
                {
                    Points = new List<PointModel>() { Pt1, Pt2, Pt3 }
                }
            }
        };

        [Test]
        public static void TestJson()
        {
            var txt = Doc.ToJson();
            Console.WriteLine(txt);
            var val = DocumentModel.FromJson(txt);
            txt = val.ToJson();
            Console.WriteLine(txt);
        }
    }
}